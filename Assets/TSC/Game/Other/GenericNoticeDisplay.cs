using Extend;
using UnityEngine;

namespace TSC.Game.Menu
{
/// <summary>
/// A display for various notices to the player, e.g. missing save game files.
/// </summary>
    public class GenericNoticeDisplay : MonoBehaviour
    {
        
        /// <summary>
        /// Deactivate this display.
        /// </summary>
        public void DeactivateThis()
        {
            MonoBehaviour monoBehaviour = this;
            monoBehaviour.Deactivate();
        }

    }

}