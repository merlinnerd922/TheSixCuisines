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
        public Text loadedGameTextInput;

        /// <summary>
        /// Populate this load game menu's text field with the contents of the provided text field <paramref name="textField"/>.
        /// </summary>
        /// <param name="textField">The text field whose text should be used to populate this load game menu's input.</param>
        public void PopulateTextField(Text textField)
        {
            this.loadedGameTextInput.text = textField.text;
        }

    }

}