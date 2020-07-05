using UnityEngine;
using UnityEngine.SceneManagement;

namespace TSC.Game.Menu
{
/// <summary>
/// The manager for the entire game, and contains methods pertinent to all scenes.
/// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// Go to the main menu scene.
        /// </summary>
        public void GoToMenuScene()
        {
            SceneManager.LoadScene("Scenes/MainHubScene");
        }

        /// <summary>
        /// Go to the setup scene.
        /// </summary>
        public void GoToSetupScene()
        {
            SceneManager.LoadScene("Scenes/SetupGame");
        }

    }

}