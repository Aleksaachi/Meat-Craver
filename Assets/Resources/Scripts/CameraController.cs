using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;

public class CameraController : MonoBehaviour 
{
    public static CameraController instance;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    float currentRotationY = 0;
    public float currentRotationX = 0;
    public Transform cameraRig;
    public float focusTime;
    Quaternion beginFocusAngleVector;
    Quaternion focusAngleVector;
    float focusTimer;
    void Awake()
    {
        instance = this;
    }
    void Update ()
    {
        if (PlayerStateManager.instance.focused)
        {
            if (focusTimer > 0) focusTimer -= Time.deltaTime;
            cameraRig.rotation = Quaternion.Lerp(focusAngleVector, beginFocusAngleVector, focusTimer / focusTime);
            currentRotationY = cameraRig.rotation.eulerAngles.x;
            currentRotationX = Mathf.Clamp(cameraRig.rotation.eulerAngles.y, -90, 90);
        }
        else
        {
            // Get rotational input from the mouse
            float rotationY = Input.GetAxis("Mouse Y") * sensitivityY;
            float rotationX = Input.GetAxis("Mouse X") * sensitivityX;

            currentRotationX += rotationX;
            currentRotationX = CorrectRotation(currentRotationX);

            currentRotationY += rotationY;
            currentRotationY = CorrectRotation(currentRotationY);

            currentRotationY = Mathf.Clamp(currentRotationY, -90, 90);

            cameraRig.rotation = Quaternion.Euler(new Vector3(-currentRotationY, currentRotationX, 0));
        }
    }
    void Start ()
    {   
        Cursor.lockState = CursorLockMode.Locked;
    }

    float CorrectRotation(float rotation){
        rotation = rotation % 360;
        return rotation;
    }

    public void FocusOnObject(Vector3 cameraDirection){
        focusTimer = focusTime;
        focusAngleVector = Quaternion.Euler(cameraDirection);
        beginFocusAngleVector = Camera.main.transform.rotation;
    }
}