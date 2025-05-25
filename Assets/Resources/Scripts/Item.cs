using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Item")]
public class Item : ScriptableObject
{
    public enum TypeOfItem{
        notag,
        grillable
    }

    public TypeOfItem itemType;

    public GameObject prefab;

    public static void SpawnItemOfType(ItemSpot itemSpot, Item item)
    {
        ItemInstance itemObject = Instantiate(item.prefab, itemSpot.transform.position, itemSpot.transform.rotation).GetComponent<ItemInstance>();
        itemSpot.itemInSpot = itemObject.gameObject;

        itemObject.thisItem = Instantiate(item);
        itemObject.spot = itemSpot;

        itemObject.transform.rotation = Quaternion.identity;
    }
}
