using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpCamera : AbstractCameraController
{

    private Camera managedCamera;

    [SerializeField] public float lerpSpeed = 6;

    // private Vector3 velocity = Vector3.zero;



    private Vector3 previousPlayerPosition;


    private void Awake()
    {
        this.managedCamera = this.gameObject.GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        var targetPosition = this.target.transform.position;
        var cameraPosition = this.managedCamera.transform.position;


        this.managedCamera.transform.position = Vector3.Lerp(cameraPosition, new Vector3(targetPosition.x, targetPosition.y, cameraPosition.z), lerpSpeed * Time.deltaTime);
      
    }
}
