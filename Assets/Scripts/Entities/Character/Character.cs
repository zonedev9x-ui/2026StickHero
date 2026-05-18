using TMPro;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int strengthScore;
    public TMP_Text txtStrengthScore;

    public Animator animator;
    protected string currentAnim;

    void Start()
    {
        //txtStrengthScore.text = strengthScore.ToString();
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
}
