using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using AdventOfCode.Common.SpanExtensions;

namespace AdventOfCode.Common.Geometry;

// Adds parsing support to Vector2D<T>.
public readonly partial struct Vector2D<T>
	: ISpanParsable<Vector2D<T>>, IParsable<Vector2D<T>>
	where T : unmanaged, INumber<T>
{

	/// <summary>
	/// Parses specified string into a <see cref="Vector2D{T}"/>.
	/// </summary>
	/// <param name="s">The string to parse.</param>
	/// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s"/>.</param>
	/// <exception cref="ArgumentNullException"><paramref name="s"/> is <see langword="null"/>.</exception>
	/// <exception cref="FormatException"><paramref name="s"/> is not in the correct format.</exception>
	/// <exception cref="OverflowException"><paramref name="s"/> is not representable by <typeparamref name="T"/>.</exception>
	/// <returns>A new <see cref="Vector2D{T}"/> parsed from <paramref name="s"/>.</returns>
	/// <remarks>
	/// The format of <paramref name="s"/> can be either
	/// <list type="bullet">
	/// <item><c>[X, Y]</c></item>
	/// <item><c>X, Y</c></item>
	/// </list>
	/// with whitespace between elements ignored where <c>X</c> and <c>Y</c> are
	/// the string representations of the <see cref="X"/> and <see cref="X"/> values.
	/// </remarks>
	public static Vector2D<T> Parse(string s, IFormatProvider? provider = null)
	{
		ArgumentNullException.ThrowIfNull(s);
		return Parse(s.AsSpan(), provider);
	}

	/// <summary>
	/// Parses specified span of characters into a <see cref="Vector2D{T}"/>.
	/// </summary>
	/// <param name="s">The span of characters to parse.</param>
	/// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s"/>.</param>
	/// <exception cref="FormatException"><paramref name="s"/> is not in the correct format.</exception>
	/// <exception cref="OverflowException"><paramref name="s"/> is not representable by <typeparamref name="T"/>.</exception>
	/// <returns>A new <see cref="Vector2D{T}"/> parsed from <paramref name="s"/>.</returns>
	/// <remarks>
	/// The format of <paramref name="s"/> can be either
	/// <list type="bullet">
	/// <item><c>[X, Y]</c></item>
	/// <item><c>X, Y</c></item>
	/// </list>
	/// with whitespace between elements ignored where <c>X</c> and <c>Y</c> are
	/// the string representations of the <see cref="X"/> and <see cref="X"/> values.
	/// </remarks>
	public static Vector2D<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider = null)
	{
		const int CoordCount = 2;
		const char Separator = ',';
		const char OpenChar = '[';
		const char CloseChar = ']';
		try
		{
			ReadOnlySpan<char> trimmedSpan = s.Trim();
			if (trimmedSpan.IsEmpty)
			{
				throw new FormatException("The input text is empty.");
			}
			if (trimmedSpan[0] == OpenChar)
			{
				if (trimmedSpan[^1] != CloseChar)
				{
					throw new FormatException($"The opening '{OpenChar}' did not have a closing '{CloseChar}'.");
				}
				// We know that the the span has at least two chars as otherwise we would step into prev if.
				trimmedSpan = trimmedSpan[1..^1];
			}
			Span<T> values = stackalloc T[CoordCount];
			SpanSplit<char> splitter = trimmedSpan.Split(Separator);
			int partCount = 0;
			foreach (ReadOnlySpan<char> part in splitter)
			{
				if (partCount >= CoordCount)
				{
					throw new FormatException($"Too many coordinates. Expected {CoordCount}, found at least {partCount}.");
				}
				values[partCount++] = T.Parse(part, provider);
			}
			if (partCount < CoordCount)
			{
				throw new FormatException($"Too few coordinates. Expected {CoordCount}, found {partCount}.");
			}
			return new Vector2D<T>(values[0], values[1]);
		}
		catch (Exception e)
		{
			Debug.Assert(e is FormatException or OverflowException);
			throw new FormatException($"Could not parse \"{s}\" as a vector.", e);
		}
	}

	/// <summary>
	/// Tries to parse a string into a <see cref="Vector2D{T}"/>.
	/// </summary>
	/// <param name="s">The string to try to parse.</param>
	/// <param name="result">The result of parsing <paramref name="s"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="s"/> was successfully parsed; otherwise, <see langword="false"/>.</returns>
	/// <remarks>
	/// The format of <paramref name="s"/> can be either
	/// <list type="bullet">
	/// <item><c>[X, Y]</c></item>
	/// <item><c>X, Y</c></item>
	/// </list>
	/// with whitespace between elements ignored where <c>X</c> and <c>Y</c> are
	/// the string representations of the <see cref="X"/> and <see cref="X"/> values.
	/// </remarks>
	public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Vector2D<T> result)
		=> TryParse(s, null, out result);

	/// <summary>
	/// Tries to parse a string into a <see cref="Vector2D{T}"/>.
	/// </summary>
	/// <param name="s">The string to try to parse.</param>
	/// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s"/>.</param>
	/// <param name="result">The result of parsing <paramref name="s"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="s"/> was successfully parsed; otherwise, <see langword="false"/>.</returns>
	/// <remarks>
	/// The format of <paramref name="s"/> can be either
	/// <list type="bullet">
	/// <item><c>[X, Y]</c></item>
	/// <item><c>X, Y</c></item>
	/// </list>
	/// with whitespace between elements ignored where <c>X</c> and <c>Y</c> are
	/// the string representations of the <see cref="X"/> and <see cref="X"/> values.
	/// </remarks>
	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Vector2D<T> result)
	{
		ArgumentNullException.ThrowIfNull(s);
		return TryParse(s.AsSpan(), provider, out result);
	}

	/// <summary>
	/// Tries to parse a span of characters into a <see cref="Vector2D{T}"/>.
	/// </summary>
	/// <param name="s">The span of characters to try to parse.</param>
	/// <param name="result">The result of parsing <paramref name="s"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="s"/> was successfully parsed; otherwise, <see langword="false"/>.</returns>
	/// <remarks>
	/// The format of <paramref name="s"/> can be either
	/// <list type="bullet">
	/// <item><c>[X, Y]</c></item>
	/// <item><c>X, Y</c></item>
	/// </list>
	/// with whitespace between elements ignored where <c>X</c> and <c>Y</c> are
	/// the string representations of the <see cref="X"/> and <see cref="X"/> values.
	/// </remarks>
	public static bool TryParse([NotNullWhen(true)] ReadOnlySpan<char> s, [MaybeNullWhen(false)] out Vector2D<T> result)
		=> TryParse(s, null, out result);

	/// <summary>
	/// Tries to parse a span of characters into a <see cref="Vector2D{T}"/>.
	/// </summary>
	/// <param name="s">The span of characters to try to parse.</param>
	/// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s"/>.</param>
	/// <param name="result">The result of parsing <paramref name="s"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="s"/> was successfully parsed; otherwise, <see langword="false"/>.</returns>
	/// <remarks>
	/// The format of <paramref name="s"/> can be either
	/// <list type="bullet">
	/// <item><c>[X, Y]</c></item>
	/// <item><c>X, Y</c></item>
	/// </list>
	/// with whitespace between elements ignored where <c>X</c> and <c>Y</c> are
	/// the string representations of the <see cref="X"/> and <see cref="X"/> values.
	/// </remarks>
	public static bool TryParse([NotNullWhen(true)] ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Vector2D<T> result)
	{
		const int CoordCount = 2;
		const char Separator = ',';
		const char OpenChar = '[';
		const char CloseChar = ']';
		result = default;
		ReadOnlySpan<char> trimmedSpan = s.Trim();
		if (trimmedSpan.IsEmpty)
		{
			throw new FormatException("The input text is empty.");
		}
		if (trimmedSpan[0] == OpenChar)
		{
			if (trimmedSpan[^1] != CloseChar)
			{
				throw new FormatException($"The opening '{OpenChar}' did not have a closing '{CloseChar}'.");
			}
			// We know that the the span has at least two chars as otherwise we would step into prev if.
			trimmedSpan = trimmedSpan[1..^1];
		}
		Span<T> values = stackalloc T[CoordCount];
		SpanSplit<char> splitter = trimmedSpan.Split(Separator);
		int partCount = 0;
		foreach (ReadOnlySpan<char> part in splitter)
		{
			if (partCount >= CoordCount)
			{
				return false;
			}
			if (!T.TryParse(part, provider, out T parsed))
			{
				return false;
			}
			values[partCount++] = parsed;
		}
		if (partCount < CoordCount)
		{
			return false;
		}
		result = new Vector2D<T>(values[0], values[1]);
		return true;
	}

}

