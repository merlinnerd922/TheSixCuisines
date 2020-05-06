using System;
using System.IO;
using BDT;
using Extend;
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
        /// TODO
        /// </summary>
        public GameObject saveGameViewContent;

        /// <summary>
        /// TODO
        /// </summary>
        public GameObject GAME_SAVE_TEXT_ITEM_PREFAB;

        /// <summary>
        /// TODO
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
            PathInst targetPath = SaveGameManager.InitializeSaveFilePathFromFileName($"{this.inputField.text}.tscgame");
            SaveGameManager.SaveGame(SaveFile.CreateSaveFile(this.gameSceneManager.gameState), targetPath);
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
            foreach (string saveFileName in SaveGameManager.GetSaveFileNames())
            {
                saveGameViewContent.AddChild(GetSavedGameTextItem(saveFileName), false);
            }
            
            // TODO
            scrollBar.value = 1;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="saveFileName"></param>
        /// <returns></returns>
        private GameObject GetSavedGameTextItem(string saveFileName)
        {
            GameObject saveTextNode = Instantiate(GAME_SAVE_TEXT_ITEM_PREFAB) as GameObject;
            saveTextNode.GetComponent<Text>().text = Path.GetFileName(saveFileName);
            return saveTextNode;
        }

    }

}