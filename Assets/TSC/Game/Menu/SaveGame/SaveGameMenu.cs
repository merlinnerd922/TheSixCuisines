using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BDT;
using Extend;
using GeneralUtils;
using Helper;

namespace TSC.Game.Menu
{

    /// <summary>
    /// The in-game save game menu. 
    /// </summary>
    public class SaveGameMenu : GameFileMenu
    {

        /// <summary>
        /// The field for providing the name of a file to save.
        /// </summary>
        public SaveGameInputField inputField;

        /// <summary>
        /// Save the current game with the provided name.
        /// </summary>
        public void SaveGame()
        {
            // Do nothing if the input field is empty.
            string baseFileName = this.inputField.text.RemoveEnd(".tscgame");
            if (IsInvalidBaseFileName(baseFileName))
            {
                return;
            }

            // Otherwise, create a Path object from the provided text content and check it for validity. If the file
            // path provided is illegal, then simply return.
            // TODO_LATER Add a message about illegal file name.
            PathInst targetPath = SaveGameManager.InitializeSaveFilePathFromFileName($"{baseFileName}.tscgame");
            string path = targetPath.ToString();
            if (!LocalFileUtils.ValidateDllPath(ref path))
            {
                return;
            }

            // Save the game at the desired path.
            SaveGameManager.SaveGame(SaveFile.CreateSaveFile(this.gameSceneManager.gameState), targetPath);

            // Clear out the text input, and reload the saved games.
            this.inputField.text = "";
            this.LoadSavedGames();
        }

        /// <summary>
        /// Return true iff the provided BASE file name is invalid. (I.e. a filename that would be valid if
        /// the suffix ".tscgame" was added at the front.
        /// </summary>
        /// <param name="fileName" >The name of the file to validate.</param>
        /// <returns>true iff the provided BASE file name is invalid. (I.e. a filename that would be valid if
        /// the suffix ".tscgame" was added at the front.</returns>
        private static bool IsInvalidBaseFileName(string fileName)
        {
            return fileName.IsNullOrEmpty() || fileName.Count(x => x == '.') >= 1;
        }

    }

}