using BDT;
using Extend;
using Helper.ExtendSpace;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityUtils;

namespace TSC.Game.Menu
{
/// <summary>
/// A script using for saving game files.
/// </summary>
    public class GameFileMenu : InGameMenu
    {
        /// <summary>
        /// The text displaying the game to be loaded.
        /// </summary>
        [FormerlySerializedAs("fileNameTextInput")] [FormerlySerializedAs("loadedGameTextInput")]
        public Text fileNameText;
        
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
        private GameMenuManager _gameMenuManager => this.gameSceneManager.menuManager;

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
            if (!this._savedGamesLoaded)
            {
                this.LoadSavedGames();
            }
        }

        /// <summary>
        /// Create a saved game text item to be displayed in the list of saved games. Then, return that GameObject.
        /// </summary>
        /// <param name="saveFileName">The name of the file to save.</param>
        /// <returns>a saved game text item to be displayed in the list of saved games.</returns>
        protected GameObject CreateSavedGameTextItem(string saveFileName)
        {
            GameObject saveTextNode = Instantiate( this.GAME_SAVE_TEXT_ITEM_PREFAB);
            saveTextNode.GetComponent<Text>().text = saveFileName;
            return saveTextNode;
        }
        
        /// <summary>
        /// Load all saved games into the save game panel.
        /// </summary>
        protected void LoadSavedGames()
        {
            // Destroy all of this GameObject's children (representing the different save files) to start fresh.
            this.saveGameViewContent.DestroyAllChildren();

            // For each save file, add a node to the content viewer for that save file.
            foreach (string saveFileName in SaveGameManager.GetSaveFileNames())
            {
                this.saveGameViewContent.AddChild(this.CreateSavedGameTextItem(saveFileName), false);
            }

            // Scroll the list back all the way to the top.
            this.scrollBar.value = 1;
        }

    }

}