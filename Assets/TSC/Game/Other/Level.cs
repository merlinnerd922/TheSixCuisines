using System;
using System.Collections.Generic;
using Helper.ExtendSpace;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
        /// The type of victory condition that must be fulfilled to beat the level.
        /// </summary>
        public VictoryCondition victoryCondition;

        /// <summary>
        /// The number of customers in a level.
        /// </summary>
        public int customerCount;

        /// <summary>
        /// The amount of cash that the player starts with.
        /// </summary>
        public int startingCash;

        /// <summary>
        /// Extract and return level information stored in a saved JSON file.
        /// </summary>
        /// <param name="levelToCreate">The level to load.</param>
        /// <returns>An object containing level information, as stored in a saved JSON file.</returns>
        public static Level LoadLevel(int levelToCreate)
        {
            TextAsset textAsset = Resources.Load<TextAsset>($"Data/Levels/Level{levelToCreate}");
            string textFile = textAsset.text;
            Level returnedLevel = JsonConvert.DeserializeObject<Level>(textFile);
            ValidateLevel(returnedLevel);
            return returnedLevel;
        }

        /// <summary>
        /// Validate the provided level object. Throw an Exception if one of the validations isn't met.
        /// </summary>
        /// <param name="returnedLevel"></param>
        [UsedImplicitly]
        private static void ValidateLevel(Level returnedLevel)
        {
            if (returnedLevel.victoryCondition == null)
            {
                throw new RuntimeException("The victory condition must be supplied!");
            }
        }

    }

}