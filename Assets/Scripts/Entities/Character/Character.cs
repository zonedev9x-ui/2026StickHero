using TMPro;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterState currentState = CharacterState.Idle;
    public WeaponType weaponType = WeaponType.None;
    public int strengthScore;
    public Animator animator;

    [HideInInspector] public Tower currentTower;

    [HideInInspector] public Floor currentFloor;

    [HideInInspector] public Character currentTarget;

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

    protected void ChangeAnimAttack(WeaponType newType)
    {
        weaponType = newType;
        int randomAnimAttack;

        switch (weaponType)
        {   
            case WeaponType.None:
                randomAnimAttack = Random.Range(ConstantData.ANIM_ATTACK_EMPTY_MIN, ConstantData.ANIM_ATTACK_EMPTY_MAX);
                animator.SetFloat(ConstantData.ANIM_BLEND_ATTACK, randomAnimAttack);
                break;
            case WeaponType.Sword:
                randomAnimAttack = Random.Range(ConstantData.ANIM_ATTACK_SWORD_MIN, ConstantData.ANIM_ATTACK_SWORD_MAX);
                animator.SetFloat(ConstantData.ANIM_BLEND_ATTACK, randomAnimAttack);
                break;
            case WeaponType.Hammer_Axe:
                randomAnimAttack = Random.Range(ConstantData.ANIM_ATTACK_HAMMER_AXE_MIN, ConstantData.ANIM_ATTACK_HAMMER_AXE_MAX);
                animator.SetFloat(ConstantData.ANIM_BLEND_ATTACK, randomAnimAttack);
                break;
            case WeaponType.Dagger:
                randomAnimAttack = Random.Range(ConstantData.ANIM_ATTACK_DAGGER_MIN, ConstantData.ANIM_ATTACK_DAGGER_MAX);
                animator.SetFloat(ConstantData.ANIM_BLEND_ATTACK, randomAnimAttack);
                break;
        }
    }
}
