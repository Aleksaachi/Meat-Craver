using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemInstance : Interactable
{
    public Item thisItem;
    public ItemSpot spot;
    public override void Interact()
    {
        if (PlayerInteractor.instance.rightHand.transform.childCount == 0){
            if (spot != null) spot.itemInSpot = null;
            Grab();
        }else{
            PlayerInteractor.instance.heldItem.GetComponent<ItemInstance>().PutDown(spot);
            Grab();
        }
    }

    public void Grab(){
        transform.SetParent(PlayerInteractor.instance.rightHand.transform);
        transform.localPosition = Vector3.zero;
        GetComponent<Collider>().enabled = false;
        PlayerInteractor.instance.heldItem = gameObject;
        if (spot != null && spot.itemInSpot == gameObject){
             spot.itemInSpot = null;
        }
        spot = null;

        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void PutDown(ItemSpot newSpot){
        if (newSpot.takeOnly) return;
        
        transform.SetParent(newSpot.transform);
        transform.localPosition = Vector3.zero;
        GetComponent<Collider>().enabled = true;
        PlayerInteractor.instance.heldItem = null;
        spot = newSpot;
        spot.itemInSpot = gameObject;

        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
