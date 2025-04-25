using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;

public class CameraController : MonoBehaviour 
{
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    float currentRotationY = 0;
    public float currentRotationX = 0;
    public Transform cameraRig;
    void Update ()
    {
        //Gets rotational input from the mouse
        float rotationY = Input.GetAxis("Mouse Y") * sensitivityY;
        float rotationX = Input.GetAxis("Mouse X") * sensitivityX;

        currentRotationX += rotationX;
        currentRotationX = CorrectRotation(currentRotationX);

        currentRotationY += rotationY;
        currentRotationY = CorrectRotation(currentRotationY);

        currentRotationY = Mathf.Clamp(currentRotationY, -90, 90);

        cameraRig.rotation = Quaternion.Euler(new Vector3(-currentRotationY, currentRotationX, 0));
    }
    void Start ()
    {   
        Cursor.lockState = CursorLockMode.Locked;
    }

    float CorrectRotation(float rotation){
        rotation = rotation % 360;
        return rotation;
    }
}