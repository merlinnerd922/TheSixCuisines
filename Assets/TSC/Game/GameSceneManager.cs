using System;
using System.Collections;
using System.Collections.Generic;
using Extend;
using Helper;
using TSC.Game;
using UnityEngine;

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
        
        // Have the customers buy a dish. (Right now, the only dish that is available is French Fries).
        //
        // Make sure that the number of customers that bought the dish does not exceed the amount the user has in stock.
        int numberOfCustomersToBuyDish = Math.Min(numberOfCustomers, this.gameState.menuInventory[Dish.FRENCH_FRIES]);
        
        // As a result of that, update the user's information on both the amount of dishes sold yesterday as well
        // as the amount of that dish sold in inventory, and have the UI update to that information accordingly.
        this.gameState.menuInventory[Dish.FRENCH_FRIES] -= numberOfCustomersToBuyDish;
        this.gameState.soldYesterdayMapping[Dish.FRENCH_FRIES] = numberOfCustomersToBuyDish;
        this.hudMenuManager.dishMenu.SetDishDetails(this.gameState);
        
        // Then, decrement the dish's amount by the amount that customers bought.
        this.gameState.cashOnHand += numberOfCustomersToBuyDish * Dish.FRENCH_FRIES.GetRetailPrice();
        this.cashOnHandDisplay.SetCashOnHand(this.gameState.cashOnHand);
        
        // Finally, increment the current turn.
        this.SetCurrentTurn(this.turnNumber + 1);
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

        // For now, only new games are supported, so load a new game from a newly generated state.
        this.LoadNewGameFromNewGameState();
    }

    /// <summary>
    /// Create and load a brand new game state into the current scene.
    /// </summary>
    private void LoadNewGameFromNewGameState()
    {
        // Create a new game state.
        this.gameState = GameState.CreateNew();

        // Initialize info on the current turn, the amount of cash the player has on hand, as well as the amount of 
        // food the player has in their inventory.
        this.LoadGameFromGameState(this.gameState);
    }

    /// <summary>
    /// Load the provided game state into the current scene.
    /// </summary>
    /// <param name="_gameState">The state of the game to load.</param>
    public void LoadGameFromGameState(GameState _gameState)
    {
        // Set the game state to match the given game state.
        this.gameState = _gameState;

        // Initialize the starting turn number, iff it isn't set.
        this.gameState.InitializeFieldsIfNull();

        // Set all the UI displays for all the elements in the save file.
        this.SetCurrentTurn(this.gameState.turnNumber);
        this.cashOnHandDisplay.SetCashOnHand(this.gameState.cashOnHand);
        this.hudMenuManager.dishMenu.SetDishDetails(this.gameState);
    }

    /// <summary>
    /// Set the current turn to <paramref name="turnToSet"/>.
    /// </summary>
    /// <param name="turnToSet">The value to set the current turn to.</param>
    private void SetCurrentTurn(int turnToSet)
    {
        turnNumber = turnToSet;
        turnDisplay.SetTurnNumber(turnToSet);
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

}