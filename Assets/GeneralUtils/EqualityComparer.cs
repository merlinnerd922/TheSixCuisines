﻿using System;
using System.Collections.Generic;

namespace GeneralUtils
{

    /// <summary>
    /// A generic equality comparer, meant to be used with Dictionaries.
    /// </summary>
    /// <typeparam name="T">The type of the item being compared.</typeparam>
    [Serializable]
    public class EqualityComparer<T> : IEqualityComparer<T>
    {

        /// <summary>
        /// Return true iff the provided objects are equal.
        /// </summary>
        /// <param name="obj1">The first object to compare.</param>
        /// <param name="obj2">The second object to compare.</param>
        /// <returns>true iff the provided objects are equal.</returns>
        public bool Equals(T obj1, T obj2)
        {
            return object.Equals(obj1, obj2);
        }

        /// <summary>
        /// Return a hash representation of the provided object.
        /// </summary>
        /// <param name="obj">The object to return a hash representation of.</param>
        /// <returns>A hash representation of the provided object.</returns>
        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }

    }

}