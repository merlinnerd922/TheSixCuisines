using Extend;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The display for managing the amount of cash the player has on hand.
/// </summary>
public class CashOnHand : MonoBehaviour
{

    /// <summary>
    /// The amount of cash the player has on hand, as displayed in the UI.
    /// </summary>
    public float cashOnHand;

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
    }

    /// <summary>
    /// Decrement the amount of cash the player has on hand by <paramref name="amountToDecrement"/>.
    /// </summary>
    /// <param name="amountToDecrement">The amount to decrement the player's cash on hand by.</param>
    public void DecrementCashOnHand(float amountToDecrement)
    {
        SetCashOnHand(cashOnHand - amountToDecrement);
    }

}