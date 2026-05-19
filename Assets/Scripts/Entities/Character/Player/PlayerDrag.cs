using UnityEngine;

public class PlayerDrag : MonoBehaviour
{
    private Player player;
    private Camera mainCamera;
    private Vector3 offset;
    private Vector3 oldParent;
    public bool isDragging = false;

    private Tower currentTower;
    private Floor currentFloor;
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
        player.TriggerAnim(ConstantData.ANIM_TRIGGER_GRAB);
        isDragging = true;

        oldParent = transform.parent.position;

        currentTower = TowerController.Instance.SetCurrentTower();

        if (currentTower != null)
        {
            currentTower.ShowAllHighlightNormal();
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
                        if (floor != currentFloor)
                        {
                            if (currentFloor != null)
                            {
                                currentFloor.HideHighLightSelect();
                                currentFloor.ShowHighLight();
                            }

                            currentFloor = floor;

                            currentFloor.HideHighLight();
                            currentFloor.ShowHighLightSelect();
                            currentEnemy = currentFloor.GetCurrentEnemy();
                        }
                    }
                    else 
                    {
                        currentFloor = floor;
                    }

                    return;
                }
            }
        }

        currentFloor = null;
        currentEnemy = null;
        if (currentTower != null)
        {
            currentTower.ShowAllHighlightNormal();
            currentTower.HideAllHighlightSelect();
        }
    }

    private void OnMouseUp()
    {
        if (!isDragging) return;

        isDragging = false;

        player.TriggerAnim(ConstantData.ANIM_TRIGGER_GRAB_RELEASE);

        if (currentFloor != null)
        {
            transform.position = currentFloor.SetPlayerPos();
            transform.SetParent(currentFloor.GetTransformChild());
        }
        else
        {
            transform.position = oldParent;
        }

        if (currentTower != null)
        {
            currentTower.HideAllHighlight();
        }

        if (currentEnemy != null)
        {
            player.SetCombatTarget(currentEnemy, currentFloor);
        }
    }
}
