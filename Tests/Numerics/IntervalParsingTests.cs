using AdventOfCode.Common.Numerics;

namespace AdventOfCode.Common.Tests.Numerics;

public class IntervalParsingTests
{
	[Theory]
	[InlineData("1..2", "..", 1, 2)]
	[InlineData("-10..6", "..", -10, 6)]
	[InlineData("0 - 0", "-", 0, 0)]
	public void TestInt32IntervalParsingFromString(string s, string separator, int start, int end)
	{
		Interval<int> interval = Interval<int>.Parse(s, separator);
		Assert.Equal(start, interval.Start);
		Assert.Equal(end, interval.End);
		bool parsed = Interval<int>.TryParse(s, separator, out _);
		Assert.True(parsed);
	}

	[Theory]
	[InlineData("1..2", "..", 1, 2)]
	[InlineData("-10..6", "..", -10, 6)]
	public void TestInt8IntervalParsingFromString(string s, string separator, int start, int end)
	{
		Interval<sbyte> interval = Interval<sbyte>.Parse(s, separator);
		Assert.Equal(start, interval.Start);
		Assert.Equal(end, interval.End);
		bool parsed = Interval<sbyte>.TryParse(s, separator, out _);
		Assert.True(parsed);
	}
}
