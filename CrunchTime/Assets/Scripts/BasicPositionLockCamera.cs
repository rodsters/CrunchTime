using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code taken from ECS 189L Project 2
public class BasicPositionLockCamera : MonoBehaviour
{
    private Camera managedCamera;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        this.managedCamera = this.gameObject.GetComponent<Camera>();
    }

    void LateUpdate()
    {
        var targetPosition = this.target.transform.position;
        var cameraPosition = this.managedCamera.transform.position;

        if (targetPosition.x != cameraPosition.x)
        {
            cameraPosition.x = targetPosition.x;
        }

        if (targetPosition.y != cameraPosition.y)
        {
            cameraPosition.y = targetPosition.y;
        }

        this.managedCamera.transform.position = cameraPosition;
    }
}
