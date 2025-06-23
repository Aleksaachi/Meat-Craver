using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadBox : Interactable
{
    public Transform door;
    public float doorTime;
    float timer;
    public ItemSpot itemSpot;
    public Item bread;
    public bool open;

    public override void Interact()
    {
        open = !open;
        if (open)
        {
            itemSpot.SpawnItem(bread);
        }
    }

    void Update()
    {
        if (!open)  timer += Time.deltaTime;
        else       timer -= Time.deltaTime;

        timer = Mathf.Clamp(timer, 0, doorTime);
        door.rotation = Quaternion.Euler(Mathf.Lerp(0, -90, timer/doorTime), 0, 0);
    }

}
