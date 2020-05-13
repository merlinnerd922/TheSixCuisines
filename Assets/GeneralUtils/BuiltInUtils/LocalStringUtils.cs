﻿#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Extend;

#endregion

namespace Helper
{

    /// <summary>A helper class containing methods for handling Strings.</summary>
    public static class LocalStringUtils
    {

        /// <summary>A textinfo thread containing information on English spelling and grammatical conventions.</summary>
        public static readonly TextInfo TI = Thread.CurrentThread.CurrentCulture.TextInfo;

        /// <summary>
        ///     Return the formatted String <paramref name="formatString" /> with the array <paramref name="parameters" /> to
        ///     substitute into the format String as parameters.
        /// </summary>
        /// <param name="formatString">The format String in which parameters will be substituted in.</param>
        /// <param name="parameters">The parameters to substitute into the format String to be returned.</param>
        /// <returns>
        ///     Return the formatted String <paramref name="formatString" /> with the array <paramref name="parameters" /> to
        ///     substitute into the format String as parameters.
        /// </returns>
        public static string Format(this string formatString, params object[] parameters)
        {
            return string.Format(formatString, parameters);
        }

        /// <summary>
        ///     Given the String enumeration <paramref name="lines" />, return a String consisting of all the lines in
        ///     <paramref name="lines" /> concatenated together, separated by the String <paramref name="separator" />.
        /// </summary>
        /// <param name="lines">The enumeration of lines to convert into a String.</param>
        /// <param name="separator">The String separator separating every line in the output String.</param>
        /// <returns>
        ///     A String consisting of each line in <paramref name="lines" /> concatenated together, delimited by
        ///     <paramref name="separator" />
        /// </returns>
        public static string Join(this IEnumerable<string> lines, string separator)
        {
            return string.Join(separator, lines);
        }

        /// <summary>
        ///     Given a string and some objects as parameters, format the string with those parameters. This method would
        ///     otherwise be called Format, but there is already a static method called string.format. So, we append this method
        ///     with the substring "Extend".
        /// </summary>
        /// <param name="formatString">The String that we're formatting with arguments.</param>
        /// <param name="args">The list of arguments.</param>
        /// <returns>The <see cref="string" />.</returns>
        /// <exception cref="ArgumentNullException">
        ///     Throw this Exception if <paramref name="formatString" /> or
        ///     <paramref name="args" /> is null.
        /// </exception>
        /// <exception cref="FormatException">
        ///     Throw this Exception if <paramref name="formatString" /> is invalid, or the index of a format item is less than
        ///     zero, or greater than or equal to the length of the
        ///     <paramref
        ///         name="args" />
        ///     array.
        /// </exception>
        public static string FormatExtend(this string formatString, params object[] args)
        {
            return string.Format(formatString, args);
        }

        /// <summary>Given a character <paramref name="digitChar" />, return true iff it's a representation of a number.</summary>
        /// <param name="digitChar">The character we're asserting is a representation of a digit.</param>
        /// <returns>true iff <paramref name="digitChar" /> is a representation of a number.</returns>
        public static bool IsDigitExtend(this char digitChar)
        {
            return char.IsDigit(digitChar);
        }

        /// <summary>
        ///     Given a string <paramref name="stringToMatch" /> and a regex pattern
        ///     <paramref
        ///         name="pattern" />
        ///     , return true iff the string matches the regex pattern IN ENTIRETY.
        /// </summary>
        /// <param name="stringToMatch">The string to match with the pattern <paramref name="pattern" />.</param>
        /// <param name="pattern">The pattern to use to match with <paramref name="stringToMatch" />.</param>
        /// <returns>true iff the string matches the regex pattern IN ENTIRETY.</returns>
        /// <exception cref="ArgumentException">A regular expression parsing error occurred.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="stringToMatch" /> or <paramref name="pattern" /> is null.</exception>
        public static bool MatchesEntireRegex(this string stringToMatch, string pattern)
        {
            return Regex.IsMatch(stringToMatch, pattern);
        }

        /// <summary>Given a string, remove all whitespace from it and return the subsequently returned string.</summary>
        /// <param name="stringToRemoveWhitespaceFrom">The string to strip whitespace from.</param>
        /// <exception cref="ArgumentNullException"><paramref name="`source" /> is null.</exception>
        /// <returns>The <see cref="string" />.</returns>
        public static string RemoveWhitespace(this string stringToRemoveWhitespaceFrom)
        {
            return new string(stringToRemoveWhitespaceFrom.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray());
        }

        /// <summary>
        ///     Given a string <paramref name="stringToSplit" />, split the string along all newlines and return the
        ///     subsequent string.
        /// </summary>
        /// <param name="stringToSplit">The string to split alone newlines.</param>
        /// <returns>The newly split string.</returns>
        public static string[] SplitOnNewline(this string stringToSplit)
        {
            return stringToSplit.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        ///     Given a string <paramref name="thisString" /> and a regex string
        ///     <paramref
        ///         name="regexStr" />
        ///     , return true iff <paramref name="regexStr" /> matches the beginning of <paramref name="thisString" />.
        /// </summary>
        /// <param name="thisString">The string we're checking to see starts with the given <paramref name="regexStr" />.</param>
        /// <param name="regexStr">The regex string.</param>
        /// <returns>true iff <paramref name="regexStr" /> matches the beginning of <paramref name="thisString" />.</returns>
        public static bool StartsWithRegex(this string thisString, string regexStr)
        {
            return Regex.IsMatch(thisString, "^" + regexStr);
        }

        /// <summary>
        ///     Given a string <paramref name="stringToMatch" /> and a pattern
        ///     <paramref
        ///         name="patternToMatch" />
        ///     , match <paramref name="stringToMatch" /> with
        ///     <paramref
        ///         name="patternToMatch" />
        ///     at the beginning of the string, and return the matched string.
        /// </summary>
        /// <param name="stringToMatch">The string to match with.</param>
        /// <param name="patternToMatch">The pattern to use to match the given string.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string TakeRegexPattern(this string stringToMatch, string patternToMatch)
        {
            return Regex.Match(stringToMatch, patternToMatch).Value;
        }

        /// <summary>
        ///     Given a string (stringToTrim) that is written in all caps, with different words separated by single
        ///     underscores OR spaces, convert the string to camel case and return that string.
        /// </summary>
        /// <param name="thisString">The string we're converting to camel case.</param>
        /// <returns>The string to convert to CamelCase.</returns>
        public static string ToCamelCase(this string thisString)
        {
            // Split the string along the underscore....
            return LocalCollectionUtils.Join(thisString.Split('_', ' ', '\t', '\r')

                // ...retain nonempty strings only...
                .Where(strWord => strWord != string.Empty)

                // ...Convert each individual word to title case...
                .Select(strWord => strWord.ToTitleCase()), string.Empty);
        }

        /// <summary>
        ///     Given a string <paramref name="singularStr" /> that presumably describes a singular object, return the plural
        ///     form of the string.
        /// </summary>
        /// <param name="singularStr">The singular string to create a plural of.</param>
        /// <returns>The <see cref="string" /> to pluralize.</returns>
        public static string ToPlural(this string singularStr)
        {
            return singularStr + "s";
        }

        /// <summary>
        ///     Given a string, return a representation of it in title case (uppercase first letter, lowercase for all other
        ///     letters).
        /// </summary>
        /// <param name="stringToConvert">The string to convert into title case.</param>
        /// <returns>The converted string.</returns>
        public static string ToTitleCase(this string stringToConvert)
        {
            return TI.ToTitleCase(stringToConvert.ToLower());
        }

        /// <summary>
        ///     Given a string <paramref name="stringToTrim" /> and another string
        ///     <paramref
        ///         name="subStr" />
        ///     that <paramref name="stringToTrim" /> potentially ends with, remove <paramref name="subStr" /> from the end of
        ///     <paramref name="stringToTrim" /> if
        ///     <paramref
        ///         name="stringToTrim" />
        ///     ends with <paramref name="stringToTrim" />.
        /// </summary>
        /// <param name="stringToTrim">The string to trim the end off of.</param>
        /// <param name="subStr">The substring to trim off the end of <paramref name="stringToTrim" />.</param>
        /// <returns>The trimmed <see cref="string" /> version of <paramref name="stringToTrim" />.</returns>
        public static string RemoveEnd(this string stringToTrim, string subStr)
        {
            // Remove the suffix off the end of the string if necessary, before returning it.
            if (stringToTrim.EndsWith(subStr)) stringToTrim = stringToTrim.Remove(stringToTrim.Length - subStr.Length);
            return stringToTrim;
        }

        /// <summary>
        ///     Given a string <paramref name="stringToTrim" /> and a possible substring
        ///     <paramref
        ///         name="substring" />
        ///     that occurs at the beginning of formatString, remove
        ///     <paramref
        ///         name="substring" />
        ///     from the beginning and return it. If this substring does NOT occur, simply return <paramref name="stringToTrim" />.
        /// </summary>
        /// <param name="stringToTrim">The s.</param>
        /// <param name="substring">The substring to trim off the start.</param>
        /// <returns>The <see cref="string" /> from which we should trim off.</returns>
        public static string RemoveStart(this string stringToTrim, string substring)
        {
            if (!stringToTrim.StartsWith(substring)) return stringToTrim;

            return new string(stringToTrim.Skip(substring.Length).ToArray());
        }

        /// <summary>Return true if the char <paramref name="c" /> represents a hex digit, and false otherwise.</summary>
        /// <param name="c">The c.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsHex(this char c)
        {
            return c >= '0' && c <= '9' || c >= 'a' && c <= 'f' || c >= 'A' && c <= 'F';
        }

        /// <summary>Return true iff the string <paramref name="potentialHexString" /> represents a hex string.</summary>
        /// <param name="potentialHexString">The string we're checking to be a hex string.</param>
        /// <returns>true iff the string <paramref name="potentialHexString" /> represents a hex string.</returns>
        /// <exception cref="ArgumentException">A regular expression parsing error occurred.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="potentialHexString" />.</exception>
        public static bool IsHex(this string potentialHexString)
        {
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            return Regex.IsMatch(potentialHexString, @"\A\b[0-9a-fA-F]+\b\Z");
        }

        /// <summary>
        ///     Given an object <paramref name="obj" />, convert that object to its String representation in title case and
        ///     return it.
        /// </summary>
        /// <param name="obj">The object to render as a title case String.</param>
        /// <returns>The title case string representation of <paramref name="obj" />.</returns>
        public static string ToTitleCaseString(this object obj)
        {
            return obj.ToString().ToTitleCase();
        }

        /// <summary>
        /// Return a camel case String representation of the provided <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">The object to return as a String.</param>
        /// <returns>A camel case String representation of the provided <paramref name="obj"/>.</returns>
        public static string ToCamelCaseString(this object obj)
        {
            return obj.ToString().ToCamelCase().Replace("_", "");
        }

        /// <summary>
        ///     Given an object <paramref name="obj" />, convert that object to its String representation in title case
        /// while replacing all underscores with spaces (to make the text appear more like natural language) and
        ///     return it.
        /// </summary>
        /// <param name="obj">The object to render as a spaced title case String.</param>
        /// <returns>The spaced title case string representation of <paramref name="obj" />.</returns>
        public static string ToTitleCaseSpacedString(this object obj)
        {
            return obj.ToTitleCaseString().Replace("_", " ");
        }

    }

}