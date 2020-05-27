using System.Collections.Generic;
using System.Linq;
using Extend;
using Helper;
using Helper.ExtendSpace;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils;

namespace TSC.Game.HUDOptions
{

    /// <summary>
    /// A display for the current news and trends in cuisine.
    /// </summary>
    public class TrendsDisplay : HUDDisplay
    {
/// <summary>
/// TODO`
/// </summary>
        public GameObject trendingDishesHolder;
        
/// <summary>
/// TODO
/// </summary>
public GameObject trendingDishPrefab;

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
        /// The text displaying the estimated number of customers that the player will receive at the restaurant today.
        /// </summary>
        public Text estimatedCustomersText;

        /// <summary>
        /// The estimated number of customers that the player will have this turn.
        /// </summary>
        internal int estimatedNumberOfCustomers;

        /// <summary>
        /// Initialize information on the daily trends for food.
        /// </summary>
        public void InitializeDailyTrends()
        {
            // Initialize the string that the trend string will be set to.
            string trendString = "";

            // Get a list of all different types of dishes.
            List<Dish> dishList = this.gameSceneManager.GetAcquiredRecipes().ToList();
            
            // Initialize likelihoods adding up to 1 indicating how popular the dish will be.
            List<float> foodPopularities = NumberRandomizer.GenerateNFloatsWhoSumToM(dishList.Count, 1f).ToList();

            // TODO
            trendingDishesHolder.DestroyAllChildren();
            
            // Append each dish and its popularity onto the trend string.
            dishPopularityMapping = new Dictionary<Dish, float>();
            for (int index = 0; index < dishList.Count; index++)
            {
                Dish dish = dishList[index];
                float dishPopularity = foodPopularities[index];
                
                // Be sure to keep track of how popular each dish is in a Dictionary.
                dishPopularityMapping[dish] = dishPopularity;

                // TODO
                GameObject trendingDish = Instantiate(this.trendingDishPrefab) as GameObject;
                this.trendingDishesHolder.AddChild(trendingDish, false);
                Image image = trendingDish.GetComponent<Image>();
                image.sprite = DishUtils.GetDishSprite(dish);
                int imageSize = (int) (225 * dishPopularity);
                image.SetImageSize(imageSize, imageSize) ;
            }

            // Finally, set the trend string.
            this.trendingFoodText.text = trendString;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="trendString"></param>
        /// <param name="d"></param>
        /// <param name="dishPopularity"></param>
        /// <param name="index"></param>
        /// <param name="dishList"></param>
        /// <returns></returns>
        private static string AppendPopularityString(string trendString, Dish d, float dishPopularity, int index, List<Dish> dishList)
        {
            trendString += $"{d.ToTitleCaseSpacedString()}: {dishPopularity * 100:F2}%";

            // Append a newline to the end of the string if we still have more recipe popularities to render.
            if (index < dishList.Count - 1)
            {
                trendString += "\n";
            }

            return trendString;
        }

        /// <summary>
        /// Set the estimated number of customers the player will have to <paramref name="numCustomers"/>.
        /// </summary>
        /// <param name="numCustomers">The value to set the estimated number of customers the player will have.</param>
        public void SetNumberOfEstimatedCustomers(int numCustomers)
        {
            estimatedNumberOfCustomers = numCustomers;
            this.estimatedCustomersText.text = $"Projected customers: {numCustomers}";
        }

    }

}