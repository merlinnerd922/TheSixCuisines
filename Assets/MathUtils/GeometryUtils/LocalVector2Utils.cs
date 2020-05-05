﻿#region

using System;
using UnityEngine;

#endregion

namespace Helper
{

    public static class LocalVector2Utils
    {

        /// <summary>
        ///     Given a Vector2 <paramref name="vec" /> and predicate <paramref name="pred" /> that maps floats to booleans,
        ///     run the LINQ Any() method on (vec)'s components and return the result (i.e. for each component (x, y) of
        ///     <paramref name="vec" />, if (pred)(x) or (pred)(y), return true.)
        /// </summary>
        /// <param name="vec">The vector we are running <paramref name="pred" /> over.</param>
        /// <param name="pred">The predicate to check (vec)'s components on.</param>
        /// <returns>true iff <paramref name="pred" /> holds for any of (vec)'s components,</returns>
        /// <exception cref="Exception">Throw an Exception if a delegate callback throws one.</exception>
        public static bool Any(this Vector2 vec, Func<float, bool> pred)
        {
            return pred(vec[0]) || pred(vec[1]);
        }

        /// <summary>
        ///     Given some Vector2's <paramref name="fromPosition" /> and <paramref name="toPosition" /> representing
        ///     coordinates in 2D as well as a direction <paramref name="targetDirection" />, return true iff the coordinates
        ///     <paramref name="toPosition" /> can be accessed from <paramref name="fromPosition" /> if moving in direction (dir).
        /// </summary>
        /// <param name="fromPosition">The position from which we should move.</param>
        /// <param name="toPosition">The position we should move to</param>
        /// <param name="targetDirection">The direction in which we are moving in.</param>
        /// <returns>
        ///     true iff the coordinates <paramref name="toPosition" /> can be accessed from
        ///     <paramref
        ///         name="fromPosition" />
        ///     if moving in direction <paramref name="targetDirection" />.
        /// </returns>
        public static bool DirectionIsTowards(this Vector2 fromPosition, Vector2 toPosition, Direction targetDirection)
        {
            // Only accept the directions UP, LEFT, RIGHT or DOWN.
            switch (targetDirection)
            {
                case Direction.LEFT:
                {
                    return fromPosition[1] > toPosition[1];
                }
                case Direction.RIGHT:
                {
                    return fromPosition[1] < toPosition[1];
                }
                case Direction.DOWN:
                {
                    return fromPosition[0] > toPosition[0];
                }
                case Direction.UP:
                {
                    return fromPosition[0] < toPosition[0];
                }
            }

            // Other directions cannot be validly used to compare directions.
            throw new UnityException(
                "The given direction {0} cannot be used with the method DirectionIsTowards, since we are operating (relative to the grid) in 2D. Accepted directions are UP, DOWN, LEFT and RIGHT.");
        }

        /// <summary>Given a vector <paramref name="vec1" />, return the distance between it and (vec2).</summary>
        /// <param name="vec1">The vector whose distance we're comparing to (vec2).</param>
        /// <param name="vec2">The vector whose distance we're comparing to (vec1).</param>
        /// <returns>The distance between the two vectors.</returns>
        public static float GetDistanceTo(this Vector2 vec1, Vector2 vec2)
        {
            return Vector2.Distance(vec1, vec2);
        }

        /// <summary>
        ///     Given a 2D vector of floats <paramref name="vector" /> and a float <paramref name="newMagnitude" /> , return a
        ///     resized version of <paramref name="vector" /> so that it has size <paramref name=" newMagnitude " /> .
        /// </summary>
        /// <param name="vector">The vector that we should scale to have magnitude <paramref name="newMagnitude" />.</param>
        /// <param name="newMagnitude">The new magnitude <paramref name="vector" /> should have.</param>
        /// <returns>The newly scaled vector.</returns>
        public static Vector3 ScaleToMagnitude(this Vector2 vector, float newMagnitude)
        {
            return vector.normalized * newMagnitude;
        }

        /// <summary>
        ///     Given a 2D vector <paramref name="vec" /> and a float <paramref name="newX" /> , set the x component of
        ///     <paramref name="vec" /> to <paramref name="newX" /> .
        /// </summary>
        /// <param name="vec">The vector whose X-position we should set.</param>
        /// <param name="newX">The new X.</param>
        /// <returns>The <see cref="Vector2" /> whose X-position we should set and subsequently return.</returns>
        public static Vector2 SetXReturn(this Vector2 vec, float newX)
        {
            vec.Set(newX, vec.y);
            return vec;
        }

        /// <summary>
        ///     Given a Vector2 <paramref name="vec" /> and a float <paramref name="newY" /> , set the object's position in
        ///     the y direction to <paramref name="newY" /> .
        /// </summary>
        /// <param name="vec">The vector whose Y position we should set.</param>
        /// <param name="newY">The new Y.</param>
        public static void SetY(this Vector2 vec, float newY)
        {
            Vector2 newDirVec = vec;
            newDirVec.y = newY;
            vec = new Vector2(newDirVec.x, newDirVec.y);
        }

        /// <summary>
        ///     Given a Vector2 object <paramref name="vec" />, return a Vector2Int representation of the same vector.
        ///     Moreover, assert that the vector's components are strictly integers.
        /// </summary>
        /// <param name="vec">The vector to convert into a strict integer vector.</param>
        /// <returns>The integer representation of the vector.</returns>
        public static Vector2Int ToVector2Int(this Vector2 vec)
        {
            return new Vector2Int(vec[0].ToIntStrict(), vec[1].ToIntStrict());
        }

        /// <summary>Given a Vector2, round its components to the nearest whole numbers.</summary>
        /// <param name="vec">The Vector2 whose components we are rounding to the nearest whole numbers.</param>
        /// <returns>(vec) with its components rounded.</returns>
        public static Vector2 ToWholeVector(this Vector2 vec)
        {
            return new Vector2(vec[0].Round(), vec[1].Round());
        }

        /// <summary>
        ///     Given a 2D vector <paramref name="vec" />, return a version of <paramref name="vec" /> where any components
        ///     that are extremely close to 0, are zeroed out.
        /// </summary>
        /// <param name="vec">The vector whose component we should zero out.</param>
        /// <returns>The <see cref="Vector2" /> value with its close-to-zero components zeroed out.</returns>
        public static Vector2 GetZeroedOut(this Vector2 vec)
        {
            vec[0] = vec[0].RoundNearZeroComponentsIncreaseMagnitude();
            vec[1] = vec[1].RoundNearZeroComponentsIncreaseMagnitude();
            return vec;
        }

    }

}