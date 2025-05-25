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

    void Start()
    {
        for (int i = 0; i < sausageSpots.Count; i++){
            cookTime.Add(0);
            cookStage.Add(0);
        }
    }
    void Update()
    {
        for (int i = 0; i < sausageSpots.Count; i++){
            if (sausageSpots[i].itemInSpot != null){
                // Find the dog
                GameObject sausage = sausageSpots[i].itemInSpot;
                // Rotated the dog
                sausage.transform.rotation = Quaternion.Euler(0, 0, sausage.transform.rotation.eulerAngles.z + 180 * Time.deltaTime);
                // Cook the dog
                cookTime[i] += Time.deltaTime;

                // Check cook stages
                if (cookTime[i] > 10f && sausageSpots[i].GetItem().name == "Sausage"){
                    // Stage 1
                    sausageSpots[i].ClearItem();
                    sausageSpots[i].SpawnItem(sausageCooked);
                    cookStage[i]++;
                }else if (cookTime[i] > 20f && sausageSpots[i].GetItem().name == "SausageCooked"){
                    // Stage 2
                    sausageSpots[i].ClearItem();
                    sausageSpots[i].SpawnItem(sausageBurnt);
                    cookStage[i]++;
                }
            }else if (cookTime[i] != 0f){
                // Reset spot
                cookTime[i] = 0;
                cookStage[i] = 0;
            }
        }
    }
}
