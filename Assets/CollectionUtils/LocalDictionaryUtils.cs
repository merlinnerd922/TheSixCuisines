﻿#region

using System;
using System.Collections.Generic;
using System.Linq;
using Extend;

#endregion

namespace Helper
{

    /// <summary>A helper class containing helper methods for manipulating Dictionaries.</summary>
    public static class LocalDictionaryUtils
    {

        /// <summary>Return a prettified String representation of the extended Dictionary <paramref name="dict" />.</summary>
        /// <param name="dict">The Dictionary to convert into a String.</param>
        /// <typeparam name="TKey">The type of the keys in the Dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the Dictionary.</typeparam>
        /// <returns>A prettified String representation of the extended Dictionary <paramref name="dict" />.</returns>
        public static string ToPrettyString<TKey, TValue>(this IDictionary<TKey, TValue> dict)
        {
            IEnumerable<string> lines = dict.Select(keyValuePair => $"{keyValuePair.Key}: {keyValuePair.Value}");
            return lines.Join(Environment.NewLine);
        }

        /// <summary>
        ///     Given a dictionary <paramref name="dict" /> , a key <paramref name="key" /> and a value <paramref name="value" /> ,
        ///     set the entry for <paramref name="dict" /> under
        ///     <paramref
        ///         name="key" />
        ///     to <paramref name="value" /> , but ONLY if <paramref name="key" /> is not already one of <paramref name="dict" />
        ///     's keys.
        /// </summary>
        /// <typeparam name="T1">The type of the key.</typeparam>
        /// <typeparam name="T2">The type of the value.</typeparam>
        /// <param name="dict">
        ///     The dictionary we're checking to see has the key/pair value <paramref name="key" />,
        ///     <paramref name="value" />.
        /// </param>
        /// <param name="key">The key of the item we want to add.</param>
        /// <param name="value">The value of the item we want to add.</param>
        public static void AddIfNull<T1, T2>(this Dictionary<T1, T2> dict, T1 key, T2 value)
        {
            if (!dict.ContainsKey(key) || dict[key] == null) dict[key] = value;
        }

        /// <summary>
        ///     Given a dictionary (dict) of type Dictionary(AxisDirection, List(T2)), and items of type T2 (keyObject) and
        ///     AxisDirection (valueAddItem), set the entry (dict)[(keyObject)] to a new empty list if (dict) does not already have
        ///     the entry (keyObject). Then, add the item (valueAddItem) to the list (dict)[(keyObject)].
        /// </summary>
        /// <typeparam name="T1">The type of the Dictionary's key.</typeparam>
        /// <typeparam name="T2">The type of the item in the value's list.</typeparam>
        /// <param name="collection">The dictionary whose entry at (keyToAdd) we're changing.</param>
        /// <param name="keyToAdd">The key to add to the Dictionary.</param>
        /// <param name="itemToAddToList">The item to add to the list of type T2.</param>
        public static void AddInitIfNull<T1, T2>(this Dictionary<T1, List<T2>> collection, T1 keyToAdd,
            T2 itemToAddToList)
        {
            // Initialize the list at the dictionary entry if it doesn't exist; then, add the item.
            if (!collection.ContainsKey(keyToAdd)) collection[keyToAdd] = new List<T2>();

            collection[keyToAdd].Add(itemToAddToList);
        }

        /// <summary>
        ///     Given a dictionary of objects of types AxisDirection and T2, return a cloned copy of the dictionary as a
        ///     dictionary.
        /// </summary>
        /// <typeparam name="T1">The type of the Dictionary's key.</typeparam>
        /// <typeparam name="T2">The type of the Dictionary's value.</typeparam>
        /// <param name="dictionaryToClone">The dictionary object we want to clone.</param>
        /// <returns>A cloned copy of the dictionary <paramref name="dictionaryToClone" />.</returns>
        public static Dictionary<T1, T2> Clone<T1, T2>(this Dictionary<T1, T2> dictionaryToClone)
        {
            return dictionaryToClone.ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>Return true iff the given dictionary (dict) contains the key (key), and the value of that key is non-null.</summary>
        /// <typeparam name="T1">The type of the key of the dictionary (dict).</typeparam>
        /// <typeparam name="T2">The value of the key of the dictionary (dict).</typeparam>
        /// <param name="dict">The dictionary we're checking for a corresponding non-null value.</param>
        /// <param name="key">The key we're using to check the dictionary.</param>
        /// <returns>true iff the given dictionary (dict) contains the key (key), and the value of that key is non-null.</returns>
        public static bool ContainsKeyNonNullValue<T1, T2>(this Dictionary<T1, T2> dict, T1 key)
        {
            return dict.ContainsKey(key) && dict[key] != null;
        }

        /// <summary>
        ///     Return true iff the given dictionary (dict) contains the key (key), and the value of that key is non-null and
        ///     an object of type (objectType).
        /// </summary>
        /// <typeparam name="T1">The type of the key of the dictionary (dict).</typeparam>
        /// <typeparam name="T2">The value of the key of the dictionary (dict).</typeparam>
        /// <param name="dict">The dictionary we're checking for a corresponding non-null value.</param>
        /// <param name="key">The key we're using to check the dictionary.</param>
        /// <param name="objectType">The type of the object we're looking for/</param>
        /// <returns>
        ///     true iff the given dictionary (dict) contains the key (key), and the value of that key is non-null and an
        ///     object of type (objectType).
        /// </returns>
        public static bool ContainsKeyNonNullValueOfType<T1, T2>(this Dictionary<T1, T2> dict, T1 key, Type objectType)
        {
            return dict.ContainsKeyNonNullValue(key) && dict[key].GetType().IsSubClassOrEquals(objectType);
        }

        /// <summary>
        ///     Return true if the given dictionaries, <paramref name="dic1" /> and
        ///     <paramref
        ///         name="dic2" />
        ///     , have identical key-pair entries.
        /// </summary>
        /// <typeparam name="T1">The type of the key in the dictionary.</typeparam>
        /// <typeparam name="T2">The type of the value in the dictionary.</typeparam>
        /// <param name="dic1">The first dictionary to compare.</param>
        /// <param name="dic2">The second dictionary to compare.</param>
        /// <returns>true iff the items in both dictionaries are identical.</returns>
        public static bool DictionaryEquals<T1, T2>(this Dictionary<T1, T2> dic1, Dictionary<T1, T2> dic2)
        {
            // Return true if either (1) both dictionaries point to the same object, OR
            return dic1 == dic2 ||

                   // (2) the dictionaries have the same number of elements, and the difference of
                   // dic1 - dic2 is empty.
                   dic1.Count == dic2.Count && !dic1.Except(dic2).Any();
        }

        /// <summary>
        ///     Given a dictionary <paramref name="dict">dict</paramref>, return another dictionary that maps the unique
        ///     values (val) of <paramref name="dict" /> to a list of all keys in <paramref name="dict" /> that have (val) as their
        ///     value. // RUNTIME: O(n) where n is the number of entries in <paramref name="dict" />.
        /// </summary>
        /// <typeparam name="T2">The type of the value in the given <paramref name="dict" />.</typeparam>
        /// <typeparam name="T1">The type of the key in the given <paramref name="dict" />.</typeparam>
        /// <param name="dict">The dictionary to get a key/value mapping from.</param>
        /// <returns>
        ///     A
        ///     <see>
        ///         <cref>Dictionary</cref>
        ///     </see>
        ///     that maps all of <paramref name="dict" />'s values to all keys that share the same dictionary value.
        /// </returns>
        public static Dictionary<T2, HashSet<T1>> GetValueKeyMapping<T2, T1>(this Dictionary<T1, T2> dict)
        {
            // Initialize return dictionary.
            Dictionary<T2, HashSet<T1>> returnDict = new Dictionary<T2, HashSet<T1>>();

            // Iterate through all the values
            foreach (KeyValuePair<T1, T2> kvPair in dict)
            {
                // Add the value as a key to the return dictionary.
                if (!returnDict.ContainsKey(kvPair.Value) || returnDict[kvPair.Value] == null)
                    returnDict[kvPair.Value] = new HashSet<T1>();

                // Then, add the key to the set.
                returnDict[kvPair.Value].Add(kvPair.Key);
            }

            // Finally, return the dictionary.
            return returnDict;
        }

        /// <summary>
        ///     Return true iff the dictionary (dict) not only has the key (key), but its corresponding value is a non-null
        ///     object of type (valueType).
        /// </summary>
        /// <typeparam name="T1">The key's type.</typeparam>
        /// <typeparam name="T2">The value's type.</typeparam>
        /// <param name="dict">The dictionary we're checking for a key.</param>
        /// <param name="key">The key we're using to search the dictionary.</param>
        /// <param name="valueType">The type of the value we're searching for.</param>
        /// <returns>
        ///     true iff the dictionary (dict) not only has the key (key), but its corresponding value is a non-null object of
        ///     type (valueType)
        /// </returns>
        public static bool HasKeyValueType<T1, T2>(this Dictionary<T1, T2> dict, T1 key, Type valueType)
        {
            // Throw an Exception if the provided key is null.
            if (key == null)
                throw new ArgumentException("We should not be accessing this dictionary with a null key!!");

            return dict.ContainsKey(key) && dict[key] != null && dict[key].GetType().IsSubClassOrEquals(valueType);
        }

        /// <summary>
        ///     Given a dictionary <paramref name="dict" /> of the type (AxisDirection, List(T2)) and an item (key) of type
        ///     <typeparamref name="T1" /> and an item (value) of type
        ///     <typeparamref
        ///         name="T2" />
        ///     , do the following. Remove the entry (value) from the list indexed by key (key) in the dictionary
        ///     <paramref name="dict" />, if it exists.
        /// </summary>
        /// <typeparam name="T1">The type of the key of <paramref name="dict" />.</typeparam>
        /// <typeparam name="T2">The type of the item in the value of <paramref name="dict" />.</typeparam>
        /// <param name="dict">
        ///     The dictionary from which we are removing an array element corresponding to the key
        ///     <paramref name="keyToIndex" />.
        /// </param>
        /// <param name="keyToIndex">T</param>
        /// <param name="itemToAddToList">The item To Add To List.</param>
        public static void RemoveFromListValue<T1, T2>(this Dictionary<T1, List<T2>> dict, T1 keyToIndex,
            T2 itemToAddToList)
        {
            // The dictionary and the associated key entry should be non-null.
            if (dict == null)
                throw new NullReferenceException("The given dictionary should have been initialized already!");

            // The list located at the given index should not be null.
            if (!keyToIndex.IsAny(dict.Keys)) return;

            // Otherwise, remove the item as needed.
            dict[keyToIndex].Remove(itemToAddToList);
        }

        /// <summary>Given a dictionary, return a nice string representation of it.</summary>
        /// <typeparam name="T1">The type of the keys of the Dictionary.</typeparam>
        /// <typeparam name="T2">The type of the values of the Dictionary.</typeparam>
        /// <param name="dictionaryObject">The dictionary to convert into a neat string.</param>
        /// <returns>The <see cref="string" /> representation of the dictionary <paramref name="dictionaryObject" />.</returns>
        public static string ToNeatString<T1, T2>(this Dictionary<T1, T2> dictionaryObject)
        {
            // Initialize the left bracket.
            string returnValueString = "[";

            // Add each key-value pair to the dictionary.
            foreach (T1 key in dictionaryObject.Keys)
                returnValueString += "({0}, {1})".FormatExtend(key, dictionaryObject[key]);

            // Add the right bracket.
            return returnValueString + "]";
        }

        /// <summary>
        ///     Given a dictionary <paramref name="sourceDict" /> mapping instances of class
        ///     <typeparamref
        ///         name="T1" />
        ///     to <typeparamref name="T2" />, return an inverse mapping of the dictionary (inverseDict) from
        ///     <typeparamref name="T2" /> to HashSets of <typeparamref name="T1" />, so that the values of
        ///     <paramref name="sourceDict" /> are the keys of (inverseDict), and the values of (inverseDict) are sets containing
        ///     keys from <paramref name="sourceDict" /> that have the corresponding key as a value.
        /// </summary>
        /// <typeparam name="T1">The type of the key in the source dictionary.</typeparam>
        /// <typeparam name="T2">The type of the value in the source dictionary.</typeparam>
        /// <param name="sourceDict">The source dictionary to convert into a many-1 dictionary.</param>
        /// <returns>
        ///     The
        ///     <see>
        ///         <cref>Dictionary</cref>
        ///     </see>
        ///     we're going to be converting to an inverse dictionary.
        /// </returns>
        public static Dictionary<T2, HashSet<T1>> ToOneManyInverseDictionary<T1, T2>(this Dictionary<T1, T2> sourceDict)
        {
            // Initialize the dictionary.
            Dictionary<T2, HashSet<T1>> returnDict = new Dictionary<T2, HashSet<T1>>();

            // Iterate over all of the source dictionary keys.
            foreach (T1 sourceKey in sourceDict.Keys)
            {
                // If the <sourceDict> value is not present in the <paramref name="inverseDict"/> as
                // a value, then add it to the return dictionary as a value.
                ////if (sourceDict[sourceKey].IsNotAny(returnDict.Keys)) {
                if (!returnDict.ContainsKey(sourceDict[sourceKey]))
                    returnDict[sourceDict[sourceKey]] = new HashSet<T1>();

                // After ensuring the hash set corresponding to the <sourceValue> exists, add the
                // <sourceKey> to the set.
                returnDict[sourceDict[sourceKey]].Add(sourceKey);
            }

            // Return after all key-value pairs have been mapped.
            return returnDict;
        }

        /// <summary>
        /// Return the provided enumeration of key-value pairs as a Dictionary.
        /// </summary>
        /// <param name="keyValuePairs">The key/value pair collection to return as a Dictionary.</param>
        /// <typeparam name="S">The type of the key in the Dictionary.</typeparam>
        /// <typeparam name="T">The type of the value in the Dictionary.</typeparam>
        /// <returns><paramref name="keyValuePairs"/> as a Dictionary.</returns>
        public static  Dictionary<S, T> ToDictionaryFromKVEnumerable<S,T>(this IEnumerable<KeyValuePair<S, T>> keyValuePairs)
        {
            return keyValuePairs.ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        /// Given a Dictionary <paramref name="dict"/>, return the set of its keys for which its key-value pairs match
        /// the given predicate <paramref name="predicate"/>.
        /// </summary>
        /// <param name="dict">The Dictionary whose keys should be selectively returned.</param>
        /// <param name="predicate">The predicate on which to filter the Dictionary keys.</param>
        /// <typeparam name="S">The type of key in the provided Dictionary.</typeparam>
        /// <typeparam name="T">The type of value in the provided Dictionary.</typeparam>
        /// <returns>The set of <paramref name="dict"/>'s keys that match <paramref name="predicate"/>.</returns>
        public static Dictionary<S, T>.KeyCollection GetKeysWhere<S, T>(this Dictionary<S, T> dict, 
            Func<KeyValuePair<S, T>, bool> predicate)
        {
            IEnumerable<KeyValuePair<S, T>> keyValuePairs = dict.Where(predicate);
            return keyValuePairs.ToDictionaryFromKVEnumerable().Keys;
        }

    }

}