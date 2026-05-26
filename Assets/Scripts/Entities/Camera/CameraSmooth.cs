using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmooth : MonoBehaviour
{   
    public float moveSpeed = 5f;

    private Vector3 targetPos;
    public List<float> distanceTowers;
    private int currentDistance = 0;
    
    public void InitCamera(List<float> distanceTowers)
    {
        this.distanceTowers = distanceTowers;
        transform.position = new Vector3(this.distanceTowers[currentDistance], transform.position.y, transform.position.z);
        targetPos = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveNextDistanceTargets();
        }
    }

    public void MoveNextDistanceTargets()
    {
        if(distanceTowers == null && distanceTowers.Count <= 0) return;

        if(currentDistance < distanceTowers.Count - 1)
        {
            currentDistance++;
            targetPos = new Vector3(distanceTowers[currentDistance], transform.position.y, transform.position.z);
        }
    }
}
