using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Common.StringExtensions;

/// <summary>
/// Extension methods for <see cref="string"/>.
/// </summary>
public static class StringExtensions
{
	/// <summary>
	/// <see cref="string"/> extensions
	/// </summary>
	/// <param name="str">The string to be operated on.</param>
	extension(string str)
	{
		/// <summary>
		/// Enumerates the lines of the string.
		/// </summary>
		/// <remarks>
		/// The enumerable is agnostic of line endings, that is it will work with any
		/// as does <see cref="StringReader.ReadLine"/>.
		/// </remarks>
		public IEnumerable<string> EnumerateLines()
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

}
