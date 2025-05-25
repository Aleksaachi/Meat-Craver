using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausageMachine : Interactable
{
    public ItemSpot sausageSpot;
    public Item sausage;
    public override void Interact()
    {
        PlayerStateManager.FocusOnObject(transform.position + transform.forward, -transform.rotation.eulerAngles);
    }

    public void SpawnSausage(){
        sausageSpot.SpawnItem(sausage);
    }
}
