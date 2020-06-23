using Extend;
using TSC.Game.Other;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The display for managing the amount of cash the player has on hand.
/// </summary>
public class CashOnHand : MonoBehaviour
{
    /// <summary>
    /// The progress bar for keeping track of how close the user is to their target cash goal.
    /// </summary>
    public ProgressBar progressBar;

    /// <summary>
    /// The amount of cash the player has on hand, as displayed in the UI.
    /// </summary>
    public float cashOnHand;

    /// <summary>
    /// The game scene manager managing this cash on hand object.
    /// </summary>
    public GameSceneManager gameSceneManager;
    

    /// <summary>
    /// The text displaying the player's current cash on hand.
    /// </summary>
    public Text cashOnHandText;

    /// <summary>
    /// Set the value of the player's current cash on hand to <paramref name="cashOnHandToSet"/>.
    /// </summary>
    /// <param name="cashOnHandToSet">The value to set the player's current cash on hand to.</param>
    public void SetCashOnHand(float cashOnHandToSet)
    {
        this.cashOnHand = cashOnHandToSet;
        this.cashOnHandText.text = $"${cashOnHandToSet}";
        
        // Set the progress of the user in reaching their target cash goal, if the victory condition is a cash goal.
        if (this._victoryCondition == VictoryCondition.CASH)
        {
            this.progressBar.SetCashOnHandProgress(cashOnHandToSet, this.progressBar.targetCashOnHand);
        }
    }

    /// <summary>
    /// The victory condition type that must be fulfilled to win a level.
    /// </summary>
    private VictoryCondition _victoryCondition => this.gameSceneManager.gameState.levelBeingPlayed.victoryCondition;

    /// <summary>
    /// Decrement the amount of cash the player has on hand by <paramref name="amountToDecrement"/>.
    /// </summary>
    /// <param name="amountToDecrement">The amount to decrement the player's cash on hand by.</param>
    public void DecrementCashOnHand(float amountToDecrement)
    {
        SetCashOnHand(cashOnHand - amountToDecrement);
    }

    /// <summary>
    /// Increment the amount of cash the player has on hand by <paramref name="amountToIncrement"/>.
    /// </summary>
    /// <param name="amountToIncrement">The amount to increment the player's cash by.</param>
    public void IncrementCashOnHand(float amountToIncrement)
    {
        SetCashOnHand(this.cashOnHand + amountToIncrement);
    }

}