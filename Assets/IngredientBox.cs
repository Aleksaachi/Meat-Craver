using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBox : Interactable
{
    public int amountLeft;
    public GameObject ingredientPile;
    public Vector3 pileLowPoint;
    public Vector3 pileHighPoint;
    public Food ingredient;

    void Awake()
    {
        ingredient = Instantiate(ingredient);
    }
    public override void SecondaryInteract()
    {
        if (amountLeft == 0 || PlayerInteractor.instance.heldItem == null) return;

        // Find the players hand item

        ItemInstance handItem = PlayerInteractor.instance.heldItem.GetComponent<ItemInstance>();
        if (handItem == null) return;

        // Find  the hand food
        Food mainFood = handItem.thisItem.GetFood();

        // If the hand item isn't a bun return
        if (mainFood == null || mainFood.foodType != Food.FoodItem.bun) return;

        ItemInstance newFood = Instantiate(ingredient.prefab).GetComponent<ItemInstance>();
        mainFood.StackFood(handItem, newFood);

        amountLeft--;
        float pileZ = Mathf.Lerp(pileLowPoint.z, pileHighPoint.z, amountLeft/10f);
        ingredientPile.transform.localPosition = new Vector3(ingredientPile.transform.localPosition.x, ingredientPile.transform.localPosition.y, pileZ);
    }
}
