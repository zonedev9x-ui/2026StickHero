using UnityEngine;

public class Player : Character
{
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

        oldParent = transform.parent.position;
    }

    private void OnMouseUp()
    {
        PlayAnim(ConstantData.ANIM_TRIGGER_GRAB_RELEASE);
        isDragging = false;
        transform.position = oldParent;
    }
}
