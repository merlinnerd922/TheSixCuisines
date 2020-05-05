﻿#region

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Helper;

#endregion

/// <summary>A Dictionary that uses a pair of objects as a key.</summary>
/// <typeparam name="T1">The type of the key's first element.</typeparam>
/// <typeparam name="T2">The type of the key's second element.</typeparam>
/// <typeparam name="T3">The type of the value.</typeparam>
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class DualKeyDictionary<T1, T2, T3>
{

    /// <summary>The internal dictionary used by this dictionary.</summary>
    private Dictionary<Tuple<T1, T2>, T3> _internalDictionary;

    /// <summary>Construct a new blank dual key dictionary.</summary>
    public DualKeyDictionary()
    {
        this._internalDictionary = new Dictionary<Tuple<T1, T2>, T3>();
    }

	/// <summary>Construct a new dictionary from the dictionary parameter (comparer).</summary>
	/// <param name="internalDictionary">The dictionary to construct this dictionary from.</param>
	public DualKeyDictionary(Dictionary<Tuple<T1, T2>, T3> internalDictionary)
    {
        this._internalDictionary = internalDictionary;
    }

	/// <summary>
	///     Given an equality comparer object to compare values, initialize the internal dictionary using the comparer
	///     (tupleComparer).
	/// </summary>
	/// <param name="tupleComparer">The tuple comparer to use.</param>
	public DualKeyDictionary(EqualityComparer<Tuple<T1, T2>> tupleComparer)
    {
        this._internalDictionary = new Dictionary<Tuple<T1, T2>, T3>(tupleComparer);
    }

	/// <summary>Return the item of type T3 at the paired indices (key1, key2).</summary>
	/// <param name="key1">The first item of the tuple key we're using to access an associated entry.</param>
	/// <param name="key2">The second item of the tuple key we're using to access an associated entry.</param>
	/// <returns>The item of type T3 at the paired indices (key1, key2).</returns>
	public T3 this[T1 key1, T2 key2] {
        get {
            // Form the tuple that we'll use to access the dictionary.
            Tuple<T1, T2> keyTuple = new Tuple<T1, T2>(key1, key2);

            // Raise an Exception if we couldn't' find the key; otherwise, return the associated value.
            if (!this._internalDictionary.ContainsKey(keyTuple))
                throw new IndexOutOfRangeException(
                    "This dictionary lacks the tuple {0} as a key!!".FormatExtend(keyTuple));
            return this._internalDictionary[keyTuple];
        }

        // Set the value associated with the paired index.
        set => this._internalDictionary[new Tuple<T1, T2>(key1, key2)] = value;
    }

	/// <summary>Return true iff this dictionary has the paired key (key1, key2).</summary>
	/// <param name="key1">The first item of the tuple key.</param>
	/// <param name="key2">The second item of the tuple key.</param>
	/// <returns>The returned value associated with the tuple key..</returns>
	public bool ContainsKey(T1 key1, T2 key2)
    {
        return this._internalDictionary.ContainsKey(new Tuple<T1, T2>(key1, key2));
    }

}