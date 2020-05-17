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

#if UNITY_EDITOR
        /// <summary>
        /// Create a brand new condo tile by opening the file dialog and saving a brand new location for the tile.
        ///
        /// Add this as a menu item so we can create one in Unity.
        /// </summary>
        [MenuItem("Assets/Create/CondoTile")]
        public static void CreateCondoTile()
        {
            LocalUnityUtils.CreateTileOfType<CondoTile>("New " + nameof(CondoTile), "Asset", 
                "Assets", "Save " + nameof(CondoTile));
        }
#endif
    }
}