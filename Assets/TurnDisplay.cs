using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The display for the current turn.
/// </summary>
public class TurnDisplay : MonoBehaviour
{

    /// <summary>
    /// The current turn.
    /// </summary>
    private int turnNumber => gameSceneManager.turnNumber;

    /// <summary>
    /// The text component displaying the current turn.
    /// </summary>
    public Text textComponent;

    /// <summary>
    /// The GameSceneManager managing this turn display.
    /// </summary>
    public GameSceneManager gameSceneManager;



    /// <summary>
    /// Set the current turn number to <paramref name="_turnNumber"/>.
    /// </summary>
    /// <param name="_turnNumber">The value to set the current turn to.</param>
    internal void SetTurnNumber(int _turnNumber)
    {
        this.textComponent.text = string.Format("Turn: {0}", _turnNumber);
    }


}