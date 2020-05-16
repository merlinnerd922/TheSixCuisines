using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructures.RandomSelector;
using Extend;
using Helper;
using Helper.ExtendSpace;
using TSC.Game;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils;
using Object = UnityEngine.Object;

/// <summary>
/// A manager for the game scene.
/// </summary>
public class GameSceneManager : MonoBehaviour
{

    /// <summary>
    /// The manager for the in-game menu.
    /// </summary>
    public GameMenuManager menuManager;

    /// <summary>
    /// The current state of the game.
    /// </summary>
    public GameState gameState;

    /// <summary>
    /// The display for the current turn.
    /// </summary>
    public TurnDisplay turnDisplay;

    /// <summary>
    /// The display for the player's current cash on hand.
    /// </summary>
    public CashOnHand cashOnHandDisplay;

    /// <summary>
    /// The manager for all the HUD displays in the scene.
    /// </summary>
    public HUDMenuManager hudMenuManager;

    /// <summary>
    /// The prefab containing the food controller, for modifying and viewing information on a dish in the user's
    /// inventory.
    /// </summary>
    public GameObject FOOD_CONTROLLER_PREFAB;

    /// <summary>
    /// The current turn number.
    /// </summary>
    public int turnNumber {
        get => this.gameState.turnNumber;
        set => this.gameState.turnNumber = value;
    }

    /// <summary>
    /// Increment the current turn, and carry out all actions associated with the turn being incremented.
    /// </summary>
    public void IncrementTurn()
    {
        // Have a random number of customers buy food, between 40 and 60 (50 +- 10 customers).
        int numberOfCustomers = NumberRandomizer.GetIntBetweenExclusive(40, 60);

        // Have the generated customers buy a dish.
        this.DoBuyDishes(numberOfCustomers);

        // Initialize new values for the daily trends.
        this.hudMenuManager.trendsDisplay.InitializeDailyTrends();

        // Finally, increment the current turn.
        this.SetCurrentTurn(this.turnNumber + 1);
    }

    /// <summary>
    /// Carry out the purchase of dishes for the current day.
    /// </summary>
    /// <param name="numberOfCustomers">The number of customers buying dishes.</param>
    private void DoBuyDishes(int numberOfCustomers)
    {
        // Initialize a mapping between all the dishes and how many were bought.
        Dictionary<Dish, int> dishPurchaseCounts = this.GetAcquiredRecipes().ToDictionary(x => x, x => 0).ToSerializableDictionary();

        // Generate a randomized selector attuned to current trends in food.
        DynamicRandomSelector<Dish> dishSelector = this.GetRandomDishSelector();

        // For each customer, select a randomized dish for that customer to purchase and increment the amount of that
        // dish incremented by 1.
        for (int i = 0; i < numberOfCustomers; i++)
        {
            Dish randomDish = dishSelector.SelectRandomItem();
            dishPurchaseCounts[randomDish]++;
        }

        // After the purchase counts have been calculated, decrement the user's recipe inventory and increment the user's
        // cash on hand accordingly.
        foreach (Dish d in dishPurchaseCounts.Keys)
        {
            this.BuyDishNTimesByCustomers(dishPurchaseCounts[d], d);
        }
    }

    /// <summary>
    /// Return a random dish selector that selects a random dish based on the popularity of each dish.
    /// </summary>
    /// <returns> a random dish selector that selects a random dish based on the popularity of each dish.</returns>
    private DynamicRandomSelector<Dish> GetRandomDishSelector()
    {
        // Add each dish and its popularity to the randomized selector.
        DynamicRandomSelector<Dish> dishSelector = new DynamicRandomSelector<Dish>();
        foreach (Dish d in this.GetAcquiredRecipes())
        {
            dishSelector.Add(d, this.hudMenuManager.trendsDisplay.dishPopularityMapping[d]);
        }

        // Build and return the selector.
        dishSelector.Build();
        return dishSelector;
    }

    /// <summary>
    /// Buy the provided dish <paramref name="dish"/> <paramref name="numberOfCustomers"/> times.
    /// </summary>
    /// <param name="numberOfCustomers">The number of customers buying the dish.</param>
    /// <param name="dish">The dish being bought.</param>
    private void BuyDishNTimesByCustomers(int numberOfCustomers, Dish dish)
    {
        // Have the customers buy the given dish.
        //
        // Make sure that the number of customers that bought the dish does not exceed the amount the user has in stock.
        int numberOfCustomersToBuyDish = Math.Min(numberOfCustomers, this.gameState.menuInventory[dish]);

        // As a result of that, update the user's information on both the amount of dishes sold yesterday as well
        // as the amount of that dish sold in inventory, and have the UI update to that information accordingly.
        this.gameState.menuInventory[dish] -= numberOfCustomersToBuyDish;
        this.gameState.soldYesterdayMapping[dish] = numberOfCustomersToBuyDish;
        this.hudMenuManager.dishMenu.SetDishDetails(this.gameState);

        // Then, decrement the dish's amount by the amount that customers bought.
        this.gameState.cashOnHand += numberOfCustomersToBuyDish * dish.GetRetailPrice();
        this.cashOnHandDisplay.SetCashOnHand(this.gameState.cashOnHand);
    }

    /// <summary>
    /// Start this script.
    /// </summary>
    public void Start()
    {
        // Make sure to hide the game menu at the very start.
        if (this.menuManager.isActiveAndEnabled)
        {
            this.HideGameMenu();
        }
        
        // Hide all menus except for the dish menu.
        this.hudMenuManager.DeactivateDisplay(this.hudMenuManager.newDishesDisplay);
        this.hudMenuManager.DeactivateDisplay(this.hudMenuManager.trendsDisplay);
        this.hudMenuManager.ActivateDisplay(this.hudMenuManager.dishMenu);

        // For now, only new games are supported, so load a new game from a newly generated state.
        this.LoadNewGameFromNewGameState();

    }

    /// <summary>
    /// Create and load a brand new game state into the current scene.
    /// </summary>
    internal void LoadNewGameFromNewGameState()
    {
        // Create a new game state.
        this.gameState = GameState.CreateNew();

        // Initialize info on the current turn, the amount of cash the player has on hand, as well as the amount of 
        // food the player has in their inventory.
        this.LoadAndInitializeScene(this.gameState);
    }

    /// <summary>
    /// Load the game from the provided game state, and initialize any other variables required.
    /// </summary>
    /// <param name="_gameState">The GameState to load from.</param>
    [SuppressMessage("ReSharper", "LoopCanBePartlyConvertedToQuery")]
    internal void LoadAndInitializeScene(GameState _gameState)
    {
        // Load the UI for the menu display.
        // Clear out the menu because we're currently loading the dish menu anew.
        GameObject dishMenuMenuHolder = this.hudMenuManager.dishMenu.menuHolder;
        dishMenuMenuHolder.DestroyAllChildren();

        // Set the game state to match the given game state.
        this.gameState = _gameState;

        // Initialize the starting turn number, iff it isn't set.
        this.gameState.InitializeFieldsIfNull();

        // Set all the UI displays for all the elements in the save file.
        this.SetCurrentTurn(this.gameState.turnNumber);
        this.cashOnHandDisplay.SetCashOnHand(this.gameState.cashOnHand);

        // For each dish, instantiate a prefab to represent the dish.
        foreach (Dish dish in this.GetAcquiredRecipes())
        {
            GameObject instantiatedMenuItem = Instantiate(this.FOOD_CONTROLLER_PREFAB);
            dishMenuMenuHolder.AddChild(instantiatedMenuItem, false);

            // Then, initialize the script attached to the prefab with information on this GameSceneManager.
            FoodItemController foodItemController = instantiatedMenuItem.GetComponent<FoodItemController>();
            foodItemController.Initialize(dish, this);
        }
        this.hudMenuManager.dishMenu.SetDishDetails(this.gameState);

        // Initialize some random daily trends for determining the popularity of foods.
        this.hudMenuManager.trendsDisplay.InitializeDailyTrends();
        
        // Initialize info on the recipes not purchased.
        this.hudMenuManager.newDishesDisplay.LoadUnboughtDishes();
    }

    /// <summary>
    /// Return a list of all recipes that this user has acquired.
    /// </summary>
    /// <returns>a list of all recipes that this user has acquired.</returns>
    public HashSet<Dish> GetAcquiredRecipes()
    {
        return this.gameState.acquiredDishes;
    }

    /// <summary>
    /// Set the current turn to <paramref name="turnToSet"/>.
    /// </summary>
    /// <param name="turnToSet">The value to set the current turn to.</param>
    private void SetCurrentTurn(int turnToSet)
    {
        this.turnNumber = turnToSet;
        this.turnDisplay.SetTurnNumber(turnToSet);
    }

    /// <summary>
    /// Show the game menu.
    /// </summary>
    public void ShowGameMenu()
    {
        // Show the menu manager.
        this.menuManager.Activate();

        // Hide all sub-menus except for the options summary view.
        this.menuManager.saveGameMenu.Deactivate();
        this.menuManager.ActivateMenu(this.menuManager.optionsSummaryMenu);
    }

    /// <summary>
    /// Hide the game menu.
    /// </summary>
    public void HideGameMenu()
    {
        this.menuManager.Deactivate();
    }

    /// <summary>
    /// Return the Sprite containing the image for the dish <paramref name="dish"/>.
    /// </summary>
    /// <param name="dish">The dish whose sprite should be returned.</param>
    /// <returns> the Sprite containing the image for the dish <paramref name="dish"/>.</returns>
    public static Sprite GetDishSprite(Dish dish)
    {
        return Resources.Load<Sprite>($"Sprites/FoodSprites/{dish.ToCamelCaseString()}");
    }

}