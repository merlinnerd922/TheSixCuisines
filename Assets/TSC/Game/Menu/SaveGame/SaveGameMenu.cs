using System;
using System.Collections.Generic;
using System.IO;
using BDT;
using Extend;
using GeneralUtils;

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
            if (this.inputField.text.IsEmpty())
            {
                return;
            }

            // Otherwise, create a Path object from the provided text content and check it for validity. If the file
            // path provided is illegal, then simply return.
            // TODO_LATER Add a message about illegal file name.
            PathInst targetPath = SaveGameManager.InitializeSaveFilePathFromFileName($"{this.inputField.text}.tscgame");
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

    }

}