using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LWin : BaseUI
{
    public Button btNext;

    public float timeShowBtNext = 2f;

    private bool isDoingNextLevel = false;

    protected override void Awake()
    {
        base.Awake();
        btNext.onClick.AddListener(ClickBtNext);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        btNext.gameObject.SetActive(false);

        this.StartDelayAction(timeShowBtNext, () =>
        {
            btNext.gameObject.SetActive(true);
            btNext.transform.localScale = Vector3.zero;
            btNext.transform.DOScale(1, 0.2f);
        }
        );
    }

    private void ClickBtNext()
    {
        UIManager.Instance.FadeToLoadScene(ConstantData.SCENE_GAME, () =>
        {
            UIManager.Instance.ClearAllUI();
        });
    }
}
