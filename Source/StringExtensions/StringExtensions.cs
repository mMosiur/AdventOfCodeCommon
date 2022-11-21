using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Common.StringExtensions;

public static class StringExtensions
{
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
