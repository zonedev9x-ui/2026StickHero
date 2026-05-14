using UnityEngine;

public class Player : Character
{
    public Floor currentFloor;

    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 oldParent;

    void Update()
    {
        if (isDragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

        PlayAnim(ConstantData.ANIM_TRIGGER_GRAB);

        isDragging = true;

        Tower currentTower = TowerController.Instance.SetCurrentTower();
        currentTower.ShowAllHighlightNormal();
    }

    private void OnMouseUp()
    {
        PlayAnim(ConstantData.ANIM_TRIGGER_GRAB_RELEASE);
        isDragging = false;

        Tower currentTower = TowerController.Instance.SetCurrentTower();
        currentTower.HideAllHighlightNormal();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entered floor trigger: ");

        if (other.CompareTag(ConstantData.TAG_FLOOR))
        {   
            Debug.Log("Player entered floor trigger: " + other.name);

            Floor floor = other.GetComponentInParent<Floor>();

            if (floor != null)
            {
                currentFloor = floor;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(ConstantData.TAG_FLOOR))
        {
            Floor floor = other.GetComponent<Floor>();

            if (floor != null && floor == currentFloor)
            {
                currentFloor = null;
            }
        }
    }
}
