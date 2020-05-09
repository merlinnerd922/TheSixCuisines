using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
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
        [FormerlySerializedAs("gameFileMenu")] public SaveGameMenu saveGameMenu;

        /// <summary>
        /// Initialize this script.
        /// </summary>
        protected override void Start()
        {
            base.Start();
            this.saveGameMenu = this.GetComponentInParent<SaveGameMenu>();
        }


        
        /// <summary>
        /// A flag indicating if this input field was active in the previous frame. By default, this is equal
        /// to false.
        /// </summary>
        private bool wasPreviouslyInFocus = false;
        
        /// <summary>
        /// Update this MonoBehaviour.
        /// </summary>
        private void Update () {

            // If there is text being submitted, then save the current game.
            if (this.wasPreviouslyInFocus && (text.Length > 0) && (Input.GetKey (KeyCode.Return) 
                                                                   || Input.GetKey(KeyCode.KeypadEnter))) {
                this.saveGameMenu.SaveGame();
                this.wasPreviouslyInFocus = false;
            } 
            
            // Otherwise, keep track of whether this element is in focus.
            else
                this.wasPreviouslyInFocus = isFocused;
        }

    }

}