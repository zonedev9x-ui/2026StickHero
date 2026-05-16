using UnityEngine;

public enum PlayerState
{
    Idle,
    Moving,
    Dragging,
    Attacking,
    Finish,
}

public class Player : Character
{
    public Floor currentFloor;
    public PlayerState currentState = PlayerState.Idle;

    private Tower currentTower;
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 oldParent;
    private Camera mainCamera;

    void Start()
    {
        oldParent = transform.parent.position;
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (IsIdle() == true)
        {
            currentState = PlayerState.Idle;
        }
        else
        {
            switch (currentState)
            {
                case PlayerState.Dragging:
                    if (isDragging)
                    {
                        Vector3 mousePos = Input.mousePosition;
                        mousePos.z = 3f;
                        transform.position = mainCamera.ScreenToWorldPoint(mousePos) + offset;
                    }
                    break;
                case PlayerState.Moving:
                    break;
                case PlayerState.Attacking:
                    PlayAnim(ConstantData.ANIM_TRIGGER_ATTACK);
                    break;
                case PlayerState.Finish:
                    break;
            }
        }
    }

    private bool IsIdle()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Armature|idle"))
        {
            return true;
        }

        return false;
    }

    public void Attack(Enemy enemy) 
    {

    }

    #region Drag Player

    private void OnMouseDown()
    {
        offset = transform.position - mainCamera.ScreenToWorldPoint(Input.mousePosition);

        PlayAnim(ConstantData.ANIM_TRIGGER_GRAB);

        isDragging = true;

        currentTower = TowerController.Instance.SetCurrentTower();
        currentTower.ShowAllHighlightNormal();
    }

    private void OnMouseDrag()
    {
        if (!isDragging) return;

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

        if (currentTower != null)
        {
            currentTower.ShowAllHighlightNormal();
            currentTower.HideAllHighlightSelect();
        }
    }

    private void OnMouseUp()
    {   
        if(isDragging == false) return;

        isDragging = false;
        PlayAnim(ConstantData.ANIM_TRIGGER_GRAB_RELEASE);

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

        if (currentTower != null)
        {
            currentTower.HideAllHighlight();
        }
    }

    #endregion
}
