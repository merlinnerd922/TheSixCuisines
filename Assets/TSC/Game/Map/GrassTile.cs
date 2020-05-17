using Extend;
using UnityEditor;
using UnityEngine.Tilemaps;

namespace TSC.Game
{

    /// <summary>
    /// A grass tile. You can place advertisements on here.
    /// </summary>
    public class GrassTile : Tile
    {
#if UNITY_EDITOR
        /// <summary>
        /// Create a brand new grass tile by opening the file dialog and saving a brand new location for the tile.
        /// </summary>
        [MenuItem("Assets/Create/GrassTile")]
        public static void CreateGrassTile()
        {
            LocalUnityUtils.CreateTileOfType<GrassTile>("New Tile", "Asset", "Assets", "Save Tile");
        }
#endif
    }

}