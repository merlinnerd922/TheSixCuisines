using System;

namespace TSC.Game
{
/// <summary>
/// A save file containing a saved game.
/// </summary>
[Serializable]
    public class SaveFile
    {
/// <summary>
/// The state of the game stored in this save file.
/// </summary>
internal GameState gameState;

        /// <summary>
        /// Create and return a brand new save file from the provided GameState.
        /// </summary>
        /// <param name="gameState">The GameState to create a file from.</param>
        /// <returns>A brand new save file from the provided GameState.</returns>
        public static object CreateSaveFile(GameState gameState)
        {
            SaveFile saveFile = new SaveFile();
            saveFile.gameState = gameState;
            return saveFile;
        }

    }

}