
using System;
using System.Collections.Generic;

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
    public float startingCash = 5000f;
    
    /// <summary>
    /// The inventory of dishes the player has.
    /// </summary>
    public SerializableDictionary<FoodItem, int> menuInventory = new SerializableDictionary<FoodItem, int>();

    /// <summary>
    /// Initialize and return a brand new GameState.
    /// </summary>
    /// <returns>The new game state to return.</returns>
    public static GameState CreateNew()
    {
        GameState gameState = new GameState();
        gameState.turnNumber = 1;
        gameState.startingCash = 5000f;
        return gameState;
    }

}