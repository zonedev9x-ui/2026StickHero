using System.Collections;
using UnityEngine;

public class CharacterAnimEvent : MonoBehaviour
{
    public Character character;
    public float duration = 0.2f;
    Coroutine currentScale;

    public void OnStrikeHit(int direction)
    {
        if (character.GetTarget() != null)
        {
            character.GetTarget().TakeHit(direction);
        }
    }

    public void OnAttackFinished()
    {
        if (character.GetTarget() != null)
        {
            character.GetTarget().Die();
        }
    }

    public void OnChangeState(CharacterState state)
    {
        character.currentState = state;
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

            character.transform.localScale =
                Vector3.Lerp(startScale, targetScale, t);

            yield return null;
        }

        character.transform.localScale = targetScale;
    }

    public void BreakWalls()
    {
        character.GetComponent<Collider>().isTrigger = false;
        character.currentTower.BreakWalls();
    }

    public void EnableTriggerCharacter()
    {
        character.GetComponent<Collider>().isTrigger = true;
    }
}
