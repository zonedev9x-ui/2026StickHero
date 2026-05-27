using UnityEngine;

public class Entity : MonoBehaviour
{
    public StrengthScore strengthScore;

    private bool isInteraction = false;

    public bool IsInteraction(bool isOn)
    {
        return isInteraction = isOn;
    }

    protected virtual void EnableStrengthScore(bool isOn)
    {
        strengthScore.gameObject.SetActive(isOn);
    }

    protected virtual void UpdateStrengthScore()
    {

    }
}
