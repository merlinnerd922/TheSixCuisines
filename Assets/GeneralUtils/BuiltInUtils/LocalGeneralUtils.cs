﻿#region

using System;
using System.Collections.Generic;
using System.Linq;
using Extend;
using SysRandom = System.Random;

#endregion

/// <summary>A static class contain various helper methods.</summary>
public static class LocalGeneralUtils
{

    /// <summary>An instance of a random module that runs certain functions related to probability.</summary>
    public static readonly SysRandom SYS_RANDOM = new SysRandom();

    /// <summary>The Nullable type.</summary>
    public static readonly Type TYPE_OF_NULLABLE = typeof(Nullable<>);

	/// <summary>Return an Array object of all enum values of type T.</summary>
	/// <typeparam name="T">The type parameter of the enum object we want to return.</typeparam>
	/// <returns>The Array of enum objects of type T.</returns>
	public static Array GetEnumValues<T>()
    {
        return Enum.GetValues(typeof(T));
    }

	/// <summary>Given a reference to an object, set it to the passed-in value if it is null.</summary>
	/// <typeparam name="T">THe type of the object whose value we want to set.</typeparam>
	/// <param name="obj">The reference to the object we want to set.</param>
	/// <param name="value">The value we want to set the object to.</param>
	public static void SetIfNull<T>(ref T obj, T value)
    {
        if (obj == null) obj = value;
    }

	/// <summary>Given two references to objects (obj1) and (obj2), swap the references.</summary>
	/// <typeparam name="T">A type that inherits from the Object type.</typeparam>
	/// <param name="obj1">The reference to the first object.</param>
	/// <param name="obj2">The reference to the second object.</param>
	public static void Swap<T>(ref T obj1, ref T obj2)
    {
        T tempObj = obj1;
        obj1 = obj2;
        obj2 = tempObj;
    }

	/// <summary>Given a TimeSpan, represent it as a string containing minutes and seconds.</summary>
	/// <param name="timeSpan">The TimeSpan we're converting into a String as minutes and seconds</param>
	/// <returns>The String representation of the TimeSpan as minutes and seconds.</returns>
	public static string ToStringMinutesSeconds(this TimeSpan timeSpan)
    {
        return String.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }

	/// <summary>Return true iff the given type (type) is equal to or derived from any of the types listed in (typeArr).</summary>
	/// <param name="type">The type we're checking for equality or derivation.</param>
	/// <param name="typeArr">A parameterized array of types we are meant to cross-reference (type) against.</param>
	/// <returns>true iff the given type (type) is equal to or derived from any of the types listed in (typeArr)</returns>
	/// <exception cref="ArgumentNullException">Throw this Exception iff the given Type object is null.</exception>
	public static bool IsSubClassOrEquals(this Type type, params Type[] typeArr)
    {
        return typeArr.Any(arrayType => type.IsSubclassOf(arrayType) || arrayType == type);
    }

	/// <summary>Return true iff the given type is nullable.</summary>
	/// <param name="targetType">The type we are checking for nullability.</param>
	/// <returns>true iff the given type is nullable.</returns>
	public static bool IsNullable(this Type targetType)
    {
        return targetType.IsGenericType && targetType.GetGenericTypeDefinition() == TYPE_OF_NULLABLE;
    }

	/// <summary>Given a string <paramref name="s" />, return true if it is null or empty, and false otherwise.</summary>
	/// <param name="s">The formatString.</param>
	/// <returns>The <see cref="bool" />.</returns>
	public static bool IsNullOrEmpty(this string s)
    {
        return s.IsAny(null, String.Empty);
    }

	/// <summary>
	/// Return the Enum representation of the provided String.
	/// </summary>
	/// <param name="enumAsString">The String to reprseent as an enum.</param>
	/// <typeparam name="T">The type of the enum to represent the given string as.</typeparam>
	/// <returns>The Enum representation of the provided String.</returns>
	public static T ParseEnum<T>(string enumAsString)
	{
		return (T) Enum.Parse(typeof(T), enumAsString);
	}

	/// <summary>
	/// Convert the provided key value pair <paramref name="keyValuePair"/> to a Tuple of Strings, and return that Tuple.
	/// </summary>
	/// <param name="keyValuePair">The KeyValuePair to convert into a Tuple of Strings.</param>
	/// <typeparam name="K">The Key in the pair.</typeparam>
	/// <typeparam name="V">The value in the pair.</typeparam>
	/// <returns>The Tuple representation of the provided key/value pair.</returns>
	public static Tuple<string, string> ToStringTuple<K, V>(this KeyValuePair<K, V> keyValuePair)
	{
		return Tuple.Create(keyValuePair.Key.ToString(), keyValuePair.Value.ToString());
	}

	/// <summary>
	/// Return a list of all instances of the enum of type <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">The type of the enum whose instances we're retrieving.</typeparam>
	/// <returns>a list of all instances of the enum of type <typeparamref name="T"/>.</returns>
	public static List<T> GetEnumList<T>()
	{
		return Enum.GetValues(typeof(T)).OfType<T>().ToList();
	}

}