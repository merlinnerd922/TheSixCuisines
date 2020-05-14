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
    [FormerlySerializedAs("startingCash")] public float cashOnHand = 5000f;

    /// <summary>
    /// The list of dishes that the user has purchased.
    /// </summary>
    public List<Dish> acquiredDishes = new List<Dish>() {Dish.FRENCH_FRIES};

    /// <summary>
    /// The inventory of dishes the player has.
    /// </summary>
    public SerializableDictionary<Dish, int> menuInventory = InitializeZeroMapping();

    /// <summary>
    /// The mapping for the amount of each dish sold the previous day.
    /// </summary>
    internal SerializableDictionary<Dish, int> soldYesterdayMapping = InitializeZeroMapping();

    /// <summary>
    /// Initialize and return a brand new Dictionary mapping between each Dish type and the number 0.
    /// </summary>
    /// <returns>A brand new Dictionary mapping between each Dish type and the number 0.</returns>
    public static SerializableDictionary<Dish, int> InitializeZeroMapping()
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
        gameState.menuInventory = InitializeZeroMapping();
        gameState.soldYesterdayMapping = InitializeZeroMapping();
        return gameState;
    }

    /// <summary>
    /// The starting turn number, which is always set to 1 (as opposed to 0).
    /// </summary>
    public const int STARTING_TURN_NUMBER = 1;

    /// <summary>
    /// The amount of cash that a player always starts with.
    /// </summary>
    public const int STARTING_CASH = 5000;

    /// <summary>
    /// Initialize each of this game state's fields, if they have null values.
    /// </summary>
    public void InitializeFieldsIfNull()
    {
        // Initialize the turn number to 1, if it's not set.
        if (this.turnNumber == null)
        {
            this.turnNumber = STARTING_TURN_NUMBER;
        }

        // Initialize the player's menu inventory, iff it isn't set.
        if (this.menuInventory == null)
        {
            this.menuInventory = InitializeZeroMapping();
        }

        // Initialize the player's cash on hand, iff it isn't set.
        if (this.cashOnHand == null)
        {
            this.cashOnHand = STARTING_CASH;
        }

        // Initialize the mapping of the amount of ingredients that were sold yesterday, if it isn't set.
        if (this.soldYesterdayMapping == null)
        {
            this.soldYesterdayMapping = InitializeZeroMapping();
        }
    }

    /// <summary>
    /// Return the amount of the dish <paramref name="dishManaged"/> sold yesterday.
    /// </summary>
    /// <param name="dishManaged">The dish whose sales from yesterday we're trying to retrieve.</param>
    /// <returns>the amount of the dish <paramref name="dishManaged"/> sold yesterday.</returns>
    public int getSoldYesterday(Dish dishManaged)
    {
        // If there is no information on this dish being sold yesterday, then obviously the number that was sold is 0.
        if (!this.soldYesterdayMapping.ContainsKey(dishManaged))
        {
            return 0;
        }

        // Otherwise, return the corresponding entry.
        return soldYesterdayMapping[dishManaged];
    }

}