using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public List<Transform> spawnPos;
    public List<GameObject> highlights;

    private bool isFloorEmpty = false;
    private bool isLock = false;
    private int currentEnemyIndex = 1;

    private void Start()
    {
        for (int i = 0; i < highlights.Count; i++)
        {
            highlights[i].gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(ConstantData.TAG_DRAGGABLE))
        {
            if (isFloorEmpty == false)
            {   
                ShowHighLightSelect();
            }
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag(ConstantData.TAG_DRAGGABLE))
    //    {
    //        if (isFloorEmpty == false)
    //        {
    //            HideHighLightSelect();
    //        }
    //    }
    //}

    private void ShowHighLight()
    {
        highlights[0].gameObject.SetActive(true);
    }

    private void HideHighLight()
    {
        highlights[0].gameObject.SetActive(false);
    }

    private void ShowHighLightSelect()
    {
        highlights[0].gameObject.SetActive(false);
        highlights[1].gameObject.SetActive(true);
    }

    public void HideHighLightSelect()
    {
        highlights[1].gameObject.SetActive(false);
    }

    public Vector3 SetPlayerPos()
    {
        return spawnPos[1].position;
    }
}
