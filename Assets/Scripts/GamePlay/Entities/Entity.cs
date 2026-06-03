using UnityEngine;

public class Entity : MonoBehaviour
{
    public StrengthScore strengthScore;
    public Entity currentTarget;

    public bool isActive = true;

    protected virtual void EnableStrengthScore(bool isOn)
    {
        strengthScore.gameObject.SetActive(isOn);
    }

    public void SetActive(bool isOn) 
    {
        isActive = isOn;
    }

    public bool IsActive()
    {
        return isActive;
    }
}
