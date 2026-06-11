using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LGamePlay : BaseUI
{
    public Button btBack;

    public TMP_Text txtLevel;

    protected override void Awake()
    {
        btBack.onClick.AddListener(OnClickBackLobby);
    }

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

    public void OnClickBackLobby()
    {
        UIManager.Instance.FadeToLoadScene(ConstantData.SCENE_LOBBY, () =>
        {
            UIManager.Instance.ClearAllUI();
        });
    }
}
