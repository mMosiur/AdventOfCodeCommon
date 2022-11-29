using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Common.StringExtensions;

/// <summary>
/// Extension methods for <see cref="string"/>.
/// </summary>
public static class StringExtensions
{

	/// <summary>
	/// Enumerates the lines of the string.
	/// </summary>
	/// <param name="str">The string the enumerator will work on.</param>
	/// <remarks>
	/// The enumerable is agnostic of line endings, that is it will work with any
	/// as does <see cref="StringReader.ReadLine"/>.
	/// </remarks>
	public static IEnumerable<string> EnumerateLines(this string str)
	{
		StringReader reader = new(str);
		string? line = reader.ReadLine();
		while (line is not null)
		{
			yield return line;
			line = reader.ReadLine();
		}
	}

}
