using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public List<ItemWeapon> itemWeapons;
    private Coroutine interactCoroutine;

    protected override void Start()
    {
        base.Start();
        EnablePhysics(false);
    }

    private void UpdateIdle()
    {
        if (currentState == CharacterState.Idle)
        {
            //Tower currentTower = LevelController.Instance.SetCurrentTower();
            //if (currentTower != null)
            //{
            //    currentState = CharacterState.ChangeSize;
            //    PlayAnim(ConstantData.ANIM_TRIGGER_CHANGE_SIZE);
            //    return;
            //}
        }
    }

    private void UpdateChangeSize()
    {
        if (currentState == CharacterState.ChangeSize)
        {
            //timer += Time.deltaTime;
            //float t = timer / duration;
            //transform.localScale = Vector3.Lerp(startScale, targetScale, t);

            //if(t >= duration)
            //{

            //}
        }
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

    private IEnumerator IEntityInteraction()
    {
        yield return new WaitForSeconds(0.5f);

        if (currentTarget is Enemy)
        {
            Enemy currentEnemy = (Enemy)currentTarget;
            AttackEnemy(currentEnemy);
        }
    }

    public void EntityInteraction()
    {
        if (currentTarget is Enemy)
        {
            if (interactCoroutine != null)
            {
                StopCoroutine(interactCoroutine);
            }

            interactCoroutine = StartCoroutine(IEntityInteraction());
        }
        else if (currentTarget is Weapon)
        {
            Weapon currentWeapon = (Weapon)currentTarget;
            UpdateStrengthScore(currentWeapon.strengthScore);
            EquipWeapon(currentWeapon);
        }
        else if (currentTarget is Trap)
        {
            Trap currentTrap = (Trap)currentTarget;
            currentTrap.Attack();
            UpdateStrengthScore(currentTrap.strengthScore);

        }
        else
        {   
            currentTarget.gameObject.SetActive(false);
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
