using UnityEngine;

public class Character : MonoBehaviour
{
    public Animator animator;
    protected string currentAnim;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    #region Anim
    
    protected virtual void PlayAnim(string anim)
    {
        if (animator == null) return;
        if (currentAnim == anim)
        {
            return;
        }
        if (!string.IsNullOrEmpty(currentAnim)) animator.ResetTrigger(currentAnim);

        currentAnim = anim;
        animator.SetTrigger(anim);
    }

    #endregion

    protected void Attack()
    {

    }
}
