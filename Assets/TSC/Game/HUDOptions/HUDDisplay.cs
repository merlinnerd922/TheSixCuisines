using Extend;
using UnityEngine;

/// <summary>
/// A HUD display for displaying relevant statistics for the player.
/// </summary>
public class HUDDisplay : MonoBehaviour
{
    /// <summary>
    /// Show this menu.
    /// </summary>
    public void ToggleMenu()
    {
        if (!isActiveAndEnabled)
        {
            this.Activate();
        }
        else
        {
            this.Deactivate();
        }

    }

}