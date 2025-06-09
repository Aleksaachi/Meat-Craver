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
        if (spot != null)
        {
            spot.Interact();
        }else if (PlayerInteractor.instance.heldItem == null) {
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

    public void PutDown(ItemSpot newSpot)
    {
        if (newSpot.takeOnly) return;

        transform.SetParent(newSpot.transform);
        transform.localPosition = thisItem.slotOffset;

        GetComponent<Collider>().enabled = true;
        PlayerInteractor.instance.heldItem = null;
        spot = newSpot;
        spot.itemInSpot = gameObject;

        Vector3 newRotation = new Vector3(newSpot.transform.rotation.eulerAngles.x + 180, newSpot.transform.rotation.eulerAngles.y, newSpot.transform.rotation.eulerAngles.z);
        transform.localRotation = Quaternion.Euler(newRotation);
    }
}
