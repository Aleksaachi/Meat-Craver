using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager instance;
    public bool focused;
    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (focused){
            if (Input.GetKeyDown(KeyCode.Escape)){
                focused = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
    public static void FocusOnObject(Vector3 stayPosition, Vector3 cameraDirection){
        PlayerController.instance.LockPositionAt(stayPosition);
        CameraController.instance.FocusOnObject(cameraDirection);
        Cursor.lockState = CursorLockMode.None;

        instance.focused = true;
    }
}
