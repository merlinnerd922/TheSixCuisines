﻿#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace Extend
{

    /// <summary>A class containing extension methods for determining membership of a collection.</summary>
    public static class MemberUtils
    {

		/// <summary>Return true if the given object (item) is equal to each item in (comparedItems), and false otherwise.</summary>
		/// <typeparam name="T">The type of the item in the list.</typeparam>
		/// <param name="item">The item we're cross-checking against others.</param>
		/// <param name="comparedItems">The list of items we're checking (item) against.</param>
		/// <returns>true iff the given item is in the list (comparedItems).</returns>
		public static bool IsAll<T>(this T item, params T[] comparedItems)
        {
            return comparedItems.All(t => t.Equals(item));
        }

		/// <summary>
		///     Return true if the given object <paramref name="item" /> is in the sequence
		///     <paramref
		///         name="comparedItems" />
		///     , and false otherwise.
		/// </summary>
		/// <typeparam name="T">The type of the element we're checking for to be in <paramref name="comparedItems" />.</typeparam>
		/// <param name="item">The item we're checking to be in <paramref name="comparedItems" /></param>
		/// <param name="comparedItems">The array of items we're checking for <paramref name="item" />.</param>
		/// <returns>The <see cref="bool" />.</returns>
		public static bool IsAny<T>(this T item, List<T> comparedItems)
        {
            return item.IsAny(comparedItems.ToArray() as IEnumerable<T>);
        }

		/// <summary>
		///     Given a sequence of parameters of a type <typeparamref name="T" /> and an item of type
		///     <typeparamref name="T" />, return true iff the item is in the sequence.
		/// </summary>
		/// <typeparam name="T">The type of the item in the given sequence <paramref name="sequence" />.</typeparam>
		/// <param name="item">The item we're checking to be in the sequence <paramref name="sequence" />.</param>
		/// <param name="sequence">The sequence we're checking for the item.</param>
		/// <returns>true iff <paramref name="item" /> is in the given sequence.</returns>
		public static bool IsAny<T>(this T item, params T[] sequence)
        {
            return new List<T>(sequence).Contains(item);
        }

		/// <summary>
		///     Given a sequence of items of a type <typeparamref name="T" /> and an item of type <typeparamref name="T" />,
		///     return true if the item is in the sequence, and false otherwise.
		/// </summary>
		/// <typeparam name="T">The type of elements in the sequence <paramref name="sequence" />.</typeparam>
		/// <param name="item">The item we're checking to be in the sequence <paramref name="sequence" />.</param>
		/// <param name="sequence">The sequence we're checking for the item <paramref name="sequence" />.</param>
		/// <returns>The <see cref="bool" />.</returns>
		public static bool IsAny<T>(this T item, IEnumerable<T> sequence)
        {
            return new List<T>(sequence).Contains(item);
        }

		/// <summary>
		///     Given a sequence of items of a type <typeparamref name="T" /> and an item of type <typeparamref name="T" />,
		///     return true iff the item is absent from the sequence.
		/// </summary>
		/// <typeparam name="T">The type of the object in <paramref name="sequence" />.</typeparam>
		/// <param name="item">The item we're checking to be in the sequence <paramref name="sequence" />.</param>
		/// <param name="sequence">The sequence we're checking for the item <paramref name="item" />.</param>
		/// <returns>true iff the item is absent from the sequence.</returns>
		public static bool IsNotAny<T>(this T item, IEnumerable<T> sequence)
        {
            return !item.IsAny(sequence);
        }

		/// <summary>
		///     Given a sequence of parameters of a type <typeparamref name="T" /> and an item of type
		///     <typeparamref name="T" />, return false if the item is in the sequence, and false otherwise.
		/// </summary>
		/// <typeparam name="T">The type of the parameter to check to be nonexistent in the sequence.</typeparam>
		/// <param name="item">The item</param>
		/// <param name="sequence">The sequence.</param>
		/// <returns>The <see cref="bool" />.</returns>
		public static bool IsNotAny<T>(this T item, params T[] sequence)
        {
            return !item.IsAny(sequence as IEnumerable<T>);
        }

    }

}