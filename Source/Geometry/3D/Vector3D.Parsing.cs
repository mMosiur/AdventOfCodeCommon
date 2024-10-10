using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace AdventOfCode.Common.Geometry;

// Adds parsing support to Vector3D<T>.
public readonly partial struct Vector3D<T>
	: ISpanParsable<Vector3D<T>>, IParsable<Vector3D<T>>
	where T : unmanaged, INumber<T>
{

	/// <summary>The count of values used in parsing.</summary>
	private const int ParsingValueCount = 3;

	/// <summary>The separator <see cref="char"/> used in parsing.</summary>
	private const char ParsingSeparatorChar = ',';

	/// <summary>The opening bracket <see cref="char"/> used in parsing.</summary>
	private const char ParsingOpeningBracketChar = '[';

	/// <summary>The closing bracket <see cref="char"/> used in parsing.</summary>
	private const char ParsingClosingBracketChar = ']';

	/// <summary>
	/// Parses specified string into a <see cref="Vector3D{T}"/>.
	/// </summary>
	/// <param name="s">The string to parse.</param>
	/// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s"/>.</param>
	/// <exception cref="ArgumentNullException"><paramref name="s"/> is <see langword="null"/>.</exception>
	/// <exception cref="FormatException"><paramref name="s"/> is not in the correct format.</exception>
	/// <returns>A new <see cref="Vector3D{T}"/> parsed from <paramref name="s"/>.</returns>
	/// <remarks>
	/// The format of <paramref name="s"/> can be either
	/// <list type="bullet">
	/// <item><c>[X, Y, Z]</c></item>
	/// <item><c>X, Y, Z</c></item>
	/// </list>
	/// with whitespace between elements ignored where <c>X</c>, <c>Y</c> and <c>Z</c> are
	/// the string representations of the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> values.
	/// </remarks>
	public static Vector3D<T> Parse(string s, IFormatProvider? provider = null)
	{
		ArgumentNullException.ThrowIfNull(s);
		return Parse(s.AsSpan(), provider);
	}

	/// <summary>
	/// Parses specified span of characters into a <see cref="Vector3D{T}"/>.
	/// </summary>
	/// <param name="s">The span of characters to parse.</param>
	/// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s"/>.</param>
	/// <exception cref="FormatException"><paramref name="s"/> is not in the correct format.</exception>
	/// <returns>A new <see cref="Vector3D{T}"/> parsed from <paramref name="s"/>.</returns>
	/// <remarks>
	/// The format of <paramref name="s"/> can be either
	/// <list type="bullet">
	/// <item><c>[X, Y, Z]</c></item>
	/// <item><c>X, Y, Z</c></item>
	/// </list>
	/// with whitespace between elements ignored where <c>X</c>, <c>Y</c> and <c>Z</c> are
	/// the string representations of the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> values.
	/// </remarks>
	public static Vector3D<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider = null)
	{
		Span<T> values = stackalloc T[ParsingValueCount];
		try
		{
			Helpers.TupleParsing.ParseValueTupleIntoSpan(
				s,
				result: values,
				new(ParsingSeparatorChar, ParsingOpeningBracketChar, ParsingClosingBracketChar),
				provider
			);
		}
		catch (FormatException e)
		{
			throw new FormatException($"Could not parse Vector3D from \"{s}\".", e);
		}
		return new(values[0], values[1], values[2]);
	}

	/// <summary>
	/// Tries to parse a string into a <see cref="Vector3D{T}"/>.
	/// </summary>
	/// <param name="s">The string to try to parse.</param>
	/// <param name="result">The result of parsing <paramref name="s"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="s"/> was successfully parsed; otherwise, <see langword="false"/>.</returns>
	/// <remarks>
	/// The format of <paramref name="s"/> can be either
	/// <list type="bullet">
	/// <item><c>[X, Y, Z]</c></item>
	/// <item><c>X, Y, Z</c></item>
	/// </list>
	/// with whitespace between elements ignored where <c>X</c>, <c>Y</c> and <c>Z</c> are
	/// the string representations of the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> values.
	/// </remarks>
	public static bool TryParse([NotNullWhen(true)] string? s, out Vector3D<T> result)
		=> TryParse(s, null, out result);

	/// <summary>
	/// Tries to parse a string into a <see cref="Vector3D{T}"/>.
	/// </summary>
	/// <param name="s">The string to try to parse.</param>
	/// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s"/>.</param>
	/// <param name="result">The result of parsing <paramref name="s"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="s"/> was successfully parsed; otherwise, <see langword="false"/>.</returns>
	/// <remarks>
	/// The format of <paramref name="s"/> can be either
	/// <list type="bullet">
	/// <item><c>[X, Y, Z]</c></item>
	/// <item><c>X, Y, Z</c></item>
	/// </list>
	/// with whitespace between elements ignored where <c>X</c>, <c>Y</c> and <c>Z</c> are
	/// the string representations of the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> values.
	/// </remarks>
	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out Vector3D<T> result)
	{
		ArgumentNullException.ThrowIfNull(s);
		return TryParse(s.AsSpan(), provider, out result);
	}

	/// <summary>
	/// Tries to parse a span of characters into a <see cref="Vector3D{T}"/>.
	/// </summary>
	/// <param name="s">The span of characters to try to parse.</param>
	/// <param name="result">The result of parsing <paramref name="s"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="s"/> was successfully parsed; otherwise, <see langword="false"/>.</returns>
	/// <remarks>
	/// The format of <paramref name="s"/> can be either
	/// <list type="bullet">
	/// <item><c>[X, Y, Z]</c></item>
	/// <item><c>X, Y, Z</c></item>
	/// </list>
	/// with whitespace between elements ignored where <c>X</c>, <c>Y</c> and <c>Z</c> are
	/// the string representations of the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> values.
	/// </remarks>
	public static bool TryParse(ReadOnlySpan<char> s, out Vector3D<T> result)
		=> TryParse(s, null, out result);

	/// <summary>
	/// Tries to parse a span of characters into a <see cref="Vector3D{T}"/>.
	/// </summary>
	/// <param name="s">The span of characters to try to parse.</param>
	/// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s"/>.</param>
	/// <param name="result">The result of parsing <paramref name="s"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="s"/> was successfully parsed; otherwise, <see langword="false"/>.</returns>
	/// <remarks>
	/// The format of <paramref name="s"/> can be either
	/// <list type="bullet">
	/// <item><c>[X, Y, Z]</c></item>
	/// <item><c>X, Y, Z</c></item>
	/// </list>
	/// with whitespace between elements ignored where <c>X</c>, <c>Y</c> and <c>Z</c> are
	/// the string representations of the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> values.
	/// </remarks>
	public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out Vector3D<T> result)
	{
		result = default;
		Span<T> values = stackalloc T[ParsingValueCount];
		bool parsed = Helpers.TupleParsing.TryParseValueListIntoSpanExact(
			s,
			in values,
			new(ParsingSeparatorChar, ParsingOpeningBracketChar, ParsingClosingBracketChar),
			provider
		);
		if (parsed)
		{
			result = new(values[0], values[1], values[2]);
		}
		return parsed;
	}

}
