﻿#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Helper;

#endregion

namespace Extend
{

    /// <summary>A class of extension methods for collections.</summary>
    public static class LocalCollectionUtils
    {

        /// <summary>
        ///     Given a list of items <paramref name="items" /> of type <typeparamref name="T" /> and a sequence
        ///     <paramref name="sequence" /> of other items of type <typeparamref name="T" />, add all those items to the list.
        /// </summary>
        /// <typeparam name="T">The type of item to add to the given sequence.</typeparam>
        /// <param name="sequence">The sequence of items to add to.</param>
        /// <param name="items">The set of items to add to <paramref name="sequence" />.</param>
        public static void AddAll<T>(this List<T> sequence, params T[] items)
        {
            sequence.AddRange(items);
        }

        /// <summary>
        ///     Given a List of items of type T <paramref name="thisList" /> , and a parameterized sequence of enumerable
        ///     items of type T, append all those sequences in sequence to <paramref name="thisList" />.
        /// </summary>
        /// <typeparam name="T">The type of the item in the given array <paramref name="parameterizedNestedSequence" />.</typeparam>
        /// <param name="thisList">The this List.</param>
        /// <param name="parameterizedNestedSequence">The parameterized Nested Sequence.</param>
        public static void AddRangeAll<T>(this List<T> thisList, params IEnumerable<T>[] parameterizedNestedSequence)
        {
            // For each enumerable sequence of items in the sequence, add all items in that sequence
            // to (thisList).
            parameterizedNestedSequence.ForEach(x => thisList.AddRange(x));
        }

        /// <summary>Given a list of booleans, return true if any of the values of the booleans is also true.</summary>
        /// <param name="thisList">The list of booleans we are checking to see contains the value true.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool AnyIsTrue(this IEnumerable<bool> thisList)
        {
            return thisList.Contains(true);
        }

        /// <summary>
        ///     Return true if the given type is nullable, and false otherwise. Given a sequence of elements, and a function
        ///     mapping elements of type T to integers, return the item that gives the highest integer when calculated.
        /// </summary>
        /// <typeparam name="T">The type of the object in the enumerable sequence.</typeparam>
        /// <param name="sequence">The sequence of items from which we are trying to find the maximum object.</param>
        /// <param name="function">The function we are using to determine the maximum value.</param>
        /// <returns>
        ///     The item that is considered the argmax of the sequence <paramref name="sequence" />, relative to
        ///     <paramref name="function" />.
        /// </returns>
        public static T Argmax<T>(this IEnumerable<T> sequence, Func<T, int> function)
        {
            return sequence.OrderByDescending(function).First();
        }

        /// <summary>
        ///     Given a sequence of elements, and a function mapping elements of type T to float, return the item that gives
        ///     the highest float when calculated.
        /// </summary>
        /// <typeparam name="T">The type of object in the given sequence <paramref name="sequence" />.</typeparam>
        /// <param name="sequence">The sequence of objects we are checking for the argmax.</param>
        /// <param name="function">The function mapping objects of type T.</param>
        /// <returns>The type of object in the given sequence of objects <paramref name="sequence" />.</returns>
        public static T Argmax<T>(this IEnumerable<T> sequence, Func<T, float> function)
        {
            return sequence.OrderByDescending(function).First();
        }

        /// <summary>Return a cloned version of <paramref name="sequence" />.</summary>
        /// <typeparam name="T">The type of the object in <paramref name="sequence" />.</typeparam>
        /// <param name="sequence">The sequence to clone.</param>
        /// <returns><paramref name="sequence" />, cloned.</returns>
        public static HashSet<T> Clone<T>(this HashSet<T> sequence)
        {
            return new HashSet<T>(sequence);
        }

        /// <summary>
        ///     Given an IEnumerable sequence <paramref name="sequence" /> of objects of type T, return a cloned copy of
        ///     <paramref name="sequence" /> as a list.
        /// </summary>
        /// <typeparam name="T">The type of the item in the enumerable object.</typeparam>
        /// <param name="sequence">The sequence to clone as a list.</param>
        /// <returns><paramref name="sequence" />, returned as a list.</returns>
        public static IEnumerable<T> CloneAsList<T>(this IEnumerable<T> sequence)
        {
            return new List<T>(sequence);
        }

        /// <summary>
        ///     Given a DualKeyDictionary with a key pair of type ( <typeparamref name="T1" />,
        ///     <typeparamref name="T2"></typeparamref>), which maps to a value that is an enumeration over elements of type
        ///     <typeparamref name="T4" />, return true iff there is an key entry consisting of the pair (
        ///     <paramref name="keyPart1" />, <paramref name="keyPart2" />) that has a non-null and non-empty key.
        /// </summary>
        /// <typeparam name="T1">The type of the first element of the paired key</typeparam>
        /// <typeparam name="T2">The type of the second element of the paired key.</typeparam>
        /// <typeparam name="T3">The type of the enumeration that is the value of the dictionary.</typeparam>
        /// <typeparam name="T4">The type of the elements inside the enumeration value.</typeparam>
        /// <param name="dict">The dual-key dictionary that we're checking for a key pair (with a nonempty and nonnull value)</param>
        /// <param name="keyPart1">The first item of the paired key.</param>
        /// <param name="keyPart2">The second item of the paired key.</param>
        /// <returns>
        ///     true iff there is an key entry consisting of the pair ( <paramref name="keyPart1" />,
        ///     <paramref name="keyPart2" />) that has a non-null and non-empty key.
        /// </returns>
        public static bool ContainsKeyNonEmptyNotNullValue<T1, T2, T3, T4>(this DualKeyDictionary<T1, T2, T3> dict,
            T1 keyPart1, T2 keyPart2) where T3 : HashSet<T4>
        {
            // Return false if the key doesn't exist.
            if (!dict.ContainsKey(keyPart1, keyPart2)) return false;

            // Return a value associated not just with checking if the value <keyPart1, keyPart2> is
            // in the Dictionary, and that its value is non-null and non-empty.
            T3 dictValue = dict[keyPart1, keyPart2];
            return dictValue != null && !dictValue.IsEmpty();
        }

        /// <summary>
        ///     Given an enumerable sequence of elements and some element, return an array containing the indices of all of
        ///     those elements.
        /// </summary>
        /// <typeparam name="T">The type of the object in the enumerable <paramref name="values" />.</typeparam>
        /// <param name="values">The values.</param>
        /// <param name="val">The val.</param>
        /// <returns>
        ///     The
        ///     <see>
        ///         <cref>int[]</cref>
        ///     </see>
        ///     . containing the indices of all instances of
        ///     <paramref
        ///         name="val" />
        ///     in <paramref name="values" />.
        /// </returns>
        public static int[] FindAllIndexOf<T>(this IEnumerable<T> values, T val)
        {
            return values.Select((b, i) => Equals(b, val) ? i : -1).Where(i => i != -1).ToArray();
        }

        /// <summary>
        ///     Given a nested list of items of type T named <paramref name="nestedList" /> , flatten the list and return it
        ///     as a unidimensional list.
        /// </summary>
        /// <typeparam name="T">The type of element in the nested list.</typeparam>
        /// <param name="nestedList">The nested list to flatten.</param>
        /// <param name="predicate">The predicate to check all items in all nested lists in <paramref name="nestedList" /> against.</param>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        /// <returns>
        ///     The
        ///     <see>
        ///         <cref>List</cref>
        ///     </see>
        ///     now containing all flattened items from nestedList.
        /// </returns>
        public static List<T> Flatten<T>(this List<List<T>> nestedList, Func<T, bool> predicate = null)
        {
            // initialize the return list.
            List<T> returnLst = new List<T>();

            // Loop iteratively over the elements of the nested list (first by iterating over the
            // outer list).
            foreach (List<T> innerList in nestedList)
            foreach (T item in innerList
                ) // If there is no precondition to add the element to the return list, or if there
                // is a precondition and the predicate holds, add the item to the return list.
                if (predicate == null || predicate(item))
                    returnLst.Add(item);

            // Return the final list.
            return returnLst;
        }

        /// <summary>
        ///     Given an enumerable sequence of type T named <paramref name="source" />, as well as an action that can be
        ///     performed on an object of type T, perform that action on each item in the enumeration.
        /// </summary>
        /// <typeparam name="T">The type of the parameter in the enumerable array.</typeparam>
        /// <param name="source">The sequence of objects we want to perform <paramref name="actionToPerform" /> on.</param>
        /// <param name="actionToPerform">The action we want to perform.</param>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> actionToPerform)
        {
            foreach (T item in source) actionToPerform(item);
        }

        /// <summary>Given an IEnumerator, advance the enumerator forward to get the next object.</summary>
        /// <param name="ie">The enumerator to advance and extract the marked item from.</param>
        /// <returns>The object returned after iterating the <paramref name="ie" /> forward once.</returns>
        public static object GetAndMoveNext(this IEnumerator ie)
        {
            ie.MoveNext();
            return ie.Current;
        }

        /// <summary>
        ///     Given a sequence of elements of a type <typeparamref name="T" />, return a sequence of elements of all
        ///     elements that have duplicates.
        /// </summary>
        /// <typeparam name="T">The type of the object in the array.</typeparam>
        /// <param name="sequence">The sequence of items to extract duplicates from.</param>
        /// <returns>An enumerable consisting of all items that are duplicated in the <paramref name="sequence" />.</returns>
        public static IEnumerable<T> GetDuplicates<T>(this IEnumerable<T> sequence)
        {
            // Store a list of all items found we've already found.
            HashSet<T> uniqueItemsFound = new HashSet<T>();

            // Store a set of all items found in the sequence.
            HashSet<T> duplicateItemsFound = new HashSet<T>();

            // If an item that we iterate over has already been found, add it to the master list.
            foreach (T item in sequence) // Make a record that we've found some
                // <paramref name="item"/>
                // , if we aren't already keeping a record of it.
                if (!uniqueItemsFound.Contains(item))
                    uniqueItemsFound.Add(item);

                // Otherwise, add the item to the list of duplicate items we've found, if we haven't
                // added it already.
                else if (!duplicateItemsFound.Contains(item)) duplicateItemsFound.Add(item);

            // When we've finished iterating over the sequence, return the list of all duplicate
            // items that we've found.
            return duplicateItemsFound;
        }

        /// <summary>Given an array <paramref name="arr" /> of type <T />, return the array as an IEnumerable object.</summary>
        /// <typeparam name="T">The type of object in the array <paramref name="arr" />.</typeparam>
        /// <param name="arr">The array to return.</param>
        /// <returns>The array itself.</returns>
        public static IEnumerable<T> GetEnumerable<T>(this T[] arr)
        {
            return arr;
        }

        /// <summary>
        ///     Given a sequence of elements <paramref name="sequence" /> of type
        ///     <typeparamref
        ///         name="T" />
        ///     , return the last element in this sequence.
        /// </summary>
        /// <typeparam name="T">The type of object in the array <paramref name="sequence" />.</typeparam>
        /// <param name="sequence">The sequence of items to get the last item from.</param>
        /// <returns>The last item in the given sequence <paramref name="sequence" />.</returns>
        public static T GetLastItem<T>(this IEnumerable<T> sequence)
        {
            // Raise an exception if the sequence of items we're trying to get from is empty.
            IEnumerable<T> enumerable = sequence as T[] ?? sequence.ToArray();
            if (enumerable.ToArray().IsEmpty())
                throw new InvalidOperationException("You cannot take the final item of an empty sequence!");

            return enumerable.ToArray()[enumerable.Count() - 1];
        }

        /// <summary>
        ///     Given a sequence of elements <paramref name="elementEnumeration" /> of type
        ///     <typeparamref
        ///         name="T" />
        ///     , and a predicate on <typeparamref name="T" />, return the last element in this sequence that fulfills this
        ///     condition.
        /// </summary>
        /// <typeparam name="T">The type of the element within <paramref name="elementEnumeration" />.</typeparam>
        /// <param name="elementEnumeration">
        ///     The sequence from which to extract the last object that fulfills the condition
        ///     <paramref name="condition" />.
        /// </param>
        /// <param name="condition">The condition to fulfill.</param>
        /// <returns>The type <see cref="T" /> of the enumeration of elements.</returns>
        public static T GetLastItemWhere<T>(this IEnumerable<T> elementEnumeration, Func<T, bool> condition)
        {
            List<T> filteredSequence = elementEnumeration.Where(condition).ToList();

            // Raise an exception if the sequence of items we're trying to get from, filtered by this
            // condition, is empty.
            if (filteredSequence.IsEmpty())
                throw new InvalidOperationException("You cannot take the final item of an empty sequence!");

            // Otherwise, return the last item that fulfills this condition.
            return filteredSequence.GetLastItem();
        }

        /// <summary>Given a 2D array <paramref name="array2D/" />, return an iterator over all the nested elements of the array.</summary>
        /// <typeparam name="T">The type of the element in the given array <paramref name="array2D" />.</typeparam>
        /// <param name="array2D">The 2D array whose elements should be iterated over.</param>
        /// <returns>An <see cref="IEnumerable" /> iterating over all nested items in the given array <paramref name="array2D" />.</returns>
        public static IEnumerable<T> GetNestedItemwiseEnumerator<T>(this IEnumerable<T[]> array2D)
        {
            // Iterate over each of the arrays in array2D, then iterate again over each of the
            // elements in each of those arrays.
            foreach (T[] nestedArray in array2D)
            foreach (T nestedItem in nestedArray)
                yield return nestedItem;
        }

        /// <summary>
        ///     Given a 2D array <paramref name="array2D/" />, return the sum of the number of elements in each of its
        ///     subarrays.
        /// </summary>
        /// <typeparam name="T">The type of the item in the nested enumeration of items.</typeparam>
        /// <param name="array2D">The 2D array whose nested sum we are calculating.</param>
        /// <returns>The <see cref="int" /> representing the total sum of all the arrays nested within <paramref name="array2D" />.</returns>
        public static int GetNestedLength<T>(this IEnumerable<T[]> array2D)
        {
            return array2D.Sum(nestedArray => nestedArray.Length);
        }

        /// <summary>
        ///     Given an IEnumerable sequence <paramref name="sequence" /> of objects of type T, and an integer
        ///     <paramref name="n" /> , return a random, shuffled subsequence of the sequence as a list, with <paramref name="n" />
        ///     items, or return the entire sequence if
        ///     <paramref
        ///         name="n" />
        ///     is greater than the length of the sequence.
        /// </summary>
        /// <typeparam name="T">The type of the item in the sequence.</typeparam>
        /// <param name="sequence">The sequence of items from which we are retrieving <paramref name="n" /> random items.</param>
        /// <param name="n">The number of items we're retrieving from the sequence.</param>
        /// <returns>
        ///     The
        ///     <see>
        ///         <cref>List</cref>
        ///     </see>
        ///     of <paramref name="n" /> random items we're retrieving from the sequence.
        /// </returns>
        public static List<T> GetNRandomItems<T>(this IEnumerable<T> sequence, int n)
        {
            IEnumerable<T> enumerable = sequence as T[] ?? sequence.ToArray();
            List<T> clonedSequence = enumerable.CloneAsList().ToList();
            clonedSequence.Shuffle();
            return clonedSequence.Take(Math.Min(n, enumerable.Count())).ToList();
        }

        /// <summary>
        ///     Given a 2D array of items <paramref name="array2D" /> and a pair of integers
        ///     <paramref
        ///         name="intPair" />
        ///     , use the pair as an index into the array and return the item at the given index.
        /// </summary>
        /// <param name="array2D">The 2D array of items to get an indexed item from.</param>
        /// <param name="intPair">The pair of integers to use to index into the array.</param>
        /// <typeparam name="T">The type of the item in the multidimensional array <paramref name="array2D" />.</typeparam>
        /// <returns>The item of type <see cref="T" /> at the given index.</returns>
        public static T GetPairByIndex<T>(this T[][] array2D, Vector2Int intPair)
        {
            return array2D[intPair[0]][intPair[1]];
        }

        /// <summary>Given a HashSet of items, return a random item from the List.</summary>
        /// <param name="lst">The list to extract a random item from.</param>
        /// <typeparam name="T">The type of the item in the <paramref name="lst" />.</typeparam>
        /// <returns>The item of type <see cref="T" /> returned by the randomizer.</returns>
        public static T GetRandomItem<T>(this HashSet<T> lst)
        {
            return lst.ToList()[NumberRandomizer.Next(0, lst.Count)];
        }

        /// <summary>Given an array of items, return a random item from the List.</summary>
        /// <typeparam name="T">The type of item in the array <paramref name="lst" />.</typeparam>
        /// <param name="lst">The list to extract a random item from.</param>
        /// <returns>The randomized item of type <see cref="T" /> to return from the array.</returns>
        public static T GetRandomItem<T>(this T[] lst)
        {
            return lst[NumberRandomizer.Next(0, lst.Length)];
        }

        /// <summary>Given a list interface of items, return a random item from the List.</summary>
        /// <typeparam name="T">The type of the item in <paramref name="listToGetFrom" />.</typeparam>
        /// <param name="listToGetFrom">The list to extract a random item from.</param>
        /// <returns>The randomized item to return from the list.</returns>
        public static T GetRandomItem<T>(this IList<T> listToGetFrom)
        {
            int index = NumberRandomizer.Next(0, listToGetFrom.Count);
            if (listToGetFrom.HasIndex(index))
            {
                return listToGetFrom[index];
            }

            throw new IndexOutOfRangeException("We tried to get an item from a list at an index" + "that was" + 
                                               "out of range!");

        }

        /// <summary>Given an ArrayList of items, return a random item from the List.</summary>
        /// <typeparam name="T">The type of item in the list <paramref name="lst" />.</typeparam>
        /// <param name="lst">The list to extract a random item from.</param>
        /// <returns>A random item of type <see cref="T" /> from the list.</returns>
        public static T GetRandomItem<T>(this ArrayList lst)
        {
            return (T) lst[NumberRandomizer.Next(0, lst.Count)];
        }

        /// <summary>Given an Array of items, return a random item from the List.</summary>
        /// <typeparam name="T">The type of item in the given <paramref name="lst" />.</typeparam>
        /// <param name="lst">The list to get a random item from.</param>
        /// <returns>The random item as returned from <paramref name="lst" />.</returns>
        public static T GetRandomItem<T>(this Array lst)
        {
            return (T) lst.GetValue(NumberRandomizer.Next(0, lst.Length));
        }

        /// <summary>
        ///     Given an IEnumerable sequence <paramref name="sequence" /> of items of type
        ///     <typeref
        ///         name="T" />
        ///     , return a shuffled version of <paramref name="sequence" /> as a list.
        /// </summary>
        /// <typeparam name="T">The type of object in <paramref name="sequence" />.</typeparam>
        /// <param name="sequence">The sequence to shuffle.</param>
        /// <returns>A shuffled copy of the given list <paramref name="sequence" />.</returns>
        public static List<T> GetShuffled<T>(this IEnumerable<T> sequence)
        {
            List<T> returnVal = sequence.CloneAsList().ToList();
            returnVal.Shuffle();
            return returnVal;
        }

        /// <summary>
        ///     Given a sequence of parameters of a type <typeparamref name="T" />, return true if the sequence has
        ///     duplicates, and false otherwise.
        /// </summary>
        /// <typeparam name="T">The type of the item in the sequence <paramref name="sequence" />.</typeparam>
        /// <param name="sequence">The sequence of items to check for duplicates.</param>
        /// <returns>true iff <paramref name="sequence" /> has duplicates.</returns>
        public static bool HasDuplicates<T>(this IEnumerable<T> sequence)
        {
            // Obviously, sets will not have duplicate elements.
            if (sequence is HashSet<T>) return false;

            // Store all the values that we find to be part of the set in a HashSet<T>, and attempt
            // to add each element in the sequence to the set. If we fail to add an element, it is
            // because the element already exists in the set, and thus this function will return
            // true; otherwise, if it succeeds in adding all elements, then we return false.
            HashSet<T> hashset = new HashSet<T>();
            return sequence.Any(e => !hashset.Add(e));
        }

        /// <summary>
        ///     Given an array of elements <paramref name="arrayToSearch" /> and an integer
        ///     <paramref
        ///         name="index" />
        ///     , return true if <paramref name="arrayToSearch" /> can be indexed with the index <paramref name="index" />.
        /// </summary>
        /// <typeparam name="T">The type of the item in the given <paramref name="arrayToSearch" />.</typeparam>
        /// <param name="arrayToSearch">The array to check to see if there is an item with index <paramref name="index" />.</param>
        /// <param name="index">The index.</param>
        /// <returns>
        ///     A flag indicating whether the given array <paramref name="arrayToSearch" /> has an item at index
        ///     <paramref name="index" />.
        /// </returns>
        public static bool HasIndex<T>(this IEnumerable<T> arrayToSearch, int index)
        {
            // See if we can represent the IEnumerable as an array; if so, return the number of
            // elements in it using Length, not count().
            return arrayToSearch is T[] enumerableAsArr
                ? index.InInclusiveRange(0, enumerableAsArr.Length - 1)
                : index.InInclusiveRange(0, arrayToSearch.Count() - 1);
        }

        /// <summary>
        ///     Given a Dictionary <paramref name="dict" />, return true if <paramref name="dictKey" />,
        ///     <paramref name="dictValue" /> is an entry in the Dictionary.
        /// </summary>
        /// <typeparam name="T1">The type of the key in the dictionary.</typeparam>
        /// <typeparam name="T2">The type of the item in the dictionary.</typeparam>
        /// <param name="dict">The dictionary to extract a pair from.</param>
        /// <param name="dictKey">The key to check the dictionary for.</param>
        /// <param name="dictValue">The value to check the dictionary for.</param>
        /// <returns>
        ///     true iff the dictionary has a key-value pair consisting of <paramref name="dictKey" />,
        ///     <paramref name="dictValue" />.
        /// </returns>
        public static bool HasPair<T1, T2>(this Dictionary<T1, T2> dict, T1 dictKey, T2 dictValue)
        {
            return dict.ContainsKey(dictKey) && dict[dictKey].Equals(dictValue);
        }

        /// <summary>
        ///     Given a nested array <paramref name="arr" /> of objects of type T, return true iff the array has an item at
        ///     paired index <paramref name="indexPair" />.
        /// </summary>
        /// <typeparam name="T">The type of the item in the nested array.</typeparam>
        /// <param name="arr">The array to check for the existence of an item with paired index <paramref name="indexPair" />.</param>
        /// <param name="indexPair">The paired index object to use to access <paramref name="arr" />'s contents.</param>
        /// <returns>true iff the array has an item at paired index <paramref name="indexPair" />.</returns>
        public static bool HasPairedIndex<T>(this T[][] arr, Vector2Int indexPair)
        {
            // The return value depends on if there <paramref name="arr"/> has an <indexPair[0]>th
            // entry, and if <paramref name="arr"/> at index <indexPair[0]> has an entry at index <indexPair[1]>.
            bool hasIndex = arr.HasIndex(indexPair[0]);

            // Determine if the item we're trying to access is not null.
            T[] nestedArray = arr[indexPair[0]];

            // Finally, we know we can return the item if it (nestedArray) exists and in turn also
            // has an item at index (indexPair[1]).
            return hasIndex && nestedArray != null && nestedArray.HasIndex(indexPair[1]);
        }

        /// <summary>Given an enumerable sequence, return true if it is empty and false otherwise.</summary>
        /// <typeparam name="T">The type of the item in the sequence.</typeparam>
        /// <param name="sequence">The sequence of items to check to see is empty.</param>
        /// <returns>true iff the given sequence is empty.</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> sequence)
        {
            return new List<T>(sequence).Count == 0;
        }

        /// <summary>
        ///     Given an IEnumerable sequence of items of type <typeparamref name="T" />, return true iff the sequence is null
        ///     or has no items.
        /// </summary>
        /// <typeparam name="T">The type of the item in <paramref name="sequence" />.</typeparam>
        /// <param name="sequence">The sequence of items to check for nullity or emptiness.</param>
        /// <returns>true iff the given <paramref name="sequence" /> has no items.</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> sequence)
        {
            return sequence == null || sequence.IsEmpty();
        }

        /// <summary>
        ///     Given an enumerable sequence of items <paramref name="enumerable" /> and a string <paramref name="s" />,
        ///     return a string with the elements in <paramref name="enumerable" /> separated by <paramref name="s" />.
        /// </summary>
        /// <typeparam name="T">The type of item in the given object <paramref name="enumerable" />.</typeparam>
        /// <param name="enumerable">The enumeration whose items we should join together.</param>
        /// <param name="s">The string with which to delimit <paramref name="enumerable" />'s items by.</param>
        /// <returns>
        ///     The <see cref="string" /> containing a concatenation of all of
        ///     <paramref
        ///         name="enumerable" />
        ///     's items.
        /// </returns>
        public static string Join<T>(this IEnumerable<T> enumerable, string s)
        {
            return String.Join(s, enumerable.Select(x => x.ToString()).ToArray());
        }

        /// <summary>Given a list of possibly null integers <paramref name="intList" />, return the largest integer in the list.</summary>
        /// <param name="intList">The list from which to return the maximum integer.</param>
        /// <returns>The maximum value of the list <paramref name="intList" />.</returns>
        public static int? Max(this IEnumerable<int?> intList)
        {
            int? retVal = null;

            foreach (int? i in intList)
                if (i != null && (retVal == null || i > retVal))
                    retVal = i;

            return retVal;
        }

        /// <summary>
        ///     Remove the element <paramref name="elementToRemove" /> from the set
        ///     <paramref
        ///         name="thisSet" />
        ///     . If it is successfully removed, return; otherwise, throw an InvalidOperationException.
        /// </summary>
        /// <typeparam name="T">The type of the object inside the set <paramref name="thisSet" />.</typeparam>
        /// <param name="thisSet">The set we're removing <paramref name="elementToRemove" /> from.</param>
        /// <param name="elementToRemove">The element to remove from <paramref name="thisSet" />.</param>
        public static void RemoveForced<T>(this HashSet<T> thisSet, T elementToRemove)
        {
            // Return here, because the removal was successful.
            if (thisSet.Remove(elementToRemove)) return;

            // If the passed-in exception is null, then initialize a new one to be thrown. Otherwise,
            // we throw the Exception specified.
            throw new InvalidOperationException(
                "We just tried to remove an element from a set, but there is no such element in the set!");
        }

        /// <summary>
        ///     Given an enumeration over type <typeparamref name="T1" /> and a function
        ///     <typeparamref
        ///         name="T2" />
        ///     mapping instances of <typeparamref name="T1" /> to <typeparamref name="T2" />, return an array of
        ///     <typeparamref name="T2" /> objects such that for every element in the original array
        ///     <paramref name="t1Enumeration" />, the returned array at that particular index is a T2 object so that
        ///     <paramref name="selectFunction" />(t1Object) = t2Object.
        /// </summary>
        /// <typeparam name="T1">The type of the objects in the input enumeration <paramref name="t1Enumeration" />.</typeparam>
        /// <typeparam name="T2">The type of the objects in the returned array.</typeparam>
        /// <param name="t1Enumeration">The input enumeration we are going to process.</param>
        /// <param name="selectFunction">
        ///     The method that maps the elements of the input enumeration
        ///     <paramref
        ///         name="t1Enumeration" />
        ///     to the elements of the return array.
        /// </param>
        /// <returns>The array of T2 objects mapped by the input array <paramref name="t1Enumeration" /> of T1 objects</returns>
        public static T2[] SelectAsArray<T1, T2>(this IEnumerable<T1> t1Enumeration, Func<T1, T2> selectFunction)
        {
            IEnumerable<T2> enumerable = t1Enumeration.Select(selectFunction);
            return enumerable.ToArray();
        }

        /// <summary>Given a List, shuffle and return it.</summary>
        /// <typeparam name="T">The type of object within the given list.</typeparam>
        /// <param name="listToShuffle">The list we are to shuffle before returning.</param>
        /// <returns>The newly shuffled list.</returns>
        public static IList<T> Shuffle<T>(this IList<T> listToShuffle)
        {
            int n = listToShuffle.Count;
            while (n > 1)
            {
                n--;
                int k = NumberRandomizer.Next(n + 1);
                T value = listToShuffle[k];
                listToShuffle[k] = listToShuffle[n];
                listToShuffle[n] = value;
            }

            return listToShuffle;
        }

        /// <summary>
        ///     Given an enumerable sequence of items <paramref name="strSequence" />, return the sequence of items as a
        ///     string delimited by commas and the word "and" (where needed).
        /// </summary>
        /// <typeparam name="T">The type of object in the given sequence <paramref name="strSequence" />.</typeparam>
        /// <param name="strSequence">The sequence of objects to string together.</param>
        /// <returns>The string containing a concatenated sequence of objects as specified in <paramref name="strSequence" />.</returns>
        public static string ToGrammaticalString<T>(this IEnumerable<T> strSequence)
        {
            // If there are no elements in the sequence, return a string simply saying "N/A"
            T[] enumerable = strSequence as T[] ?? strSequence.ToArray();
            if (!enumerable.Any()) return "{}";

            switch (enumerable.Length)
            {
                // If there's only one element, return the string rep of that element.
                case 1:
                    return enumerable[0].ToString();

                // If there are two elements, delimit them by the word "and".
                case 2:
                    return "{0} and {1}".FormatExtend(enumerable[0], enumerable[1]);
            }

            // IF there are more than two elements, then delimit them by the word "and" and commas.
            // Initialize a string to return later.
            string returnStr = enumerable[0].ToString();
            for (int i = 1; i < enumerable.Length - 1; i++) returnStr += ", {0}".FormatExtend(enumerable[i]);
            returnStr += " and {0}".FormatExtend(enumerable[enumerable.Length - 1]);

            // Finally, return the string.
            return returnStr;
        }

        /// <summary>Given an enumerable sequence <paramref name="source" /> of some items of type T, return the sequence as a set.</summary>
        /// <typeparam name="T">The</typeparam>
        /// <param name="source">The source enumerable object to convert into a HashSet.</param>
        /// <returns>
        ///     The
        ///     <see>
        ///         <cref xml:space="preserve">HashSet</cref>
        ///     </see>
        ///     representation of the given object <paramref name="source" />.
        /// </returns>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }

        /// <summary>
        ///     Given an array <paramref name="sequence" /> of items of type <typeparamref name="T" />, return a dictionary
        ///     that maps the items to their indices.
        /// </summary>
        /// <typeparam name="T">The type of the item in the array.</typeparam>
        /// <param name="sequence">The sequence of items to convert to a dictionary mapping items to indices.</param>
        /// <returns>
        ///     The
        ///     <see>
        ///         <cref>Dictionary</cref>
        ///     </see>
        ///     containing indices of the items in the sequence <paramref name="sequence" />.
        /// </returns>
        public static Dictionary<T, int> ToInverseIndexDictionary<T>(this IList<T> sequence)
        {
            // Store the return value of the dictionary.
            Dictionary<T, int> returnDict = new Dictionary<T, int>();

            // Get all items and add them as a mapping to the dictionary, before returning the final dictionary.
            for (int i = 0; i < sequence.Count; i++) returnDict[sequence[i]] = i;

            return returnDict;
        }

        /// <summary>Given an IEnumerable <paramref name="iterator" /> of some sequence of items, return an IEnumerator of type T.</summary>
        /// <typeparam name="T">The type of object in the given iterator <paramref name="iterator" />.</typeparam>
        /// <param name="iterator">The iterator to convert into a list.</param>
        /// <returns>A list representation of all items in the given <paramref name="iterator" />.</returns>
        public static List<T> ToList<T>(this IEnumerator iterator)
        {
            // Initialize empty list.
            List<T> returnLst = new List<T>();

            // ADd each item in the iterator to the list (as long as there are items left).
            while (iterator.MoveNext())
            {
                returnLst.Add((T) iterator.Current);
                if (returnLst.Count > 7) break;
            }

            // Finally, when all is done, return the list.
            return returnLst;
        }

        /// <summary>Given a list of items, return a neat string representation of it.</summary>
        /// <typeparam name="T">The type of the item in the given <paramref name="listToConvert" />.</typeparam>
        /// <param name="listToConvert">The list to convert into a neat string.</param>
        /// <returns>The <see cref="string" /> representation of the given <paramref name="listToConvert" />.</returns>
        public static string ToNeatString<T>(this IList<T> listToConvert)
        {
            // Left bracket of the string representation.
            string returnValueListAsString = "{";

            // Append string rep of each item.
            foreach (T item in listToConvert)
            {
                returnValueListAsString += item.ToString();

                // Delimit items with a comma.
                if (listToConvert.IndexOf(item) != listToConvert.Count - 1) returnValueListAsString += ", ";
            }

            // Right bracket of the string representation.
            returnValueListAsString += "}";

            return returnValueListAsString;
        }

        /// <summary>
        ///     Given an enumerable sequence <paramref name="source" /> of some items of type T, return the sequence as a
        ///     queue.
        /// </summary>
        /// <typeparam name="T">The type of the object in the enumerable object <paramref name="source" />.</typeparam>
        /// <param name="source">The source enumerable to convert into a Queue.</param>
        /// <returns>The <see cref="Queue" /> representation of the given IEnumerable source object.</returns>
        public static Queue<T> ToQueue<T>(this IEnumerable<T> source)
        {
            return new Queue<T>(source);
        }

        /// <summary>
        ///     Given a set <paramref name="thisSet" /> and a collection of other sets
        ///     <paramref
        ///         name="otherSets" />
        ///     , return the union of <paramref name="thisSet" /> with all sets in <paramref name="otherSets" />.
        /// </summary>
        /// <typeparam name="T">The type of the object in <paramref name="thisSet" />.</typeparam>
        /// <param name="thisSet">The current set we're unionizing.</param>
        /// <param name="otherSets">The set of other sets we're combining <paramref name="thisSet" /> with.</param>
        /// <returns>The union of <paramref name="thisSet" /> and <paramref name="otherSets" />.</returns>
        public static HashSet<T> UnionAll<T>(this HashSet<T> thisSet, params HashSet<T>[] otherSets)
        {
            // Raise Exceptions for null sets.
            if (thisSet == null) throw new ArgumentException("The set (thisSet) is null!");

            // Clone (thisSet).
            HashSet<T> returnSet = thisSet.Clone();

            // Then, unionize it with all sets in (otherSets).
            foreach (HashSet<T> hashSet in otherSets) returnSet.UnionWith(hashSet);

            // Return the unionized set.
            return returnSet;
        }

        /// <summary>
        /// Return a List of elements that are mapped to by the elements of <paramref name="enumeration"/> by the
        /// function <paramref name="func"/>.
        /// </summary>
        /// <param name="enumeration">The enumeration of objects to be mapped to a list.</param>
        /// <param name="func">The function used to map instances of <typeparamref name="S"/> to <typeparamref name="T"/>.</param>
        /// <typeparam name="S">The type of parameter in <paramref name="enumeration"/>.</typeparam>
        /// <typeparam name="T">The type of parameter mapped to by <paramref name="func"/>.</typeparam>
        /// <returns> a List of elements that are mapped to by the elements of <paramref name="enumeration"/> by the
        /// function <paramref name="func"/>.</returns>
        public static List<T> SelectAsList<S, T>(this IEnumerable<S> enumeration, Func<S, T> func)
        {
            return enumeration.Select(func).ToList();
        }

        /// <summary>
        /// Perform the action <paramref name="actionToPerform"/> on each element in the array <paramref name="array"/>.
        /// </summary>
        /// <param name="array">The array whose elements should have an action performed on them.</param>
        /// <param name="actionToPerform">The action to perform on each element in the provided array.</param>
        /// <typeparam name="T">The type of element in the provided array.</typeparam>
        public static void ForEach<T>(this Array array, Action<T> actionToPerform)
        {
            array.Cast<T>().ForEach<T>(actionToPerform);
        }

        /// <summary>
        ///Given a map mapping elements of type <typeparamref name="T"/> to elements of <see name="HashSet"/> containing elements of type
        /// <typeparamref name="S"/>, return a reverse mapping of the provided set, where every element that exists in
        /// any of the hashset values of <paramref name="dict"/> are keys, AND their values are the set of all
        /// elements of type <typeparamref name="T"/> that map to that element in <paramref name="dict"/>.
        /// 
        /// </summary>
        /// <param name="dict">The dictionary containing HashSets as values to reverse.</param>
        /// <returns>The fully reversed dictionary</returns>
        public static Dictionary<S, HashSet<T>> GetReverseMapping<S,T>(Dictionary<T, HashSet<S>> dict)
        {
            Dictionary<S, HashSet<T>> reverseMapping = new Dictionary<S, HashSet<T>>();
            
            // Iteratively process each key in (dict), as well as each element within the HashSet mapped to by that
            // key in the dict.
            foreach (T key in dict.Keys)
            {
                foreach (S valueElement in dict[key])
                {
                    // Add the value to the reverse dict as a key if it doesn't exist, with an empty set as the value.
                    if (!reverseMapping.ContainsKey(valueElement))
                    {
                        reverseMapping[valueElement] = new HashSet<T>();
                    }

                    // Add (key) to the value HashSet mapped to by the key (value).
                    reverseMapping[valueElement].Add(key);
                }
            }
            return reverseMapping;
        }

        /// <summary>
        /// Return a copy of <paramref name="dict"/> that only contain elements matching <paramref name="whereClause"/>.
        /// </summary>
        /// <param name="dict">The Dictionary to return a filtered copy of.</param>
        /// <param name="whereClause">The clause denoting all elements that will be retained.</param>
        /// <returns>a copy of <paramref name="dict"/> that only contain elements matching <paramref name="whereClause"/>.</returns>
        public static Dictionary<S, T> WhereToDictionary<S, T>(
            this Dictionary<S, T> dict,
            Func<KeyValuePair<S, T>, bool> whereClause)
        {
            return dict.Where(whereClause).ToDictionary
            (x => x.Key, x =>
                x.Value);
        }

        /// <summary>
        /// Return a random item from the provided enumeration.
        /// </summary>
        /// <param name="iEnumerable">The enumeration from which to return a random item.</param>
        /// <returns>A random item from the provided enumeration.</returns>
        public static T GetRandomItem<T>(this IEnumerable<T> iEnumerable)
        {
            return iEnumerable.ToList().GetRandomItem();
        }

        /// <summary>
        /// Return a random item from the provided collection <paramref name="collection"/> that fulfills the predicate
        /// <paramref name="predicate"/>.
        /// </summary>
        /// <param name="collection">The collection from which we're retrieving an item.</param>
        /// <param name="predicate">The predicate that the returned item must fulfilled.</param>
        /// <returns>a random item from the provided collection <paramref name="collection"/> that fulfills the predicate
        /// <paramref name="predicate"/>.</returns>
        public static T GetRandomWhere <T> (this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            return collection.Where(predicate).GetRandomItem();
        }

    }

}