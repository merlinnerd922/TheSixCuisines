using System.Collections;
using System.Collections.Generic;
using Helper;
using TSC.Game.HUDMenus.Display;
using TSC.Game.Other;
using TSC.Game.SaveGame;
using UnityEngine;
using UnityUtils;

/// <summary>
/// The display for purchasing new decor.
/// </summary>
public class DecorDisplay : HUDDisplay
{
/// <summary>
/// The prefab for an unbought decor UI item.
/// </summary>
    public GameObject unboughtDecorPrefab;
/// <summary>
/// The holder for all the decor UI items.
/// </summary>
public GameObject decorHolder;


/// <summary>
/// Initialize this unbought decor display.
/// </summary>
/// <param name="gameSceneManager">The GameSceneManager that this display is initialized from.</param>
    public void Initialize(GameSceneManager gameSceneManager)
    {
        // For each unbought ad, generate an entry in the display.
        foreach (DecorAds decorAd in gameSceneManager.gameState.GetUnboughtDecorations())
        {
            GameObject instantiatedObj = Instantiate(this.unboughtDecorPrefab);
            decorHolder.AddChild(instantiatedObj, false);
            
            // Initialize this decor component.
            UnboughtDecor decor = instantiatedObj.GetComponent<UnboughtDecor>();
            decor.SetImage(decorAd.GetSprite());
            decor.SetText(decorAd.ToTitleCaseSpacedString());
        }
    }

}