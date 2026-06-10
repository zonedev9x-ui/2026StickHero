using UnityEngine;

public class BO_Hotgirl : BonusObject
{
    public Animator animator;

    public override void TakeAction()
    {
        base.TakeAction();

        if(animator != null)
        {
            animator.SetFloat(ConstantData.ANIM_BLEND_HOTGIRL_DANCE,ConstantData.ANIM_BLEND_HOTGIRL_WIN_6);
            animator.SetTrigger(ConstantData.ANIM_HOTGIRL_DANCE);
        }
    }
}
