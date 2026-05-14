using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public List<Floor> floors;

    private void OnEnable()
    {
        
    }

    public void ShowAllHighlightNormal()
    {
        for (int i = 0; i < floors.Count; i++)
        {
            floors[i].ShowHighLight();
        }
    }

    public void HideAllHighlightNormal()
    {
        for (int i = 0; i < floors.Count; i++)
        {
            floors[i].HideHighLight();
        }
    }
}