using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;

namespace AdventOfCode.Common.Numerics;

/// <summary>
/// Represents a set of intervals in their most reduced form, where no two are overlapping or creating a continuous range.
/// </summary>
/// <typeparam name="T">The type of the interval endpoints.</typeparam>
public sealed partial class MultiInterval<T>
	: IReadOnlyCollection<Interval<T>>, ICloneable
	where T : unmanaged, IBinaryInteger<T>
{
	private readonly List<Interval<T>> _intervals;

	/// <summary>
	/// Gets the number of intervals in the collection.
	/// </summary>
	public int Count => _intervals.Count;

	/// <summary>
	/// Initializes a new <see cref="MultiInterval{T}"/> instance that is empty.
	/// </summary>
	public MultiInterval()
	{
		_intervals = new();
	}

	/// <summary>
	/// Initializes a new <see cref="MultiInterval{T}"/> instance that is empty and has the specified initial capacity interval.
	/// </summary>
	/// <param name="expectedCount">The number of elements that the underlying list can initially store.</param>
	public MultiInterval(int expectedCount)
	{
		_intervals = new(expectedCount);
	}

	/// <summary>
	/// Initializes a new <see cref="MultiInterval{T}"/> instance from single provided interval.
	/// </summary>
	/// <param name="range">The interval to add to the collection.</param>
	public MultiInterval(Interval<T> range)
		: this(1)
	{
		if (range.IsEmpty) return;
		_intervals.Add(range);
	}

	/// <summary>
	/// Initializes a new <see cref="MultiInterval{T}"/> instance that contains elements copied from the specified MultiInterval.
	/// </summary>
	/// <param name="other">The <see cref="MultiInterval{T}"/> whose elements are copied into the new one.</param>
	private MultiInterval(MultiInterval<T> other)
	{
		_intervals = new(other._intervals);
	}

	/// <summary>
	/// Initializes a new <see cref="MultiInterval{T}"/> instance that contains elements copied from the specified collection of intervals.
	/// </summary>
	/// <param name="ranges">The collection whose elements are copied to the new list.</param>
	public MultiInterval(IEnumerable<Interval<T>> ranges)
		: this(ranges.TryGetNonEnumeratedCount(out int count) ? count : 2)
	{
		foreach (var range in ranges)
		{
			if (range.IsEmpty) continue;
			Add(range);
		}
	}

	/// <inheritdoc/>
	[Pure]
	public IEnumerator<Interval<T>> GetEnumerator() => _intervals.GetEnumerator();

	/// <inheritdoc/>
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	/// <summary>
	/// Creates a new <see cref="MultiInterval{T}"/> that is an exact copy of the current instance.
	/// </summary>
	/// <returns>A new <see cref="MultiInterval{T}"/> that is a copy of this instance</returns>
	public MultiInterval<T> Clone() => new(this);

	/// <inheritdoc/>
	object ICloneable.Clone() => Clone();

	/// <inheritdoc/>
	public override string ToString() => $"[{string.Join(", ", _intervals)}]";
}
