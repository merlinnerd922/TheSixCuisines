using System.Collections.Generic;

namespace CollectionUtils
{

    /// <summary>
    /// A collection of list-related helper methods.
    /// </summary>
    public static class LocalListUtils
    {
/// <summary>
/// Construct a brand new list from the provided array, and return it.
/// </summary>
/// <param name="array">The array to construct a list from.</param>
/// <returns>A brand new list from the provided array</returns>
        public static IEnumerable<T> of<T>(params T[] array)
        {
            return new List<T>(array);
        }

    }

}