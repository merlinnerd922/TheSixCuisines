using System;
using System.Collections;
using System.Collections.Generic;
using TSC;
using TSC.Game.Other;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
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
    /// Load the level stored in the only existing JSON level file, and then load the game scene to play that level.
    /// </summary>
    public void LoadLevelGameScene()
    {
        // Store information on the level that's about to be played in a script within the level info GameObject.
        this.levelInfoScript.level = Level.CreateLevel1();

        // Convert the level info object to a root-level object so that it can be persisted across scenes.
        GameObject _levelInfoObject = this.levelInfoScript.gameObject;
        _levelInfoObject.SetParent(null);
        DontDestroyOnLoad(_levelInfoObject);
        
        // Finally, load the game scene, with info stored on this level info.
        SceneManager.LoadScene("Scenes/GameScene");
    }

}