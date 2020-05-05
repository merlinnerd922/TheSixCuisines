﻿#region

using UnityEngine;

#endregion

/// <summary>A class representing a sphere in Unity world space</summary>
public class Sphere
{

    /// <summary>Represents the center point of the sphere.</summary>
    public Vector3 center;

    /// <summary>Represents the radius of the sphere.</summary>
    public float radius;

    /// <summary>Given a float representing the radius and a vector representing the center position, create a new sphere.</summary>
    /// <param name="center">The Vector3 representing the center of this sphere.</param>
    /// <param name="radius">The radius of this sphere.</param>
    public Sphere(Vector3 center, float radius)
    {
        this.radius = radius;
        this.center = center;
    }

}