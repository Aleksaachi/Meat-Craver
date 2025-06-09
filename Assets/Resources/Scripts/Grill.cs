using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grill : MonoBehaviour
{
    public List<ItemSpot> sausageSpots = new();
    List<float> cookTime = new();
    List<int> cookStage = new();

    public Item sausageCooked;
    public Item sausageBurnt;

    public float timeToCook = 10f;
    public float timeToBurn = 10f;

    void Start()
    {
        for (int i = 0; i < sausageSpots.Count; i++)
        {
            cookTime.Add(0);
            cookStage.Add(0);

            sausageSpots[i].placedNewItem += UpdateSpot;
        }
    }
    void Update()
    {
        for (int i = 0; i < sausageSpots.Count; i++)
        {
            if (sausageSpots[i].itemInSpot != null)
            {
                // Find the dog
                GameObject sausage = sausageSpots[i].itemInSpot;
                // Rotated the dog
                sausage.transform.rotation = Quaternion.Euler(sausage.transform.rotation.eulerAngles.x, sausage.transform.rotation.eulerAngles.y, sausage.transform.rotation.eulerAngles.z + 180 * Time.deltaTime);
                // Cook the dog
                cookTime[i] += Time.deltaTime;

                // Check cook stages
                if (cookTime[i] > timeToCook && sausageSpots[i].GetItem().name == "Sausage")
                {
                    // Stage 1
                    sausageSpots[i].ClearItem();
                    sausageSpots[i].SpawnItem(sausageCooked);
                    cookStage[i]++;
                }
                else if (cookTime[i] > timeToCook + timeToBurn && sausageSpots[i].GetItem().name == "SausageCooked")
                {
                    // Stage 2
                    sausageSpots[i].ClearItem();
                    sausageSpots[i].SpawnItem(sausageBurnt);
                    cookStage[i]++;
                }
            }
            else if (cookTime[i] != 0f)
            {
                // Reset spot
                cookTime[i] = 0;
                cookStage[i] = 0;
            }
        }
    }

    void UpdateSpot(ItemSpot spotToUpdate)
    {
        for (int i = 0; i < sausageSpots.Count; i++)
        {
            if (sausageSpots[i] == spotToUpdate)
            {
                cookTime[i] = 0;
                cookStage[i] = 0;
                
                if (sausageSpots[i].GetItem().name == "SausageCooked")
                {
                    // Stage 1
                    cookTime[i] = timeToCook;
                    cookStage[i] = 1;
                }
                else if (sausageSpots[i].GetItem().name == "SausageBurnt")
                {
                    // Stage 2
                    cookTime[i] = timeToCook + timeToBurn;
                    cookStage[i] = 2;
                }

                break;
            }
        }
    }
}
