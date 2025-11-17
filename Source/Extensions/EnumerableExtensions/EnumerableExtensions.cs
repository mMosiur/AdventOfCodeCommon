using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Common.EnumerableExtensions;

/// <summary>
/// Provides extension methods for <see cref="Enumerable"/> and <see cref="IEnumerable{T}"/> types.
/// </summary>
public static class EnumerableExtensions
{
	/// <param name="source">A sequence of values to invoke a transform function on.</param>
	/// <typeparam name="T">The type of items in source sequence.</typeparam>
	extension<T>(IEnumerable<T> source)
	{
		/// <summary>
		/// Projects each element of a sequence into a tuple of that item with its index.
		/// </summary>
		public IEnumerable<(T Item, int Index)> WithIndex()
		{
			return source.Select((item, index) => (item, index));
		}

		/// <summary>
		/// Splits a sequence into groups of a specified size.
		/// </summary>
		/// <param name="groupSize">The size of the resulting groups.</param>
		/// <param name="allowIncompleteLastGroup">Whether to accept the last group if it is not full or throw instead.</param>
		/// <exception cref="ArgumentNullException">Source sequence is <see langword="null"/>.</exception>
		/// <exception cref="InvalidOperationException">The last group is incomplete and <paramref name="allowIncompleteLastGroup"/> is <see langword="false"/>.</exception>
		public IEnumerable<IReadOnlyList<T>> SplitIntoGroups(int groupSize, bool allowIncompleteLastGroup = true)
		{
			ArgumentNullException.ThrowIfNull(source);
			List<T> group = new(groupSize);
			foreach (T item in source)
			{
				group.Add(item);
				if (group.Count == groupSize)
				{
					yield return group;
					group = new(groupSize);
				}
			}
			if (group.Count > 0)
			{
				if (allowIncompleteLastGroup)
				{
					yield return group;
				}
				else
				{
					throw new InvalidOperationException("The last group is incomplete.");
				}
			}
		}
	}

	/// <param name="source">A sequence of values to invoke a transform function on.</param>
	/// <typeparam name="T">The type of items in source sequence that is <see cref="IEquatable{TSource}"/> to itself.</typeparam>
	extension<T>(IEnumerable<T> source) where T : IEquatable<T>
	{
		/// <summary>
		/// Splits a sequence into groups in between separator items.
		/// </summary>
		/// <param name="separator">The separator item on which the source is split into groups.</param>
		/// <exception cref="ArgumentNullException">Source sequence or <paramref name="separator"/> is <see langword="null"/>.</exception>
		public IEnumerable<IReadOnlyList<T>> SplitWithSeparator(T separator)
		{
			ArgumentNullException.ThrowIfNull(source);
			ArgumentNullException.ThrowIfNull(separator);
			List<T> group = new();
			foreach (T item in source)
			{
				if (separator.Equals(item))
				{
					yield return group;
					group = new();
				}
				else
				{
					group.Add(item);
				}
			}
			if (group.Count > 0)
			{
				yield return group;
			}
		}
	}

}
