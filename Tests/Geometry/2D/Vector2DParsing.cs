using AdventOfCode.Common.Geometry;

namespace AdventOfCode.Common.Tests.Geometry;

public class Vector2DParsing
{
	[Theory]
	[InlineData("1, 2")]
	[InlineData("1, -2")]
	[InlineData("[1, 0]")]
	[InlineData("1 ,2  ")]
	public void Test1(string s)
	{
		Assert.True(Vector2D<int>.TryParse(s, out _));
	}
}
