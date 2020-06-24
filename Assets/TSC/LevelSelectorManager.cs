using System;
using System.Collections;
using System.Collections.Generic;
using TSC;
using TSC.Game.Other;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityUtils;

/// <summary>
/// The main menu setup scene for selecting a level to play.
/// </summary>
public class LevelSelectorManager : MonoBehaviour
{

    /// <summary>
    /// An object containing information on the level that is about to be loaded and played.
    /// </summary>
    [FormerlySerializedAs("levelInfoObject")]
    public LevelInfoObject levelInfoScript;

    /// <summary>
    /// The currently selected level.
    /// </summary>
    private int _currentSelectedLevel = 1;

    /// <summary>
    /// Start this script.
    /// </summary>
    public void Start()
    {
        SetCurrentLevel(1);
    }

    /// <summary>
    /// Set the current level being displayed to <paramref name="levelNumber"/>.
    /// </summary>
    /// <param name="levelNumber">The level number to set the selector's level to.</param>
    private void SetCurrentLevel(int levelNumber)
    {
        this.levelDisplay.text = levelNumber.ToString();
        this._currentSelectedLevel = levelNumber;
    }

    /// <summary>
    /// Load the level stored in the only existing JSON level file, and then load the game scene to play that level.
    /// </summary>
    public void LoadLevelGameScene()
    {
        // Store information on the level that's about to be played in a script within the level info GameObject.
        this.levelInfoScript.level = Level.LoadLevel(this._currentSelectedLevel);
        
        // Do nothing if the level doesn't exist.
        if (this.levelInfoScript.level == null)
        {
            Debug.Log("Level does not exist.");
            return;
        }

        // Convert the level info object to a root-level object so that it can be persisted across scenes.
        GameObject _levelInfoObject = this.levelInfoScript.gameObject;
        _levelInfoObject.SetParent(null);
        DontDestroyOnLoad(_levelInfoObject);

        // Finally, load the game scene, with info stored on this level info.
        SceneManager.LoadScene("Scenes/GameScene");
    }

    /// <summary>
    /// The maximum available level to the player.
    /// </summary>
    private int MAX_LEVEL = 100;

    /// <summary>
    /// Decrement the currently selected level.
    /// </summary>
    public void DecrementLevel()
    {
        // Prevent the level from decrementing to below 1.
        if (this._currentSelectedLevel == 1)
        {
            return;
        }

        // Otherwise, decrement the currently selected level.
        SetCurrentLevel(this._currentSelectedLevel - 1);
    }

    /// <summary>
    /// Increment the currently displayed level by 1.
    /// </summary>
    public void IncrementLevel()
    {
        // Prevent the level from incrementing above the maximum. 
        if (this._currentSelectedLevel == this.MAX_LEVEL)
        {
            return;
        }
        
        // Otherwise, increment the currently selected level.
        SetCurrentLevel(this._currentSelectedLevel + 1);
    }

    /// <summary>
    /// The display for the current level.
    /// </summary>
    public Text levelDisplay;

}