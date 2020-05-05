﻿#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

#endregion

namespace Helper
{

    public static class LocalColorUtils
    {

        /// <summary>
        ///     Given a color, flash this color back and forth between colours <paramref name="colour1" /> and
        ///     <paramref name="colour2" /> with speed <paramref name="speed" />.
        /// </summary>
        /// <param name="mainColor">The reference to the colour object to flash a different colour.</param>
        /// <param name="colour1">The first colour to flash between.</param>
        /// <param name="colour2">The second colour to flash between.</param>
        /// <param name="speed">The speed.</param>
        /// <param name="addShift">The add Shift.</param>
        public static void Flash(ref Color mainColor, Color colour1, Color colour2, float speed, float addShift = 0f)
        {
            mainColor = LinearInterpolate(colour1, colour2, 0.5f * Mathf.Cos(Time.time * speed + addShift) + 0.5f);
        }

        /// <summary>
        ///     Return a string that represents the hexadecimal value of a color that is a linear interpolation of colors
        ///     colour1 and colour2 based on how much time has passed.
        /// </summary>
        /// <param name="c1">The first colour to interpolate between.</param>
        /// <param name="c2">The second colour to interpolate between.</param>
        /// <param name="speed">The speed at which to interpolate the colours.</param>
        /// <param name="addShift">The amount of shift to add to the interpolation.</param>
        /// <returns>The hexadecimal value of the colour after the speed and shift.</returns>
        public static string FlashColorToString(Color c1, Color c2, float speed, float addShift = 0f)
        {
            return LinearInterpolate(c1, c2, 0.5f * Mathf.Cos(Time.time * speed + addShift) + 0.5f).ToHexString();
        }

        /// <summary>
        ///     Given 4 ints representing the alpha, red, green and blue values of a colour, return the associated Unity
        ///     Colour object.
        /// </summary>
        /// <param name="alpha">The alpha (amount of transparency).</param>
        /// <param name="red">The red component of the colour.</param>
        /// <param name="green">The green component of the colour.</param>
        /// <param name="blue">The blue component of the colour.</param>
        /// <returns>The <see cref="Color" /> representation of the given values.</returns>
        public static Color FromArgb(int alpha, int red, int green, int blue)
        {
            return new Color(red / 255.0f, green / 255.0f, blue / 255.0f, alpha / 255.0f);
        }

        /// <summary>Return the colour represented by the color (r, g, b).</summary>
        /// <param name="r">The red component of the colour, between values [0, 255].</param>
        /// <param name="g">The green component of the colour, between values [0, 255].</param>
        /// <param name="b">The blue component of the colour, between values [0, 255].</param>
        /// <returns>The <see cref="Color" /> represented by (r, g, b).</returns>
        public static Color GetColorFromRgb(int r, int g, int b)
        {
            return new Color(r / 255f, g / 255f, b / 255f);
        }

        /// <summary>Given a hexadecimal representation of a color <paramref name="hexString" />, convert it to an actual colour.</summary>
        /// <param name="hexString">The hexadecimal representation of a colour.</param>
        /// <returns>The <see cref="Color" /> representation of the given <paramref name="hexString" />.</returns>
        public static Color HexToColor(string hexString)
        {
            // Deal with the case that the string starts with a "#", as sometimes it does.
            if (hexString[0] == '#') hexString = hexString.Substring(1);

            // Process the individual components of the hexadecimal string, and return the associated colour.
            byte r = Byte.Parse(hexString.Substring(0, 2), NumberStyles.HexNumber); // R: first two digits
            byte g = Byte.Parse(hexString.Substring(2, 2), NumberStyles.HexNumber); // G: third, fourth digits
            byte b = Byte.Parse(hexString.Substring(4, 2), NumberStyles.HexNumber); // B: last two digits.
            return new Color32(r, g, b, 255);
        }

        /// <summary>
        ///     Given two colours <paramref name="color1" /> and <paramref name="color2" /> and a float between 0 and 1,
        ///     return a linear interpolation of the two colours, using <paramref name="linearInterpolationVal" /> to determine how
        ///     heavily to blend each colour.
        /// </summary>
        /// <param name="color1">The first colour to interpolate between.</param>
        /// <param name="color2">The second colour to interpolate between.</param>
        /// <param name="linearInterpolationVal">The amount of elapsed time, which is used to determine what colour to create.</param>
        /// <returns>
        ///     The <see cref="Color" /> linearly interpolated between <paramref name="color1" /> and
        ///     <paramref name="color2" />.
        /// </returns>
        internal static Color LinearInterpolate(Color color1, Color color2, float linearInterpolationVal)
        {
            Color newColor = new Color
            {
                r = linearInterpolationVal * color2.r + (1 - linearInterpolationVal) * color1.r,
                g = linearInterpolationVal * color2.g + (1 - linearInterpolationVal) * color1.g,
                b = linearInterpolationVal * color2.b + (1 - linearInterpolationVal) * color1.b,
                a = 1f
            };
            return newColor;
        }

        /// <summary>Return a lightened version of the given color (that has been lightened by amount (inAmount)).</summary>
        /// <param name="inColor">The colour whose brightness we need to add to.</param>
        /// <param name="inAmount">The amount of brightness to add to the colour.</param>
        /// <returns>The brightened colour.</returns>
        public static Color AddToBrightness(this Color inColor, float inAmount)
        {
            HSBColor hsbColor = inColor.ToHSBColor();
            hsbColor.b = Math.Min(1f, hsbColor.b + inAmount);
            return hsbColor.ToColor();
        }

        /// <summary>Given a colour <paramref name="colourToClone" />, return a copy of that colour.</summary>
        /// <param name="colourToClone">The colour to clone.</param>
        /// <returns>The cloned colour.</returns>
        public static Color Clone(this Color colourToClone)
        {
            return new Color(colourToClone.r, colourToClone.g, colourToClone.b, colourToClone.a);
        }

        /// <summary>Return a darkened version of the given color (that has been darkened by amount (inAmount)).</summary>
        /// <param name="inColor">The color to darken.</param>
        /// <param name="inAmount">The amount to darken the colour by.</param>
        /// <returns>The darkened colour.</returns>
        public static Color Darken(this Color inColor, float inAmount)
        {
            return new Color((int) Math.Max(0, Math.Floor(inColor.r * (1f - inAmount))),
                (int) Math.Max(0, Math.Floor(inColor.g * (1f - inAmount))),
                (int) Math.Max(0, Math.Floor(inColor.b * (1f - inAmount))));
        }

        /// <summary>
        ///     Given a colour <paramref name="colourToModify" /> and a new alpha value
        ///     <paramref
        ///         name="alphaValue" />
        ///     , return a version of <paramref name="colourToModify" /> with an alpha with that value.
        /// </summary>
        /// <param name="colourToModify">The colour whose modified alpha value we should return.</param>
        /// <param name="alphaValue">The new value to set colourToClone's alpha value to.</param>
        /// <returns>The colour <paramref name="colourToModify" />, but with a modified alpha value <paramref name="alphaValue" />.</returns>
        public static Color GetWithModifiedAlpha(this Color colourToModify, float alphaValue)
        {
            return new Color(colourToModify.r, colourToModify.g, colourToModify.b, alphaValue);
        }

        /// <summary>Given a color, modify it so the alpha is modified to a new value, and return the newly modified colour.</summary>
        /// <param name="c">The colour whose alpha value we should modify.</param>
        /// <param name="newAlpha">The new value of the alpha to modify to.</param>
        /// <returns>The <see cref="Color" /><paramref name="c" /> with its alpha value modified.</returns>
        public static Color ModifyReturnAlpha(this Color c, float newAlpha)
        {
            c = new Color(c.r, c.g, c.b, newAlpha);
            return c;
        }

        /// <summary>Return a lightened version of the given colour whose brightness has been set to (inAmount).</summary>
        /// <param name="inColor">The color whose brightness we should set.</param>
        /// <param name="inAmount">The amount we should set the brightness to.</param>
        /// <returns>The <see cref="Color" /> whose brightness we should set.</returns>
        public static Color SetBrightness(this Color inColor, float inAmount)
        {
            HSBColor hsbColor = inColor.ToHSBColor();
            hsbColor.b = inAmount;
            return hsbColor.ToColor();
        }

        /// <summary>Given an array of exactly four floating-point numbers, use them to initialize a new color and return it.</summary>
        /// <param name="colorSequence">The sequence of floats we want to represent as a Color.</param>
        /// <returns>The <see cref="Color" /> that is represented by the given array of floats.</returns>
        public static Color ToColor(this IEnumerable<float> colorSequence)
        {
            float[] colorAsArray = colorSequence.ToArray();

            // Raise an issue if the given array does NOT have a count of 4, or is not initialized.
            if (colorAsArray == null || colorAsArray.Length != 4)
                throw new ArgumentException("The given array of floats has the improper format and must" +
                                            " consist of 4 floats! ");

            // Raise an exception if any of the floats are NOT in the range [0, 1].
            if (colorAsArray.Any(x => !x.InInclusiveRange(0f, 1f)))
                throw new IndexOutOfRangeException("The values of the floats for this colour should be in the range" +
                                                   " [0, 1]!");

            return new Color(colorAsArray[0], colorAsArray[1], colorAsArray[2], colorAsArray[3]);
        }

        /// <summary>Given a colour <paramref name="colour" />, convert it to a hexadecimal string.</summary>
        /// <param name="colour">The colour to convert to a hex string.</param>
        /// <returns>The hex representation of the colour <paramref name="colour" />.</returns>
        public static string ToHexString(this Color colour)
        {
            return "#" + ((int) (colour.r * 255f)).ToHexString().PadLeft(2, '0') +
                   ((int) (colour.g * 255f)).ToHexString().PadLeft(2, '0') +
                   ((int) (colour.b * 255f)).ToHexString().PadLeft(2, '0');
        }

        /// <summary>
        ///     Given a colour <paramref name="colour" />, convert it to a colour represented in terms of hue, saturation and
        ///     brightness.
        /// </summary>
        /// <param name="colour">The colour we're converting to an HSB colour.</param>
        /// <returns>The HSB representation of colour.</returns>
        public static HSBColor ToHSBColor(this Color colour)
        {
            return HSBColor.FromColor(colour);
        }

        /// <summary>
        /// The color royal blue.
        /// </summary>
        public static Color ROYAL_BLUE = new Color(0.254902F, 0.4117647F, 0.8823529F, 1f);

        /// <summary>
        /// The color orange.
        /// </summary>
        public static Color ORANGE = new Color(1F, 127 / 255.0f, 0 / 255.0f, 255 / 255.0f);

    }

}