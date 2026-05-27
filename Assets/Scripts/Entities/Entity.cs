using UnityEngine;

public class Entity : MonoBehaviour
{
    public StrengthScore strengthScore;

    public bool isInteraction = false;

    public bool IsInteraction()
    {
        return isInteraction;
    }

    public void EnableInteraction(bool isOn)
    {
        isInteraction = isOn;
    }

    protected virtual void EnableStrengthScore(bool isOn)
    {
        strengthScore.gameObject.SetActive(isOn);
    }
}
