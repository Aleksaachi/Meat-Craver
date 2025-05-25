using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public static PlayerInteractor instance;
    public float maxInteractDistance;
    public Transform rightHand;
    public GameObject heldItem;
    void Awake()
    {
        instance = this;   
    }
    void Update()
    {
        if (PlayerStateManager.instance.focused) return;
        
        if (Input.GetMouseButtonDown(0)){
            RaycastHit[] hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.forward, maxInteractDistance, LayerMask.GetMask("Interactables"));
            if (hits.Length > 0) hits[0].transform.GetComponent<Interactable>().Interact();
        }
    }
}
