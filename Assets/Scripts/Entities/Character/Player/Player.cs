using System.Collections;
using UnityEngine;

public class Player : Character
{
    public Floor currentFloor;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        switch (currentState)
        {
            case CharacterState.Moving:
                animator.SetBool(ConstantData.ANIM_BOOL_RUNNING, true);
                break;
            case CharacterState.Attack:
                break;
            case CharacterState.Dead:
                break;
        }
    }

    public void TriggerAnim(string anim)
    {
        PlayAnim(anim);
    }

    public void SetCombatTarget(Enemy enemy, Floor floor)
    {
        currentState = CharacterState.Attack;
        currentTarget = enemy;
        currentFloor = floor;
        StartCoroutine(IAttackEnemy());
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

    private IEnumerator IAttackEnemy()
    {
        yield return new WaitForSeconds(0.5f);

        AttackEnemy();
    }
}
