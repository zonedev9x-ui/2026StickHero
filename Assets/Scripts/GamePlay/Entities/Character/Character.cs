using System.Collections;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Character : Entity
{
    public CharacterState currentState = CharacterState.Idle;
    public WeaponType weaponType = WeaponType.None;

    public Animator animator;

    public Rigidbody rb;

    [HideInInspector] public Tower currentTower;

    [HideInInspector] public Floor currentFloor;

    //public Entity currentTarget;

    protected Coroutine currentCoroutine;
    protected string currentAnim;

    protected virtual void Awake()
    {
        EnablePhysics(false);
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
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = !isEnabled;
        }
    }

    #endregion

    public void ChangeState(CharacterState state)
    {
        currentState = state;
    }

    public virtual void UpdateIdle() { }

    public virtual void TakeHit(int direction)
    {
        animator.SetFloat(ConstantData.ANIM_BLEND_DAMAGE, direction);
        PlayAnim(ConstantData.ANIM_TRIGGER_DAMAGE);
    }

    public void Die(Entity entity)
    {
        currentState = CharacterState.Dead;

        isActive = false;

        EnableRagdoll(true);

        EnableStrengthScore(false);

        Vector3 hitDir = (transform.position - entity.transform.position).normalized;

        if (this is Enemy)
        {
            Enemy enemy = this as Enemy;

            if(enemy.enemyType == EnemyType.Boss)
            {
                rb.AddForce(hitDir * 200f, ForceMode.Impulse);
            }
            else
            {
                rb.AddForce(hitDir * 40f, ForceMode.Impulse);
            }
        }
    }

    private void HideCharacter()
    {
        if (currentCoroutine != null)
        {
            currentCoroutine = null;
        }

        currentCoroutine = StartCoroutine(IEHideCharacterDelay());
    }

    private IEnumerator IEHideCharacterDelay()
    {
        yield return new WaitForSeconds(6f);

        gameObject.SetActive(false);
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

