using UnityEngine;

public class Entity : MonoBehaviour
{
    public StrengthScore strengthScore;
    public Entity currentTarget;

    public bool isInteraction = true;

    public virtual void Init()
    {

    }

    protected virtual void EnableStrengthScore(bool isOn)
    {
        strengthScore.gameObject.SetActive(isOn);
    }

    public void SetActive(bool isOn) 
    {
        isInteraction = isOn;
    }

    public bool IsActive()
    {
        return isInteraction;
    }
}
