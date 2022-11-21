using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Common.EnumerableExtensions;

/// <summary>
/// Provides extension methods for <see cref="Enumerable"/> and <see cref="IEnumerable{T}"/> types.
/// </summary>
public static class EnumerableExtensions
{
	/// <summary>
	/// Projects each element of a sequence into a tuple of that item with its index.
	/// </summary>
	/// <param name="source">A sequence of values to invoke a transform function on.</param>
	/// <typeparam name="T">The type of items in source sequence.</typeparam>
	public static IEnumerable<(T Item, int Index)> WithIndex<T>(this IEnumerable<T> source)
	{
		return source.Select((item, index) => (item, index));
	}
}
