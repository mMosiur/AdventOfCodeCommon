using System;

namespace AdventOfCode.Common.SpanExtensions;

/// <summary>
/// Miscellaneous extensions for <see cref="Span{T}"/> and <see cref="ReadOnlySpan{T}"/>.
/// </summary>
public static class SpanExtensions
{
	/// <summary>
	/// Counts the number of occurrences of a value in a <paramref name="span"/>.
	/// </summary>
	/// <param name="span">The span to be searched.</param>
	/// <param name="value">the value to be counted.</param>
	/// <typeparam name="T">The type of the elements in the span.</typeparam>
	/// <returns>The number of occurrences of given value in the span.</returns>
	public static int Count<T>(this ReadOnlySpan<T> span, T value)
		where T : IEquatable<T>
	{
		int count = 0;
		foreach (var item in span)
		{
			if (item.Equals(value))
			{
				count++;
			}
		}

		return count;
	}

	/// <inheritdoc cref="Count{T}(ReadOnlySpan{T}, T)" />
	public static int Count<T>(this Span<T> span, T value)
		where T : IEquatable<T>
	{
		return Count((ReadOnlySpan<T>)span, value);
	}

	public static bool Contains<T>(this ReadOnlySpan<T> span, T value)
		where T : IEquatable<T>
	{
		foreach (var item in span)
		{
			if (item.Equals(value))
			{
				return true;
			}
		}

		return false;
	}

	public static bool Contains<T>(this Span<T> span, T value)
		where T : IEquatable<T>
	{
		return Contains((ReadOnlySpan<T>)span, value);
	}
}
