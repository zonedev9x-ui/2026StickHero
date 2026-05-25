using UnityEngine;
using TMPro;

public class StrengthScore : MonoBehaviour
{   
    public TMP_Text txt_strengthScore;

    private int strengthScore;

    public void InitStrengthScore(StrengthType type, int score)
    {
        strengthScore = score;

        switch (type)
        {
            case StrengthType.Add:
                txt_strengthScore.text = $"+ " + strengthScore.ToString();
                break;
            case StrengthType.Subtract:
                txt_strengthScore.text = $"- " + strengthScore.ToString();
                break;
            case StrengthType.Multiply:
                txt_strengthScore.text = $"x " + strengthScore.ToString();
                break;
        }
    }

    public void AddStrengthScore(int score)
    {
        strengthScore += score;
        txt_strengthScore.text = strengthScore.ToString();
    }

    public void SubtractStrengthScore(int score)
    {
        strengthScore -= score;
        txt_strengthScore.text = strengthScore.ToString();
    }
}
