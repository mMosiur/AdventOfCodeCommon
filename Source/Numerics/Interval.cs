using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace AdventOfCode.Common.Numerics;

/// <summary>
/// Represents an interval (range) of integers.
/// </summary>
/// <seealso href="https://en.wikipedia.org/wiki/Interval_(mathematics)">Interval (mathematics)</seealso>
public readonly partial struct Interval<T>
	: IEnumerable<T>, IEquatable<Interval<T>>
	where T : unmanaged, IBinaryInteger<T>
{

	/// <summary>The first value of the interval (inclusive).</summary>
	public T Start { get; }

	/// <summary>The last value of the interval (inclusive).</summary>
	public T End { get; }

	/// <summary>The count of integer values included in the interval.</summary>
	public T Count => End - Start + T.One;

	/// <summary>
	/// Initializes interval with the specified start and end values.
	/// </summary>
	/// <param name="start">The start value of the interval (inclusive).</param>
	/// <param name="end">The end value of the interval (inclusive).</param>
	/// <exception cref="ArgumentOutOfRangeException">Throws when <paramref name="end"/> is smaller than <paramref name="start"/>.</exception>
	public Interval(T start, T end)
	{
		if (end < start)
		{
			throw new ArgumentOutOfRangeException(nameof(end), "The end of the interval must be greater than or equal to the start.");
		}
		Start = start;
		End = end;
	}

	/// <summary>
	/// Creates an interval using the specified start and count values.
	/// </summary>
	/// <param name="start">The start value of the interval (inclusive).</param>
	/// <param name="end">The end value of the interval (inclusive).</param>
	/// <returns></returns>
	public static Interval<T> FromStartEnd(T start, T end) => new(start, end);

	/// <summary>
	/// Create an interval using the specified start value and desired count of values included in the interval.
	/// </summary>
	/// <param name="start">The start value of the interval.</param>
	/// <param name="count">The count of integer values that should be included in the interval.</param>
	/// <returns></returns>
	public static Interval<T> FromStartCount(T start, T count) => new(start, start + count - T.One);

	/// <summary>
	/// Determines whether the <see cref="Interval{T}"/> contains a specific value.
	/// </summary>
	/// <param name="value">The value to find in the interval.</param>
	public bool Contains(T value)
	{
		return Start <= value && value <= End;
	}

	/// <summary>
	/// Determines whether this <see cref="Interval{T}"/> fully contains specified <paramref name="interval"/>.
	/// </summary>
	/// <remarks>
	/// This method is an alias for <see cref="IsSupersetOf"/> which should be used instead as it's more descriptive.
	/// </remarks>
	public bool Contains(Interval<T> interval)
	{
		return IsSupersetOf(interval);
	}

	/// <summary>
	/// Determines whether this <see cref="Interval{T}"/> is a superset of a specified <paramref name="interval"/>.
	/// </summary>
	/// <param name="interval">The interval to be checked.</param>
	/// <seealso href="https://en.wikipedia.org/wiki/Superset">Proper superset</seealso>
	public bool IsSupersetOf(Interval<T> interval)
	{
		return Start <= interval.Start && interval.End <= End;
	}

	/// <summary>
	/// Determines whether this <see cref="Interval{T}"/> is a proper superset of a specified <paramref name="interval"/>.
	/// </summary>
	/// <param name="interval">The interval to be checked.</param>
	/// <seealso href="https://en.wikipedia.org/wiki/Superset">Proper superset</seealso>
	public bool IsProperSupersetOf(Interval<T> interval)
	{
		return IsSupersetOf(interval) && !Equals(interval);
	}

	/// <summary>
	/// Determines whether this <see cref="Interval{T}"/> is a subset of a specified <paramref name="interval"/>.
	/// </summary>
	/// <param name="interval">The interval to be checked.</param>
	/// <seealso href="https://en.wikipedia.org/wiki/Subset">Subset</seealso>
	public bool IsSubsetOf(Interval<T> interval)
	{
		return interval.Start <= Start && End <= interval.End;
	}

	/// <summary>
	/// Determines whether this <see cref="Interval{T}"/> is a proper subset of a specified <paramref name="interval"/>.
	/// </summary>
	/// <param name="interval">The interval to be checked.</param>
	/// <seealso href="https://en.wikipedia.org/wiki/Subset">Proper subset</seealso>
	public bool IsProperSubsetOf(Interval<T> interval)
	{
		return IsSubsetOf(interval) && !Equals(interval);
	}

	/// <summary>
	/// Determines whether this <see cref="Interval{T}"/> overlaps the specified <paramref name="interval"/>.
	/// </summary>
	/// <param name="interval">The interval to be checked.</param>
	/// <seealso href="https://en.wikipedia.org/wiki/Intersection_(set_theory)">Intersection</seealso>
	public bool Overlaps(Interval<T> interval)
	{
		return interval.Start <= End && Start <= interval.End;
	}

	/// <summary>
	/// Calculates the intersection of this <see cref="Interval{T}"/> with the specified <paramref name="other"/> <see cref="Interval{T}"/>.
	/// </summary>
	/// <param name="other">The other <see cref="Interval{T}"/> to be intersected with</param>
	/// <returns>An <see cref="Interval{T}"/> containing the intersection of this interval with the <paramref name="other"/> or <see langword="null"/> if the intervals do not intersect.</returns>
	public Interval<T>? IntersectedWith(Interval<T> other)
	{
		T start = T.Max(Start, other.Start);
		T end = T.Min(End, other.End);
		return start <= end ? new(start, end) : null;
	}

	/// <summary>
	/// Calculates the intervals left after removing another interval values from this one.
	/// </summary>
	/// <param name="other">The other <see cref="Interval{T}"/> with values to be removed</param>
	/// <returns>
	/// A tuple that contains two intervals <see cref="Interval{T}"/> intervals:
	/// the first one (Left) which contains the interval of values from this interval that are smaller than the <paramref name="other"/> interval,
	/// and the second one (Right) which contains the interval of values from this interval that are greater than the <paramref name="other"/> interval.
	/// Either one can be <see langword="null"/> if there are no values that are a part of this interval and not a part of the <paramref name="other"/> interval.
	/// </returns>
	public (Interval<T>? Left, Interval<T>? Right) WithRemoved(Interval<T> other)
	{
		if (Start < other.Start)
		{
			if (End < other.Start)
			{
				return (this, null);
			}

			if (End <= other.End)
			{
				return (new(Start, other.Start - T.One), null);
			}

			return (new(Start, other.Start - T.One), new(other.End + T.One, End));
		}

		if (Start > other.End)
		{
			return (this, null);
		}

		if (End <= other.End)
		{
			return (null, null);
		}

		return (null, new(other.End + T.One, End));
	}

	/// <summary>
	/// Moves this <see cref="Interval{T}"/> by the specified <paramref name="offset"/>.
	/// </summary>
	/// <param name="offset">The offset by which the interval should be moved.</param>
	/// <returns>A new <see cref="Interval{T}"/> with the same size as this one but moved by the specified <paramref name="offset"/>.</returns>
	public Interval<T> MovedBy(long offset)
	{
		var newStartLong = long.CreateChecked(Start) + offset;
		var newStartT = T.CreateChecked(newStartLong);
		var newEndLong = long.CreateChecked(End) + offset;
		var newEndT = T.CreateChecked(newEndLong);
		return new(newStartT, newEndT);
	}

	/// <summary>
	/// Determines whether this <see cref="Interval{T}"/> is equal to the specified <paramref name="other"/> interval.
	/// </summary>
	/// <param name="other">The seconds interval for equality check.</param>
	public bool Equals(Interval<T> other)
	{
		return Start == other.Start && End == other.End;
	}

	/// <inheritdoc/>
	public override bool Equals(object? obj)
	{
		return obj is Interval<T> interval && Equals(interval);
	}

	public static bool operator ==(Interval<T> left, Interval<T> right) => left.Equals(right);

	public static bool operator !=(Interval<T> left, Interval<T> right) => !(left == right);

	/// <inheritdoc/>
	public override int GetHashCode()
	{
		return HashCode.Combine(Start, End);
	}

	/// <summary>
	/// Get an enumerator that iterates through the <see cref="Interval{T}"/> values.
	/// </summary>
	public IEnumerator<T> GetEnumerator()
	{
		for (T i = Start; i <= End; i++)
		{
			yield return i;
		}
	}

	/// <inheritdoc/>
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	/// <inheritdoc/>
	public override string ToString()
	{
		return $"<{Start}; {End}>";
	}

}
