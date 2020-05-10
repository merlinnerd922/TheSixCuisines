using System.Collections;
using System.Collections.Generic;
using Extend;
using TSC.Game.Menu;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils;

/// <summary>
/// An entry within the load game menu. 
/// </summary>
public class GameFileTextEntry : MonoBehaviour
{

    /// <summary>
    /// This button's Text component.
    /// </summary>
    public Text thisText;

    /// <summary>
    /// Set the load game menu's file input field to the text stored in this text entry. 
    /// </summary>
    public void SetGameFileMenuInputField()
    {
        GameFileMenu gameFileMenu = this.GetNthAncestorGameObject(4).GetComponent<GameFileMenu>();
        
        // If the game file menu is a load game menu, then fill the text field of the load game menu with the contents
        // of the text object just clicked on.
        if (gameFileMenu.GetType() == typeof(LoadGameMenu))
        {        
            gameFileMenu.fileNameText.text = thisText.text;
        }
        
        // Otherwise, if the game file menu is a save game menu, then fill the input field instead with the contents
        // of the text object just clicked on.
        else if (gameFileMenu.GetType() == typeof(SaveGameMenu))
        {
            ((SaveGameMenu) (gameFileMenu)).inputField.text = this.thisText.text;
        }

    }

}