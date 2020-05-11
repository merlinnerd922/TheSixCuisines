﻿﻿#region

using System;
using System.Collections.Generic;
using System.Linq;
using Extend;

#endregion

namespace Helper
{

    public static class NumberRandomizer
    {

        /// <summary>Given an interval [ <paramref name="min" />, <paramref name="max" />], return a random float from this range.</summary>
        /// <param name="randomObject">The Random object we're using to generate the random float.</param>
        /// <param name="min">The minimum permitted value of the returned float.</param>
        /// <param name="max">The maximum permitted value of the returned float.</param>
        /// <returns>The randomized float from within the given range.</returns>
        public static float NextFloat(this Random randomObject, float min, float max)
        {
            double result = randomObject.NextDouble() * (max - (double) min) + min;
            return (float) result;
        }

        /// <summary>
        ///     Return a list of <paramref name="numberOfIntsToGet" /> random ints within the range ( <paramref name="min" />,
        ///     <paramref name="min" /> + <paramref name="rangeSize" />).
        /// </summary>
        /// <param name="numberOfIntsToGet">The number of integers to get within the given range.</param>
        /// <param name="min">The minimum possible value of the integer to get.</param>
        /// <param name="rangeSize">The size of the range of integers that can be possibly retrieved.</param>
        /// <returns>
        ///     a list of <paramref name="numberOfIntsToGet" /> random ints within the range (
        ///     <paramref
        ///         name="min" />
        ///     , <paramref name="min" /> + <paramref name="rangeSize" />).
        /// </returns>
        public static List<int> GetNRandomInts(int numberOfIntsToGet, int min, int rangeSize)
        {
            return Enumerable.Range(min, rangeSize).ToList().Shuffle().ToList().GetRange(0, numberOfIntsToGet);
        }

        /// <summary>Given two integers a and b, return a random integer between those integers.</summary>
        /// <param name="minValue">The minimum possible value of the returned integer.</param>
        /// <param name="maxValue">The maximum possible value of the returned integer.</param>
        /// <returns>a random integer between <paramref name="minValue" /> and <paramref name="maxValue" />.</returns>
        public static int Next(int minValue, int maxValue)
        {
            return LocalGeneralUtils.SYS_RANDOM.Next(minValue, maxValue);
        }

        /// <summary>
        ///     Given an integer <paramref name="maxValue" /> return a random positive integer with value at most
        ///     <paramref name="maxValue" /> .
        /// </summary>
        /// <param name="maxValue">The maximum value the returned integer can have.</param>
        /// <returns>The randomly selected, returned integer.</returns>
        public static int Next(int maxValue)
        {
            return LocalGeneralUtils.SYS_RANDOM.Next(maxValue);
        }

        /// <summary>Given an interval [min, max], return a random float from this range.</summary>
        /// <param name="min">The minimum inclusive bound on the float to return.</param>
        /// <param name="max">The maximum inclusive bound on the float to return.</param>
        /// <returns>a random float from this range.</returns>
        public static float NextFloat(float min, float max)
        {
            double result = LocalGeneralUtils.SYS_RANDOM.NextDouble() * (max - (double) min) + min;
            return (float) result;
        }

        /// <summary>
        /// Generate a list of <paramref name="numberOfFloats"/> between the values <paramref name="minValue"/> and

        /// <paramref name="maxValue"/>.
        /// </summary>
        /// <param name="numberOfFloats">The number of floats to generate.</param>
        /// <param name="minValue">The minimum bound of the generated floats.</param>
        /// <param name="maxValue">The maximum bound of the generated floats.</param>
        /// <returns>A list of <paramref name="numberOfFloats"/> between the values <paramref name="minValue"/> and

        /// <paramref name="maxValue"/>.</returns>
        public static List<float> GenerateNFloatsBetween(int numberOfFloats, float minValue, float maxValue)
        {
            List<float> floatList = new List<float>();
            for (int i = 0; i < numberOfFloats; i++)
            {
                floatList.Add(NextFloat(minValue, maxValue));
            }
            return floatList;
        }

/// <summary>
/// Return a random integer between the lower and upper bounds provided.
/// </summary>
/// <param name="lowerBound">The lower bound of the integer to return.</param>
/// <param name="upperBound">The upper bound of the integer to return.</param>
/// <returns>a random integer between the lower and upper bounds provided.</returns>
        public static int GetIntBetweenExclusive(int lowerBound, int upperBound)
        {
            return LocalGeneralUtils.SYS_RANDOM.Next(lowerBound, upperBound + 1);
        }

    }

}