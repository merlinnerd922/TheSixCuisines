#region

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Extend;
using UnityEngine;
using Random = System.Random;

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
        /// Generate and return a list of <paramref name="numberOfIntsToGenerate"/> of floats that add up to <paramref name="total"/>.
        /// </summary>
        /// <param name="numberOfIntsToGenerate">The number of integers to generate.</param>
        /// <param name="total">The total that the generated floats should add up to.</param>
        /// <returns>a list of <paramref name="numberOfIntsToGenerate"/> of floats that add up to <paramref name="total"/>.</returns>
        public static IEnumerable<float> GenerateNFloatsWhoSumToM(int numberOfIntsToGenerate, float total)
        {
            // Generate a list of random floats between 1 and 100.
            List<float> generateNFloatsWhoSumToM = new List<float>();
            for (int i = 0; i < numberOfIntsToGenerate; i++)
            {
                generateNFloatsWhoSumToM.Add(LocalGeneralUtils.SYS_RANDOM.NextFloat(1, 100));
            }

            // Then, take the sum of all of those floats, so we can divide each of those floats by the sum and then
            // multiply by the target total to reach our goal.
            float totalSum = generateNFloatsWhoSumToM.Sum();

            // Divide each float by the total sum of numbers, and then multiply by the target total; this will result
            // in all of the numbers equalling the total.
            for (int i = 0; i < numberOfIntsToGenerate; i++)
            {
                generateNFloatsWhoSumToM[i] /= totalSum;
                generateNFloatsWhoSumToM[i] *= total;
            }

            return generateNFloatsWhoSumToM;
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


        /// <summary>
        /// Return a random key from the provided Dictionary, where the values corresponding to the keys are the weights corresponding
        /// to how likely that item is to be chosen.
        /// </summary>
        /// <param name="mapping">The mapping containing a set of items and their weights.</param>
        /// <typeparam name="T">The type of the key in the Dictionary.</typeparam>
        /// <returns>A random key from the provided Dictionary, where the values corresponding to the keys are the weights corresponding
        /// to how likely that item is to be chosen.</returns>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static T GetRandomItemWeighted<T>(Dictionary<T, float> mapping)
        {
            // Return the default value of T iff the provided mapping is empty.
            if (mapping.Count == 0)
            {
                return default(T);
            }

            float totalWeight = mapping.Sum(c => c.Value);
            float choice = LocalGeneralUtils.SYS_RANDOM.NextFloat(0, totalWeight);
            float sum = 0;

            foreach (KeyValuePair<T, float> obj in mapping)
            {
                for (float i = sum; i < obj.Value + sum; i++)
                {
                    if (i >= choice)
                    {
                        return obj.Key;
                    }
                }

                sum += obj.Value;
            }

            return mapping.First().Key;
        }

    }

}