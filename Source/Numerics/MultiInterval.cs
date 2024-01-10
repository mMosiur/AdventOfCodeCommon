using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;

namespace AdventOfCode.Common.Numerics;

public sealed partial class MultiInterval<T>
	: IReadOnlyCollection<Interval<T>>, ICloneable
	where T : unmanaged, IBinaryInteger<T>
{
	private readonly List<Interval<T>> _intervals;

	public int Count => _intervals.Count;

	public MultiInterval()
	{
		_intervals = new();
	}

	public MultiInterval(int expectedCount)
	{
		_intervals = new(expectedCount);
	}

	public MultiInterval(Interval<T> range)
		: this(1)
	{
		_intervals.Add(range);
	}

	private MultiInterval(MultiInterval<T> other)
	{
		_intervals = new(other._intervals);
	}

	public MultiInterval(IEnumerable<Interval<T>> ranges)
		: this(ranges.TryGetNonEnumeratedCount(out int count) ? count : 2)
	{
		foreach (var range in ranges)
		{
			Add(range);
		}
	}

	[Pure]
	public IEnumerator<Interval<T>> GetEnumerator() => _intervals.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


	public MultiInterval<T> Clone() => new(this);

	object ICloneable.Clone() => Clone();

	public override string ToString() => $"[{string.Join(", ", _intervals)}]";
}
