using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemSpot : Interactable
{
    public Action<ItemSpot> placedNewItem;
    public Action<ItemSpot> takeItem;
    public GameObject itemInSpot;
    public Item.TypeOfItem filter;
    public bool takeOnly;
    public override void Interact()
    {
        if (PlayerInteractor.instance.rightHand.transform.childCount == 1)
        {
            if (takeOnly) return;

            ItemInstance handItem = PlayerInteractor.instance.heldItem.GetComponent<ItemInstance>();
            if (handItem.thisItem.itemType != filter && filter != Item.TypeOfItem.notag) return;

            if (itemInSpot == null)
            {
                handItem.PutDown(this);

                placedNewItem?.Invoke(this);
            }
            else
            {
                ItemInstance currentItem = itemInSpot.GetComponent<ItemInstance>();

                handItem.PutDown(this);
                currentItem.Grab();

                takeItem?.Invoke(this);
                placedNewItem?.Invoke(this);
            }
        }
        else if (itemInSpot != null)
        {
            ItemInstance currentItem = itemInSpot.GetComponent<ItemInstance>();

            takeItem?.Invoke(this);
            currentItem.Grab();
        }
    }

    public override void SecondaryInteract()
    {
        if (itemInSpot != null)
        {
            itemInSpot.GetComponent<ItemInstance>().SecondaryInteract();
        }
    }
    public void SpawnItem(Item item)
    {
        if (itemInSpot == null)
        {
            Item.SpawnItemOfType(this, item);
        }
        else
        {
            Debug.Log("Can't spawn a new item here. There is already an item in this slot.");
        }

        placedNewItem?.Invoke(this);
    }

    public void ClearItem()
    {
        Destroy(itemInSpot);
        itemInSpot = null;
    }

    public Item GetItem(){
        return itemInSpot.GetComponent<ItemInstance>().thisItem;
    }
}
