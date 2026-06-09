using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public List<ItemWeapon> itemWeapons;

    public override void UpdateIdle()
    {
        currentState = CharacterState.Idle;

        LevelController.Instance.CheckTowerProgress(this);
    }

    public void UpdateChangeSize()
    {
        currentState = CharacterState.ChangeSize;

        this.StartDelayAction(0.5f, () =>
        {
            PlayAnim(ConstantData.ANIM_TRIGGER_CHANGE_SIZE);
        });
    }

    public void UpdateWin()
    {
        currentState = CharacterState.Win;

        PlayAnim(ConstantData.ANIM_TRIGGER_WIN);
    }

    public void SetCombatTarget(Entity target, Floor floor)
    {
        currentState = CharacterState.Attack;
        currentTarget = target;
        currentFloor = floor;

        EntityInteraction();
    }

    public void SetCombatBossEnemy()
    {
        currentState = CharacterState.Attack;

        currentTarget = LevelController.Instance.SetBossInLevel();
        EntityInteraction();
    }

    private IEnumerator IEntityInteraction()
    {
        yield return new WaitForSeconds(0.5f);

        if (currentTarget is Enemy)
        {
            Enemy currentEnemy = (Enemy)currentTarget;
            AttackEnemy(currentEnemy);
        }
    }

    private void EntityInteraction()
    {
        if (currentTarget is Enemy)
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            currentCoroutine = StartCoroutine(IEntityInteraction());
        }
        else if (currentTarget is Weapon)
        {
            Weapon currentWeapon = (Weapon)currentTarget;
            UpdateStrengthScore(currentWeapon.strengthScore);
            EquipWeapon(currentWeapon);
            currentWeapon.SetActive(false);
        }
        else if (currentTarget is Trap)
        {
            animator.SetFloat(ConstantData.ANIM_BLEND_DAMAGE, ConstantData.ANIM_DAMAGE_STRAIGHT);
            PlayAnim(ConstantData.ANIM_TRIGGER_DAMAGE);

            Trap currentTrap = (Trap)currentTarget;
            currentTrap.Attack();
            UpdateStrengthScore(currentTrap.strengthScore);

            if (strengthScore.score <= 0)
            {
                Die(this);
            }
            else
            {
                UpdateIdle();
            }
        }
        else if (currentTarget is ItemSupport)
        {
            PlayAnim(ConstantData.ANIM_TRIGGER_GET_ITEM);

            currentTarget.gameObject.SetActive(false);
            currentTarget.SetActive(false);
            UpdateStrengthScore(currentTarget.strengthScore);
        }
    }

    private void AttackEnemy(Enemy currentEnemy)
    {
        if (currentEnemy.enemyType == EnemyType.Normal)
        {
            if (strengthScore.score > currentEnemy.strengthScore.score)
            {
                if (currentFloor.IsLastEnemyInFloor())
                {
                    int randomAnimAttackFar = Random.Range(3, 6);
                    animator.SetFloat(ConstantData.ANIM_BLEND_ATTACK, randomAnimAttackFar);
                }
                else
                {
                    int randomAnimAttack = Random.Range(0, 3);
                    animator.SetFloat(ConstantData.ANIM_BLEND_ATTACK, randomAnimAttack);
                }

                PlayAnim(ConstantData.ANIM_TRIGGER_ATTACK);
            }
            else
            {
                currentEnemy.Attack(this);
            }
        }
        else if (currentEnemy.enemyType == EnemyType.Boss)
        {
            if (strengthScore.score > currentEnemy.strengthScore.score)
            {
                animator.SetFloat(ConstantData.ANIM_BLEND_BOSS_COMBO, 0);
                PlayAnim(ConstantData.ANIM_TRIGGER_BOSS_COMBO);
            }
            else
            {
                currentEnemy.Attack(this);
            }
        }
    }

    private void EquipWeapon(Weapon weapon)
    {
        for (int i = 0; i < itemWeapons.Count; i++)
        {
            if (itemWeapons[i].weaponType == weapon.weaponType)
            {
                itemWeapons[i].gameObject.SetActive(true);
                weapon.gameObject.SetActive(false);
                PlayAnim(ConstantData.ANIM_TRIGGER_GET_ITEM);
            }
            else
            {
                itemWeapons[i].gameObject.SetActive(false);
            }
        }
    }
}
