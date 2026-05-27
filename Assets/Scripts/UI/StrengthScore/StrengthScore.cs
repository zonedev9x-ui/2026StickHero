using UnityEngine;
using TMPro;
using System.Collections;

public class StrengthScore : MonoBehaviour
{
    public StrengthScoreType scoreType = StrengthScoreType.None;
    public TMP_Text txt_strengthScore;
    public int score;
    public int uiScore;

    private Coroutine countCoroutine;

    public void InitStrengthScore(StrengthScoreType type, int score)
    {
        this.score = score;
        uiScore = score;
        scoreType = type;

        switch (scoreType)
        {   
            case StrengthScoreType.None:
                txt_strengthScore.text = uiScore.ToString();
                break;
            case StrengthScoreType.Add:
                txt_strengthScore.text = $"+ " + uiScore.ToString();
                break;
            case StrengthScoreType.Subtract:
                txt_strengthScore.text = $"- " + uiScore.ToString();
                break;
            case StrengthScoreType.Multiply:
                txt_strengthScore.text = $"x " + uiScore.ToString();
                break;
        }
    }

    public void AddStrengthScore(int score)
    {
        this.score += score;
        CountStrengthScore();
    }

    public void SubtractStrengthScore(int score)
    {
        this.score -= score;
        CountStrengthScore();
    }

    public void MultiplyStrengthScore(int score)
    {
        this.score *= score;
        CountStrengthScore();
    }

    private void CountStrengthScore()
    {
        if (countCoroutine != null)
        {
            StopCoroutine(countCoroutine);
        }

        countCoroutine = StartCoroutine(IECountScore());
    }

    private IEnumerator IECountScore()
    {
        WaitForSeconds delay = new WaitForSeconds(0.05f);

        while (uiScore != score)
        {
            Debug.Log("IECountScore");

            if (uiScore < score)
                uiScore++;
            else
                uiScore--;

            txt_strengthScore.text = uiScore.ToString();

            yield return delay;
        }

        countCoroutine = null;
    }
}
