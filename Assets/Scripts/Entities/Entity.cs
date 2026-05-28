using UnityEngine;

public class Entity : MonoBehaviour
{
    public StrengthScore strengthScore;
    public Entity currentTarget;

    protected virtual void EnableStrengthScore(bool isOn)
    {
        strengthScore.gameObject.SetActive(isOn);
    }
}
