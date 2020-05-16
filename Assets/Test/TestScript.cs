using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Extend;
using Helper;
using UnityEngine;

/// <summary>
/// A test class used for running simple scripts, equivalent to console applications for more traditional C#
/// projects.
/// </summary>
public class TestScript : MonoBehaviour
{
    /// <summary>
    /// Start this script. This is the entry point of the script.
    /// </summary>
    private void Start()
    {
        // Generate a number of floats that sum to a certain number.
        GenerateLogFloatsWhoSumToM();
    }

    /// <summary>
    /// Generate and print a number of floats that sum to the number 1.
    /// </summary>
    private static void GenerateLogFloatsWhoSumToM()
    {
        IEnumerable<float> generateNFloatsWhoSumToM = NumberRandomizer.GenerateNFloatsWhoSumToM(5, 1f);
        string convertToString = generateNFloatsWhoSumToM.AsString();
        Debug.Log(convertToString);
        Debug.Log(generateNFloatsWhoSumToM.Sum());
    }

}
