using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/New Food")]
public class Food : Item
{
    public Food()
    {
        foodStack = new List<Food>
        {
            this
        };
    }
    public enum FoodItem
    {
        bun,
        main,
        ingredient,
        seasoning
    }

    public List<Food> foodStack;
    public FoodItem foodType;

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
    }
}
