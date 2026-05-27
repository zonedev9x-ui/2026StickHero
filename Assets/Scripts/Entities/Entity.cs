using UnityEngine;

public class Entity : MonoBehaviour
{
    public StrengthScore strengthScore;

    private bool isInteraction = false;

    public bool IsInteraction(bool isOn)
    {
        return isInteraction = isOn;
    }
}
