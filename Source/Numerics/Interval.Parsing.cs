using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace AdventOfCode.Common.Numerics;

public readonly partial struct Interval<T>
	: IParsable<Interval<T>>, ISpanParsable<Interval<T>>
	where T : unmanaged, IBinaryInteger<T>
{

	/// <summary>The separator between start and end values in the interval.</summary>
	private const string ParsingSeparator = "..";

	/// <summary>
	/// Parses specified string into a <see cref="Interval{T}"/> using <c>..</c> as a default start/end separator.
	/// </summary>
	/// <param name="s">The string to parse.</param>
	/// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s"/>.</param>
	/// <exception cref="ArgumentNullException"><paramref name="s"/> is <see langword="null"/>.</exception>
	/// <exception cref="FormatException"><paramref name="s"/> is not in the correct format.</exception>
	/// <returns>A new <see cref="Interval{T}"/> parsed from <paramref name="s"/>.</returns>
	public static Interval<T> Parse(string s, IFormatProvider? provider = null)
	{
		ArgumentNullException.ThrowIfNull(s);
		return Parse(s.AsSpan(), ParsingSeparator, provider);
	}

	/// <summary>
	/// Parses specified string into a <see cref="Interval{T}"/>.
	/// </summary>
	/// <param name="s">The string to parse.</param>
	/// <param name="separator">The separator between start and end values.</param>
	/// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s"/>.</param>
	/// <exception cref="ArgumentNullException"><paramref name="s"/> or <paramref name="separator"/> is <see langword="null"/>.</exception>
	/// <exception cref="FormatException"><paramref name="s"/> is not in the correct format.</exception>
	/// <returns>A new <see cref="Interval{T}"/> parsed from <paramref name="s"/>.</returns>
	public static Interval<T> Parse(string s, string separator, IFormatProvider? provider = null)
	{
		ArgumentNullException.ThrowIfNull(s);
		ArgumentNullException.ThrowIfNull(separator);
		return Parse(s.AsSpan(), separator.AsSpan(), provider);
	}

	/// <summary>
	/// Parses specified span of characters into a <see cref="Interval{T}"/> using <c>..</c> as a default start/end separator.
	/// </summary>
	/// <param name="s">The span of characters to parse.</param>
	/// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s"/>.</param>
	/// <exception cref="FormatException"><paramref name="s"/> is not in the correct format.</exception>
	/// <returns>A new <see cref="Interval{T}"/> parsed from <paramref name="s"/>.</returns>
	public static Interval<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider = null)
		=> Parse(s, ParsingSeparator, provider);

	/// <summary>
	/// Parses specified span of characters into a <see cref="Interval{T}"/>.
	/// </summary>
	/// <param name="s">The span of characters to parse.</param>
	/// <param name="separator">The separator characters between start and end values.</param>
	/// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s"/>.</param>
	/// <exception cref="FormatException"><paramref name="s"/> is not in the correct format.</exception>
	/// <returns>A new <see cref="Interval{T}"/> parsed from <paramref name="s"/>.</returns>
	public static Interval<T> Parse(ReadOnlySpan<char> s, ReadOnlySpan<char> separator, IFormatProvider? provider = null)
	{
		try
		{
			int sepIndex = s.IndexOf(separator);
			if (sepIndex < 0)
			{
				throw new FormatException($"The separator \"{separator.ToString()}\" is not found.");
			}
			ReadOnlySpan<char> first = s[..sepIndex];
			ReadOnlySpan<char> second = s[(sepIndex + separator.Length)..];
			T start = T.Parse(first, provider);
			T end = T.Parse(second, provider);
			return new Interval<T>(start, end);
		}
		catch (Exception e)
		{
			throw new FormatException($"Could not parse interval from \"{s}\"", e);
		}
	}

	/// <summary>
	/// Tries to parse a string into a <see cref="Interval{T}"/> using <c>..</c> as a separator.
	/// </summary>
	/// <param name="s">The string to try to parse.</param>
	/// <param name="result">The result of parsing <paramref name="s"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="s"/> was successfully parsed; otherwise, <see langword="false"/>.</returns>
	public static bool TryParse([NotNullWhen(true)] string? s, out Interval<T> result)
		=> TryParse(s, ParsingSeparator, null, out result);

	/// <summary>
	/// Tries to parse a string into a <see cref="Interval{T}"/>.
	/// </summary>
	/// <param name="s">The string to try to parse.</param>
	/// <param name="separator">The string separator between start and end values.</param>
	/// <param name="result">The result of parsing <paramref name="s"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="s"/> was successfully parsed; otherwise, <see langword="false"/>.</returns>
	public static bool TryParse([NotNullWhen(true)] string? s, string separator, out Interval<T> result)
		=> TryParse(s, separator, null, out result);

	/// <summary>
	/// Tries to parse a string into a <see cref="Interval{T}"/> using <c>..</c> as a separator.
	/// </summary>
	/// <param name="s">The string to try to parse.</param>
	/// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s"/>.</param>
	/// <param name="result">The result of parsing <paramref name="s"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="s"/> was successfully parsed; otherwise, <see langword="false"/>.</returns>
	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out Interval<T> result)
		=> TryParse(s, ParsingSeparator, provider, out result);

	/// <summary>
	/// Tries to parse a string into a <see cref="Interval{T}"/>.
	/// </summary>
	/// <param name="s">The string to try to parse.</param>
	/// <param name="separator">The string separator between start and end values.</param>
	/// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s"/>.</param>
	/// <param name="result">The result of parsing <paramref name="s"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="s"/> was successfully parsed; otherwise, <see langword="false"/>.</returns>
	public static bool TryParse([NotNullWhen(true)] string? s, string separator, IFormatProvider? provider, out Interval<T> result)
	{
		ArgumentNullException.ThrowIfNull(s);
		ArgumentNullException.ThrowIfNull(separator);
		return TryParse(s.AsSpan(), separator.AsSpan(), provider, out result);
	}

	/// <summary>
	/// Tries to parse a span of characters into a <see cref="Interval{T}"/> using <c>..</c> as the separator.
	/// </summary>
	/// <param name="s">The span of characters to try to parse.</param>
	/// <param name="result">The result of parsing <paramref name="s"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="s"/> was successfully parsed; otherwise, <see langword="false"/>.</returns>
	public static bool TryParse(ReadOnlySpan<char> s, out Interval<T> result)
		=> TryParse(s, ParsingSeparator, null, out result);

	/// <summary>
	/// Tries to parse a span of characters into a <see cref="Interval{T}"/>.
	/// </summary>
	/// <param name="s">The span of characters to try to parse.</param>
	/// <param name="separator">The separator character between start and end values.</param>
	/// <param name="result">The result of parsing <paramref name="s"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="s"/> was successfully parsed; otherwise, <see langword="false"/>.</returns>
	public static bool TryParse(ReadOnlySpan<char> s, ReadOnlySpan<char> separator, out Interval<T> result)
		=> TryParse(s, separator, null, out result);

	/// <summary>
	/// Tries to parse a span of characters into a <see cref="Interval{T}"/> using <c>..</c> as the separator.
	/// </summary>
	/// <param name="s">The span of characters to try to parse.</param>
	/// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s"/>.</param>
	/// <param name="result">The result of parsing <paramref name="s"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="s"/> was successfully parsed; otherwise, <see langword="false"/>.</returns>
	public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out Interval<T> result)
		=> TryParse(s, ParsingSeparator, provider, out result);

	/// <summary>
	/// Tries to parse a span of characters into a <see cref="Interval{T}"/>.
	/// </summary>
	/// <param name="s">The span of characters to try to parse.</param>
	/// <param name="separator">The separator character between start and end values.</param>
	/// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s"/>.</param>
	/// <param name="result">The result of parsing <paramref name="s"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="s"/> was successfully parsed; otherwise, <see langword="false"/>.</returns>
	public static bool TryParse(ReadOnlySpan<char> s, ReadOnlySpan<char> separator, IFormatProvider? provider, out Interval<T> result)
	{
		result = default;
		int sepIndex = s.IndexOf(separator);
		if (sepIndex < 0)
		{
			return false;
		}
		ReadOnlySpan<char> first = s[..sepIndex];
		ReadOnlySpan<char> second = s[(sepIndex + separator.Length)..];
		if (!T.TryParse(first, provider, out T start))
		{
			return false;
		}
		if (!T.TryParse(second, provider, out T end))
		{
			return false;
		}
		result = new Interval<T>(start, end);
		return true;
	}

}
