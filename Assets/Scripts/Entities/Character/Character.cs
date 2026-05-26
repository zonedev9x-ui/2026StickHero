using TMPro;
using UnityEngine;

public class Character : Entity
{
    public CharacterState currentState = CharacterState.Idle;
    public WeaponType weaponType = WeaponType.None;
    
    public Animator animator;

    [HideInInspector] public Tower currentTower;

    [HideInInspector] public Floor currentFloor;

    public Entity currentTarget;

    protected string currentAnim;

    protected virtual void Start()
    {
        
    }

    public void InitCharacterScore(int score)
    {
        strengthScore.InitStrengthScore(StrengthScoreType.None, score);
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

    public virtual void SetCombatTarget(Entity target, Floor floor)
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
            case WeaponType.Sword01:
            case WeaponType.Sword02:
            case WeaponType.Sword03:
            case WeaponType.Sword04:
            case WeaponType.Sword05:
            case WeaponType.Sword06:
            case WeaponType.Sword07:
            case WeaponType.Sword08:
            case WeaponType.Sword09:
            case WeaponType.Sword10:
            case WeaponType.Sword11:
            case WeaponType.Sword12:
                randomAnimAttack = Random.Range(ConstantData.ANIM_ATTACK_SWORD_MIN, ConstantData.ANIM_ATTACK_SWORD_MAX);
                animator.SetFloat(ConstantData.ANIM_BLEND_ATTACK, randomAnimAttack);
                break;
            case WeaponType.Hammer01:
            case WeaponType.Hammer02:
            case WeaponType.Hammer03:
            case WeaponType.Hammer04:
            case WeaponType.Hammer05:
            case WeaponType.Hammer06:
            case WeaponType.ScrewHammer:
            case WeaponType.Axe01:
            case WeaponType.Axe02:
            case WeaponType.Axe03:
            case WeaponType.Axe05:
                randomAnimAttack = Random.Range(ConstantData.ANIM_ATTACK_HAMMER_AXE_MIN, ConstantData.ANIM_ATTACK_HAMMER_AXE_MAX);
                animator.SetFloat(ConstantData.ANIM_BLEND_ATTACK, randomAnimAttack);
                break;
            case WeaponType.Dagger01:
            case WeaponType.Dagger02:
            case WeaponType.Dagger03:
            case WeaponType.Dagger04:
            case WeaponType.Dagger05:
                randomAnimAttack = Random.Range(ConstantData.ANIM_ATTACK_DAGGER_MIN, ConstantData.ANIM_ATTACK_DAGGER_MAX);
                animator.SetFloat(ConstantData.ANIM_BLEND_ATTACK, randomAnimAttack);
                break;
        }
    }
}
