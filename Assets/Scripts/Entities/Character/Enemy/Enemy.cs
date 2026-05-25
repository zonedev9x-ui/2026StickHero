using System.Xml.Serialization;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Attack,
    Dead
}

public class Enemy : Character
{
    public EnemyType enemyType = EnemyType.None;
    public EnemyName enemyName = EnemyName.None;    
    public bool isAnimAttackSingle = false;

    protected override void Start()
    {
        base.Start();
        EnablePhysics(false);
    }

    private void Update()
    {

    }

    public void Attack(Character target)
    {
        currentState = CharacterState.Attack;
        currentTarget = target;

        if(isAnimAttackSingle == true)
        {
            animator.SetFloat(ConstantData.ANIM_BLEND_ATTACK, 0);
            PlayAnim(ConstantData.ANIM_TRIGGER_ATTACK);
        }
        else
        {
            ChangeAnimAttack(weaponType);
        }
    }
}
