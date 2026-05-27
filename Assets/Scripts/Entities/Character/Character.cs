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
        EnableInteraction(true);
        EnableRagdoll(true);
        EnableStrengthScore(false);
    }

    protected void ChangeAnimAttack(WeaponType newType)
    {
        weaponType = newType;

        int min = 0;
        int max = 0;

        switch (weaponType)
        {
            case WeaponType.None:
                min = ConstantData.ANIM_ATTACK_EMPTY_MIN;
                max = ConstantData.ANIM_ATTACK_EMPTY_MAX;
                break;
            case WeaponType.Sword:
                min = ConstantData.ANIM_ATTACK_SWORD_MIN;
                max = ConstantData.ANIM_ATTACK_SWORD_MAX;
                break;
            case WeaponType.Hammer:
            case WeaponType.Axe:
            case WeaponType.ScrewHammer:
                min = ConstantData.ANIM_ATTACK_HAMMER_AXE_MIN;
                max = ConstantData.ANIM_ATTACK_HAMMER_AXE_MAX;
                break;

            case WeaponType.Dagger:
                min = ConstantData.ANIM_ATTACK_DAGGER_MIN;
                max = ConstantData.ANIM_ATTACK_DAGGER_MAX;
                break;
        }

        int randomAnimAttack = Random.Range(min, max);
        animator.SetFloat(ConstantData.ANIM_BLEND_ATTACK, randomAnimAttack);
        PlayAnim(ConstantData.ANIM_TRIGGER_ATTACK);
    }

    public void UpdateStrengthScore(StrengthScore targetStrengthScore)
    {
        switch (targetStrengthScore.scoreType)
        {
            case StrengthScoreType.None:
            case StrengthScoreType.Add:
                this.strengthScore.AddStrengthScore(targetStrengthScore.score);
                break;
            case StrengthScoreType.Subtract:
                this.strengthScore.SubtractStrengthScore(targetStrengthScore.score);
                break;
            case StrengthScoreType.Multiply:
                this.strengthScore.MultiplyStrengthScore(targetStrengthScore.score);
                break;
        }
    }
}

