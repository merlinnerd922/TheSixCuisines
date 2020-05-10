﻿using System.Collections;
using System.Collections.Generic;
using Extend;
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
    /// Increment the current turn.
    /// </summary>
    public void IncrementTurn()
    {
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
        if (this.gameState.turnNumber == null)
        {
            this.gameState.turnNumber = 1;
        }

        // Initialize the player's menu inventory, iff it isn't set.
        if (this.gameState.menuInventory == null)
        {
            this.gameState.menuInventory = GameState.InitializeDishMenu();
        }

        // Initialize the player's cash on hand, iff it isn't set.
        if (this.gameState.cashOnHand == null)
        {
            this.gameState.cashOnHand = GameState.STARTING_CASH;
        }

// Set all the UI displays for all the elements in the save file.
        this.SetCurrentTurn(this.gameState.turnNumber);
        this.cashOnHandDisplay.SetCashOnHand(this.gameState.cashOnHand);
        this.hudMenuManager.dishMenu.SetDishMapping(this.gameState.menuInventory);
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