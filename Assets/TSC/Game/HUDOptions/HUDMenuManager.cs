using System.Collections;
using System.Collections.Generic;
using Extend;
using TSC.Game.HUDOptions;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// The manager for all of the HUD menus.
/// </summary>
public class HUDMenuManager : MonoBehaviour
{

    /// <summary>
    /// The food menu (as opposed to the gameplay menu) for displaying dishes served by the restaurant.
    /// </summary>
    [FormerlySerializedAs("foodMenu")] [FormerlySerializedAs("foodDisplay")] public DishMenu dishMenu;

    /// <summary>
    /// A display for current trends in cuisine.
    /// </summary>
    public TrendsDisplay trendsDisplay;

    /// <summary>
    /// The currently active display.
    /// </summary>
    [FormerlySerializedAs("activeMenu")] public HUDDisplay activeDisplay;

    /// <summary>
    /// A display for viewing new dishes that can be purchased.
    /// </summary>
    public NewDishesDisplay newDishesDisplay;

    /// <summary>
    /// Toggle the food menu.
    /// </summary>
    public void ToggleFoodMenu()
    {
        this.ToggleMenu(this.dishMenu);
    }

    /// <summary>
    /// Toggle the trends display.
    /// </summary>
    public void ToggleTrendsDisplay()
    {
        this.ToggleMenu(this.trendsDisplay);
    }

    /// <summary>
    /// Toggle the shop for new dishes.
    /// </summary>
    public void ToggleNewDishesDisplay()
    {
        this.ToggleMenu(this.newDishesDisplay);
    }

    /// <summary>
    /// Toggle the provided display.
    /// </summary>
    /// <param name="display">The display to toggle.</param>
    private void ToggleMenu(HUDDisplay display)
    {
        // If this display is currently active, then deactivate it.
        if (Equals(this.activeDisplay, display))
        {
            this.DeactivateDisplay(display);
            return;
        }

        // Otherwise, deactivate the currently active menu and activate the provided one.
        if (this.activeDisplay != null)
        {
            this.DeactivateDisplay(this.activeDisplay);
        }

        this.ActivateMenu(display);
    }

    /// <summary>
    /// Deactivate the provided display.
    /// </summary>
    /// <param name="hudDisplay">The display to deactivate.</param>
    private void DeactivateDisplay(HUDDisplay hudDisplay)
    {
        hudDisplay.Deactivate();
        this.activeDisplay = null;
    }

    /// <summary>
    /// Activate the provided display.
    /// </summary>
    /// <param name="hudDisplay">The display to activate.</param>
    private void ActivateMenu(HUDDisplay hudDisplay)
    {
        hudDisplay.Activate();
        this.activeDisplay = hudDisplay;
    }

}