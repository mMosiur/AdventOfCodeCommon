using System;
using System.Diagnostics;
using AdventOfCode.Common.SpanExtensions;

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
		/// <summary>How many values does the tuple have.</summary>
		public int Count { get; }

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
		public TupleParsingOptions(int count, char separator, char openingBracket, char closingBracket, bool bracketsRequired = false)
		{
			Count = count;
			Separator = separator;
			OpeningBracket = openingBracket;
			ClosingBracket = closingBracket;
			BracketsRequired = bracketsRequired;
		}

		/// <summary>
		/// The constructor that initializes a new instance without brackets.
		/// </summary>
		public TupleParsingOptions(int count, char separator)
		{
			Count = count;
			Separator = separator;
			OpeningBracket = null;
			ClosingBracket = null;
			BracketsRequired = false;
		}
	}

	/// <summary>
	/// Parse a tuple from the specified <see cref="ReadOnlySpan{char}"/> <paramref name="s"/>
	/// and write resulting values into the specified <see langword="in"/> <see cref="Span{T}"/> <paramref name="result"/>.
	/// </summary>
	/// <typeparam name="T">The type of the tuple.</typeparam>
	/// <param name="s">The span to parse.</param>
	/// <param name="provider">The format provider.</param>
	/// <param name="result">The span to write the result into.</param>
	/// <param name="options">The options to configure parsing to.</param>
	/// <exception cref="FormatException">Throws when the specified <paramref name="s"/> is invalid.</exception>
	/// <remarks>
	/// The number of values to be parsed depends on the <see cref="Span{T}.Length"/> of the result span.
	/// Uses provided <paramref name="provider"/> and <paramref name="options"/>
	/// to modify the parsing behavior.
	/// </remarks>
	public static void ParseValueTupleIntoSpan<T>(ReadOnlySpan<char> s, IFormatProvider? provider, in Span<T> result, TupleParsingOptions options)
		where T : unmanaged, ISpanParsable<T>
	{
		if (options.Count != result.Length)
		{
			throw new ArgumentException(
				$"The length of the result span ({result.Length}) is not equal to the count of values to be parsed ({options.Count}).",
				nameof(result)
			);
		}
		try
		{
			ReadOnlySpan<char> trimmedS = s.Trim();
			if (trimmedS.IsEmpty)
			{
				throw new FormatException("The input text is empty.");
			}
			if (options.HasBrackets)
			{
				if (trimmedS[0] == options.OpeningBracket && trimmedS[^1] == options.ClosingBracket)
				{
					trimmedS = trimmedS[1..^1];
				}
				else if (options.BracketsRequired)
				{
					throw new FormatException($"Matching bracket pair was not found while it was required.");
				}
			}
			else if (options.BracketsRequired)
			{
				throw new FormatException($"Missing required brackets.");
			}
			SpanSplit<char> splitter = trimmedS.Split(options.Separator);
			int partCount = 0;
			foreach (ReadOnlySpan<char> part in splitter)
			{
				if (partCount >= result.Length)
				{
					throw new FormatException($"Too many values. Expected {result.Length}, found at least {partCount}.");
				}
				result[partCount++] = T.Parse(part, provider);
			}
			if (partCount < result.Length)
			{
				throw new FormatException($"Too few values. Expected {result.Length}, found {partCount}.");
			}
		}
		catch (Exception e)
		{
			Debug.Assert(e is FormatException or OverflowException);
			throw new FormatException($"Could not parse \"{s}\".", e);
		}
	}

	/// <summary>
	/// Parse a tuple from the specified <see cref="ReadOnlySpan{char}"/> <paramref name="s"/>
	/// and write resulting values into the specified <see langword="in"/> <see cref="Span{T}"/> <paramref name="result"/>.
	/// </summary>
	/// <typeparam name="T">The type of the tuple.</typeparam>
	/// <param name="s">The span to parse.</param>
	/// <param name="provider">The format provider.</param>
	/// <param name="result">The span to write the result into.</param>
	/// <param name="options">The options to configure parsing to.</param>
	/// <returns>A <see cref="bool"/> value indicating whether the parsing was successful.</returns>
	/// <remarks>
	/// The number of values to be parsed depends on the <see cref="Span{T}.Length"/> of the result span.
	/// Uses provided <paramref name="provider"/> and <paramref name="options"/>
	/// to modify the parsing behavior.
	/// </remarks>
	public static bool TryParseValueListIntoSpan<T>(ReadOnlySpan<char> s, IFormatProvider? provider, in Span<T> result, TupleParsingOptions options)
		where T : unmanaged, ISpanParsable<T>
	{
		if (options.Count != result.Length)
		{
			throw new ArgumentException(
				$"The length of the result span ({result.Length}) is not equal to the count of values to be parsed ({options.Count}).",
				nameof(result)
			);
		}
		s = s.Trim();
		if (s.IsEmpty)
		{
			return false;
		}
		if (options.HasBrackets)
		{
			if (s[0] == options.OpeningBracket && s[^1] == options.ClosingBracket)
			{
				s = s[1..^1];
			}
			else if (options.BracketsRequired)
			{
				return false;
			}
		}
		else if (options.BracketsRequired)
		{
			return false;
		}
		SpanSplit<char> splitter = s.Split(options.Separator);
		int partCount = 0;
		foreach (ReadOnlySpan<char> part in splitter)
		{
			if (partCount >= result.Length)
			{
				return false;
			}
			result[partCount++] = T.Parse(part, provider);
		}
		if (partCount < result.Length)
		{
			return false;
		}
		return true;
	}

}
