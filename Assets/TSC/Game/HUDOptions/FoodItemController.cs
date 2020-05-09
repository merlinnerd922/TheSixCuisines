using System.Collections;
using System.Collections.Generic;
using TSC.Game;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// The controller for various traits of a food item within the food item manager (e.g. count, price, etc.)
/// </summary>
public class FoodItemController : MonoBehaviour
{

    /// <summary>
    /// The food item managed by this controller.
    /// </summary>
    [FormerlySerializedAs("foodItemManaged")]
    public Dish dishManaged;

    /// <summary>
    /// The amount of the FoodItem to buy.
    /// </summary>
    public int amountToBuy = 0;


    /// <summary>
    /// The text displaying the amount of a dish the user has in stock.
    /// </summary>
    public Text inStockText;

    /// <summary>
    /// The game scene manager managing this scene.
    /// </summary>
    public GameSceneManager gameSceneManager;

    /// <summary>
    /// The text displaying the amount of food of this type to buy.
    /// </summary>
    [FormerlySerializedAs("foodAmountText")]
    public Text foodAmountToBuyText;

    /// <summary>
    /// Increment the count for this food item.
    /// </summary>
    public void Increment()
    {
        // Do nothing if the player doesn't have enough money.
        float price = this.dishManaged.GetIngredientCost();
        if (this.gameSceneManager.cashOnHandDisplay.cashOnHand < price)
        {
            return;
        }

        // Otherwise, increment the amount to buy by the provided amount.
        this.gameSceneManager.cashOnHandDisplay.DecrementCashOnHand(price);
        this.gameSceneManager.hudMenuManager.dishMenu.SetFoodAmount(this.dishManaged,
            this.gameSceneManager.hudMenuManager.dishMenu.GetFoodAmount(this.dishManaged) + 1);
    }

    /// <summary>
    /// The amount of cash the player has on hand.
    /// </summary>
    public float playerCashOnHand => gameSceneManager.gameState.cashOnHand;

    /// <summary>
    /// Confirm the purchase of the ingredients for this dish.
    /// </summary>
    public void Confirm()
    {
    }

    /// <summary>
    /// Set the amount of food of this type to buy to <paramref name="getFoodAmount"/>.
    /// </summary>
    /// <param name="getFoodAmount">The amount to set the dish buying amount to.</param>
    public void SetAmountOfFoodToBuy(int getFoodAmount)
    {
        this.foodAmountToBuyText.text = getFoodAmount.ToString();
    }

    /// <summary>
    /// Return the currently displayed number of dishes to buy for the dish this controller is managing. <remarks>(By "buying a 
    /// dish"
    /// we mean buying all the ingredients required to make one instance of that dish. We make all of our stuff fresh!)</remarks>
    /// </summary>
    /// <returns>Return the currently displayed number of dishes to buy for the dish this controller is managing.</returns>
    public int GetAmountOfFood()
    {
        return this.amountToBuy;
    }

    /// <summary>
    /// Set the amount of dishes that the user has in stock of the stored dish to <paramref name="amountOfDishToSet"/>. 
    /// </summary>
    /// <param name="amountOfDishToSet">The value to set the amount of dishes the user has of this particular dish to.</param>
    public void SetAmountOfDishesInStockInUI(int amountOfDishToSet)
    {
        inStockText.text = amountOfDishToSet.ToString();
    }

}