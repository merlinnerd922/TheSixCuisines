using BDT;
using Extend;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace TSC.Game.Menu
{

    /// <summary>
    /// A menu for loading games.
    /// </summary>
    public class LoadGameMenu : GameFileMenu
    {

    

        /// <summary>
        /// The manager for the in-game non-HUD menus.
        /// </summary>
        public GameMenuManager gameMenuManager;

        /// <summary>
        /// 
        /// </summary>
        public GenericNoticeDisplay genericNoticeDisplay;

        /// <summary>
        /// Load the game file from the provided text name in the text input.
        /// </summary>
        public void LoadGame()
        {
            // Extract the save file from the provided text file and load it.
            PathInst gamePath = SaveGameManager.InitializeSaveFilePathFromFileName(this.fileNameText.text);

            if (gamePath.Exists())
            {
                SaveFile saveFile = SaveGameManager._bf.Deserialize(gamePath.OpenExistingFileStream()) as SaveFile;
                this.gameSceneManager.LoadAndInitializeScene(saveFile.gameState);

                // Deactivate the game menu manager and show the game window itself.
                gameMenuManager.Deactivate();
            }
            else
            {
                genericNoticeDisplay.Activate();
            }
        }

    }

}