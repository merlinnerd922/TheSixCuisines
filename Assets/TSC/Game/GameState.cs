
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Extend;
using UnityEngine.Serialization;

/// <summary>
/// The state of a game.
/// </summary>
[Serializable]
public class GameState
{
    /// <summary>
    /// The current turn number of the game state.
    /// </summary>
    public int turnNumber = 1;
    
    /// <summary>
    /// The amount of cash that this player currently has on hand.
    /// </summary>
    [FormerlySerializedAs("startingCash")] 
    public float cashOnHand = 5000f;

    /// <summary>
    /// The inventory of dishes the player has.
    /// </summary>
    public SerializableDictionary<Dish, int> menuInventory = InitializeDishMenu();

    /// <summary>
    /// TODO
    /// </summary>
    /// <returns></returns>
    public static SerializableDictionary<Dish, int> InitializeDishMenu()
    {
        return LocalGeneralUtils.GetEnumList<Dish>().ToDictionary(x => x, x => 0).ToSerializableDictionary();
    }


    /// <summary>
    /// Initialize and return a brand new GameState.
    /// </summary>
    /// <returns>The new game state to return.</returns>
    public static GameState CreateNew()
    {
        GameState gameState = new GameState();
        gameState.turnNumber = STARTING_TURN_NUMBER;
        gameState.cashOnHand = STARTING_CASH;
        gameState.menuInventory = InitializeDishMenu();
        return gameState;
    }
/// <summary>
/// The starting turn number, which is always set to 1 (as opposed to 0).
/// </summary>
    public const int STARTING_TURN_NUMBER = 1;

    /// <summary>
    /// TODO
    /// </summary>
    public const int STARTING_CASH = 5000;

}