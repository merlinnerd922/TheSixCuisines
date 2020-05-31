using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Extend;
using TSC.Game.Other;
using TSC.Game.SaveGame;
using UnityEngine.Serialization;

/// <summary>
/// The state of a game.
/// </summary>
[Serializable]
public class GameState
{

    /// <summary>
    /// The current level being played.
    /// </summary>
    public Level levelBeingPlayed;

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
    public HashSet<Dish> acquiredDishes = new HashSet<Dish>() {Dish.FRENCH_FRIES};

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
        gameState.decorAds = new List<DecorAds>();
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

        // Initialize the set of decor and ads the user has purchased, if null.
        if (this.decorAds == null)
        {
            this.decorAds = new List<DecorAds>();
        }
    }

    /// <summary>
    /// Return the amount of the dish <paramref name="dishManaged"/> sold yesterday.
    /// </summary>
    /// <param name="dishManaged">The dish whose sales from yesterday we're trying to retrieve.</param>
    /// <returns>the amount of the dish <paramref name="dishManaged"/> sold yesterday.</returns>
    public int GetSoldYesterday(Dish dishManaged)
    {
        // If there is no information on this dish being sold yesterday, then obviously the number that was sold is 0.
        if (!this.soldYesterdayMapping.ContainsKey(dishManaged))
        {
            this.soldYesterdayMapping[dishManaged] = 0;
        }

        // Return the corresponding entry.
        return soldYesterdayMapping[dishManaged];
    }

    /// <summary>
    /// Return the amount of the dish <paramref name="dish"/> that the user has in inventory.
    /// </summary>
    /// <param name="dish">The dish whose count in inventory should be returned.</param>
    /// <returns>the amount of the dish <paramref name="dish"/> that the user has in inventory.</returns>
    public int GetAmountInInventory(Dish dish)
    {
        // If there is no record of the dish being in the inventory, then obviously there is 0 of it.
        if (!menuInventory.ContainsKey(dish))
        {
            this.menuInventory[dish] = 0;
        }

        // Return the corresponding entry.
        return menuInventory[dish];
    }

    /// <summary>
    /// Return an enumeration of all the dishes that this player has NOT bought.
    /// </summary>
    /// <returns>an enumeration of all the dishes that this player has NOT bought.</returns>
    public IEnumerable<Dish> GetUnboughtDishes()
    {
        return LocalGeneralUtils.GetEnumList<Dish>().Where(x => !this.acquiredDishes.Contains(x));
    }

    /// <summary>
    /// A list of decor that the player has purchased.
    /// </summary>
    public List<DecorAds> decorAds = new List<DecorAds>();

    /// <summary>
    /// Return a list of the decorations and advertisements that this player has NOT bought.
    /// </summary>
    /// <returns>a list of the decorations and advertisements that this player has NOT bought.</returns>
    public List<DecorAds> GetUnboughtDecorations()
    {
        return LocalGeneralUtils.GetEnumList<DecorAds>().Where(decorAd => !decorAds.Contains(decorAd)).ToList();
    }

}