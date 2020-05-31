using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructures.RandomSelector;
using Extend;
using Helper;
using Helper.ExtendSpace;
using TSC;
using TSC.Game;
using TSC.Game.Other;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using UnityUtils;

/// <summary>
/// A manager for the game scene.
/// </summary>
public class GameSceneManager : MonoBehaviour
{

    /// <summary>
    /// The tilemap representing the current world.
    /// </summary>
    public Tilemap tilemap;

    /// <summary>
    /// The progress bar in the scene, indicating how much money the user has accumulated to get to their goal.
    /// </summary>
    public ProgressBar progressBar;

    /// <summary>
    /// The manager for the in-game menu.
    /// </summary>
    [FormerlySerializedAs("menuManager")] public GameMenuManager gameMenuManager;

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
    /// The manager for the camera.
    /// </summary>
    public CameraManager cameraManager;

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
        // Have a random number of customers buy food, within +-10 customers of the estimated number of customers
        // estimated to arrive.
        int estimatedCustomers = this.hudMenuManager.trendsDisplay.estimatedNumberOfCustomers;
        int numberOfCustomers =
            NumberRandomizer.GetIntBetweenExclusive(estimatedCustomers - 10, estimatedCustomers + 10);

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
        Dictionary<Dish, int> dishPurchaseCounts =
            this.GetAcquiredRecipes().ToDictionary(x => x, x => 0).ToSerializableDictionary();

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
        if (this.gameMenuManager.isActiveAndEnabled)
        {
            this.HideGameMenu();
        }

        // Hide all menus except for the dish menu.
        this.hudMenuManager.DeactivateDisplay(this.hudMenuManager.newDishesDisplay);
        this.hudMenuManager.DeactivateDisplay(this.hudMenuManager.trendsDisplay);
        this.hudMenuManager.DeactivateDisplay(this.hudMenuManager.dishMenu);

        // For now, only new games are supported, so load a new game from a newly generated state.
        Level level = GetLevelInfo();
        this.LoadNewGameFromNewGameState(level);

        // Start the camera zoom position at a reasonable zoom.
        this.cameraManager.targetZoom = this.cameraManager.managedCamera.orthographicSize;
    }

    private static Level GetLevelInfo()
    {
// Load all information on the current level being played.
        // Extract information on this current level from the LevelInfo object.
        GameObject levelInfo = GameObject.Find("LevelInfo");

        // Set all relevant level information.
        Level level;
        if (levelInfo != null)
        {
            level = levelInfo.GetComponent<LevelInfoObject>().level;
        }

        // If no level info is available, set all level variables to certain default values.
        else
        {
            level = Level.CreateLevel1();
        }

        return level;
    }

    /// <summary>
    /// Set information on the level being played by the player to <paramref name="level"/>.
    /// </summary>
    /// <param name="level">The level being played by the player.</param>
    private void SetLevelInfo(Level level)
    {
        this.gameState.levelBeingPlayed = level;
    }

    /// <summary>
    /// Create and load a brand new game state into the current scene.
    /// </summary>
    /// <param name="level">TODO</param>
    internal void LoadNewGameFromNewGameState(Level level)
    {
        // Create a new game state.
        this.gameState = GameState.CreateNew();
        this.gameState.levelBeingPlayed = level;
        this.gameState.acquiredDishes = level.startingDishes.ToHashSet();
        this.gameState.cashOnHand = level.startingCash;

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
        // Set the game state to match the given game state.
        this.gameState = _gameState;
        
        // Initialize the starting turn number, iff it isn't set.
        this.gameState.InitializeFieldsIfNull();

        // Set all the UI displays for all the elements in the save file.
        this.SetCurrentTurn(this.gameState.turnNumber);
        
        // Set how much cash the player has on hand and their target cash goal.
        this.cashOnHandDisplay.SetCashOnHand(this.gameState.cashOnHand);
        this.SetCashGoal(this.gameState.levelBeingPlayed.moneyGoal);

        // Load the UI for the menu display.
        // Clear out the menu because we're currently loading the dish menu anew.
        this.hudMenuManager.dishMenu.InitializeDishMenu(this);

        // Initialize some random daily trends for determining the popularity of foods.
        this.hudMenuManager.trendsDisplay.InitializeDailyTrends();

        // Initialize information on the projected number of customers.
        this.CalculateCustomerAmount();

        // Initialize info on the recipes not purchased.
        this.hudMenuManager.newDishesDisplay.LoadUnboughtDishes(this.gameState.levelBeingPlayed.dishDomain);

        // Initialize information on the decor the user HASN'T bought.
        this.hudMenuManager.decorDisplay.Initialize(this.gameState);
    }

    /// <summary>
    /// Set the cash goal in the current scene to <paramref name="moneyGoal"/>.
    /// </summary>
    /// <param name="moneyGoal">The goal to set the cash goal in the current scene to.</param>
    private void SetCashGoal(float moneyGoal)
    {
        this.progressBar.SetCashOnHandProgress(this.progressBar.cashOnHand, moneyGoal);
    }

    /// <summary>
    /// Calculate and set the number of customers that are projected to come to the restaurant.
    /// </summary>
    private void CalculateCustomerAmount()
    {
        int numberOfCustomers = 0;

        // Retrieve all tiles within the tilemap.
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        // For each tile in the tilemap, if the tile is a condo tile, then increment the number of customers by 3.
        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    if (tile.GetType() == typeof(CondoTile))
                    {
                        numberOfCustomers += 3;
                    }
                }
            }
        }

        // Finally, when we're done calculating the number of customers, set that number accordingly.
        this.hudMenuManager.trendsDisplay.SetNumberOfEstimatedCustomers(numberOfCustomers);
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
        this.gameMenuManager.Activate();

        // Hide all sub-menus except for the options summary view.
        this.gameMenuManager.saveGameMenu.Deactivate();
        this.gameMenuManager.ActivateMenu(this.gameMenuManager.optionsSummaryMenu);
    }

    /// <summary>
    /// Hide the game menu.
    /// </summary>
    public void HideGameMenu()
    {
        this.gameMenuManager.Deactivate();
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

    /// <summary>
    /// Update the current frame.
    /// </summary>
    public void Update()
    {
        // If none of the overlay menus are active, do any needed updates on the camera.
        if (!this.hudMenuManager.isActiveAndEnabled && !this.gameMenuManager.isActiveAndEnabled)
        {
            // Update the zoom in/out for the mouse.
            cameraManager.UpdateCameraZoom();

            // Update the camera panning.
            this.cameraManager.UpdateCameraPan();
        }
    }


    /// <summary>
    /// Set the amount of cash the user has on hand as well as in the GameState to <paramref name="newValue"/>.
    /// </summary>
    /// <param name="newValue">The value to set the player's cash on hand to.</param>
    public void SetCashOnHand(float newValue)
    {
        gameState.cashOnHand = newValue;
        cashOnHandDisplay.SetCashOnHand(newValue);
    }

}