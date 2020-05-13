using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CollectionUtils;
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
    [FormerlySerializedAs("gameFileMenu")] public SaveGameMenu saveGameMenu;

    /// <summary>
    /// The menu displaying all of the main options on the page.
    /// </summary>
    public OptionsSummaryMenu optionsSummaryMenu;

    /// <summary>
    /// The game loading menu.
    /// </summary>
    public LoadGameMenu loadGameMenu;


    /// <summary>
    /// Activate the specified menu.
    /// </summary>
    /// <param name="menuToActivate">The menu to activate.</param>
    [SuppressMessage("ReSharper", "LoopCanBePartlyConvertedToQuery")]
    public void ActivateMenu(InGameMenu menuToActivate)
    {
        // (Deactivate all other menus first in the event they are activated). 
        IEnumerable<InGameMenu> inGameMenus = LocalListUtils.of<InGameMenu>(this.saveGameMenu, this.loadGameMenu, this
            .optionsSummaryMenu);
        foreach (InGameMenu inGameMenu in inGameMenus) {
            if (!menuToActivate.Equals(inGameMenu))
            {
                inGameMenu.Deactivate();
            }
        }

        // Then, activate the menu we're targeting.
        menuToActivate.Activate();
        this.activeMenu = menuToActivate;
        
    }

}