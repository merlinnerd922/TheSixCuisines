﻿using BDT;
using Extend;
using UnityEngine;

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
        /// The manager for the game menus.
        /// </summary>
        private GameMenuManager _gameMenuManager => gameSceneManager.menuManager;

        /// <summary>
        /// Save the current game with the provided name.
        /// </summary>
        public void SaveGame()
        {
            PathInst targetPath = SaveGameManager.InitializeSaveFilePathFromFileName($"{this.inputField.text}.tscgame");
            SaveGameManager.SaveGame(SaveFile.CreateSaveFile(this.gameSceneManager.gameState),
                targetPath);
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
        }

    }

}