using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Helper;
using TSC.Game;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// The in-game map.
/// </summary>
[SuppressMessage("ReSharper", "Unity.RedundantEventFunction")]
public class Map : MonoBehaviour
{
    /// <summary>
    /// The image texture for a speech bubble.
    /// </summary>
    private Texture _speechBubbleTexture;

    /// <summary>
    /// The tilemap for storing all building tiles.
    /// </summary>
    public Tilemap buildingTilemap;

    public Tilemap terrainTilemap;

    /// <summary>
    /// Process a mouse click downwards on this map.
    /// </summary>
    public void OnMouseDown()
    {
    }

    /// <summary>
    /// Perform any GUI-related actions for the current frame.
    /// </summary>
    public void OnGUI()
    {
    }

    /// <summary>
    /// Start this map.
    /// </summary>
    public void Start()
    {
    }

/// <summary>
/// TODO
/// </summary>
    public void GenerateMap()
    {
        this.buildingTilemap.ClearAllTiles();
        this.terrainTilemap.ClearAllTiles();

        List<int> xCoordinates = NumberRandomizer.GetNRandomInts(10, -5, 11);
        List<int> yCoordinates = NumberRandomizer.GetNRandomInts(10, -5, 11);
        
        GrassTile grassTile = Resources.Load<GrassTile>("Tiles/GrassTile");
        CondoTile condoTile = Resources.Load<CondoTile>("Tiles/CondoTile");
        
        for (int i = -5; i <= 5; i++)
        {
            for (int j = -5; j <= 5; j++)
            {
                this.terrainTilemap.SetTile(new Vector3Int(i, j, 0), Instantiate(grassTile));
            }
        }

 

        this.buildingTilemap.SetTile(new Vector3Int(0, 0, 0), Instantiate(condoTile) );
        this.buildingTilemap.SetTile(new Vector3Int(0, 1, 0), Instantiate(condoTile) );
        this.buildingTilemap.SetTile(new Vector3Int(1, 0, 0), Instantiate(condoTile) );
        this.buildingTilemap.SetTile(new Vector3Int(1, 1, 0), Instantiate(condoTile) );

    }


}