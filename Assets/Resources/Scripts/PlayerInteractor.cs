using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public static PlayerInteractor instance;
    public float maxInteractDistance;
    public Transform rightHand;
    public GameObject heldItem;
    public Texture2D defaultCursor;
    public GameObject cursor;
    void Awake()
    {
        instance = this;
        SetMouseSprite(defaultCursor);
    }
    void Update()
    {
        if (PlayerStateManager.instance.focused) return;


        // Primary interact
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit[] hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.forward, maxInteractDistance, LayerMask.GetMask("Interactables"));
            if (hits.Length > 0) hits[0].transform.GetComponent<Interactable>().Interact();
        }
        // Secondary interact
        else if (Input.GetMouseButtonDown(1))
        {
            RaycastHit[] hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.forward, maxInteractDistance, LayerMask.GetMask("Interactables"));
            if (hits.Length > 0) hits[0].transform.GetComponent<Interactable>().SecondaryInteract();
        }
    }
    public static void SetCursorVisibility(bool active)
    {
        instance.cursor.SetActive(active);
    }
    public static void SetMouseSprite(Texture2D newSprite)
    {
        Cursor.SetCursor(newSprite, Vector2.zero, CursorMode.Auto);
    }
}
