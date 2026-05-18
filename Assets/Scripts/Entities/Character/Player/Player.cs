using System.Collections;
using UnityEngine;

public enum PlayerState
{
    None,
    Moving,
    Attacking,
    TakeDamage,
    Finish,
}

public class Player : Character
{
    public PlayerState currentState = PlayerState.None;

    public Tower currentTower;
    public Floor currentFloor;
    public Enemy currentEnemy;
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
        if (isDragging)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 3f;
            transform.position = mainCamera.ScreenToWorldPoint(mousePos) + offset;
        }

        switch (currentState)
        {   
            case PlayerState.Moving:
                animator.SetBool(ConstantData.ANIM_BOOL_RUNNING, true);
                break;
            case PlayerState.Attacking:
                PlayAnim(ConstantData.ANIM_TRIGGER_ATTACK);
                break;
            case PlayerState.TakeDamage:
                PlayAnim(ConstantData.ANIM_TRIGGER_DAMAGE);
                break;
            case PlayerState.Finish:
                break;
        }
    }

    //private bool IsIdle()
    //{
    //    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

    //    if (stateInfo.IsName("Armature|idle"))
    //    {
    //        return true;
    //    }

    //    return false;
    //}

    public void AttackEnemy()
    {
        currentState = PlayerState.Attacking;

        if (currentEnemy.enemyType == EnemyType.Normal)
        {
            if (strengthScore > currentEnemy.strengthScore)
            {
                int randomAnimAttack = Random.Range(1, 3);

                Debug.Log("Random Attack: " + randomAnimAttack);

                animator.SetFloat(ConstantData.ANIM_BLEND_ATTACK, randomAnimAttack);
            }
        }
    }

    private IEnumerator IAttackEnemy()
    {
        yield return new WaitForSeconds(1f);

        AttackEnemy();
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
                        currentEnemy = currentFloor.GetCurrentEnemy();
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
        isDragging = false;

        PlayAnim(ConstantData.ANIM_TRIGGER_GRAB_RELEASE);

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

        if (currentEnemy != null)
        {
            StartCoroutine(IAttackEnemy());
        }
    }

    #endregion
}
