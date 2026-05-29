using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class EntityAnimEvent : MonoBehaviour
{
    public Entity entity;
    public float duration = 0.2f;
    //public float 

    private Animator animator;
    private Coroutine currentScale;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnStrikeHit(int direction)
    {
        if (entity.currentTarget != null)
        {
            Character target = entity.currentTarget as Character;

            if (target != null)
            {
                target.TakeHit(direction);
            }
        }
    }

    public void OnAttackFinished()
    {
        if (entity.currentTarget != null)
        {
            if (entity.currentTarget is Character)
            {
                Character c = entity.currentTarget as Character;
                if (c != null)
                {
                    if (entity is Player)
                    {
                        Player player = entity as Player;
                        player.UpdateStrengthScore(c.strengthScore);
                    }

                    c.Die();
                }
            }
        }
    }

    public void OnChangeState(CharacterState state)
    {
        Character c = entity as Character;
        c.currentState = state;

        if(c.currentState == CharacterState.Idle)
        {
            c.UpdateIdle();
        }
    }

    public void OnChangeStateAttackBoss()
    {
        Player player = entity as Player;
        player.SetCombatBossEnemy();
    }

    public void ScaleTo(float target)
    {
        if (currentScale != null)
            StopCoroutine(currentScale);

        currentScale =
            StartCoroutine(ScaleRoutine(Vector3.one * target));
    }

    IEnumerator ScaleRoutine(Vector3 targetScale)
    {
        Vector3 startScale = transform.localScale;

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float t = timer / duration;

            entity.transform.localScale =
                Vector3.Lerp(startScale, targetScale, t);

            yield return null;
        }

        entity.transform.localScale = targetScale;
    }

    public void BreakWalls()
    {
        entity.GetComponent<Collider>().isTrigger = false;

        if (entity is Player)
        {
            Player player = entity as Player;
            player.currentTower.BreakWalls();
        }
    }

    public void EnableTriggerCharacter()
    {
        entity.GetComponent<Collider>().isTrigger = true;
    }

    public void HideObject()
    {
        entity.gameObject.SetActive(false);
    }
}
