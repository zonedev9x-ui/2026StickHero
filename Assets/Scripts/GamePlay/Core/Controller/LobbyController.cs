using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public Button bt_play;

    public TMP_Text txtLevel;

    private void Awake()
    {
        bt_play.onClick.AddListener(ClickBtPlay);
    }

    private void OnEnable()
    {
        txtLevel.gameObject.SetActive(true);
        //string format = LocalizeManager.Instance.GetLocalizeText("LEVEL ");
        int level = GameData.userData.profile.currentStageId;
        txtLevel.text = string.Format("LEVEL " + level);
    }

    private void ClickBtPlay()
    {
        SceneManager.LoadScene(ConstantData.SCENE_GAME);
    }
}
