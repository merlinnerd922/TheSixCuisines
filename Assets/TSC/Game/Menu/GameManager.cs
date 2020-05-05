using UnityEngine;
using UnityEngine.SceneManagement;

namespace TSC.Game.Menu
{
/// <summary>
/// TODO
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


    }

}