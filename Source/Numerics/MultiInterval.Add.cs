using System;
using System.Collections.Generic;

namespace AdventOfCode.Common.Numerics;

public sealed partial class MultiInterval<T>
{
	/// <summary>
	/// Adds the specified interval to this MultiInterval, merging it with any overlapping or touching intervals.
	/// </summary>
	/// <param name="interval">The interval to be added to this MultiInterval</param>
	/// <remarks>
	/// Note that adding and interval to MultiInterval may actually result in lowering the count of intervals stored,
	/// as the added interval may merge with one or more existing intervals.
	/// </remarks>
	public void Add(Interval<T> interval)
	{
		if (interval.IsEmpty) return;
		T newIntervalStart = interval.Start;
		T newIntervalEnd = interval.End;

		int index = 0;
		// Find the position to insert and handle merging as needed
		while (index < _intervals.Count && _intervals[index].End + T.One < interval.Start)
		{
			index++;
		}

		int intervalsToRemove = 0;
		int firstIndexToRemove = -1;
		// Check for overlap and merge if needed
		while (index < _intervals.Count && _intervals[index].Start <= interval.End)
		{
			newIntervalStart = T.Min(newIntervalStart, _intervals[index].Start);
			newIntervalEnd = T.Max(newIntervalEnd, _intervals[index].End);

			if (firstIndexToRemove == -1) firstIndexToRemove = index;
			intervalsToRemove++;

			index++;
		}

		// Remove all overlapping intervals at once
		if (intervalsToRemove > 0) _intervals.RemoveRange(firstIndexToRemove, intervalsToRemove);

		// Insert the merged or original interval at the appropriate position
		_intervals.Insert(index - intervalsToRemove, new(newIntervalStart, newIntervalEnd));
	}

	/// <summary>
	/// Adds all intervals from specified collection to this MultiInterval, merging them with any overlapping or touching intervals.
	/// </summary>
	/// <param name="intervals">The collection of intervals to be added to this MultiInterval</param>
	/// <remarks>
	/// Note that adding any amount of intervals to MultiInterval may actually result in lowering the count of intervals stored,
	/// as the added intervals may merge with one or more existing intervals.
	/// </remarks>
	/// <exception cref="ArgumentNullException">Thrown when <paramref name="intervals"/> collection is null.</exception>
	public void Add(IEnumerable<Interval<T>> intervals)
	{
		ArgumentNullException.ThrowIfNull(intervals);
		foreach (Interval<T> interval in intervals)
		{
			Add(interval);
		}
	}
}
