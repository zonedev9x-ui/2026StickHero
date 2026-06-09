using UnityEngine;

public class BaseUI : MonoBehaviour
{
    public bool isOverlay { get; set; }
    public bool isLoadFromResources { get; set; } = false;
    public bool isBackable { get; set; } = true;
    public bool isPoolingWhenClose { get; set; } = false;

    protected virtual void Awake() { }

    protected virtual void OnEnable()
    {
        
    }

    protected virtual void OnDisable()
    {
    }
}
