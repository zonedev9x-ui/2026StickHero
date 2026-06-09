using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LLose : BaseUI
{
    public Button btReplay;

    protected override void Awake()
    {
        base.Awake();
        btReplay.onClick.AddListener(ClickBtReplay);
    }

    private void ClickBtReplay()
    {
        UIManager.Instance.FadeToLoadScene(ConstantData.SCENE_GAME, () =>
        {
            UIManager.Instance.ClearAllUI();
        });
    }
}
