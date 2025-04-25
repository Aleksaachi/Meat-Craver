using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CameraController cameraController;
    Rigidbody thisRigidbody;
    public float speed = 5;

    void Start()
    {
        cameraController = GetComponent<CameraController>();
        thisRigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {   
        float forwardInput = Input.GetAxisRaw("Vertical");
        float sideInput = Input.GetAxisRaw("Horizontal");
        float rotation = cameraController.currentRotationX * Mathf.Deg2Rad;
        if (forwardInput != 0){
            Vector3 direction = new Vector3(Mathf.Sin(rotation), 0, Mathf.Cos(rotation));
            thisRigidbody.AddForce(direction * speed * forwardInput);
        }   

        if (sideInput != 0){
            float directionOffset = 90 * Mathf.Deg2Rad * sideInput;
            Vector3 direction = new Vector3(Mathf.Sin(rotation + directionOffset), 0, Mathf.Cos(rotation + directionOffset));
            thisRigidbody.AddForce(direction * speed);
        }
    }
}
