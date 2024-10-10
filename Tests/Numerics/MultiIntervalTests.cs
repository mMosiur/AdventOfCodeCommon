using AdventOfCode.Common.Numerics;

namespace AdventOfCode.Common.Tests.Numerics;

public class MultiIntervalTests
{
	[Theory]
	[MemberData(nameof(MultiIntervalAddTestData))]
	public void Add_ShouldHandleAddingCorrectly(Interval<int>[] initialIntervals, Interval<int> intervalToAdd, Interval<int>[] expected)
	{
		MultiInterval<int> multiInterval = new(initialIntervals);

		multiInterval.Add(intervalToAdd);

		Assert.True(
			multiInterval.SequenceEqual(expected),
			$"Initial: ({string.Join(", ", initialIntervals)}); Added: {intervalToAdd}; Expected: ({string.Join(", ", expected)}); Actual: ({string.Join(", ", multiInterval)})"
		);
	}

	public static TheoryData<Interval<int>[], Interval<int>, Interval<int>[]> MultiIntervalAddTestData = new()
	{
		{
			new Interval<int>[] { new(1, 2), new(5, 6), new(9, 11) },
			new Interval<int>(0, 7),
			new Interval<int>[] { new(0, 7), new(9, 11) }
		},
		{
			new Interval<int>[] { new(1, 5), new(7, 10) },
			new Interval<int>(12, 15),
			new Interval<int>[] { new(1, 5), new(7, 10), new(12, 15) }
		},
		{
			new Interval<int>[] { new(1, 3), new(5, 8), new(10, 12) },
			new Interval<int>(4, 6),
			new Interval<int>[] { new(1, 8), new(10, 12) }
		},
		{
			new Interval<int>[] { new(1, 5), new(7, 10) },
			new Interval<int>(4, 6),
			new Interval<int>[] { new(1, 6), new(7, 10) }
		},
		{
			new Interval<int>[] { new(1, 3), new(5, 8) },
			new Interval<int>(4, 6),
			new Interval<int>[] { new(1, 8) }
		},
	};

	[Theory]
	[MemberData(nameof(MultiIntervalRemoveTestData))]
	public void Remove_ShouldHandleRemovingCorrectly(Interval<int>[] initialIntervals, Interval<int> intervalToRemove, Interval<int>[] expectedInterval, bool expectedReturn)
	{
		MultiInterval<int> multiInterval = new(initialIntervals);

		bool removed = multiInterval.Remove(intervalToRemove);

		Assert.True(
			multiInterval.SequenceEqual(expectedInterval),
			$"Initial: ({string.Join(", ", initialIntervals)}); Removed: {intervalToRemove}; Expected: ({string.Join(", ", expectedInterval)}); Actual: ({string.Join(", ", multiInterval)})"
		);
		Assert.Equal(expectedReturn, removed);
	}

	public static TheoryData<Interval<int>[], Interval<int>, Interval<int>[], bool> MultiIntervalRemoveTestData = new()
	{
		{
			new Interval<int>[] { new(1, 5), new(7, 10), new(12, 15) },
			new Interval<int>(4, 8),
			new Interval<int>[] { new(1, 3), new(9, 10), new(12, 15) },
			true
		},
		{
			new Interval<int>[] { new(1, 3), new(6, 8) },
			new Interval<int>(2, 7),
			new Interval<int>[] { new(1, 1), new(8, 8) },
			true
		},
		{
			new Interval<int>[] { new(1, 8), new(10, 12) },
			new Interval<int>(3, 5),
			new Interval<int>[] { new(1, 2), new(6, 8), new(10, 12) },
			true
		},
		{
			new Interval<int>[] { new(1, 2), new(4, 6), new(10, 15) },
			new Interval<int>(7, 12),
			new Interval<int>[] { new(1, 2), new(4, 6), new(13, 15) },
			true
		},
		{
			new Interval<int>[] { new(1, 2), new(4, 6), new(10, 15) },
			new Interval<int>(7, 9),
			new Interval<int>[] { new(1, 2), new(4, 6), new(10, 15) },
			false
		},
		{
			new Interval<int>[] { new(1, 6), new(10, 12) },
			new Interval<int>(2, 5),
			new Interval<int>[] { new(1, 1), new(6, 6), new(10, 12) },
			true
		},
		{
			new Interval<int>[] { new(1, 6), new(10, 12) },
			new Interval<int>(2, 7),
			new Interval<int>[] { new(1, 1), new(10, 12) },
			true
		},
		{
			new Interval<int>[] { new(1, 2), new(4, 6), new(10, 12) },
			new Interval<int>(2, 11),
			new Interval<int>[] { new(1, 1), new(12, 12) },
			true
		},
		{
			new Interval<int>[] { new(1, 5), new(7, 10), new(12, 15) },
			new Interval<int>(0, 16),
			new Interval<int>[] { },
			true
		},
		{
			new Interval<int>[] { new(1, 3), new(6, 8) },
			new Interval<int>(0, 0),
			new Interval<int>[] { new(1, 3), new(6, 8) },
			false
		},
		{
			new Interval<int>[] { new(1, 8), new(10, 12) },
			new Interval<int>(9, 9),
			new Interval<int>[] { new(1, 8), new(10, 12) },
			false
		},
		{
			new Interval<int>[] { new(1, 2), new(4, 6), new(10, 15) },
			new Interval<int>(3, 3),
			new Interval<int>[] { new(1, 2), new(4, 6), new(10, 15) },
			false
		},
		{
			new Interval<int>[] { new(1, 2), new(4, 6), new(10, 15) },
			new Interval<int>(0, 16),
			new Interval<int>[] { },
			true
		},
		{
			new Interval<int>[] { new(1, 6), new(10, 12) },
			new Interval<int>(7, 9),
			new Interval<int>[] { new(1, 6), new(10, 12) },
			false
		},
		{
			new Interval<int>[] { new(1, 6), new(10, 12) },
			new Interval<int>(6, 10),
			new Interval<int>[] { new(1, 5), new(11, 12) },
			true
		},
		{
			new Interval<int>[] { new(1, 2), new(4, 6), new(10, 12) },
			new Interval<int>(3, 10),
			new Interval<int>[] { new(1, 2), new(11, 12) },
			true
		},
	};

	[Theory]
	[MemberData(nameof(MultiIntervalAddEmptyIntervalTestData))]
	public void Add_EmptyInterval_ShouldNotChangeMultiInterval(Interval<int>[] initialIntervals)
	{
		MultiInterval<int> multiInterval = new(initialIntervals);

		multiInterval.Add(Interval<int>.Empty);

		Assert.True(
			multiInterval.SequenceEqual(initialIntervals),
			$"Initial: ({string.Join(", ", initialIntervals)}); expected it to not change after adding Empty interval, actual result: ({string.Join(", ", multiInterval)})"
		);
	}

	public static TheoryData<Interval<int>[]> MultiIntervalAddEmptyIntervalTestData = new()
	{
		new Interval<int>[] { new(1, 2), new(5, 6), new(9, 11) },
		new Interval<int>[] { new(1, 5), new(7, 10) },
		new Interval<int>[] { new(1, 3), new(5, 8), new(10, 12) },
		new Interval<int>[] { new(1, 5), new(7, 10) },
		new Interval<int>[] { new(1, 3), new(5, 8) },
	};

	[Theory]
	[MemberData(nameof(MultiIntervalRemoveEmptyIntervalTestData))]
	public void Remove_EmptyInterval_ShouldNotChangeMultiInterval(Interval<int>[] initialIntervals)
	{
		MultiInterval<int> multiInterval = new(initialIntervals);

		bool removed = multiInterval.Remove(Interval<int>.Empty);

		Assert.True(
			multiInterval.SequenceEqual(initialIntervals),
			$"Initial: ({string.Join(", ", initialIntervals)}); expected it to not change after removing Empty interval, actual result: ({string.Join(", ", multiInterval)})"
		);
		Assert.False(removed);
	}

	public static TheoryData<Interval<int>[]> MultiIntervalRemoveEmptyIntervalTestData = new()
	{
		new Interval<int>[] { new(1, 5), new(7, 10), new(12, 15) },
		new Interval<int>[] { new(1, 3), new(6, 8) },
		new Interval<int>[] { new(1, 8), new(10, 12) },
		new Interval<int>[] { new(1, 2), new(4, 6), new(10, 15) },
		new Interval<int>[] { new(1, 2), new(4, 6), new(10, 15) },
		new Interval<int>[] { new(1, 6), new(10, 12) },
		new Interval<int>[] { new(1, 6), new(10, 12) },
		new Interval<int>[] { new(1, 2), new(4, 6), new(10, 12) },
		new Interval<int>[] { new(1, 5), new(7, 10), new(12, 15) },
		new Interval<int>[] { new(1, 3), new(6, 8) },
		new Interval<int>[] { new(1, 8), new(10, 12) },
		new Interval<int>[] { new(1, 2), new(4, 6), new(10, 15) },
		new Interval<int>[] { new(1, 2), new(4, 6), new(10, 15) },
		new Interval<int>[] { new(1, 6), new(10, 12) },
		new Interval<int>[] { new(1, 6), new(10, 12) },
		new Interval<int>[] { new(1, 2), new(4, 6), new(10, 12) },
	};
}
