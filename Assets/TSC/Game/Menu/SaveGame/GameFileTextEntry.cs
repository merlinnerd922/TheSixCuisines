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
    public void SetLoadGameFileInputField()
    {
        LoadGameMenu loadGameMenu = this.GetNthAncestorGameObject(4).GetComponent<LoadGameMenu>();
        loadGameMenu.fileNameText.text = thisText.text;
    }

}