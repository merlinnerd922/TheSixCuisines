﻿using System.Collections;
using System.Collections.Generic;
using Extend;
using TSC.Game;
using UnityEngine;
using UnityUtils;

/// <summary>
/// The UI food menu manager for managing the repertoire of food in your inventory.
/// </summary>
public class DishMenu : HUDDisplay
{

    /// <summary>
    /// A mapping between dishes and the amount of them the player has in inventory.
    /// </summary>
    public Dictionary<Dish, int> foodMenuMapping = new Dictionary<Dish, int>();


    /// <summary>
    /// The GameObject child of this dish menu holding all of the dishes represented as menu items.
    /// </summary>
    public GameObject menuHolder;

    /// <summary>
    /// Set the amount of the dish <paramref name="dishManaged"/> that this player has to <paramref name="getFoodAmount"/>. 
    /// </summary>
    /// <param name="dishManaged">The dish the player has.</param>
    /// <param name="getFoodAmount">The amount of that dish that the player has.</param>
    public void SetFoodAmount(Dish dishManaged, int getFoodAmount)
    {
        this.foodMenuMapping[dishManaged] = getFoodAmount;

        // Set the new dish amount in the UI.
        FoodItemController foodItemController = GetFoodItemController(dishManaged);
        foodItemController.SetAmountOfFoodToBuy(getFoodAmount);
    }

    /// <summary>
    /// Return the FoodItemController for the provided dish.
    /// </summary>
    /// <param name="dishManaged"></param>
    /// <returns></returns>
    private FoodItemController GetFoodItemController(Dish dishManaged)
    {
        // The FoodItemController associated with the provided dish is stored in this
        GameObject child = this.menuHolder.GetChild(dishManaged.GetCamelCaseString());
        return child.GetComponent<FoodItemController>();
    }

    /// <summary>
    /// Return the amount of the provided dish <paramref name="dish"/> that the player has.
    /// </summary>
    /// <returns>The amount of the provided dish <paramref name="dish"/> that the player has.</returns>
    public int GetFoodAmount(Dish dish)
    {
        return this.foodMenuMapping[dish];
    }

    /// <summary>
    /// Purchase all the dish ingredient sets currently being listed as to be purchased by the current UI. (I.e.
    /// if the UI currently says "Buy: X" where X is the number of ingredients sets to buy for dish Y, then increment
    /// the dish stock of that dish by X).
    /// </summary>
    public void BuyIngredients()
    {
        // For each dish being controlled:
        foreach (FoodItemController controller in this.menuHolder.GetComponentsInChildren<FoodItemController>())
        {
            // Calculate the new value of the provided ingredient in stock by adding the amount that is currently
            // set as the amount to buy to the amount the user currently has in stock.
            int amountOfFoodToSet = this.foodMenuMapping[controller.dishManaged] + controller.GetAmountOfFood();
            
            // Set this new value in both the internal food menu mapping as well as on the UI side. 
            this.foodMenuMapping[controller.dishManaged] = amountOfFoodToSet;
            controller.SetAmountOfDishesInStockInUI(amountOfFoodToSet);
            
            // Set the amount of food being bought back to 0.
            controller.SetAmountOfFoodToBuy(0);
        }
    }

}