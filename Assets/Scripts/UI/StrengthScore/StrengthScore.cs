using UnityEngine;
using TMPro;

public class StrengthScore : MonoBehaviour
{   
    public TMP_Text txt_strengthScore;

    public int score;

    public void InitStrengthScore(StrengthScoreType type, int score)
    {
        this.score = score;
        switch (type)
        {   
            case StrengthScoreType.None:
                txt_strengthScore.text = score.ToString();
                break;
            case StrengthScoreType.Add:
                txt_strengthScore.text = $"+ " + score.ToString();
                break;
            case StrengthScoreType.Subtract:
                txt_strengthScore.text = $"- " + score.ToString();
                break;
            case StrengthScoreType.Multiply:
                txt_strengthScore.text = $"x " + score.ToString();
                break;
        }
    }

    public void AddStrengthScore(int score)
    {
        this.score += score;
        txt_strengthScore.text = this.score.ToString();
    }

    public void SubtractStrengthScore(int score)
    {
        this.score -= score;
        txt_strengthScore.text = this.score.ToString();
    }
}
