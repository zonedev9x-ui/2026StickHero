using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public Button bt_play;

    private void Awake()
    {
        bt_play.onClick.AddListener(ClickBtPlay);
    }

    private void ClickBtPlay()
    {
        SceneManager.LoadScene(ConstantData.SCENE_GAME);
    }
}
