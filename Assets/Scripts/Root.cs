using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Root : MonoBehaviour
{
    public bool runInBackground = true;
    public Image imgPrecess;

    private float countTime = 0;
    private float timeFake = 1.5f;
    private bool isCheck = false;

    private void Awake()
    {
#if UNITY_EDITOR
        Application.runInBackground = runInBackground;
#else
        Application.runInBackground = true;
#endif
        Application.targetFrameRate = 60;
        DOTween.Init();
    }

    private void Start()
    {
        GameData.Load();
    }

    private void Update()
    {
        if (countTime < timeFake && !isCheck)
        {
            countTime += Time.deltaTime;
            imgPrecess.fillAmount = countTime / timeFake;

            if (countTime > timeFake)
            {
                isCheck = true;
                SceneManager.LoadScene(ConstantData.SCENE_LOBBY);
            }
        }
    }
}
