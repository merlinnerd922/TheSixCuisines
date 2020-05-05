﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPath : MonoBehaviour
{
    /// <summary>
    /// The list of physical points on the map that constitute this path's points.
    /// </summary>
    Transform[] path = null;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize this path's nodes if uninitialized, by forming that path from its child nodes.
        InitializePathNodesIfUninitialized();
    }

    /// <summary>
    /// Initialize this path's nodes if uninitialized, by forming that path from its child nodes.
    /// </summary>
    private void InitializePathNodesIfUninitialized()
    {
        if (path == null)
        {
            path = GetComponentsInChildren<Transform>();
        }
    }


    /// <summary>
    /// Return the length of this path.
    /// </summary>
    /// <returns>The length of this path.</returns>
    internal int GetLength()
    {
        // Initialize this path's nodes if uninitialized, by forming that path from its child nodes.
        InitializePathNodesIfUninitialized();
        return path.Length;
    }


    /// <summary>
    /// Return the <paramref name="i"/>th node of this path.
    /// </summary>
    /// <param name="i">The index of the node to return.</param>
    /// <returns>the <paramref name="i"/>th node of this path.</returns>
    internal Transform GetIthNode(int i)
    {
        InitializePathNodesIfUninitialized();
        return path[i];
    }
}
