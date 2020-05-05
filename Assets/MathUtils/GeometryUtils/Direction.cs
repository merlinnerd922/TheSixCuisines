﻿/// <summary>An enumeration of all possible directions.</summary>
public enum Direction
{

    /// <summary>The forwards direction, along the Z-axis.</summary>
    FORWARDS = 0,

    /// <summary>The leftwards direction, along the X-axis.</summary>
    LEFT = 1,

    /// <summary>The backwards direction, along the X-axis.</summary>
    BACKWARDS = 2,

    /// <summary>The rightwards direction, along the Z-axis.</summary>
    RIGHT = 3,

    /// <summary>
    ///     The upwards direction, along the Y-axis. Note that UP is NOT defined relative to the 2D plane, but the 3D
    ///     plane. For that direction that is relative to the 3D plane, use FORWARDS instead.
    /// </summary>
    UP = 4,

    /// <summary>
    ///     The downwards direction, along the Y-axis. Note that UP is NOT defined relative to the 2D plane, but the 3D
    ///     plane. For that direction that is relative to the 3D plane, use BACKWARDS instead.
    /// </summary>
    DOWN = 5,

    /// <summary>The null direction.</summary>
    NULL = 6

}