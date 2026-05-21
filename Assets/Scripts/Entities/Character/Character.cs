using TMPro;
using UnityEngine;

public class Character : MonoBehaviour
{   
    public CharacterState currentState = CharacterState.Idle;
    public int strengthScore;
    public Animator animator;

    public Tower currentTower;
    public Floor currentFloor;

    public Character currentTarget;
    protected string currentAnim;

    protected virtual void Start()
    {
        //txtStrengthScore.text = strengthScore.ToString();
    }

    void Update()
    {
        
    }

    #region Anim and Physics
    
    public virtual void PlayAnim(string anim)
    {
        if (animator == null) return;
        
        if (!string.IsNullOrEmpty(currentAnim)) animator.ResetTrigger(currentAnim);

        currentAnim = anim;
        animator.SetTrigger(anim);
    }

    protected virtual void EnableRagdoll(bool isEnabled)
    {
        GetComponentInChildren<Animator>().enabled = !isEnabled;

        EnablePhysics(isEnabled);
    }

    public virtual void EnablePhysics(bool isEnabled)
    {
        //Collider col = GetComponentInChildren<Collider>();
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = !isEnabled;
        }
        //if (col != null)
        //{
        //    col.enabled = isEnabled;
        //}
    }

    #endregion

    public Character GetTarget()
    {
        return currentTarget;
    }

    public virtual void SetCombatTarget(Character enemy, Floor floor)
    {
        
    }

    public virtual void TakeHit(int direction)
    {
        animator.SetFloat(ConstantData.ANIM_BLEND_DAMAGE, direction);
        PlayAnim(ConstantData.ANIM_TRIGGER_DAMAGE);
    }

    public void Die()
    {
        currentState = CharacterState.Dead;
        EnableRagdoll(true);
    }
}
