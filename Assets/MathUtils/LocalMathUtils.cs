﻿#region

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Helper;
using UnityEngine;

#endregion

/// <summary>A static class containing extension methods for numbers, as if extending the System.Math method.</summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class LocalMathUtils
{

    /// <summary>A float value constant for the value 10 ** -6.</summary>
    public const float TEN_TO_MINUS_SIX = 1E-06F;

    /// <summary>The value of the square root of 2.</summary>
    public static readonly float SQRT_2 = Mathf.Sqrt(2);

	/// <summary>
	///     A mapping between directions on the 2D plane and the amount of degrees that an object must be turned to be
	///     facing that direction.
	/// </summary>
	public static readonly Dictionary<Direction, float> ROTATION_MAPPING = new Dictionary<Direction, float>
	{
		{Direction.FORWARDS, 0},
		{Direction.RIGHT, 90},
		{Direction.BACKWARDS, 180},
		{Direction.LEFT, 270}
	};

	/// <summary>Given an array of integers, return the smallest int in the array</summary>
	/// <param name="intArray">The array of integers.</param>
	/// <returns>The smallest integer in the array.</returns>
	public static int MinAll(params int[] intArray)
    {
        return intArray.Min();
    }

	/// <summary>
	///     Given a float <paramref name="floatToRound" />, if its value is extremely close to 0, then return 0;
	///     otherwise, return <paramref name="floatToRound" /> itself.
	/// </summary>
	/// <param name="floatToRound">The float to round.</param>
	/// <returns>
	///     <paramref name="floatToRound" />, unless it is extremely close to 0; in that case, return
	///     <paramref name="floatToRound" />.
	/// </returns>
	public static float RoundNearZeroComponentsCompareSmall(this float floatToRound)
    {
        return Mathf.Abs(floatToRound) < Mathf.Pow(10, -4) ? 0 : floatToRound;
    }

	/// <summary>
	///     Given a float <paramref name="nearZeroValue" />, if its value is extremely close to 0, then return 0;
	///     otherwise, return <paramref name="nearZeroValue" />.
	/// </summary>
	/// <param name="nearZeroValue">The value that whose near zero components we're rounding.</param>
	/// <returns>
	///     The value <paramref name="nearZeroValue" /> itself, unless it is extremely close to zero (in which case we
	///     return 0).
	/// </returns>
	public static float RoundNearZeroComponentsIncreaseMagnitude(this float nearZeroValue)
    {
        return Mathf.Round(nearZeroValue * 10) / 10 == 0 ? 0 : nearZeroValue;
    }

	/// <summary>
	///     Given a float <paramref name="timeElapsed" /> , return a certain pre-calculated value between 0 and 1. This
	///     value is equal to the following: If floor(f) % 2 = 0, then return f % 1; If floor(f) % 2 = 1, then return 1 - f %
	///     1. What this essentially creates is a version of the Lerp function whose values not in range [0, 1], instead of
	///     being clamped, map to values as if the lerp function is being zigzagged, hence the name ZigzagLerp(). It also
	///     creates a version of the sine function with a more abrupt change.
	/// </summary>
	/// <param name="timeElapsed">The amount of time that has passed.</param>
	/// <returns>The value of the ZigzagLerp function at <paramref name="timeElapsed" />.</returns>
	public static float ZigzagLerp(float timeElapsed)
    {
        // Calculate the value of f mod 2, as well as its decimal component.
        float fMod2 = Mathf.Floor(timeElapsed).Mod(2);
        float decimalComponent = timeElapsed % 1;

        switch (fMod2)
		{
			// If the value of f is in [0, 1), or if f rounds down to an even number, then return the
			// decimal component of f. As such, the ranges [0, 1), [2, 3), [4, 5), etc. all map to this range.
			case 0f:
				return decimalComponent;

			// If the value of f is in [1, 2), or if f rounds down to an odd number, then return the
			// value of 1 minus the decimal component.
			case 1f:
				return 1 - decimalComponent;

			default:
				throw new ArgumentException("We should not get to this point!");
        };
    }

	/// <summary>
	///     Given the integer <paramref name="thisInt" />, reduce it or increase it to make sure it is bounded below by
	///     <paramref name="lower" />, and bounded above by <paramref name="upper" />.
	/// </summary>
	/// <param name="thisInt">The integer which we're bounding from above and below.</param>
	/// <param name="upper">The upper.</param>
	/// <param name="lower">The lower.</param>
	/// <returns>The <see cref="int" />.</returns>
	public static int Clamp(this int thisInt, int upper, int lower)
    {
        return Math.Min(Math.Max(thisInt, lower), upper);
    }

	/// <summary>
	///     Given the value (volumeToSet), cap it from above by (maxValue) and from below by (minValue), and return the
	///     newly capped value.
	/// </summary>
	/// <param name="volumeToSet">The value to bind from above and below.</param>
	/// <param name="minValue">The value to bind <paramref name="volumeToSet" /> below by.</param>
	/// <param name="maxValue">The value to bind <paramref name="volumeToSet" /> above by.</param>
	/// <returns>The newly bound value.</returns>
	public static float Clamp(this float volumeToSet, int minValue, int maxValue)
    {
        return Mathf.Max(Mathf.Min(volumeToSet, maxValue), minValue);
    }

	/// <summary>
	///     Given two floats <paramref name="lowerBound" /> and <paramref name="upperBound" />, as well as another float
	///     <paramref name="f" />, return true if <paramref name="f" /> is in the range ( <paramref name="lowerBound" />,
	///     <paramref name="upperBound" />) (an exclusive range as opposed to an inclusive one), and false otherwise.
	/// </summary>
	/// <param name="f">The floatToCheck.</param>
	/// <param name="lowerBound">The lower Bound.</param>
	/// <param name="upperBound">The upper Bound.</param>
	/// <returns>
	///     true iff the value <paramref name="f" /> is in the range ( <paramref name="lowerBound" />,
	///     <paramref name="upperBound" />).
	/// </returns>
	public static bool InExclusiveRange(this float f, float lowerBound, float upperBound)
    {
        return f < upperBound && f > lowerBound;
    }

	/// <summary>
	///     Given two floats <paramref name="lowerBound" /> and <paramref name="upperBound" />, as well as another float
	///     <paramref name="floatToCheck" />, return true iff
	///     <paramref
	///         name="floatToCheck" />
	///     is in the range [ <paramref name="lowerBound" />, <paramref name="upperBound" />].
	/// </summary>
	/// <param name="floatToCheck">The float to determine is in the given range.</param>
	/// <param name="lowerBound">The lower Bound.</param>
	/// <param name="upperBound">The upper Bound.</param>
	/// <returns>
	///     true return true iff <paramref name="floatToCheck" /> is in the range [
	///     <paramref
	///         name="lowerBound" />
	///     , <paramref name="upperBound" />].
	/// </returns>
	public static bool InInclusiveRange(this float floatToCheck, float lowerBound, float upperBound)
    {
        return floatToCheck <= upperBound && floatToCheck >= lowerBound;
    }

	/// <summary>
	///     Given two integers <paramref name="lowerBound" /> and <paramref name="upperBound" />, as well as another int
	///     <paramref name="intToCheck" />, return true if
	///     <paramref
	///         name="intToCheck" />
	///     is in the INCLUSIVE range [ <paramref name="lowerBound" />,
	///     <paramref
	///         name="upperBound" />
	///     ], and false otherwise.
	/// </summary>
	/// <param name="intToCheck">The intToCheck.</param>
	/// <param name="lowerBound">The lower Bound.</param>
	/// <param name="upperBound">The upper Bound.</param>
	/// <returns>
	///     true iff <paramref name="intToCheck" /> is in the inclusive range [
	///     <paramref
	///         name="lowerBound" />
	///     , <paramref name="upperBound" />].
	/// </returns>
	public static bool InInclusiveRange(this int intToCheck, int lowerBound, int upperBound)
    {
        return intToCheck <= upperBound && intToCheck >= lowerBound;
    }

	/// <summary>
	///     Given an integer <paramref name="i" /> and two floats <paramref name="lower" /> and <paramref name="upper" />,
	///     return true if thisInt is between them and false otherwise.
	/// </summary>
	/// <param name="i">The i.</param>
	/// <param name="lower">The lower.</param>
	/// <param name="upper">The upper.</param>
	/// <returns>The <see cref="bool" />.</returns>
	public static bool InRange(this int i, float lower, float upper)
    {
        return i <= upper && i >= lower;
    }

	/// <summary>Return true iff the given integer is in the inclusive range [min, max] and false otherwise.</summary>
	/// <param name="i">The integer we're checking to see is in the range specified by [min, max].</param>
	/// <param name="min">The lower inclusive bound.</param>
	/// <param name="max">The upper inclusive bound.</param>
	/// <returns>true iff the given integer is in the inclusive range [min, max] and false otherwise.</returns>
	public static bool InRange(this int i, int min, int max)
    {
        return i >= min && i <= max;
    }

	/// <summary>Return true iff the given float is in the inclusive range [min, max] and false otherwise.</summary>
	/// <param name="f">The float we're checking to see is in the range specified by [min, max].</param>
	/// <param name="min">The lower inclusive bound.</param>
	/// <param name="max">The upper inclusive bound.</param>
	/// <returns>true iff the given float is in the inclusive range [min, max] and false otherwise.</returns>
	public static bool InRange(this float f, float min, float max)
    {
        return f >= min && f <= max;
    }

	/// <summary>Given an integer, return true if it's even and false otherwise.</summary>
	/// <param name="i">The thisInt.</param>
	/// <returns>true iff the integer <paramref name="i" /></returns>
	public static bool IsEven(this int i)
    {
        return i % 2 == 0;
    }

	/// <summary>Given a float, return true iff it does not have a fractional part.</summary>
	/// <param name="floatToCheck">The float we're checking to be castable to an int.</param>
	/// <returns>The float</returns>
	public static bool IsInt(this float floatToCheck)
    {
        return Math.Abs(floatToCheck - (int) floatToCheck) < double.Epsilon;
    }

	/// <summary>Given an integer <paramref name="i" />, return true if it is odd and false otherwise.</summary>
	/// <param name="i">The integer we are checking for oddity.</param>
	/// <returns>true iff <paramref name="i" /> is odd.</returns>
	public static bool IsOdd(this int i)
    {
        return !i.IsEven();
    }

	/// <summary>
	///     Given three integers <paramref name="value" />, <paramref name="inclusiveMinimum" /> and
	///     <paramref name="inclusiveMaximum" />, clamp <paramref name="value" /> so that it is no larger than
	///     <paramref name="inclusiveMaximum" /> and no smaller than
	///     <paramref
	///         name="inclusiveMinimum" />
	///     ; then, return the clamped integer.
	/// </summary>
	/// <param name="value">The value we're looking to clamp.</param>
	/// <param name="inclusiveMinimum">The inclusive minimum we want <paramref name="value" /> to be bounded below by.</param>
	/// <param name="inclusiveMaximum">The inclusive maximum we want <paramref name="value" /> to be bounded above by.</param>
	/// <returns>
	///     The clamped integer, after <paramref name="value" /> has been clamped above and below by the values
	///     <paramref name="inclusiveMinimum" /> and <paramref name="inclusiveMaximum" />, respectively.
	/// </returns>
	public static int LimitToRange(this int value, int inclusiveMinimum, int inclusiveMaximum)
    {
        // Return the inclusive minimum if the value is smaller than the inclusive minimum.
        if (value < inclusiveMinimum) return inclusiveMinimum;

        // Return the inclusive maximum if the value is larger than this inclusive maximum.
        if (value > inclusiveMaximum) return inclusiveMaximum;

        // Otherwise, return the value.
        return value;
    }

	/// <summary>
	///     Given a float <paramref name="floatValue" /> and another float <paramref name="modulus" />, return the modulo
	///     of <paramref name="floatValue" /> with <paramref name="modulus" />.
	/// </summary>
	/// <param name="floatValue">The float value to take the modulus with <paramref name="modulus" />.</param>
	/// <param name="modulus">The modulus.</param>
	/// <returns>The modulated float.</returns>
	public static float Mod(this float floatValue, float modulus)
    {
        return (floatValue % modulus + modulus) % modulus;
    }

	/// <summary>Given a float, round it to the nearest integer and return it.</summary>
	/// <param name="f">The floatToCheck.</param>
	/// <exception cref="ArgumentOutOfRangeException">Raise this Exception if a float that's  out of room is provided.</exception>
	/// <returns>The <see cref="int" />.</returns>
	public static int Round(this float f)
    {
        return (int) Math.Round(f, 0);
    }

	/// <summary>Given an integer, return a hexadecimal representation of it.</summary>
	/// <param name="i">The integer to convert into a hex string.</param>
	/// <returns>The <see cref="string" /> representation of the given integer.</returns>
	public static string ToHexString(this int i)
    {
        return i.ToString("X");
    }

	/// <summary>
	///     Return the integer that is represented by this string. If the string does NOT represent an integer, then raise
	///     an Exception instead.
	/// </summary>
	/// <param name="intString">The intString.</param>
	/// <returns>The <see cref="int" /> representation of the given <paramref name="intString" />.</returns>
	public static int ToInt(this string intString)
    {
		// If we can parse the string as an int, return the parsed version as an integer.
        if (int.TryParse(intString, out int retVal)) return retVal;

        // Otherwise, raise an exception stating that we expected an integer.
        throw new ArgumentException("The function ToInt() was run, and expected a string as an input that can be" +
                                    " converted into a number, but it did not get such a string!");
    }

	/// <summary>Given a float known NOT to be an int, return it as an int.</summary>
	/// <param name="f">The float to assert is strictly an integer.</param>
	/// <returns>The <see cref="int" /> representation of the given float, which should also represent an integer.</returns>
	public static int ToIntStrict(this float f)
    {
        // Return the truncated int if it has no decimal part (or a decimal part very close to 0). Otherwise, raise an exception.
        if (Math.Abs(f - f.Truncate()) < TEN_TO_MINUS_SIX) return f.Truncate();

        throw new ArgumentException("This float should NOT have a decimal part, but has" +
                                    " a value of {0}!".FormatExtend(f));
    }

	/// <summary>Given a float, truncate it and return it as an int.</summary>
	/// <param name="f">The float that we are to truncate.</param>
	/// <returns>The truncated integer.</returns>
	public static int Truncate(this float f)
    {
        return (int) Math.Truncate((decimal) f);
    }

	/// <summary>Return the value (colNum % m); this special method is meant to work with negative numbers, unlike C#'s %.</summary>
	/// <param name="numberToMod">The number to take the mod of, with respect to (modDividend).</param>
	/// <param name="modDividend">The number to mod <paramref name="numberToMod" /> by.</param>
	/// <returns>The value (colNum % m), including cases when <paramref name="modDividend" /> is a negative number.</returns>
	public static int Mod(int numberToMod, int modDividend)
    {
        return (numberToMod % modDividend + modDividend) % modDividend;
    }

	/// <summary>
	///     Return the value of the function at the given input float <paramref name="floatInput" />. This method is
	///     supposed to symbolize "zigzagged" version of the sine function (lacks the smoothness of the normal sine function).
	/// </summary>
	/// <param name="floatInput">The independent function variable we plug into this function.</param>
	/// <returns>value of the function at the given input float <paramref name="floatInput" /></returns>
	public static float ZigzagSin(float floatInput)
    {
        // We consider the value floatInput as follows: we take the modulus of floatInput with 4f.
        // The function should be doing the following: if z is in [0,1], the value should be changing
        // from 0 up to 1: if z is in [1,2], the value should be changing from 1 down to 0; If z is
        // in [2,3], the value should be changing from 0 down to -1; If z is in [3,4], the value
        // should be changing from
        // -1 up to 0.
        float z = floatInput % 4f;

        // In the first 1 units: zig "upwards".
        if (z.InRange(0f, 1f)) return z;

        // After the first unit, zig back "down".
        if (z.InRange(1f, 3f)) return -z + 2;

        // ...and then back up again to zero.
        if (z.InRange(3f, 4f)) return z - 4;

        throw new ArgumentException(
            "We should not be reaching this point because we have taken the modulus of z with 4!");
    }

	/// <summary>
	///     Given two bounding boxes <paramref name="bounds" /> and <paramref name="target" />, return true iff
	///     <paramref name="target" /> is fully contained within (bounds).
	/// </summary>
	/// <param name="bounds">The theoretically outside bounds.</param>
	/// <param name="target">The bounds that are theoretically inside (bounds).</param>
	/// <returns>true iff the given Bounds object <paramref name="bounds" /> is within the target <paramref name="target" />.</returns>
	public static bool ContainBounds(this Bounds bounds, Bounds target)
    {
        return bounds.Contains(target.min) && bounds.Contains(target.max);
    }

	/// <summary>
	///     Given the bounds of a GameObject <paramref name="boundingBox" /> and a sphere
	///     <paramref
	///         name="sphere" />
	///     , return true iff <paramref name="boundingBox" /> intersects with <paramref name="sphere" />.
	/// </summary>
	/// <param name="boundingBox">The bounding box we want to check intersects with <paramref name="sphere" />.</param>
	/// <param name="sphere">The sphere we're checking for intersection.</param>
	/// <returns>true iff <paramref name="boundingBox" /> intersects with <paramref name="sphere" />.</returns>
	public static bool IntersectsWith(this Bounds boundingBox, Sphere sphere)
    {
        float dmin = 0;

        Vector3 center = sphere.center;
        Vector3 bmin = boundingBox.min;
        Vector3 bmax = boundingBox.max;

        if (center.x < bmin.x)
            dmin += Mathf.Pow(center.x - bmin.x, 2);
        else if (center.x > bmax.x) dmin += Mathf.Pow(center.x - bmax.x, 2);

        if (center.y < bmin.y)
            dmin += Mathf.Pow(center.y - bmin.y, 2);
        else if (center.y > bmax.y) dmin += Mathf.Pow(center.y - bmax.y, 2);

        if (center.z < bmin.z)
            dmin += Mathf.Pow(center.z - bmin.z, 2);
        else if (center.z > bmax.z) dmin += Mathf.Pow(center.z - bmax.z, 2);

        return dmin <= Math.Pow(sphere.radius, 2);
    }

	/// <summary>
	///     Given a direction <paramref name="dir" /> parallel to the xz-plane, return the amount of degrees the object
	///     must be turned in order to face that direction.
	/// </summary>
	/// <param name="dir">The direction we're converting to degrees.</param>
	/// <returns>The direction, as degrees.</returns>
	public static float ToDegrees(this Direction dir)
    {
        return ROTATION_MAPPING[dir];
    }

}