using System.Collections;
using System.Collections.Generic;
using Helper;
using TSC.Game;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// The controller for various traits of a food item within the food item manager (e.g. count, price, etc.)
/// </summary>
public class FoodItemController : MonoBehaviour
{

    /// <summary>
    /// The food item managed by this controller.
    /// </summary>
    [FormerlySerializedAs("foodItemManaged")]
    public Dish dishManaged;

    /// <summary>
    /// The amount of the FoodItem to buy.
    /// </summary>
    public int amountToBuy;


    /// <summary>
    /// The text displaying the retail price of a dish.
    /// </summary>
    public Text retailPriceText;
    
    /// <summary>
    /// The text displaying the ingredient costs for a dish.
    /// </summary>
    public Text ingredientCostText;
    
    /// <summary>
    /// The text displaying the amount of a dish the user has in stock.
    /// </summary>
    public Text inStockText;

    /// <summary>
    /// The game scene manager managing this scene.
    /// </summary>
    public GameSceneManager gameSceneManager;

    /// <summary>
    /// The text displaying the amount of food of this type to buy.
    /// </summary>
    [FormerlySerializedAs("foodAmountText")]
    public Text foodAmountToBuyText;

    /// <summary>
    /// The text displaying the amount of this ingredient the player bought yesterday.
    /// </summary>
    public Text boughtYesterdayText;

    /// <summary>
    /// An image of this food item.
    /// </summary>
    public Image image;

    /// <summary>
/// The menu of dishes this controller is being managed by.
/// </summary>
    private DishMenu _dishMenu=> this.gameSceneManager.hudMenuManager.dishMenu;


    /// <summary>
    /// Increment the count for this food item.
    /// </summary>
    public void Increment()
    {
        // Do nothing if the player doesn't have enough money.
        float price = this.dishManaged.GetIngredientCost();
        if (this.gameSceneManager.cashOnHandDisplay.cashOnHand < price)
        {
            return;
        }

        // Otherwise, increment the amount to buy by the provided amount.
        this.gameSceneManager.cashOnHandDisplay.DecrementCashOnHand(price);
        this._dishMenu.SetFoodAmountToPurchase(this.dishManaged,
            this._dishMenu.GetFoodAmountToPurchase(this.dishManaged) + 1);
    }
    
    /// <summary>
    /// Decrement the count for this food item.
    /// </summary>
    public void Decrement()
    {
        // Do nothing if the player doesn't have anything to decrement.
        if (this._dishMenu.GetFoodAmountToPurchase(this.dishManaged) == 0)
        {
            return;
        }

        // Otherwise, decrement the amount to buy by the provided amount, and refund the cost.
        float ingredientCost = this.dishManaged.GetIngredientCost();
        this.gameSceneManager.cashOnHandDisplay.IncrementCashOnHand(ingredientCost);
        this._dishMenu.SetFoodAmountToPurchase(this.dishManaged,
            this._dishMenu.GetFoodAmountToPurchase(this.dishManaged) - 1);
    }


    /// <summary>
    /// Start the coroutine to hold down the increment button to quickly increment the amount of the recipe stored
    /// in this incrementer.
    /// </summary>
    public void HoldDownIncrement()
    {
        IEnumerator holdDownCoroutine = this.GetHoldMouseDownIncrementCoroutine(true);
        StartCoroutine(holdDownCoroutine);
    }

    
    /// <summary>
    /// The amount of time to wait when holding down a button before allowing rapid incrementation/decrementation.
    /// </summary>
    private const float HOLD_DELAY = 0.5f;
    
    /// <summary>
    /// Return a coroutine that does the following: after a small delay, begin rapidly incrementing/decrementing the
    /// recipe stored in this controller.
    /// </summary>
    /// <param name="shouldIncrement">A flag that denotes whether the recipe amount should be incremented or
    /// decremented.</param>
    /// <returns>A coroutine that does the following: after a small delay, begin rapidly incrementing/decrementing
    /// the recipe stored in this controller.</returns>
    private IEnumerator GetHoldMouseDownIncrementCoroutine(bool shouldIncrement)
    {
        // Initialize a timer to keep track of how long we've held down the button.
        //
        // After a small delay, start incrementing or decrementing the recipe specified by this incrementer/decrementer.
        float elapsedHoldDelay = 0;
        bool holdDelayMet = false;

        // Run this coroutine as long as the left mouse button's being held down.
        while (Input.GetMouseButton(0))
        {
            // If the hold delay duration hasn't been met, then kept incrementing the hold delay timer.
            if (!holdDelayMet)
            {
                elapsedHoldDelay += Time.deltaTime;

                // After the button is held down for enough time, mark that this incrementer can begin rapidly incrementing
                // or decrementing.
                if (elapsedHoldDelay >= HOLD_DELAY)
                {
                    holdDelayMet = true;
                }
            }

            // Increment the amount of this ingredient.
            else if (shouldIncrement)
            {
                this.Increment();
            }

            // Decrement the amount of this ingredient. (TODO_LATER)
            else
            {
                this.Decrement();
            }

            // Break from the current frame and come back in next frame to continue the incrementation.
            yield return null;
        }
    }

    /// <summary>
    /// Start the coroutine to hold down the decrement button to quickly decrement the amount of the recipe stored
    /// in this incrementer.
    /// </summary>
    public void HoldDownDecrement()
    {
        IEnumerator holdDownCoroutine = this.GetHoldMouseDownIncrementCoroutine(false);
        StartCoroutine(holdDownCoroutine);
    }


    /// <summary>
    /// The amount of cash the player has on hand.
    /// </summary>
    public float playerCashOnHand => gameSceneManager.gameState.cashOnHand;

    /// <summary>
    /// Set the amount of food of this type to buy to <paramref name="getFoodAmount"/>.
    /// </summary>
    /// <param name="getFoodAmount">The amount to set the dish buying amount to.</param>
    public void SetAmountOfFoodToBuy(int getFoodAmount)
    {
        this.foodAmountToBuyText.text = getFoodAmount.ToString();
        this.amountToBuy = getFoodAmount;
    }

    /// <summary>
    /// Return the currently displayed number of dishes to buy for the dish this controller is managing. <remarks>(By "buying a 
    /// dish"
    /// we mean buying all the ingredients required to make one instance of that dish. We make all of our stuff fresh!)</remarks>
    /// </summary>
    /// <returns>Return the currently displayed number of dishes to buy for the dish this controller is managing.</returns>
    public int GetAmountOfFood()
    {
        return this.amountToBuy;
    }

    /// <summary>
    /// Set the amount of dishes that the user has in stock of the stored dish to <paramref name="amountOfDishToSet"/>. 
    /// </summary>
    /// <param name="amountOfDishToSet">The value to set the amount of dishes the user has of this particular dish to.</param>
    public void SetAmountOfDishesInStockInUI(int amountOfDishToSet)
    {
        inStockText.text = amountOfDishToSet.ToString();
    }

    /// <summary>
    /// Initialize this FoodItemController by populating it with information from the provided parameters.
    /// </summary>
    /// <param name="dish">The dish that this controller is managing.</param>
    /// <param name="_gameSceneManager">The GameSceneManager that made a call to this controller.</param>
    public void Initialize(Dish dish, GameSceneManager _gameSceneManager)
    {
        // Store references to the GameSceneManager that initialized this FoodItemController, as well as the dish 
        // being managed by this controller.
        this.gameSceneManager = _gameSceneManager;
        this.name = dish.ToCamelCaseString();
        this.dishManaged = dish;
        
        // Set this controller's dish's image.
        this.image.sprite = DishUtils.GetDishSprite(dish);
    }

}