using System.Collections;
using TMPro;
using UnityEngine;

public class CountLogic : MonoBehaviour
{
    public TMP_Text uiText;

    private int score = 0;
    private int uiScore = 0;

    private Coroutine currentCoroutine;

    private IEnumerator CountUp()
    {
        WaitForSeconds delay = new WaitForSeconds(0.01f);

        while (uiScore < score)
        {
            uiScore++;
            uiText.text = uiScore.ToString();

            yield return delay;
        }

        currentCoroutine = null;
    }

    private void Update()
    {
        if(currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        uiScore = score;
        score += 50;

        currentCoroutine = StartCoroutine(CountUp());
    }
}
