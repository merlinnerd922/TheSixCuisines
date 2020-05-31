using System.Collections.Generic;
using Helper;

namespace TSC.Game
{

    /// <summary>
    /// A helper class of dish-related utility methods.
    /// </summary>
    public static class DishUtils
    {

        /// <summary>
        /// A mapping between dishes and their ingredient costs.
        /// </summary>
        private static Dictionary<Dish, float> INGREDIENTS_COST_MAPPING = new Dictionary<Dish, float>()
        {
            {Dish.FRENCH_FRIES, 2}, {Dish.BURGER, 1}, {Dish.ICE_CREAM, 3}, {Dish.POTATO_SOUP, 3}, 
            {Dish.FRIED_CHICKEN, 7}
        };

        /// <summary>
        /// A mapping between dishes and their retail prices.
        /// </summary>
        private static Dictionary<Dish, float> DISH_COST_MAPPING= new Dictionary<Dish, float>()
        {
            {Dish.FRENCH_FRIES, 5}, {Dish.BURGER, 2}, {Dish.ICE_CREAM, 7}, {Dish.POTATO_SOUP, 8}, {Dish.FRIED_CHICKEN, 15}
        };

        /// <summary>
        /// Return the ingredient cost of the provided dish.
        /// </summary>
        /// <returns>The ingredient cost of the provided dish.</returns>
        public static float GetIngredientCost(this Dish dish)
        {
            return INGREDIENTS_COST_MAPPING[dish];
        }
        
        /// <summary>
        /// Return the retail price of the provided dish.
        /// </summary>
        /// <param name="dish">The dish whose retail price should be returned.</param>
        /// <returns>the retail price of the provided dish.</returns>
        public static float GetRetailPrice(this Dish dish)
        {
            return DISH_COST_MAPPING[dish];
        }

        /// <summary>
        /// TODO_LATER Convert this to Dictionary for static usage.
        /// Return a camel-case String representation of this dish. 
        /// <remarks>Camel case is how we represent dishes' names within
        /// the Unity inspector, so this method exists for that purpose.</remarks>
        /// </summary>
        /// <param name="dish">The dish to represent as a camel-case String.</param>
        /// <returns>The camel case representation of the provided dish.</returns>
        public static string GetCamelCaseString(this Dish dish)
        {
            return dish.ToString().ToCamelCase();
        }

    }

}