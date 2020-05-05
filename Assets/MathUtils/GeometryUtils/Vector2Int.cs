﻿#region

using System;
using System.Collections;
using Extend;
using UnityEngine;

#endregion

/// <summary>A 2-dimensional vector of integers.</summary>
public class Vector2Int
{

    /// <summary>The zero vector of integers.</summary>
    public static readonly Vector2Int ZERO = new Vector2Int(0, 0);

    /// <summary>An integer array that represents the vector.</summary>
    public readonly int[] vecArray = new int[2];

	/// <summary>Given two integers (x, y), initialize a new 2D vector of integers from them.</summary>
	/// <param name="x">The x-component of the vector.</param>
	/// <param name="y">The y-component of the vector.</param>
	public Vector2Int(int x, int y)
    {
        this.vecArray[0] = x;
        this.vecArray[1] = y;
    }

	/// <summary>Given an index (i) into the vector, return the integer at index i.</summary>
	/// <param name="i">The index we're using to access the vector.</param>
	/// <returns>The value of the ith component at index i.</returns>
	public int this[int i] {
        // Check that index (i) is valid before returning the integer at that index.
        get {
            CheckVectorInvalidIndexException(i);
            return this.vecArray[i];
        }

        // Check that index (i) is valid before setting the integer at that index.
        set {
            CheckVectorInvalidIndexException(i);
            this.vecArray[i] = value;
        }
    }

	/// <summary>
	///     Throw an Exception if the given index (i) is not a valid index into this vector (i.e. the index is not 0 or
	///     1).
	/// </summary>
	/// <param name="i">The index we're checking to see is valid (i.e. 0 or 1).</param>
	public static void CheckVectorInvalidIndexException(int i)
    {
        if (!i.IsAny(0, 1))
            throw new IndexOutOfRangeException("A 2-dimensional vector must be indexed with the ints 0 or 1!");
    }

	/// <summary>Given a Vector2Int <paramref name="vec" /> of integers, convert it to a standard Vector2.</summary>
	/// <param name="vec">The vector to convert to a vector of integers.</param>
	public static explicit operator Vector2(Vector2Int vec)
    {
        return new Vector2(vec[0], vec[1]);
    }

	/// <summary>Given a Vector2 of floats, convert it to a vector of integers.</summary>
	/// <param name="vec">The vector to convert to a vector of integers.</param>
	public static explicit operator Vector2Int(Vector2 vec)
    {
        return new Vector2Int(vec[0].Round(), vec[1].Round());
    }

	/// <summary>Return true iff the two vectors (v1) and (v2) are not equal.</summary>
	/// <param name="v1">The first vector we're comparing.</param>
	/// <param name="v2">The second vector we're comparing.</param>
	/// <returns>true iff the two vectors (v1) and (v2) are not equal.</returns>
	public static bool operator !=(Vector2Int v1, Vector2Int v2)
    {
        return !(v1 == v2);
    }

	/// <summary>Return the sum of the vectors (v1) and (v2).</summary>
	/// <param name="v1">The first vector summand.</param>
	/// <param name="v2">The second vector summand.</param>
	/// <returns>The sum of the vectors (v1) and (v2).</returns>
	public static Vector2Int operator +(Vector2Int v1, Vector2Int v2)
    {
        return new Vector2Int(v1.vecArray[0] + v2.vecArray[0], v1.vecArray[1] + v2.vecArray[1]);
    }

	/// <summary>Return true iff the two vectors (v1) and (v2) are equal.</summary>
	/// <param name="v1">The first vector we're comparing.</param>
	/// <param name="v2">The second vector we're comparing.</param>
	/// <returns>true iff the two vectors (v1) and (v2) are equal.</returns>
	public static bool operator ==(Vector2Int v1, Vector2Int v2)
    {
        // If the first vector is null, then we should return true iff the second vector is also null.
        if ((object) v1 == null) return (object) v2 == null;

        // Otherwise, we know that the first vector is non-null so if the second one is, then return false.
        if ((object) v2 == null) return false;

        // Otherwise, we know both objects are int vectors and thus we can do a component-wise comparison.
        return v1[0] == v2[0] && v1[1] == v2[1];
    }

	/// <summary>Return true iff this Vector2Int is equal to the object (obj).</summary>
	/// <param name="obj">The object we're comparing this Vector2Int to.</param>
	/// <returns>true iff this Vector2Int is equal to the object (obj).</returns>
	public override bool Equals(object obj)
    {
        if (obj is Vector2Int) return (Vector2Int) obj == this;

        return base.Equals(obj);
    }

	/// <summary>Return an enumerator over this vector's elements.</summary>
	/// <returns>An enumerator over this vector's elements.</returns>
	public IEnumerator GetEnumerator()
    {
        return this.vecArray.GetEnumerator();
    }

	/// <summary>Return the hash code of this vector.</summary>
	/// <returns>The hash code of this vector.</returns>
	public override int GetHashCode()
    {
        // We are computing a hash code, which can result in overflow, which is fine.
        unchecked
        {
            int hash = 19;
            hash = hash * 23 + this[0].GetHashCode();
            hash = hash * 23 + this[1].GetHashCode();
            return hash;
        }
    }

	/// <summary>Given a vector (v), return this vector's Manhattan distance to it.</summary>
	/// <param name="v">The vector whose Manhattan distance to we're computing.</param>
	/// <returns>The Manhattan distance of this vector to vector (v).</returns>
	public int GetManhattanDistanceTo(Vector2Int v)
    {
        return ManhattanNorm(this, v);
    }

	/// <summary>Convert this vector to a string.</summary>
	/// <returns>The string representation of this vector.</returns>
	public override string ToString()
    {
        return string.Format("({0}, {1})", this.vecArray[0], this.vecArray[1]);
    }

	/// <summary>Given two vectors, return the Manhattan distance between them.</summary>
	/// <param name="v1">The first vector with which we're comparing a Manhattan Norm.</param>
	/// <param name="v2">The second vector with which we're comparing a Manhattan Norm.</param>
	/// <returns>The Manhattan distance between (v1) and (v2).</returns>
	internal static int ManhattanNorm(Vector2Int v1, Vector2Int v2)
    {
        return Math.Abs(v1[0] - v2[0]) + Math.Abs(v1[1] - v2[1]);
    }

}