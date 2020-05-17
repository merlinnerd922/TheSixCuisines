using Extend;
using UnityEditor;
using UnityEngine.Tilemaps;

namespace TSC.Game
{

    /// <summary>
    /// A tile containing a restaurant.
    /// </summary>
    public class RestaurantTile : Tile
    {

        /// <summary>
        /// Create a brand new restaurant tile by opening the file dialog and saving a brand new location for the tile.
        /// </summary>
        [MenuItem("Assets/Create/RestaurantTile")]
        public static void CreateRestaurantTile()
        {
            LocalUnityUtils.CreateTileOfType<RestaurantTile>("New " + nameof(RestaurantTile), "Asset", 
                "Assets", "Save " + nameof(RestaurantTile));
        }
        

    }

}