using TMPro;
using UnityEngine;

public class LGamePlay : BaseUI
{
    public TMP_Text txtLevel;

    protected override void OnEnable()
    {
        SetLevel();
    }

    public void SetLevel()
    {
        txtLevel.gameObject.SetActive(true);
        //string format = LocalizeManager.Instance.GetLocalizeText("LEVEL ");
        int level = GameData.userData.profile.currentStageId;
        txtLevel.text = string.Format("LEVEL " + level);
    }
}
