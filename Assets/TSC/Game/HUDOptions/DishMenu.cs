using System.Collections;
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

}