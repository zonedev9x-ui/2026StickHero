using UnityEngine;

public class PlayerDrag : MonoBehaviour
{
    private Character player;
    private Camera mainCamera;
    private Vector3 offset;
    private Vector3 oldParent;
    public bool isDragging = false;

    private Enemy currentEnemy;

    void Start()
    {
        player = GetComponent<Player>();
        mainCamera = Camera.main;
        oldParent = transform.parent.position;
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
        if (player.currentState != CharacterState.Idle ) return;

        offset = transform.position - mainCamera.ScreenToWorldPoint(Input.mousePosition);
        player.PlayAnim(ConstantData.ANIM_TRIGGER_GRAB);
        isDragging = true;

        oldParent = transform.parent.position;

        player.currentTower = TowerController.Instance.SetCurrentTower();

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
                    if (TowerController.Instance.IsFloorInCurrentTower(floor) == true)
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
                            currentEnemy = player.currentFloor.GetCurrentEnemy();
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
        currentEnemy = null;
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
            transform.SetParent(player.currentFloor.GetTransformChild());
        }
        else
        {
            transform.position = oldParent;
        }

        if (player.currentTower != null)
        {
            player.currentTower.HideAllHighlight();
        }

        if (currentEnemy != null)
        {
            player.SetCombatTarget(currentEnemy, player.currentFloor);
        }
    }
}
