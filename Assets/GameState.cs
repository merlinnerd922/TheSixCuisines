
/// <summary>
/// The state of a game.
/// </summary>
public class GameState
{
    /// <summary>
    /// The current turn number of the game state.
    /// </summary>
    public int turnNumber;

    /// <summary>
    /// Initialize and return a brand new GameState.
    /// </summary>
    /// <returns>The new game state to return.</returns>
    public static GameState CreateNew()
    {
        GameState gameState = new GameState();
        gameState.turnNumber = 1;
        return gameState;
    }

}