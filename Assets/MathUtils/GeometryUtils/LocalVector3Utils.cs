﻿#region

using System;
using UnityEngine;

#endregion

namespace Helper
{

    public static class LocalVector3Utils
    {

        /// <summary>Given two 3D vectors of floats, take their dot product and return it.</summary>
        /// <param name="v1">The first vector operand in the dot product calculation.</param>
        /// <param name="v2">The second vector operand in the dot product calculation.</param>
        /// <returns>The dot product of vector1 and vector2.</returns>
        public static float DotProduct(this Vector3 v1, Vector3 v2)
        {
            return Vector3.Dot(v1, v2);
        }

        /// <summary>
        ///     Given a source vector <paramref name="vector1" /> and a target vector
        ///     <paramref
        ///         name="vector2" />
        ///     , determine the distance between them and return it.
        /// </summary>
        /// <param name="vector1">The vector whose distance we're comparing to <paramref name="vector2" />.</param>
        /// <param name="vector2">The vector whose distance we should get to <paramref name="vector1" />.</param>
        /// <returns>The distance between <paramref name="vector1" /> and <paramref name="vector2" />.</returns>
        public static float Get3DDistanceTo(this Vector3 vector1, Vector3? vector2)
        {
            return Vector3.Distance(vector1, (Vector3) vector2);
        }

        /// <summary>
        ///     Given a Vector3 <paramref name="vec" />, return a copy of the vector with the x-component set to its additive
        ///     inverse.
        /// </summary>
        /// <param name="vec">The vector whose X component we should negate.</param>
        /// <returns><paramref name="vec" />, with its x component additively negated.</returns>
        public static Vector3 GetNegatedX(this Vector3 vec)
        {
            return new Vector3(-vec.x, vec.y, vec.z);
        }

        /// <summary>Given a vector <paramref name="vector" />, return true iff any of the dimensions are equal to 0.</summary>
        /// <param name="vector">The vector we should check for nonzero dimensions.</param>
        /// <returns>true iff any of <paramref name="vector" />'s dimensions are equal to 0.7</returns>
        public static bool HasNonzeroDimension(this Vector3 vector)
        {
            return Math.Abs(vector.x) < float.Epsilon || Math.Abs(vector.y) < float.Epsilon ||
                   Math.Abs(vector.z) < float.Epsilon;
        }

        /// <summary>
        ///     Given two overworld points <paramref name="point" /> and <paramref name="sphereOrigin" /> and a positive float
        ///     <paramref name="radius" />, return true iff <paramref name="point" /> is located within the sphere with radius
        ///     <paramref name="radius" /> centered at (sphereOrigin).
        /// </summary>
        /// <param name="point">The point we're checking is inside a sphere.</param>
        /// <param name="sphereOrigin">The origin of the sphere we're checking.</param>
        /// <param name="radius">The radius of the sphere we're checking.</param>
        /// <returns>true iff point is inside the sphere centered at <paramref name="sphereOrigin" />.</returns>
        public static bool IsInsideSphereAround(this Vector3 point, Vector3 sphereOrigin, float radius)
        {
            return point.Get3DDistanceTo(sphereOrigin) <= radius;
        }

        /// <summary>Given a 3D vector of floats <paramref name="vector" />, return true iff all of its components are integers.</summary>
        /// <param name="vector">The vector we're checking for integer components.</param>
        /// <returns>true iff all of <paramref name="vector" />'s components are integers.</returns>
        public static bool IsIntVector(this Vector3 vector)
        {
            return vector[0].IsInt() && vector[1].IsInt() && vector[2].IsInt();
        }

        /// <summary>
        ///     Given a world point <paramref name="positionToCheck" /> and a script of a camera
        ///     <paramref
        ///         name="camera" />
        ///     , return true if point <paramref name="positionToCheck" /> is visible from <paramref name="camera" />, and false
        ///     otherwise.
        /// </summary>
        /// <param name="positionToCheck">The position we're checking to see is visible from the camera <paramref name="camera" />.</param>
        /// <param name="camera">The camera we're using to see if <paramref name="positionToCheck" /> is visible.</param>
        /// <returns>true iff <paramref name="positionToCheck" /> is visible from <paramref name="camera" />.</returns>
        public static bool IsVisibleFrom(this Vector3 positionToCheck, Camera camera)
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(positionToCheck);
            return Physics.Raycast(ray, out hit);
        }

        /// <summary>
        ///     Given a 3D vector of floats <paramref name="vectorToScale" /> and a float
        ///     <paramref
        ///         name="newMagnitude" />
        ///     , return a resized version of <paramref name="vectorToScale" /> so that it has size
        ///     <paramref name=" newMagnitude " /> .
        /// </summary>
        /// <param name="vectorToScale">The vector that we should resize to have magnitude <paramref name="newMagnitude" />.</param>
        /// <param name="newMagnitude">The new magnitude that <paramref name="vectorToScale" /> should have.</param>
        /// <returns>The new version of <paramref name="vectorToScale" /> with magnitude <paramref name="newMagnitude" />.</returns>
        public static Vector3 ScaleToMagnitude(this Vector3 vectorToScale, float newMagnitude)
        {
            return vectorToScale.normalized * newMagnitude;
        }

        /// <summary>
        ///     Given a Vector3 v, modify its y value to the given input <paramref name="newYVal" /> and return the newly
        ///     modified vector.
        /// </summary>
        /// <param name="vector">The vector whose Y value we should set.</param>
        /// <param name="newYVal">The new Y Val.</param>
        public static void SetY(this Vector3 vector, float newYVal)
        {
            vector.Set(vector.x, newYVal, vector.z);
        }

        /// <summary>
        ///     Given a vector <paramref name="vec" /> and a float <paramref name="newY" /> , return a version of
        ///     <paramref name="vec" /> with its Y component set to (newY).
        /// </summary>
        /// <param name="vec">The vector whose Y position we should set.</param>
        /// <param name="newY">The new Y value we should set <paramref name="vec" />'s value to.</param>
        /// <returns>The <see cref="Vector3" /> that has its Y value newly set.</returns>
        public static Vector3 SetYTo(this Vector3 vec, float newY)
        {
            Vector3 newPosition = vec;
            newPosition.y = newY;
            return newPosition;
        }

        /// <summary>Given a 3D vector <paramref name="vec" />, convert it to a quaternion and return the quaternion.</summary>
        /// <param name="vec">The Vector3 we're converting to a quaternion.</param>
        /// <returns>The quaternion representation of (vec).</returns>
        public static Quaternion ToQuaternion(this Vector3 vec)
        {
            return Quaternion.Euler(vec[0], vec[1], vec[2]);
        }

        /// <summary>Given a 3D float vector, form a new 2D float vector from its X and Z components, and return it.</summary>
        /// <param name="vec">The vector whose X and Z components we are using to create a 2D vector.</param>
        /// <returns>A 2D vector with vec's x and z components.</returns>
        public static Vector2 ToVector2XZ(this Vector3 vec)
        {
            return new Vector2(vec.x, vec.z);
        }

        /// <summary>Given a vector of floats, return the vector so that its decimal parts have been removed.</summary>
        /// <param name="v">A vector whose decimal components we should truncate.</param>
        /// <returns>The vector with its decimal components truncated.</returns>
        public static Vector3 Truncate(this Vector3 v)
        {
            return new Vector3(v.x.Truncate(), v.y.Truncate(), v.z.Truncate());
        }

        /// <summary>
        ///     Given a 3D vector of floats <paramref name="vec" />, set the Y component to zero and return the resultant
        ///     vector.
        /// </summary>
        /// <param name="vec">The vector whose y-component we should zero out.</param>
        /// <returns>The resultant vector with the zeroed-out Y component.</returns>
        public static Vector3 ZeroOutY(this Vector3 vec)
        {
            return new Vector3(vec.x, 0, vec.z);
        }

        /// <summary>Given three integers, generate a vector of ints from them.</summary>
        /// <param name="i">The 1st component of the vector.</param>
        /// <param name="j">The 2nd component of the vector.</param>
        /// <param name="k">The 3rd component of the vector.</param>
        /// <returns>A Vector3 constructed from i, j and k.</returns>
        public static Vector3 ToVector3(int i, int j, int k)
        {
            return new Vector3(i, j, k);
        }

        /// <summary>
        ///     Given a pointer to a Vector3 <paramref name="posVec" />, modify the Y value to the value
        ///     <paramref name="newYVal" />.
        /// </summary>
        /// <param name="posVec">The reference to the vector whose value we should modify.</param>
        /// <param name="newYVal">The new y value of the vector should modify..</param>
        public static void SetYRef(ref Vector3 posVec, float newYVal)
        {
            posVec = new Vector3(posVec.x, newYVal, posVec.z);
        }

        /// <summary>
        ///     Given a 3D float vector <paramref name="vec" /> and a float value <paramref name="newY" /> , set the vector's
        ///     Y value to <paramref name="newY" /> and return the resultant vector.
        /// </summary>
        /// <param name="vec">The vector whose Y component we should modify.</param>
        /// <param name="newY">The value we should set <paramref name="vec" />'s Y component to.</param>
        /// <returns>The modified version of <paramref name="vec" />, with new value <paramref name="newY" />.</returns>
        public static Vector3 GetVectorWithAdjustedY(this Vector3 vec, float newY)
        {
            return new Vector3(vec.x, newY, vec.z);
        }

    }

}