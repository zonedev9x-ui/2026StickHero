using UnityEngine;

public enum PlayerState
{
    Idle,
    Moving,
    Attacking,
    Winning,
    Dead
}

public class Player : Character
{
    public Floor currentFloor;
    public PlayerState state;

    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 oldParent;

    void Start()
    {
        oldParent = transform.parent.position;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = Input.mousePosition;

            mousePos.z = 3f;

            transform.position = Camera.main.ScreenToWorldPoint(mousePos) + offset;
        }
    }

    private void Attack()
    {
        state = PlayerState.Attacking;


    }

    #region Drag Player

    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

        PlayAnim(ConstantData.ANIM_TRIGGER_GRAB);

        isDragging = true;

        Tower currentTower = TowerController.Instance.SetCurrentTower();
        currentTower.ShowAllHighlightNormal();
    }

    private void OnMouseDrag()
    {
        if (!isDragging) return;

        Tower currentTower = TowerController.Instance.SetCurrentTower();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if (hit.collider.CompareTag(ConstantData.TAG_FLOOR))
            {
                Floor floor = hit.collider.GetComponentInParent<Floor>();

                if (floor != null && TowerController.Instance.IsFloorInCurrentTower(floor) == true)
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
                    }

                    return;
                }
            }
        }

        currentFloor = null;

        currentTower.ShowAllHighlightNormal();
        currentTower.HideAllHighlightSelect();
    }

    private void OnMouseUp()
    {
        PlayAnim(ConstantData.ANIM_TRIGGER_GRAB_RELEASE);
        isDragging = false;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0f;

        transform.position = Camera.main.ScreenToWorldPoint(mousePos) + offset;

        if (currentFloor != null)
        {
            transform.position = currentFloor.SetPlayerPos();
        }
        else
        {
            transform.position = oldParent;
        }

        Tower currentTower = TowerController.Instance.SetCurrentTower();
        currentTower.HideAllHighlight();
    }

    #endregion
}
