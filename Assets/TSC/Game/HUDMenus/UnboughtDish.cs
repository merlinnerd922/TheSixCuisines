using System.Collections;
using System.Collections.Generic;
using Helper;
using TSC.Game;
using TSC.Game.HUDOptions;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils;

/// <summary>
/// A dish as displayed in the unbought dishes menu.
/// </summary>
public class UnboughtDish : MonoBehaviour
{
/// <summary>
/// An image of this dish.
/// </summary>
    public Image dishImage;
    
    /// <summary>
    /// The text displaying which dish this is.
    /// </summary>
    public Text dishText;

    /// <summary>
    /// The dish being stored in this script.
    /// </summary>
    private Dish thisDish;


    /// <summary>
    /// Initialize this dish by setting it to the dish <param name="dish"></param>.
    /// </summary>
    /// <param name="dish">The dish to set this object to.</param>
    public void Initialize(Dish dish)
    {
        dishImage.sprite = DishUtils.GetDishSprite(dish);
        dishText.text = dish.ToTitleCaseSpacedString();
        thisDish = dish;
    }

    /// <summary>
    /// Buy this dish from the menu.
    /// </summary>
    public void BuyThisDish()
    {
        // Get a reference to the new dishes display.
        NewDishesDisplay newDishesDisplay = this.gameObject.GetGrandparent().GetComponent<NewDishesDisplay>();
        
        // Do nothing if the player doesn't have enough money to buy this dish.
        GameSceneManager gameSceneManager = newDishesDisplay.gameSceneManager;
        if (gameSceneManager.gameState.cashOnHand < 50f)
        {
            return;
        }
        
        // Otherwise, buy this dish and add it to the player's inventory...
        gameSceneManager.gameState.cashOnHand -= 50f;
        gameSceneManager.gameState.acquiredDishes.Add(this.thisDish);
        
        // ...and reload the game state.
        gameSceneManager.LoadAndInitializeScene(gameSceneManager.gameState);
    }

}
