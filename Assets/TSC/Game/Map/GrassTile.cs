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

        [MenuItem("Assets/Create/GrassTile")]
        public static void CreateGrassTile()
        {
            LocalUnityUtils.CreateTileOfType<GrassTile>("New Tile", "Asset", "Assets", "Save Tile");
        }

    }

}