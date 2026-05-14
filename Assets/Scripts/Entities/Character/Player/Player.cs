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

        if (currentFloor != null)
        {
            oldParent = currentFloor.SetPlayerPos();
        }
        else
        {
            oldParent = transform.parent.position;
        }
    }

    private void OnMouseUp()
    {
        PlayAnim(ConstantData.ANIM_TRIGGER_GRAB_RELEASE);
        isDragging = false;

        if (currentFloor != null)
        {
            transform.position = currentFloor.SetPlayerPos();
        }
        else
        {
            transform.position = oldParent;
        }
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
