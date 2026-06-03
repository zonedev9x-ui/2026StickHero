using TMPro;
using UnityEngine;

public class ItemSupport : Entity
{
    public GameObject itemHeart;
    public GameObject itemShield;
    public GameObject itemPower;

    public void InitItemSupport(ItemSuportType suportType, StrengthScoreType strengthType, int score)
    {
        switch (suportType)
        {
            case ItemSuportType.Heart:
                itemHeart.SetActive(true);
                itemShield.SetActive(false);
                itemPower.SetActive(false);
                strengthScore.InitStrengthScore(strengthType, score);
                break;
            case ItemSuportType.Shield:
                itemHeart.SetActive(false);
                itemShield.SetActive(true);
                itemPower.SetActive(false);
                strengthScore.InitStrengthScore(strengthType, score);
                break;
            case ItemSuportType.Power:
                itemHeart.SetActive(false);
                itemShield.SetActive(false);
                itemPower.SetActive(true);
                strengthScore.InitStrengthScore(strengthType, score);
                break;
        }
    }
}
