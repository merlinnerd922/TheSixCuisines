using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Extend;
using Helper.ExtendSpace;
using TSC.Game;
using UnityEngine;
using UnityUtils;

/// <summary>
/// The UI food menu manager for managing the repertoire of food in your inventory.
/// </summary>
public class DishMenu : HUDDisplay
{

    /// <summary>
    /// The GameObject child of this dish menu holding all of the dishes represented as menu items.
    /// </summary>
    public GameObject menuHolder;

    /// <summary>
    /// The scene manager for the current game being played.
    /// </summary>
    public GameSceneManager gameSceneManager;

    /// <summary>
    /// The display for the player's cash on hand.
    /// </summary>
    public CashOnHand cashDisplay;

    /// <summary>
    /// Set the amount of the dish <paramref name="dishManaged"/> that this player has to <paramref name="getFoodAmount"/>. 
    /// </summary>
    /// <param name="dishManaged">The dish the player has.</param>
    /// <param name="getFoodAmount">The amount of that dish that the player has.</param>
    public void SetFoodAmountToPurchase(Dish dishManaged, int getFoodAmount)
    {
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
    /// Purchase all the dish ingredient sets currently being listed as to be purchased by the current UI. (I.e.
    /// if the UI currently says "Buy: X" where X is the number of ingredients sets to buy for dish Y, then increment
    /// the dish stock of that dish by X).
    /// </summary>
    public void BuyIngredients()
    {
        // For each dish being controlled:
        foreach (FoodItemController controller in this.GetFoodItemControllers())
        {
            // Calculate the new value of the provided ingredient in stock by adding the amount that is currently
            // set as the amount to buy to the amount the user currently has in stock.
            int amountOfFoodToSet = this.gameSceneManager.gameState.GetAmountInInventory(controller.dishManaged) +
                                    controller.GetAmountOfFood();

            // Set this new value in both the internal food menu mapping as well as on the UI side. 
            this.gameSceneManager.gameState.menuInventory[controller.dishManaged] = amountOfFoodToSet;
            controller.SetAmountOfDishesInStockInUI(amountOfFoodToSet);

            // Set the amount of food being bought back to 0.
            SetFoodAmountToPurchase(controller.dishManaged, 0);
        }

        // Set the player's cash on hand accordingly.
        this.gameSceneManager.gameState.cashOnHand = cashDisplay.cashOnHand;
    }

    /// <summary>
    /// Return an enumeration of all of this menu's controllers for its FoodItem amounts.
    /// </summary>
    /// <returns>an enumeration of all of this menu's controllers for its FoodItem amounts.</returns>
    public IEnumerable<FoodItemController> GetFoodItemControllers()
    {
        return this.menuHolder.GetComponentsInChildren<FoodItemController>();
    }

    /// <summary>
    /// Return the amount of the dish <paramref name="dishManaged"/> that is being marked as being bought as currently displayed in the UI.
    /// </summary>
    /// <param name="dishManaged">Ths dish being purchased.</param>
    /// <returns>The amount of the dish <paramref name="dishManaged"/> that is being marked as being bought as currently displayed in the UI.</returns>
    public int GetFoodAmountToPurchase(Dish dishManaged)
    {
        return GetFoodItemController(dishManaged).amountToBuy;
    }

    /// <summary>
    /// Set the inventory dish counts for all dishes in inventory to the values set in the mapping <paramref name="dishMapping"/>.
    /// </summary>
    /// <param name="dishMapping">The mapping to set the dishes' count as displayed in the UI to.</param>
    public void SetDishInventory(SerializableDictionary<Dish, int> dishMapping)
    {
        foreach (FoodItemController controller in this.GetFoodItemControllers())
        {
            // If the dish is in the inventory, populate the dish count from the inventory.
            int amountOfDishToSet = int.MinValue;
            if (dishMapping.ContainsKey(controller.dishManaged))
            {
                amountOfDishToSet = dishMapping[controller.dishManaged];
            }
            
            // Otherwise, if it's not, set the count simply to 0.
            else
            {
                amountOfDishToSet = 0;
            }

            controller.SetAmountOfDishesInStockInUI(amountOfDishToSet);
        }
    }

    /// <summary>
    /// Set the count of the dish <paramref name="dish"/> that the player has in inventory to <paramref name="dishCount"/>.
    /// </summary>
    /// <param name="dish">The dish whose count should be set.</param>
    /// <param name="dishCount">The count to set the dish to.</param>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public void SetDishInInventory(Dish dish, int dishCount)
    {
        FoodItemController foodItemController = GetFoodItemController(dish);
        foodItemController.SetAmountOfDishesInStockInUI(dishCount);
    }

    /// <summary>
    /// Set information on this menu's dishes to that of the dishes stored in the GameState object <paramref name="gameState"/>.
    /// </summary>
    /// <param name="gameState">The GameState whose dish details should be set from.</param>
    public void SetDishDetails(GameState gameState)
    {
        // Set the amount of each dish the player has in inventory to the value stored in the menu inventory provided.
        this.SetDishInventory(gameState.menuInventory);

        // Set information on each dish's (1) ingredient cost; (2) retail price; (3) amount bought yesterday.
        foreach (FoodItemController foodItemController in this.GetFoodItemControllers())
        {
            Dish dishManaged = foodItemController.dishManaged;
            
            foodItemController.retailPriceText.text = $"${dishManaged.GetRetailPrice()}";
            foodItemController.ingredientCostText.text =
                $"Ingredient Cost: ${dishManaged.GetIngredientCost()}";
            foodItemController.boughtYesterdayText.text =
                $"Sold yesterday: {gameState.GetSoldYesterday(dishManaged)}";
        }
        
    }

    /// <summary>
    /// Initialize this menu of dishes.
    /// </summary>
    /// <param name="_gameSceneManager">The GameSceneManager to </param>
    public void InitializeDishMenu(GameSceneManager _gameSceneManager)
    {
        // Start by clearing out all entries in the menu.
        GameObject dishMenuMenuHolder = this.menuHolder;
        dishMenuMenuHolder.DestroyAllChildren();

        // For each dish, instantiate a prefab to represent the dish.
        foreach (Dish dish in _gameSceneManager.GetAcquiredRecipes())
        {
            GameObject instantiatedMenuItem = Instantiate(_gameSceneManager.FOOD_CONTROLLER_PREFAB);
            dishMenuMenuHolder.AddChild(instantiatedMenuItem, false);

            // Then, initialize the script attached to the prefab with information on this GameSceneManager.
            FoodItemController foodItemController = instantiatedMenuItem.GetComponent<FoodItemController>();
            foodItemController.Initialize(dish, _gameSceneManager);
        }

        this.SetDishDetails(this.gameSceneManager.gameState);
    }

}