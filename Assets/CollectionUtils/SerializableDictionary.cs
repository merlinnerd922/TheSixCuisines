﻿#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using Extend;

#endregion

/// <summary>A Dictionary that can be serialized.</summary>
/// <typeparam name="K">The type of the key of the Dictionary.</typeparam>
/// <typeparam name="V">The type of the value of the Dictionary.</typeparam>
[Serializable]
public class SerializableDictionary<K, V> : Dictionary<K, V>
{

    /// <summary>
    /// The key used to store this dictionary when serializing it to a SerializationInfo object.
    /// </summary>
    private const string SERIALIZATION_INFO_KEY = "dict";

    /// <summary>
    ///     The name of the key used to store this Dictionary when attempting to access or write to an internal mapping
    ///     that represents this Dictionary as a list of tuples.
    /// </summary>
    private string _serializationDataKey;


    /// <summary>Construct a brand new MySerializableDictionary.
    ///     <remarks>This constructor exists solely to prevent outside initialization using this constructor.</remarks>
    /// </summary>
    public SerializableDictionary(GeneralUtils.EqualityComparer<K> equalityComparer)
    {
    }

    /// <summary>
    /// Construct a brand new SerializableDictionary object.
    /// </summary>
    public SerializableDictionary()
    {
    }

    /// <summary>
    /// Construct a brand new SerializableDictionary object with the provided serialization parameters.
    /// </summary>
    /// <param name="info">The SerializationInfo object that we'll be loading a serialized version of a Dictionary
    /// from.</param>
    /// <param name="context">A context object containing important streaming variables.</param>
    public SerializableDictionary(SerializationInfo info, StreamingContext context)
    {
        // Load a representation of a Dictionary as a list of tuples from the info object (info).
        List<Tuple<K, V>> dataKeyName = (List<Tuple<K, V>>) info.GetValue(SERIALIZATION_INFO_KEY, typeof(List<Tuple<K, V>>));

        // Then, add each individual entry to this dictionary.
        foreach ((K item1, V item2) in dataKeyName)
        {
            this[item1] = item2;
        }
    }

    /// <summary>
    /// Serialize this dictionary by adding this value as an entry to the provided SerializationInfo structure <paramref name="info"/>.
    /// </summary>
    /// <param name="info">The info object to save this Dictionary to.</param>
    /// <param name="context">The context object containing any additionally needed info for streaming functionality.</param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        // Represent this dictionary as a list of tuples, and serialize and save the result to the parameterized info object (info).
        List<Tuple<K, V>> objList = this.SelectAsList(entry => Tuple.Create(entry.Key, entry.Value));
        info.AddValue(SERIALIZATION_INFO_KEY, objList);
    }

}