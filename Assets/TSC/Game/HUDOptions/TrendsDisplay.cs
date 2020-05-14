using System.Collections.Generic;
using System.Linq;
using Helper;
using UnityEngine.UI;

namespace TSC.Game.HUDOptions
{

    /// <summary>
    /// A display for the current news and trends in cuisine.
    /// </summary>
    public class TrendsDisplay : HUDDisplay
    {

        /// <summary>
        /// The text displaying the currently trending cuisine.
        /// </summary>
        public Text trendingFoodText;

        /// <summary>
        /// A mapping tracking the popularity of each dish.
        /// </summary>
        internal Dictionary<Dish, float> dishPopularityMapping;

        /// <summary>
        /// The scene manager for the game scene.
        /// </summary>
        public GameSceneManager gameSceneManager;

        /// <summary>
        /// Initialize information on the daily trends for food.
        /// </summary>
        public void InitializeDailyTrends()
        {
            // Initialize the string that the trend string will be set to.
            string trendString = "";

            // Get a list of all different types of dishes.
            List<Dish> dishList = this.gameSceneManager.GetAcquiredRecipes();
            
            // Initialize likelihoods adding up to 1 indicating how popular the dish will be.
            List<float> foodPopularities = NumberRandomizer.GenerateNFloatsWhoSumToM(dishList.Count, 1f).ToList();

            // Append each dish and its popularity onto the trend string.
            dishPopularityMapping = new Dictionary<Dish, float>();
            for (int index = 0; index < dishList.Count; index++)
            {
                Dish d = dishList[index];
                float dishPopularity = foodPopularities[index];
                trendString += $"{d.ToTitleCaseSpacedString()}: {dishPopularity * 100:F2}%\n";

                // Be sure to keep track of how popular each dish is in a Dictionary.
                dishPopularityMapping[d] = dishPopularity;
            }

            // Finally, set the trend string.
            this.trendingFoodText.text = trendString;
        }

    }

}