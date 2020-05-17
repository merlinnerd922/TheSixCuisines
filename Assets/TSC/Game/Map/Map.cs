using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

    /// <summary>
    /// Process a mouse click downwards on this map.
    /// </summary>
    public void OnMouseDown()
    {
        // Perform a series of commands to test out the functionality of the tilemap-based map.
//        Grid grid = this.GetComponentInParent<Grid>();
//        Tilemap tilemap = this.GetComponent<Tilemap>();
//        Vector3 mousePosition = Input.mousePosition;
//        Vector3 screenToWorldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
//        Debug.Log(mousePosition);
//        Debug.Log(screenToWorldPoint);
//        Debug.Log(tilemap.WorldToCell(screenToWorldPoint));
//        TileBase tile = tilemap.GetTile(tilemap.WorldToCell(screenToWorldPoint));
//        Debug.Log(tile);
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

}