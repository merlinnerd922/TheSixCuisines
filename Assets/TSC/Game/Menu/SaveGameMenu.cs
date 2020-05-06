using System;
using System.Collections.Generic;
using System.IO;
using BDT;
using Extend;
using GeneralUtils;
using Helper.ExtendSpace;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils;
using Object = UnityEngine.Object;

namespace TSC.Game.Menu
{

    /// <summary>
    /// The in-game save game menu. 
    /// </summary>
    public class SaveGameMenu : InGameMenu
    {

        /// <summary>
        /// The field for providing the name of a file to save.
        /// </summary>
        public SaveGameInputField inputField;

        /// <summary>
        /// The manager for the current game scene.
        /// </summary>
        public GameSceneManager gameSceneManager;

        /// <summary>
        /// A flag indicating if the saved games have been loaded.
        /// </summary>
        private bool _savedGamesLoaded = false;

        /// <summary>
        /// The content holder for displaying all saved games.
        /// </summary>
        public GameObject saveGameViewContent;

        /// <summary>
        /// The prefab used to initialize a text entry in the saved games window.
        /// </summary>
        public GameObject GAME_SAVE_TEXT_ITEM_PREFAB;

        /// <summary>
        /// The scroll bar for scrolling through the different save files.
        /// </summary>
        public Scrollbar scrollBar;

        /// <summary>
        /// The manager for the game menus.
        /// </summary>
        private GameMenuManager _gameMenuManager => gameSceneManager.menuManager;

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

        /// <summary>
        /// Hide this menu, and activate the options summary menu again.
        /// </summary>
        public void HideMenu()
        {
            this._gameMenuManager.ActivateMenu(this._gameMenuManager.optionsSummaryMenu);
            this.Deactivate();
        }

        /// <summary>
        /// Show this menu and hide the active menu.
        /// </summary>
        public void ShowMenu()
        {
            this._gameMenuManager.activeMenu.Deactivate();
            this._gameMenuManager.ActivateMenu(this);

            // If the saved games haven't been loaded, then load them.
            if (!_savedGamesLoaded)
            {
                LoadSavedGames();
            }
        }

        /// <summary>
        /// Load all saved games into the save game panel.
        /// </summary>
        private void LoadSavedGames()
        {
            // Destroy all of this GameObject's children (representing the different save files) to start fresh.
            this.saveGameViewContent.DestroyAllChildren();

            // For each save file, add a node to the content viewer for that save file.
            foreach (string saveFileName in SaveGameManager.GetSaveFileNames())
            {
                saveGameViewContent.AddChild(this.CreateSavedGameTextItem(saveFileName), false);
            }

            // Scroll the list back all the way to the top.
            scrollBar.value = 1;
        }

        /// <summary>
        /// Create a saved game text item to be displayed in the list of saved games. Then, return that GameObject.
        /// </summary>
        /// <param name="saveFileName">The name of the file to save.</param>
        /// <returns>a saved game text item to be displayed in the list of saved games.</returns>
        private GameObject CreateSavedGameTextItem(string saveFileName)
        {
            GameObject saveTextNode = Instantiate(this.GAME_SAVE_TEXT_ITEM_PREFAB);
            saveTextNode.GetComponent<Text>().text = saveFileName;
            return saveTextNode;
        }

    }

}