namespace AdventOfCode.Common.Numerics;

public sealed partial class MultiInterval<T>
{
	/// <summary>
	/// Removes a specified interval of values from this MultiInterval.
	/// </summary>
	/// <param name="intervalToRemove">The interval to remove from this MultiInterval.</param>
	/// <returns><see langword="true"/> if anything was removed, <see langword="false"/> otherwise.</returns>
	/// <remarks>
	/// Note that removing an interval strongly interferes with the internal structure of the MultiInterval,
	/// it may fully remove some stored intervals and split others into two.
	/// </remarks>
	public bool Remove(Interval<T> intervalToRemove)
	{
		bool anythingRemoved = false;
		for (int i = 0; i < _intervals.Count; i++)
		{
			var currentInterval = _intervals[i];

			if (currentInterval.End < intervalToRemove.Start)
			{
				continue;
			}

			if (currentInterval.Start > intervalToRemove.End)
			{
				return anythingRemoved;
			}

			anythingRemoved = true;

			var (untouchedLeft, untouchedRight) = currentInterval.WithRemoved(intervalToRemove);

			switch (untouchedLeft.IsEmpty, untouchedRight.IsEmpty)
			{
				case (true, true):
					_intervals.RemoveAt(i);
					i--;
					break;
				case (false, true):
					_intervals[i] = untouchedLeft;
					break;
				case (true, false):
					_intervals[i] = untouchedRight;
					break;
				case (false, false):
					_intervals[i] = untouchedLeft;
					i++;
					_intervals.Insert(i, untouchedRight);
					break;
			}
		}

		return anythingRemoved;
	}
}
