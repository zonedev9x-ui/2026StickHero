using UnityEngine;

public class CharacterAnimEvent : MonoBehaviour
{
    public Character target;

    public void OnStrikeHit(int direction)
    {
        if (target.GetTarget() != null)
        {   
            Debug.Log("PlayerAnimEvent: OnStrikeHit called with direction " + direction);
            target.GetTarget().TakeHit(direction);
        }
    }

    public void OnAttackFinished()
    {
        if (target.GetTarget() != null)
        {
            target.GetTarget().Die();
        }
    }

    public void OnChangeState(CharacterState state)
    {
        target.currentState = state;
    }
}
