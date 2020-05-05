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
    /// TODO
    /// </summary>
    public SaveGameMenu saveGameMenu;

    /// <summary>
    /// TODO
    /// </summary>
    public OptionsSummaryMenu optionsSummaryMenu;

    /// <summary>
    /// TODO
    /// </summary>
    public void SaveGameOption()
    {
        this.activeMenu.Deactivate();
        ActivateMenu(this.saveGameMenu);
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="menuToActivate"></param>
    public void ActivateMenu(InGameMenu menuToActivate)
    {
        menuToActivate.Activate();
        this.activeMenu = menuToActivate;
    }

}