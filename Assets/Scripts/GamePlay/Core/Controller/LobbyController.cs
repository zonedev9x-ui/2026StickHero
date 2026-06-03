using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public Button bt_play;

    private void ClickBtPlay()
    {
        SceneManager.LoadScene(ConstantData.SCENE_GAME);
    }
}
