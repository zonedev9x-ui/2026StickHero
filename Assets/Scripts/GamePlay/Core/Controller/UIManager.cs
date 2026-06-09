using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum FadeColor
{
    White,
    Black
}

public class UIManager : Singleton<UIManager>
{
    public string UI_PREFAB_PATH = "UI/";

    public Image imgFade;

    public RectTransform groupScreenOverlayUI;

    private bool isFading;
    private Dictionary<string, BaseUI> cachedUIs = new Dictionary<string, BaseUI>();
    private Stack<BaseUI> activeUIs = new Stack<BaseUI>();

    private const int DEFAULT_SORTING_ORDER_OVERLAY = 1000;
    private const int DEFAULT_SORTING_ORDER = 5;
    private const int SORTING_ORDER_STEP = 20;

    protected void Awake()
    {
        DontDestroyOnLoad(this);
    }

    protected void Start()
    {
        float width = groupScreenOverlayUI.sizeDelta.x;
        float height = groupScreenOverlayUI.sizeDelta.x;
        Debug.Log($"SCREEN SIZE: {width}, {height}");

        //if (width > height)
        //{
        //    GameConfig.RATIO = width / height;
        //}
        //else
        //{
        //    GameConfig.RATIO = height / width;

        //}
        //GameConfig.IS_TABLET = GameConfig.RATIO <= 1.6f;
    }

    public BaseUI LoadUI(string key, bool isBackable = true, bool isPoolingWhenClose = false, bool isOverlay = true, bool isPauseMusic = false)
    {
        BaseUI obj = null;
        if (isPauseMusic)
        {
            //AudioManager.Instance.PauseMusic();
        }

        if (cachedUIs.ContainsKey(key))
        {
            obj = cachedUIs[key];

            if (obj == null)
            {
                DebugCustom.LogError("Error obj null in dictionary (key=" + key + "). Remove and reload.");
                cachedUIs.Remove(key);
            }
            else
            {
                obj.transform.SetParent(null);
            }
        }

        if (obj == null)
        {
            BaseUI prefab = Resources.Load<BaseUI>(UI_PREFAB_PATH + key);

            if (prefab == null)
            {
                DebugCustom.LogError("UI key not found=" + key);
                return null;
            }

            obj = Instantiate(prefab);
            obj.gameObject.name = key;
            cachedUIs.Add(key, obj);
        }

        if (activeUIs.Contains(obj) == false)
            activeUIs.Push(obj);

        Canvas canvas = obj.GetComponent<Canvas>();
        if (canvas != null)
        {
            if (isOverlay)
            {
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;

                List<BaseUI> remainingUIs = activeUIs.Where(x => x != null && x.isOverlay).ToList();
                canvas.sortingOrder = DEFAULT_SORTING_ORDER_OVERLAY + ((remainingUIs.Count + 1) * SORTING_ORDER_STEP);
            }
            else
            {
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                canvas.worldCamera = Camera.main;
                canvas.sortingLayerName = ConstantData.SORTING_LAYER_OVERLAY;

                List<BaseUI> remainingUIs = activeUIs.Where(x => x != null && x.isOverlay == false).ToList();
                canvas.sortingOrder = DEFAULT_SORTING_ORDER + ((remainingUIs.Count + 1) * SORTING_ORDER_STEP);
            }
        }

        obj.isOverlay = isOverlay;
        obj.isBackable = isBackable;
        obj.isPoolingWhenClose = isPoolingWhenClose;
        obj.isLoadFromResources = true;
        obj.gameObject.SetActive(true);
        return obj;
    }

    //public void HideUI(BaseUI uiObject)
    //{
    //    if (!cachedUIs.ContainsKey(uiObject.gameObject.name) && uiObject.isPoolingWhenClose)
    //    {
    //        cachedUIs.Add(uiObject.gameObject.name, uiObject);
    //    }

    //    BaseUI lastestPopup = activeUIs.Peek();
    //    if (lastestPopup != null)
    //    {
    //        if (lastestPopup == uiObject)
    //        {
    //            activeUIs.Pop();

    //            if (!uiObject.isPoolingWhenClose)
    //            {
    //                if (cachedUIs.ContainsKey(uiObject.gameObject.name))
    //                {
    //                    cachedUIs.Remove(uiObject.gameObject.name);
    //                }

    //                Destroy(uiObject.gameObject);
    //            }
    //            else
    //            {
    //                uiObject.transform.parent = groupScreenOverlayUI;
    //            }
    //        }
    //        else
    //        {
    //            DebugCustom.Log(string.Format("HideUI={0}, LastestUI={1}", uiObject.name, lastestPopup.name));

    //            if (cachedUIs.ContainsKey(uiObject.gameObject.name))
    //            {
    //                cachedUIs.Remove(uiObject.gameObject.name);
    //            }

    //            Destroy(uiObject.gameObject);
    //        }
    //    }
    //}

    #region Fade

    public void FadeToLoadScene(string sceneName = null, UnityAction actionBeforeLoad = null)
    {
        Fade(color: FadeColor.Black, toMaxCallback: () =>
        {
            if (actionBeforeLoad != null)
                actionBeforeLoad();

            if (!string.IsNullOrEmpty(sceneName))
            {
                ConstantData.sceneNext = sceneName;
                SceneManager.LoadScene(sceneName);
            }
        });
    }

    public void Fade(FadeColor color = FadeColor.White, float fadingSpeedToMax = 7f, float fadingSpeedBackToMin = 1f,
        UnityAction toMaxCallback = null, UnityAction toMinCallback = null)
    {
        if (isFading == false)
        {
            isFading = true;
            StartCoroutine(CoroutineFade(color, fadingSpeedToMax, fadingSpeedBackToMin, toMaxCallback, toMinCallback));
        }
    }

    private IEnumerator CoroutineFade(FadeColor color, float fadingSpeedToMax, float fadingSpeedBackToMin,
        UnityAction toMaxCallback, UnityAction toMinCallback)
    {
        imgFade.color = color == FadeColor.White ? Color.white : Color.black;
        Color c = imgFade.color;
        c.a = 0f;
        imgFade.color = c;
        imgFade.gameObject.SetActive(true);
        bool isFadingToMax = true;

        while (isFading)
        {
            if (isFadingToMax)
            {
                c.a = Mathf.MoveTowards(c.a, 1f, fadingSpeedToMax * Time.deltaTime);
                imgFade.color = c;

                if (c.a >= 0.95f)
                {
                    c.a = 1f;
                    imgFade.color = c;
                    isFadingToMax = false;

                    if (toMaxCallback != null)
                    {
                        yield return new WaitForEndOfFrame();
                        toMaxCallback();
                    }
                }
            }
            else
            {
                c.a = Mathf.MoveTowards(c.a, 0f, fadingSpeedBackToMin * Time.deltaTime);
                imgFade.color = c;

                if (c.a <= 0.05f)
                {
                    c.a = 0f;
                    imgFade.color = c;
                    isFading = false;

                    if (toMinCallback != null)
                    {
                        yield return new WaitForEndOfFrame();
                        toMinCallback();
                    }

                    //ActiveShield(false);
                    imgFade.gameObject.SetActive(false);
                }
            }

            yield return null;
        }
    }

    public void ClearAllUI()
    {
        activeUIs.Clear();

        foreach (var kv in cachedUIs)
        {
            if (kv.Value != null)
            {
                Destroy(kv.Value.gameObject);
            }
        }

        cachedUIs.Clear();
    }

    #endregion
}
