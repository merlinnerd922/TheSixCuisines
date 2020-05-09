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
        private static Dictionary<Dish, float> DISH_COST_MAPPING = new Dictionary<Dish, float>()
        {
            {Dish.FRENCH_FRIES, 25}
        };

        /// <summary>
        /// Return the ingredient cost of the provided dish.
        /// </summary>
        /// <returns>The ingredient cost of the provided dish.</returns>
        public static float GetIngredientCost(this Dish dish)
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