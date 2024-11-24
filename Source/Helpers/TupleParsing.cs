using System;
using System.Diagnostics;

namespace AdventOfCode.Common.Helpers;

/// <summary>
/// A static class that contains helper methods for parsing tuples.
/// </summary>
internal static class TupleParsing
{

	/// <summary>
	/// Options defining how to parse a list.
	/// </summary>
	public readonly struct TupleParsingOptions
	{
		/// <summary>Flags options not empty</summary>
		/// <remarks>
		/// Should always be set to <see langword="true"/> by every constructor and so will be
		/// <see langword="false"/> only if the default constructor is used.
		/// </remarks>
		private readonly bool _isNonEmpty;

		/// <summary>Flags whether the options are empty.</summary>
		internal bool IsEmpty => !_isNonEmpty;

		/// <summary>The separator used in between tuple values.</summary>
		public char Separator { get; }

		/// <summary>The opening bracket the tuple can potentially have.</summary>
		public char? OpeningBracket { get; }

		/// <summary>The closing bracket the tuple can potentially have.</summary>
		public char? ClosingBracket { get; }

		/// <summary>Whether brackets are required while parsing tuple.</summary>
		public bool BracketsRequired { get; }

		/// <summary>Whether options had defined opening and closing brackets for parsing.</summary>
		public bool HasBrackets => OpeningBracket is not null; // Closing bracket should also not null in tha case

		/// <summary>
		/// The constructor that initializes a new instance with all parameters.
		/// </summary>
		public TupleParsingOptions(char separator, char openingBracket, char closingBracket, bool bracketsRequired = false)
		{
			_isNonEmpty = true;
			Separator = separator;
			OpeningBracket = openingBracket;
			ClosingBracket = closingBracket;
			BracketsRequired = bracketsRequired;
		}

		/// <summary>
		/// The constructor that initializes a new instance without brackets.
		/// </summary>
		public TupleParsingOptions(char separator)
		{
			_isNonEmpty = true;
			Separator = separator;
			OpeningBracket = null;
			ClosingBracket = null;
			BracketsRequired = false;
		}
	}

	/// <summary>
	/// Parse a tuple from the specified span <paramref name="s"/> and write
	/// resulting values into the specified <see cref="Span{T}"/> <paramref name="result"/>.
	/// </summary>
	/// <typeparam name="T">The type of the tuple.</typeparam>
	/// <param name="s">The span to parse.</param>
	/// <param name="result">The span to write the result into.</param>
	/// <param name="options">The options to configure parsing.</param>
	/// <param name="provider">The optional format provider for parsing <typeparamref name="T"/> values.</param>
	/// <exception cref="FormatException">Thrown when the specified <paramref name="s"/> is not in valid format.</exception>
	/// <exception cref="ArgumentException">Thrown when input span <paramref name="s"/>, output span <paramref name="result"/> or <paramref name="options"/> are empty.</exception>
	/// <remarks>
	/// The exact number of values to be parsed depends on the <see cref="Span{T}.Length"/> of the <paramref name="result"/> span.
	/// Uses <paramref name="options"/> and provided <paramref name="provider"/> to modify the parsing behavior.
	/// </remarks>
	public static void ParseValueTupleIntoSpan<T>(ReadOnlySpan<char> s, in Span<T> result, TupleParsingOptions options, IFormatProvider? provider = null)
		where T : ISpanParsable<T>
	{
		s = s.Trim();

		if (s.IsEmpty)
		{
			throw new ArgumentException("The input text is empty.", nameof(s));
		}

		if (result.IsEmpty)
		{
			throw new ArgumentException("The result span is empty.", nameof(result));
		}

		if (options.IsEmpty)
		{
			throw new ArgumentException("The options are empty.", nameof(options));
		}

		try
		{
			ParseValueTupleIntoSpanInternal(s, result, options, provider);
		}
		catch (Exception e)
		{
			Debug.Assert(e is FormatException or OverflowException);
			throw new FormatException($"Could not parse \"{s}\".", e);
		}
	}

	/// <summary>
	/// Internal function to parse a tuple from the specified span <paramref name="trimmedSpan"/> and write
	/// resulting values into the specified <see cref="Span{T}"/> <paramref name="result"/>.
	/// </summary>
	/// <typeparam name="T">The type of the tuple.</typeparam>
	/// <param name="trimmedSpan">The span to parse, should be trimmed before calling.</param>
	/// <param name="result">The span to write the result into.</param>
	/// <param name="options">The options to configure parsing.</param>
	/// <param name="provider">The optional format provider for parsing <typeparamref name="T"/> values.</param>
	/// <exception cref="FormatException">Thrown when the specified <paramref name="trimmedSpan"/> is not in valid format.</exception>
	/// <remarks>
	/// The exact number of values to be parsed depends on the <see cref="Span{T}.Length"/> of the <paramref name="result"/> span.
	/// Uses <paramref name="options"/> and provided <paramref name="provider"/> to modify the parsing behavior.
	/// </remarks>
	private static void ParseValueTupleIntoSpanInternal<T>(ReadOnlySpan<char> trimmedSpan, in Span<T> result, TupleParsingOptions options, IFormatProvider? provider = null)
		where T : ISpanParsable<T>
	{
		if (options.HasBrackets)
		{
			if (trimmedSpan[0] == options.OpeningBracket && trimmedSpan[^1] == options.ClosingBracket)
			{
				trimmedSpan = trimmedSpan[1..^1];
			}
			else if (options.BracketsRequired)
			{
				throw new FormatException("Matching bracket pair was not found while it was required.");
			}
		}

		int partCount = 0;
		foreach (Range partRange in trimmedSpan.Split(options.Separator))
		{
			if (partCount >= result.Length)
			{
				throw new FormatException("The number of parsed values was bigger than expected.");
			}

			result[partCount] = T.Parse(trimmedSpan[partRange], provider);
			partCount++;
		}

		if (partCount != result.Length)
		{
			throw new FormatException("The number of parsed values did not match expected number.");
		}
	}

	/// <summary>
	/// Parse a tuple from the specified span <paramref name="s"/> and write resulting
	/// values into the specified <see cref="Span{T}"/> <paramref name="result"/>.
	/// Fails when number of parsed values is not equal to the length of <paramref name="result"/>.
	/// </summary>
	/// <typeparam name="T">The type of the tuple.</typeparam>
	/// <param name="s">The span to parse.</param>
	/// <param name="result">The span to write the result into.</param>
	/// <param name="options">The options to configure parsing.</param>
	/// <param name="provider">The optional format provider for parsing <typeparamref name="T"/> values.</param>
	/// <returns>A <see cref="bool"/> value indicating whether the parsing was successful.</returns>
	/// <remarks>
	/// The number of values to be parsed depends on the <see cref="Span{T}.Length"/> of the <paramref name="result"/> span.
	/// Uses provided <paramref name="provider"/> and <paramref name="options"/>
	/// to modify the parsing behavior.
	/// </remarks>
	public static bool TryParseValueListIntoSpanExact<T>(ReadOnlySpan<char> s, in Span<T> result, TupleParsingOptions options, IFormatProvider? provider)
		where T : ISpanParsable<T>
	{
		s = s.Trim();

		if (s.IsEmpty || result.IsEmpty || options.IsEmpty)
		{
			return false;
		}

		return TryParseValueListIntoSpanExactInternal(s, result, options, provider);
	}

	/// <summary>
	/// Internal function to parse a tuple from the specified span <paramref name="trimmedSpan"/> and write
	/// resulting values into the specified <see cref="Span{T}"/> <paramref name="result"/>.
	/// Fails when number of parsed values is not equal to the length of <paramref name="result"/>.
	/// </summary>
	/// <typeparam name="T">The type of the tuple.</typeparam>
	/// <param name="trimmedSpan">The span to parse, should be trimmed before calling.</param>
	/// <param name="result">The span to write the result into.</param>
	/// <param name="options">The options to configure parsing.</param>
	/// <param name="provider">The optional format provider for parsing <typeparamref name="T"/> values.</param>
	/// <returns>A <see cref="bool"/> value indicating whether the parsing was successful.</returns>
	/// <remarks>
	/// The exact number of values to be parsed depends on the <see cref="Span{T}.Length"/> of the <paramref name="result"/> span.
	/// Uses provided <paramref name="provider"/> and <paramref name="options"/> to modify the parsing behavior.
	/// </remarks>
	private static bool TryParseValueListIntoSpanExactInternal<T>(ReadOnlySpan<char> trimmedSpan, in Span<T> result, TupleParsingOptions options, IFormatProvider? provider)
		where T : ISpanParsable<T>
	{
		if (options.HasBrackets)
		{
			if (trimmedSpan[0] == options.OpeningBracket && trimmedSpan[^1] == options.ClosingBracket)
			{
				trimmedSpan = trimmedSpan[1..^1];
			}
			else if (options.BracketsRequired)
			{
				return false; // Matching bracket pair was not found while it was required
			}
		}

		int partCount = 0;
		foreach (Range partRange in trimmedSpan.Split(options.Separator))
		{
			if (partCount >= result.Length)
			{
				return false; // Too many values to parse
			}

			if (!T.TryParse(trimmedSpan[partRange], provider, out T? parsedPart))
			{
				return false; // Parsing failed
			}

			result[partCount] = parsedPart;
			partCount++;
		}

		return partCount == result.Length; // Should parse exactly the number of values in the result span
	}
}
