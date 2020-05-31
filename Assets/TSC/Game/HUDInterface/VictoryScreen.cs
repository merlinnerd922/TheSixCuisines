using Extend;
using TSC.Game.Other;
using UnityEngine;

/// <summary>
/// The victory screen for displaying the user's victory in a game.
/// </summary>
public class VictoryScreen : MonoBehaviour
{

    /// <summary>
    /// The scene manager for the game.
    /// </summary>
    public GameSceneManager gameSceneManager;

    /// <summary>
    /// Start a brand new game.
    /// </summary>
    public void StartNewGame()
    {
        // Hide this victory panel.
        this.Deactivate();

        // Initialize all the variables for a new game.
        this.gameSceneManager.LoadNewGameFromNewGameState(Level.CreateLevel1());
    }

}