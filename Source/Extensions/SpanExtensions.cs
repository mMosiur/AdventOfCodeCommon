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
	public static SpanSplit<T> Split<T>(this ReadOnlySpan<T> span, T separator) where T : IEquatable<T>
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
	public static SpanSplit<T> Split<T>(this Span<T> span, T separator) where T : IEquatable<T>
	{
		return new(span, separator);
	}

}
