using System.Collections;
using System.Collections.Generic;
using TSC.Game.SaveGame;
using UnityEngine;

/// <summary>
/// The display for purchasing new decor.
/// </summary>
public class DecorDisplay : HUDDisplay
{
    
    
/// <summary>
/// TODO
/// </summary>
/// <param name="gameSceneManager"></param>
    public void Initialize(GameSceneManager gameSceneManager)
    {
        foreach (DecorAds decorAd in gameSceneManager.gameState.GetUnboughtDecorations())
        {
            
        }

    }

}
