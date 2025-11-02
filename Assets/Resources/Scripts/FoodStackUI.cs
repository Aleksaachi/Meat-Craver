using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodStackUI : MonoBehaviour
{
    public static FoodStackUI instance;
    public TextMeshProUGUI valueBox;
    public GameObject uiObject;
    public List<Image> badges;
    public Sprite emptyBadge;
    void Awake()
    {
        instance = this;
    }
    public void FoodStackChanged(Food foodStack)
    {
        uiObject.SetActive(true);
        float value = 0;
        for (int i = 0; i < 5; i++)
        {
            if (i < foodStack.foodStack.Count)
            {
                badges[i].sprite = foodStack.foodStack[i].badge;
                value += foodStack.foodStack[i].baseValue;
            }
            else
            {
                badges[i].sprite = emptyBadge;
            }
        }
        valueBox.text = "Value: " + value.ToString() + "$";
    }
}
