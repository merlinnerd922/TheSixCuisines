using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A manager for the game scene.
/// </summary>
public class GameSceneManager : MonoBehaviour
{
    /// <summary>
    /// The current state of the game.
    /// </summary>
    public GameState gameState;

    /// <summary>
    /// The display for the current turn.
    /// </summary>
    public TurnDisplay turnDisplay;

    /// <summary>
    /// The current turn number.
    /// </summary>
    public int turnNumber {
        get => this.gameState.turnNumber;
        set => this.gameState.turnNumber = value;
    }

    /// <summary>
    /// Increment the current turn.
    /// </summary>
    public void IncrementTurn()
    {
        this.SetCurrentTurn(this.turnNumber + 1);
    }
    /// <summary>
    /// Start this script.
    /// </summary>
    public void Start()
    {

        // Create a new game state, and set the current turn to 1.
        gameState = GameState.CreateNew();

        // Initialize the current turn to 1.
        SetCurrentTurn(1);
    }

    /// <summary>
    /// Set the current turn to <paramref name="turnToSet"/>.
    /// </summary>
    /// <param name="turnToSet">The value to set the current turn to.</param>
    private void SetCurrentTurn(int turnToSet)
    {
        turnNumber = turnToSet;
        turnDisplay.SetTurnNumber(turnToSet);
    }

}