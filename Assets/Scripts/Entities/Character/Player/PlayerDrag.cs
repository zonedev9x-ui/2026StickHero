using UnityEngine;

public class PlayerDrag : MonoBehaviour
{
    public Player player;
    private Camera mainCamera;
    private Vector3 offset;
    private Vector3 oldParentPos;
    private Transform oldParentTransform;

    private bool isDragging = false;
    private Entity currentTarget;

    void Start()
    {
        mainCamera = Camera.main;
        if (transform.parent != null)
        {
            oldParentPos = transform.parent.position;
            oldParentTransform = transform.parent;
        }
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 3f;
            transform.position = mainCamera.ScreenToWorldPoint(mousePos) + offset;
        }
    }

    private void OnMouseDown()
    {
        if (LevelController.Instance.cameraSmooth.IsMoving) return;

        if (player.currentState != CharacterState.Idle) return;

        offset = transform.position - mainCamera.ScreenToWorldPoint(Input.mousePosition);
        player.PlayAnim(ConstantData.ANIM_TRIGGER_GRAB);
        isDragging = true;

        oldParentTransform = transform.parent;
        oldParentPos = oldParentTransform != null ? oldParentTransform.position : transform.position;
        
        transform.SetParent(null);
        
        player.currentFloor = null;
        player.currentTarget = null;
        player.currentTower = LevelController.Instance.SetCurrentTower();

        if (player.currentTower != null)
        {
            player.currentTower.ShowAllHighlightNormal();
        }   
    }

    private void OnMouseDrag()
    {
        if (!isDragging) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if (hit.collider.CompareTag(ConstantData.TAG_FLOOR))
            {
                Floor floor = hit.collider.GetComponentInParent<Floor>();

                if (floor != null)
                {
                    if (LevelController.Instance.IsFloorInCurrentTower(floor) == true)
                    {
                        if (floor != player.currentFloor)
                        {
                            if (player.currentFloor != null)
                            {
                                player.currentFloor.HideHighLightSelect();
                                player.currentFloor.ShowHighLight();
                            }

                            player.currentFloor = floor;

                            player.currentFloor.HideHighLight();
                            player.currentFloor.ShowHighLightSelect();

                            currentTarget = player.currentFloor.GetCurrentEntity();
                        }
                    }
                    else
                    {
                        player.currentFloor = floor;
                    }

                    return;
                }
            }
        }

        player.currentFloor = null;
        currentTarget = null;
        if (player.currentTower != null)
        {
            player.currentTower.ShowAllHighlightNormal();
            player.currentTower.HideAllHighlightSelect();
        }
    }

    private void OnMouseUp()
    {
        if (!isDragging) return;

        isDragging = false;

        player.PlayAnim(ConstantData.ANIM_TRIGGER_GRAB_RELEASE);

        if (player.currentFloor != null)
        {
            transform.position = player.currentFloor.SetPlayerPos();
            transform.SetParent(player.currentFloor.transform);
            player.currentFloor.GetNextEntity();
        }
        else
        {
            transform.position = oldParentPos;
            if (oldParentTransform != null && oldParentTransform.gameObject.activeSelf)
            {
                transform.SetParent(oldParentTransform);
            }
            else if (player.currentTower != null)
            {
                transform.SetParent(player.currentTower.transform);
            }
        }

        if (player.currentTower != null)
        {
            player.currentTower.HideAllHighlight();
            player.currentTower.SortSummitAndFloorsDown();
        }

        if (currentTarget != null)
        {
            player.SetCombatTarget(currentTarget, player.currentFloor);
        }
    }
}
