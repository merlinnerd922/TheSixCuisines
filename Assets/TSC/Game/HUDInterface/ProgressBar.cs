using System.Collections;
using System.Collections.Generic;
using Extend;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// A bar tracking the current progress of the user's goal.
/// </summary>
public class ProgressBar : MonoBehaviour
{

    /// <summary>
    /// The foreground image of the progress bar.
    /// </summary>
    public Image foreground;

    /// <summary>
    /// The text displaying the user's progress in reaching their goal.
    /// </summary>
    public Text progressText;

    /// <summary>
    /// The RectTransform of the foreground of the progress bar.
    /// </summary>
    public RectTransform foregroundRectTransform;

    /// <summary>
    /// The target amount of cash the player has to reach.
    /// </summary>
    [FormerlySerializedAs("TARGET_CASH_ON_HAND")]
    public float targetCashOnHand = 10000;

    /// <summary>
    /// The victory screen on this
    /// </summary>
    public VictoryScreen victoryScreen;

    public float cashOnHand;

    /// <summary>
    /// Set the cash on hand that the player has to <paramref name="cashOnHandToSet"/>, and update the player's progress accordingly.
    /// </summary>
    /// <param name="cashOnHandToSet">The value to set the amount of cash the player has on hand.</param>
    /// <param name="_targetCashOnHand">
    /// The target amount of cash that the player should get to win the level/</param>
    public void SetCashOnHandProgress(float cashOnHandToSet, float _targetCashOnHand)
    {
        this.cashOnHand = cashOnHandToSet;
        this.targetCashOnHand = _targetCashOnHand;

        // Set the text display of the user's progress.
        this.progressText.text = $"Progress : ${cashOnHandToSet}/${_targetCashOnHand}";

        // Cap the maximum amount of the fill of the progress bar by 100% from above.
        float progressPercentage = Mathf.Min(cashOnHandToSet / _targetCashOnHand, 1f);

        // Finally, set the width of the RectTransform of the progress bar foreground accordingly.
        this.foregroundRectTransform.SetAnchorMaxX(progressPercentage);

        // Trigger the victory screen if the maximum target has been reached.
        if (progressPercentage >= 1)
        {
            this.victoryScreen.Activate();
        }
    }

}