using System.Collections;
using UnityEngine;

public class Player : Character
{
    public float duration = 2f;

    private Vector3 startScale = Vector3.one;
    private Vector3 targetScale = Vector3.one * 6f;

    private float timer;


    protected override void Start()
    {
        base.Start();
        EnablePhysics(false);
    }

    private void Update()
    {
        UpdateIdle();
        UpdateChangeSize();
    }

    private void UpdateIdle()
    {
        if (currentState == CharacterState.Idle)
        {
            Tower currentTower = TowerController.Instance.SetCurrentTower();
            if (currentTower != null && currentTower.IsAllFloorEmpty() == true)
            {
                currentState = CharacterState.ChangeSize;
                PlayAnim(ConstantData.ANIM_TRIGGER_CHANGE_SIZE);
                return;
            }
        }
    }

    private void UpdateChangeSize()
    {
        if(currentState == CharacterState.ChangeSize)
        {
            //timer += Time.deltaTime;
            //float t = timer / duration;
            //transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            
            //if(t >= duration)
            //{

            //}
        }
    }

    public void TriggerAnim(string anim)
    {
        PlayAnim(anim);
    }

    public void EnablePhysicsAndColliders(bool isEnabled)
    {
        Collider col = GetComponentInChildren<Collider>();
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = !isEnabled;
        }
        if (col != null)
        {
            col.enabled = !isEnabled;
        }
    }

    public override void SetCombatTarget(Character enemy, Floor floor)
    {
        currentState = CharacterState.Attack;
        currentTarget = enemy;
        currentFloor = floor;

        StartCoroutine(IAttackEnemy());
    }

    private IEnumerator IAttackEnemy()
    {
        yield return new WaitForSeconds(0.5f);

        AttackEnemy();
    }

    public void AttackEnemy()
    {
        Enemy enemy = currentTarget as Enemy;

        if (enemy.enemyType == EnemyType.Normal)
        {
            if (strengthScore > enemy.strengthScore)
            {
                if (currentFloor.IsLastEnemy())
                {
                    int randomAnimAttackFar = Random.Range(4, 7);
                    animator.SetFloat(ConstantData.ANIM_BLEND_ATTACK, randomAnimAttackFar);
                    PlayAnim(ConstantData.ANIM_TRIGGER_ATTACK);
                }
                else
                {
                    int randomAnimAttack = Random.Range(1, 4);
                    animator.SetFloat(ConstantData.ANIM_BLEND_ATTACK, randomAnimAttack);
                    PlayAnim(ConstantData.ANIM_TRIGGER_ATTACK);
                }
            }
            else
            {
                enemy.Attack(this);
            }
        }
    }
}
