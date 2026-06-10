using UnityEngine;

public class Entity : MonoBehaviour
{
    public StrengthScore strengthScore;

    [HideInInspector] public Entity currentTarget;

    private bool isInteraction = true;

    public virtual void Init()
    {

    }

    protected virtual void EnableStrengthScore(bool isOn)
    {
        if (strengthScore == null) return;
        strengthScore.gameObject.SetActive(isOn);
    }

    public void SetInteraction(bool isOn) 
    {
        isInteraction = isOn;
    }

    public bool IsInteraction()
    {
        return isInteraction;
    }
}
