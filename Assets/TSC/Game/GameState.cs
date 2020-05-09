
using System;
using System.Collections.Generic;
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
    public SerializableDictionary<Dish, int> menuInventory = new SerializableDictionary<Dish, int>();

    /// <summary>
    /// Initialize and return a brand new GameState.
    /// </summary>
    /// <returns>The new game state to return.</returns>
    public static GameState CreateNew()
    {
        GameState gameState = new GameState();
        gameState.turnNumber = 1;
        gameState.cashOnHand = 5000f;
        
        // For each existing dish, set that player's inventory of that dish to 0.
        foreach (Dish dish in LocalGeneralUtils.GetEnumList<Dish>())
        {
            gameState.menuInventory[dish] = 0;
        }

        return gameState;
    }

}