using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public List<ItemWeapon> itemWeapons;
    private Coroutine currentCoroutine;

    protected override void Start()
    {
        base.Start();
        EnablePhysics(false);
    }

    public override void UpdateIdle()
    {
        currentState = CharacterState.Idle;

        bool isAllEntityCurrentTowerCleaned = LevelController.Instance.IsAllEntityInCurrentTowerCleaned();

        if (isAllEntityCurrentTowerCleaned == true)
        {
            if (LevelController.Instance.IsLastTower() && LevelController.Instance.SetBossInLevel() != null)
            {
                LevelController.Instance.cameraSmooth.MoveLastTargetAndScale();
                if (currentCoroutine != null) currentCoroutine = null;

                currentCoroutine = StartCoroutine(IEUpdateChangeSize());
            }

            LevelController.Instance.MoveCameraToNextTower();
            LevelController.Instance.NextTower();
        }
    }

    private IEnumerator IEUpdateChangeSize()
    {
        yield return new WaitForSeconds(0.5f);

        currentState = CharacterState.ChangeSize;
        PlayAnim(ConstantData.ANIM_TRIGGER_CHANGE_SIZE);
    }

    public void EnablePhysicsAndColliders(bool isEnabled)
    {
        Collider col = GetComponentInChildren<Collider>();
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = !isEnabled;
        }
        if (col != null)
        {
            col.enabled = !isEnabled;
        }
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

        if (currentTarget != null)
        {
            currentTarget = null;
        }

        currentTarget = LevelController.Instance.SetBossInLevel();

        EntityInteraction();
    }

    public IEnumerator IEAttackBoss()
    {
        yield return new WaitForSeconds(0.5f);

        if (currentTarget != null)
        {
            currentTarget = null;
        }

        currentTarget = LevelController.Instance.SetBossInLevel();

        if (currentTarget != null)
        {
            SetCombatTarget(currentTarget, null);
        }
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
                Die();
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
                    int randomAnimAttackFar = Random.Range(4, 7);
                    animator.SetFloat(ConstantData.ANIM_BLEND_ATTACK, randomAnimAttackFar);
                }
                else
                {
                    int randomAnimAttack = Random.Range(1, 4);
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
