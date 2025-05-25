using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpot : Interactable
{
    public GameObject itemInSpot;
    public Item.TypeOfItem filter;
    public bool takeOnly;
    public override void Interact()
    {
        if (PlayerInteractor.instance.rightHand.transform.childCount == 1){
            ItemInstance handItem = PlayerInteractor.instance.heldItem.GetComponent<ItemInstance>();
            if (handItem.thisItem.itemType != filter && filter != Item.TypeOfItem.notag) return;

            if (itemInSpot == null){
                handItem.PutDown(this);
            }else{
                ItemInstance currentItem = itemInSpot.GetComponent<ItemInstance>();

                handItem.PutDown(this);
                currentItem.Grab();
            }
        }else if (itemInSpot != null){
            ItemInstance currentItem = itemInSpot.GetComponent<ItemInstance>();
            currentItem.Grab();
        }
    }

    public void SpawnItem(Item item){
        Item.SpawnItemOfType(this, item);
    }

    public void ClearItem(){
        Destroy(itemInSpot);
        itemInSpot = null;
    }

    public Item GetItem(){
        return itemInSpot.GetComponent<ItemInstance>().thisItem;
    }
}
