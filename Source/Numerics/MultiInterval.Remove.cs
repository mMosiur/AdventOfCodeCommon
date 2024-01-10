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
				break;
			}

			anythingRemoved = true;

			var (untouchedLeft, untouchedRight) = currentInterval.WithRemoved(intervalToRemove);

			if (untouchedLeft is null && untouchedRight is null)
			{
				_intervals.RemoveAt(i);
				i--;
			}
			else if (untouchedLeft is not null && untouchedRight is null)
			{
				_intervals[i] = untouchedLeft.Value;
			}
			else if (untouchedLeft is null && untouchedRight is not null)
			{
				_intervals[i] = untouchedRight.Value;
			}
			else
			{
				_intervals[i] = untouchedLeft!.Value;
				i++;
				_intervals.Insert(i, untouchedRight!.Value);
			}
		}

		return anythingRemoved;
	}
}
