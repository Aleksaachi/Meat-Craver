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
        if (focused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Unfocus();
            }
        }
    }
    public static void FocusOnObject(Vector3 stayPosition, Vector3 cameraDirection)
    {
        PlayerController.instance.LockPositionAt(stayPosition);
        CameraController.instance.FocusOnObject(cameraDirection);

        Cursor.lockState = CursorLockMode.None;
        PlayerInteractor.SetCursorVisibility(false);

        instance.focused = true;
    }

    public static void Unfocus()
    {
        instance.focused = false;
        Cursor.lockState = CursorLockMode.Locked;
        PlayerInteractor.SetCursorVisibility(true);

        PlayerInteractor.SetMouseSprite(PlayerInteractor.instance.defaultCursor);
    }
}
