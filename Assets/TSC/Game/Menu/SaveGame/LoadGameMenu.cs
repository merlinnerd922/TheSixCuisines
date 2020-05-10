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
        /// The text displaying the game to be loaded.
        /// </summary>
        [FormerlySerializedAs("fileNameTextInput")] [FormerlySerializedAs("loadedGameTextInput")]
        public Text fileNameText;

        /// <summary>
        /// The manager for the in-game non-HUD menus.
        /// </summary>
        public GameMenuManager gameMenuManager;

        /// <summary>
        /// Populate this load game menu's text field with the contents of the provided text field <paramref name="textField"/>.
        /// </summary>
        /// <param name="textField">The text field whose text should be used to populate this load game menu's input.</param>
        public void PopulateTextField(Text textField)
        {
            this.fileNameText.text = textField.text;
        }

        /// <summary>
        /// Load the game file from the provided text name in the text input.
        /// </summary>
        public void LoadGame()
        {
            // Extract the save file from the provided text file and load it.
            SaveFile saveFile = SaveGameManager.LoadGame(this.fileNameText.text) as SaveFile;
            this.gameSceneManager.LoadGameFromGameState(saveFile.gameState);

            // Deactivate the game menu manager and show the game window itself.
            gameMenuManager.Deactivate();
        }

    }

}