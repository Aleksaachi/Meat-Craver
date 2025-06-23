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
        }
        else if (PlayerInteractor.instance.heldItem == null)
        {
            Grab();
        }
    }

    public override void SecondaryInteract()
    {
        // Find the players hand item
        ItemInstance handItem = PlayerInteractor.instance.heldItem.GetComponent<ItemInstance>();
        if (handItem == null) return;

        // Find if both the ground and hand items are food
        Food thisFood = thisItem.GetFood();
        Food mainFood = handItem.thisItem.GetFood();

        if (thisFood != null && mainFood != null)
        {
            // Find a bun
            Food bun = null;
            if (thisFood.foodType == Food.FoodItem.bun)
            {
                bun = thisFood;
            }

            if (mainFood.foodType == Food.FoodItem.bun)
            {
                // Return if both are buns
                if (bun != null)
                {
                    Debug.Log("You cannot double bun!");
                    return;
                }
                bun = mainFood;
            }

            // If a bun was found assemble them
            if (bun != null)
            {
                if (bun == thisFood)
                {
                    bun.StackFood(this, handItem);
                }
                else
                {
                    bun.StackFood(handItem, this);
                }
            }
        }
    }

    public void Grab()
    {
        transform.SetParent(PlayerInteractor.instance.rightHand.transform);
        transform.localPosition = Vector3.zero;
        GetComponent<Collider>().enabled = false;
        PlayerInteractor.instance.heldItem = gameObject;
        UntangleFromSlot();        

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

    public void UntangleFromSlot()
    {
        if (spot != null && spot.itemInSpot == gameObject)
        {
            spot.itemInSpot = null;
        }
        spot = null;
    }
}
