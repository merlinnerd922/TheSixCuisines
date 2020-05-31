using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace TSC.Game.Other
{

    /// <summary>
    /// An object containing information on a level that can be played within the game.
    /// </summary>
    [Serializable]
    public class Level
    {

        /// <summary>
        /// Construct a brand new level.
        /// </summary>
        private Level()
        {
        }

        /// <summary>
        /// The target amount of money that the player must reach to win the level.
        /// </summary>
        public float moneyGoal;

        /// <summary>
        /// The index of this level, indexed from 1.
        /// </summary>
        public int level;

        /// <summary>
        /// The list of dishes that the user can purchase for the current level.
        /// </summary>
        [FormerlySerializedAs("purchasableDishes")]
        public List<Dish> dishDomain;

        /// <summary>
        /// The set of dishes the user starts the level with.
        /// </summary>
        public List<Dish> startingDishes;

        /// <summary>
        /// The amount of cash that the player starts with.
        /// </summary>
        public int startingCash;

        /// <summary>
        /// Extract and return level information stored in a saved JSON file.
        /// </summary>
        /// <returns>An object containing level information, as stored in a saved JSON file.</returns>
        public static Level CreateLevel1()
        {
            TextAsset textAsset = Resources.Load<TextAsset>("Data/Levels/Level1");
            string textFile = textAsset.text;
            return JsonUtility.FromJson<Level>(textFile);
        }

    }

}