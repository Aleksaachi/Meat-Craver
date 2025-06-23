using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/New Item")]
public class Item : ScriptableObject
{
    public enum TypeOfItem
    {
        notag,
        grillable
    }
    public TypeOfItem itemType;
    public GameObject prefab;

    public Vector3 slotOffset;

    public static void SpawnItemOfType(ItemSpot itemSpot, Item item)
    {
        string itemName = item.name;
        ItemInstance itemObject = Instantiate(item.prefab, itemSpot.transform).GetComponent<ItemInstance>();
        itemSpot.itemInSpot = itemObject.gameObject;

        itemObject.thisItem = Instantiate(item);
        itemObject.thisItem.name = itemName;

        itemObject.spot = itemSpot;

        itemObject.transform.localPosition = item.slotOffset;
        Vector3 newRotation = new Vector3(itemSpot.transform.rotation.eulerAngles.x + 180, itemSpot.transform.rotation.eulerAngles.y, itemSpot.transform.rotation.eulerAngles.z);
        itemObject.transform.localRotation = Quaternion.Euler(newRotation);
        Vector3 newScale = new Vector3(1 / itemSpot.transform.lossyScale.x, 1 / itemSpot.transform.lossyScale.y, 1 / itemSpot.transform.lossyScale.z);
        itemObject.transform.localScale = newScale;
    }

    public virtual Food GetFood()
    {
        return null;
    }
}
