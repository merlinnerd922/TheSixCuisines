using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A menu button.
/// </summary>
public class MenuButton : MonoBehaviour
{

    /// <summary>
    /// Go to the setup scene.
    /// </summary>
    public void GoToSetupScene()
    {
        SceneManager.LoadScene("Scenes/SetupGame");
    }

    /// <summary>
    /// Go to the main menu scene.
    /// </summary>
    public void GoToMenuScene()
    {
        SceneManager.LoadScene("Scenes/MainHubScene");
    }

    /// <summary>
    /// Go to the Game scene to start a new game.
    /// </summary>
    public void GoToGameScene()
    {
        SceneManager.LoadScene("Scenes/GameScene");
    }

}