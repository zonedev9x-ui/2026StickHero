using UnityEngine;

public class TrapBear : Trap
{   
    public Animator animator;

    public override void Attack()
    {
        base.Attack();

        animator.SetTrigger(ConstantData.ANIM_TRAP_BEAR_IS_ACTIVE);
    }
}
