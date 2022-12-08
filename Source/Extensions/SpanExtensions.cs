using System;

namespace AdventOfCode.Common.SpanExtensions;

/// <summary>
/// Extension methods for <see cref="Span{T}"/> and <see cref="ReadOnlySpan{T}"/>.
/// </summary>
public static class SpanExtensions
{

	/// <summary>
	/// Returns a <see cref="SpanSplit{T}"/> that can be used to split the <paramref name="span"/> into
	/// parts separated by the specified <typeparamref name="T"/> <paramref name="separator"/>.
	/// </summary>
	/// <param name="span">The span to be split.</param>
	/// <param name="separator">The separator to be used to split the span.</param>
	/// <typeparam name="T">The type of the elements in the span.</typeparam>
	/// <returns>The <see cref="SpanSplit{T}"/> that allows enumeration of the split.</returns>
	public static SpanSplit<T> Split<T>(this ReadOnlySpan<T> span, T separator)
		where T : IEquatable<T>
	{
		return new(span, separator);
	}

	/// <summary>
	/// Returns a <see cref="SpanSplit{T}"/> that can be used to split the <paramref name="span"/> into
	/// parts separated by the specified <typeparamref name="T"/> <paramref name="separator"/>.
	/// </summary>
	/// <param name="span">The span to be split.</param>
	/// <param name="separator">The separator to be used to split the span.</param>
	/// <typeparam name="T">The type of the elements in the span.</typeparam>
	/// <returns>The <see cref="SpanSplit{T}"/> that allows enumeration of the split.</returns>
	public static SpanSplit<T> Split<T>(this Span<T> span, T separator)
		where T : IEquatable<T>
	{
		return new(span, separator);
	}

	/// <summary>
	/// Splits given <see cref="ReadOnlySpan{T}"/> <paramref name="span"/> in exactly two parts
	/// with the specified <typeparamref name="T"/> <paramref name="separator"/>.
	/// </summary>
	/// <param name="span">The <see cref="ReadOnlySpan{T}"/> to be split.</param>
	/// <param name="separator">The value used to split the span.</param>
	/// <param name="first">The first part of the split.</param>
	/// <param name="second">The second part of the split.</param>
	/// <param name="allowMultipleSeparators">
	/// A flag that determines whether to treat more than one separator as an error (when <see langword="false"/>),
	/// or to include it in the second span (when <see langword="true"/>).
	/// </param>
	/// <typeparam name="T">The type of the span items.</typeparam>
	/// <returns>Whether the split was successful.</returns>
	public static bool TrySplitInTwo<T>(this ReadOnlySpan<T> span, T separator, out ReadOnlySpan<T> first, out ReadOnlySpan<T> second, bool allowMultipleSeparators = false)
		where T : IEquatable<T>
	{
		first = default;
		second = default;
		int index = span.IndexOf(separator);
		if (index < 0)
		{
			return false;
		}
		ReadOnlySpan<T> firstSpan = span[..index];
		ReadOnlySpan<T> secondSpan = span[(index + 1)..];
		if (!allowMultipleSeparators && secondSpan.IndexOf(separator) >= 0)
		{
			return false;
		}
		first = firstSpan;
		second = secondSpan;
		return true;
	}

	/// <summary>
	/// Splits given <see cref="Span{T}"/> <paramref name="span"/> in exactly two parts
	/// with the specified <typeparamref name="T"/> <paramref name="separator"/>.
	/// </summary>
	/// <param name="span">The <see cref="Span{T}"/> to be split.</param>
	/// <param name="separator">The value used to split the span.</param>
	/// <param name="first">The first part of the split.</param>
	/// <param name="second">The second part of the split.</param>
	/// <param name="allowMultipleSeparators">
	/// A flag that determines whether to treat more than one separator as an error (when <see langword="false"/>),
	/// or to include it in the second span (when <see langword="true"/>).
	/// </param>
	/// <typeparam name="T">The type of the span items.</typeparam>
	/// <returns>Whether the split was successful.</returns>
	public static bool TrySplitInTwo<T>(this Span<T> span, T separator, out Span<T> first, out Span<T> second, bool allowMultipleSeparators = false)
		where T : IEquatable<T>
	{
		first = default;
		second = default;
		int index = span.IndexOf(separator);
		if (index < 0)
		{
			return false;
		}
		Span<T> firstSpan = span[..index];
		Span<T> secondSpan = span[(index + 1)..];
		if (!allowMultipleSeparators && secondSpan.IndexOf(separator) >= 0)
		{
			return false;
		}
		first = firstSpan;
		second = secondSpan;
		return true;
	}

	/// <summary>
	/// Splits given <see cref="ReadOnlySpan{T}"/> <paramref name="span"/> in exactly two parts
	/// with the specified <typeparamref name="T"/> <paramref name="separator"/>.
	/// </summary>
	/// <param name="span">The <see cref="ReadOnlySpan{T}"/> to be split.</param>
	/// <param name="separator">The sequence used to split the span.</param>
	/// <param name="first">The first part of the split.</param>
	/// <param name="second">The second part of the split.</param>
	/// <param name="allowMultipleSeparators">
	/// A flag that determines whether to treat more than one separator as an error (when <see langword="false"/>),
	/// or to include it in the second span (when <see langword="true"/>).
	/// </param>
	/// <typeparam name="T">The type of the span items.</typeparam>
	/// <returns>Whether the split was successful.</returns>
	public static bool TrySplitInTwo<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> separator, out ReadOnlySpan<T> first, out ReadOnlySpan<T> second, bool allowMultipleSeparators = false)
		where T : IEquatable<T>
	{
		first = default;
		second = default;
		int index = span.IndexOf(separator);
		if (index < 0)
		{
			return false;
		}
		ReadOnlySpan<T> firstSpan = span[..index];
		ReadOnlySpan<T> secondSpan = span[(index + separator.Length)..];
		if (!allowMultipleSeparators && secondSpan.IndexOf(separator) >= 0)
		{
			return false;
		}
		first = firstSpan;
		second = secondSpan;
		return true;
	}

	/// <summary>
	/// Splits given <see cref="Span{T}"/> <paramref name="span"/> in exactly two parts
	/// with the specified <typeparamref name="T"/> <paramref name="separator"/>.
	/// </summary>
	/// <param name="span">The <see cref="Span{T}"/> to be split.</param>
	/// <param name="separator">The sequence used to split the span.</param>
	/// <param name="first">The first part of the split.</param>
	/// <param name="second">The second part of the split.</param>
	/// <param name="allowMultipleSeparators">
	/// A flag that determines whether to treat more than one separator as an error (when <see langword="false"/>),
	/// or to include it in the second span (when <see langword="true"/>).
	/// </param>
	/// <typeparam name="T">The type of the span items.</typeparam>
	/// <returns>Whether the split was successful.</returns>
	public static bool TrySplitInTwo<T>(this Span<T> span, Span<T> separator, out Span<T> first, out Span<T> second, bool allowMultipleSeparators = false)
		where T : IEquatable<T>
	{
		first = default;
		second = default;
		int index = span.IndexOf(separator);
		if (index < 0)
		{
			return false;
		}
		Span<T> firstSpan = span[..index];
		Span<T> secondSpan = span[(index + separator.Length)..];
		if (!allowMultipleSeparators && secondSpan.IndexOf(separator) >= 0)
		{
			return false;
		}
		first = firstSpan;
		second = secondSpan;
		return true;
	}

	/// <summary>
	/// Splits given <see cref="ReadOnlySpan{T}"/> <paramref name="span"/> in exactly three parts
	/// with the specified <typeparamref name="T"/> <paramref name="separator"/>.
	/// </summary>
	/// <param name="span">The <see cref="ReadOnlySpan{T}"/> to be split.</param>
	/// <param name="separator">The value used to split the span.</param>
	/// <param name="first">The first part of the split.</param>
	/// <param name="second">The second part of the split.</param>
	/// <param name="third">The third part of the split.</param>
	/// <param name="allowMultipleSeparators">
	/// A flag that determines whether to treat more than two separators as an error (when <see langword="false"/>),
	/// or to include it in the third span (when <see langword="true"/>).
	/// </param>
	/// <typeparam name="T">The type of the span items.</typeparam>
	/// <returns>Whether the split was successful.</returns>
	public static bool TrySplitInThree<T>(this ReadOnlySpan<T> span, T separator, out ReadOnlySpan<T> first, out ReadOnlySpan<T> second, out ReadOnlySpan<T> third, bool allowMultipleSeparators = false)
		where T : IEquatable<T>
	{
		first = default;
		second = default;
		third = default;
		int index = span.IndexOf(separator);
		if (index < 0)
		{
			return false;
		}
		ReadOnlySpan<T> firstSpan = span[..index];
		ReadOnlySpan<T> secondSpan = span[(index + 1)..];
		index = secondSpan.IndexOf(separator);
		if (index < 0)
		{
			return false;
		}
		ReadOnlySpan<T> thirdSpan = secondSpan[(index + 1)..];
		secondSpan = secondSpan[..index];
		if (!allowMultipleSeparators && thirdSpan.IndexOf(separator) >= 0)
		{
			return false;
		}
		first = firstSpan;
		second = secondSpan;
		third = thirdSpan;
		return true;
	}

	/// <summary>
	/// Splits given <see cref="Span{T}"/> <paramref name="span"/> in exactly three parts
	/// with the specified <typeparamref name="T"/> <paramref name="separator"/>.
	/// </summary>
	/// <param name="span">The <see cref="Span{T}"/> to be split.</param>
	/// <param name="separator">The value used to split the span.</param>
	/// <param name="first">The first part of the split.</param>
	/// <param name="second">The second part of the split.</param>
	/// <param name="third">The third part of the split.</param>
	/// <param name="allowMultipleSeparators">
	/// A flag that determines whether to treat more than two separators as an error (when <see langword="false"/>),
	/// or to include it in the third span (when <see langword="true"/>).
	/// </param>
	/// <typeparam name="T">The type of the span items.</typeparam>
	/// <returns>Whether the split was successful.</returns>
	public static bool TrySplitInThree<T>(this Span<T> span, T separator, out Span<T> first, out Span<T> second, out Span<T> third, bool allowMultipleSeparators = false)
		where T : IEquatable<T>
	{
		first = default;
		second = default;
		third = default;
		int index = span.IndexOf(separator);
		if (index < 0)
		{
			return false;
		}
		Span<T> firstSpan = span[..index];
		Span<T> secondSpan = span[(index + 1)..];
		index = secondSpan.IndexOf(separator);
		if (index < 0)
		{
			return false;
		}
		Span<T> thirdSpan = secondSpan[(index + 1)..];
		secondSpan = secondSpan[..index];
		if (!allowMultipleSeparators && thirdSpan.IndexOf(separator) >= 0)
		{
			return false;
		}
		first = firstSpan;
		second = secondSpan;
		third = thirdSpan;
		return true;
	}

	/// <summary>
	/// Splits given <see cref="ReadOnlySpan{T}"/> <paramref name="span"/> in exactly three parts
	/// with the specified <typeparamref name="T"/> <paramref name="separator"/>.
	/// </summary>
	/// <param name="span">The <see cref="ReadOnlySpan{T}"/> to be split.</param>
	/// <param name="separator">The sequence used to split the span.</param>
	/// <param name="first">The first part of the split.</param>
	/// <param name="second">The second part of the split.</param>
	/// <param name="third">The third part of the split.</param>
	/// <param name="allowMultipleSeparators">
	/// A flag that determines whether to treat more than two separators as an error (when <see langword="false"/>),
	/// or to include it in the third span (when <see langword="true"/>).
	/// </param>
	/// <typeparam name="T">The type of the span items.</typeparam>
	/// <returns>Whether the split was successful.</returns>
	public static bool TrySplitInThree<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> separator, out ReadOnlySpan<T> first, out ReadOnlySpan<T> second, out ReadOnlySpan<T> third, bool allowMultipleSeparators = false)
		where T : IEquatable<T>
	{
		first = default;
		second = default;
		third = default;
		int index = span.IndexOf(separator);
		if (index < 0)
		{
			return false;
		}
		ReadOnlySpan<T> firstSpan = span[..index];
		ReadOnlySpan<T> secondSpan = span[(index + separator.Length)..];
		index = secondSpan.IndexOf(separator);
		if (index < 0)
		{
			return false;
		}
		ReadOnlySpan<T> thirdSpan = secondSpan[(index + separator.Length)..];
		secondSpan = secondSpan[..index];
		if (!allowMultipleSeparators && thirdSpan.IndexOf(separator) >= 0)
		{
			return false;
		}
		first = firstSpan;
		second = secondSpan;
		third = thirdSpan;
		return true;
	}

	/// <summary>
	/// Splits given <see cref="Span{T}"/> <paramref name="span"/> in exactly three parts
	/// with the specified <typeparamref name="T"/> <paramref name="separator"/>.
	/// </summary>
	/// <param name="span">The <see cref="Span{T}"/> to be split.</param>
	/// <param name="separator">The sequence used to split the span.</param>
	/// <param name="first">The first part of the split.</param>
	/// <param name="second">The second part of the split.</param>
	/// <param name="third">The third part of the split.</param>
	/// <param name="allowMultipleSeparators">
	/// A flag that determines whether to treat more than two separators as an error (when <see langword="false"/>),
	/// or to include it in the third span (when <see langword="true"/>).
	/// </param>
	/// <typeparam name="T">The type of the span items.</typeparam>
	/// <returns>Whether the split was successful.</returns>
	public static bool TrySplitInThree<T>(this Span<T> span, Span<T> separator, out Span<T> first, out Span<T> second, out Span<T> third, bool allowMultipleSeparators = false)
		where T : IEquatable<T>
	{
		first = default;
		second = default;
		third = default;
		int index = span.IndexOf(separator);
		if (index < 0)
		{
			return false;
		}
		Span<T> firstSpan = span[..index];
		Span<T> secondSpan = span[(index + separator.Length)..];
		index = secondSpan.IndexOf(separator);
		if (index < 0)
		{
			return false;
		}
		Span<T> thirdSpan = secondSpan[(index + separator.Length)..];
		secondSpan = secondSpan[..index];
		if (!allowMultipleSeparators && thirdSpan.IndexOf(separator) >= 0)
		{
			return false;
		}
		first = firstSpan;
		second = secondSpan;
		third = thirdSpan;
		return true;
	}

}
