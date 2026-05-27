using UnityEngine;
using TMPro;
using System.Collections;

public class StrengthScore : MonoBehaviour
{   
    public TMP_Text txt_strengthScore;
    public int score;

    private int uiScore = 0;
    private Coroutine countCoroutine;

    public void InitStrengthScore(StrengthScoreType type, int score)
    {
        this.score = score;
        uiScore = score;

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

    private IEnumerator CountUp()
    {
        WaitForSeconds delay = new WaitForSeconds(0.01f);

        while (uiScore < score)
        {
            uiScore++;
            txt_strengthScore.text = uiScore.ToString();

            yield return delay;
        }

        countCoroutine = null;

        yield return delay;
    }
}
