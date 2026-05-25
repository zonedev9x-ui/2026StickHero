using TMPro;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public StrengthScore strengthScore;
    
    public void InitTrap(StrengthType type, int score)
    {
        strengthScore.InitStrengthScore(type, score);
    }

    public void GetParent(Transform parent)
    {
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
    }
}
