using Extend;
using UnityEditor;
using UnityEngine.Tilemaps;

namespace TSC.Game
{

    /// <summary>
    /// A road tile upon which vehicles can travel.
    /// </summary>
    public class RoadTile : Tile
    {

#if UNITY_EDITOR
        [MenuItem("Assets/Create/RoadTile")]
        public static void CreateRoadTile()
        {
            LocalUnityUtils.CreateTileOfType<RoadTile>("New Tile", "Asset", "Assets", "Save Tile");
        }
#endif

    }

}