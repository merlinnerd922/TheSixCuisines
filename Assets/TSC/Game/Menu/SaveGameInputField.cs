using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TSC.Game.Menu
{

    /// <summary>
    /// An input field for saving a game.
    /// </summary>
    public class SaveGameInputField : InputField
    {

        /// <summary>
        /// The save game menu managing this input field.
        /// </summary>
        public SaveGameMenu saveGameMenu;

        /// <summary>
        /// TODO
        /// </summary>
        protected override void Start()
        {
            base.Start();
            this.saveGameMenu = this.GetComponentInParent<SaveGameMenu>();
        }


        
        /// <summary>
        /// TODO
        /// </summary>
        bool allowEnter = false;
        
        /// <summary>
        /// TODO
        /// </summary>
        void Update () {
             
            if (allowEnter && (text.Length > 0) && (Input.GetKey (KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))) {
                saveGameMenu.SaveGame();
                allowEnter = false;
            } else
                allowEnter = isFocused;
        }

    }

}