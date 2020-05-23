using System.Collections;
using System.Collections.Generic;
using Helper;
using Helper.ExtendSpace;
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
/// The GameSceneManager that contains this display.
/// </summary>
public GameSceneManager gameSceneManager;


/// <summary>
/// Initialize this unbought decor display.
/// </summary>
/// <param name="gameState"></param>
public void Initialize(GameState gameState)
    {
        // Clear out this decor holder's children. 
        this.decorHolder.DestroyAllChildren();
        
        // For each unbought ad, generate an entry in the display.
        foreach (DecorAds decorAd in gameState.GetUnboughtDecorations())
        {
            GameObject instantiatedObj = Instantiate(this.unboughtDecorPrefab);
            this.decorHolder.AddChild(instantiatedObj, false);
            
            // Initialize this decor component.
            UnboughtDecor decor = instantiatedObj.GetComponent<UnboughtDecor>();
            decor.Initialize(decorAd);
        }
    }

}