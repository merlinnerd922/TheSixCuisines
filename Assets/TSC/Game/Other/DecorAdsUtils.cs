using Helper;
using TSC.Game.SaveGame;
using UnityEngine;

namespace TSC.Game.Other
{

    /// <summary>
    /// A class containing helper methods for items of decor and advertisements.
    /// </summary>
    public static class DecorAdsUtils
    {

        /// <summary>
        /// Load and return the sprite representing the provided ad <paramref name="decorAd"/>. 
        /// </summary>
        /// <param name="decorAd">The ad whose sprite to return.</param>
        /// <returns>the sprite representing the provided ad <paramref name="decorAd"/>.</returns>
        public static Sprite GetSprite(this DecorAds decorAd)
        {
            string decorAdAsString = decorAd.ToCamelCaseString();
            return Resources.Load<Sprite>("Sprites/DecorSprites/" + decorAdAsString);
        }

    }

}