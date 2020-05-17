using Extend;
using UnityEditor;
using UnityEngine.Tilemaps;

namespace TSC.Game
{
/// <summary>
/// A condo tile that will provide a source of customers!
/// </summary>
    public class CondoTile : Tile
    {

        
        /// <summary>
        /// Create a brand new condo tile by opening the file dialog and saving a brand new location for the tile.
        ///
        /// Add this as a menu item so we can create one in Unity.
        /// </summary>
        [MenuItem("Assets/Create/CondoTile")]
        public static void CreateCondoTile()
        {
            LocalUnityUtils.CreateTileOfType<RestaurantTile>("New " + nameof(RestaurantTile), "Asset", 
                "Assets", "Save " + nameof(RestaurantTile));
        }
    }

}