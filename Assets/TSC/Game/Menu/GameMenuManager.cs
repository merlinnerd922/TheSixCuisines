using Extend;
using TSC.Game.Menu;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// The manager for the game menu.
/// </summary>
public class GameMenuManager : MonoBehaviour
{
    /// <summary>
    /// The currently active in-game menu.
    /// </summary>
    [FormerlySerializedAs("inGameMenu")]
    public InGameMenu activeMenu;

    /// <summary>
    /// The game saving menu.
    /// </summary>
    public SaveGameMenu saveGameMenu;

    /// <summary>
    /// The menu displaying all of the main options on the page.
    /// </summary>
    public OptionsSummaryMenu optionsSummaryMenu;


    /// <summary>
    /// Activate the specified menu.
    /// </summary>
    /// <param name="menuToActivate">The menu to activate.</param>
    public void ActivateMenu(InGameMenu menuToActivate)
    {
        menuToActivate.Activate();
        this.activeMenu = menuToActivate;
    }

}