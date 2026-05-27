using System.Collections;
using UnityEngine;

public class Player : Character
{
    protected override void Start()
    {
        base.Start();
        EnablePhysics(false);
    }

    private void Update()
    {
    }

    private void UpdateIdle()
    {
        if (currentState == CharacterState.Idle)
        {
            //Tower currentTower = LevelController.Instance.SetCurrentTower();
            //if (currentTower != null)
            //{
            //    currentState = CharacterState.ChangeSize;
            //    PlayAnim(ConstantData.ANIM_TRIGGER_CHANGE_SIZE);
            //    return;
            //}
        }
    }

    private void UpdateChangeSize()
    {
        if (currentState == CharacterState.ChangeSize)
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

    public override void SetCombatTarget(Entity target, Floor floor)
    {
        currentState = CharacterState.Attack;
        currentTarget = target;
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
        if (currentTarget is Enemy)
        {
            Enemy currentEnemy = (Enemy)currentTarget;

            if (currentEnemy.enemyType == EnemyType.Normal)
            {
                if (strengthScore.score > currentEnemy.strengthScore.score)
                {
                    if (currentFloor.IsLastEnemyInFloor())
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
                    currentEnemy.Attack(this);
                }
            }
        }
    }
}
