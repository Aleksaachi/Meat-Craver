using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/New Food")]
public class Food : Item
{
    public enum FoodItem
    {
        bun,
        main,
        ingredient,
        seasoning
    }

    public List<Food> foodStack;
    public FoodItem foodType;
    public Sprite badge;
    public float baseValue;
    public float penalty;

    public override Food GetFood()
    {
        return this;
    }

    public void StackFood(ItemInstance thisItem, ItemInstance otherItem)
    {
        foodStack.Add(otherItem.thisItem.GetFood());

        otherItem.UntangleFromSlot();

        otherItem.transform.SetParent(thisItem.transform);
        otherItem.transform.rotation = thisItem.transform.rotation;
        otherItem.transform.localPosition = otherItem.thisItem.slotOffset;

        otherItem.GetComponent<Collider>().enabled = false;

        if (thisItem.gameObject == PlayerInteractor.instance.heldItem)
        {
            FoodStackUI.instance.FoodStackChanged(thisItem.thisItem.GetFood());
        }
        else
        {
            FoodStackUI.instance.uiObject.SetActive(false);
        }
    }
}
