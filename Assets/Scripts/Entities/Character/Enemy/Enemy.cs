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

    protected override void Start()
    {
        base.Start();
        EnablePhysics(false);
    }

    private void Update()
    {

    }

    public void Attack(Character character)
    {
        currentState = CharacterState.Attack;

        currentTarget = character;

        int randomAnimAttackFar = Random.Range(1, 4);
        animator.SetFloat(ConstantData.ANIM_BLEND_ATTACK, randomAnimAttackFar);
        PlayAnim(ConstantData.ANIM_TRIGGER_ATTACK);
    }
}
