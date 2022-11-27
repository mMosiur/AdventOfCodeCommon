using AdventOfCode.Common.Geometry;

namespace AdventOfCode.Common.Tests.Geometry;

public class Point2DParsing
{
	[Theory]
	[InlineData("1, 2", 1, 2)]
	[InlineData("1, -2", 1, -2)]
	[InlineData("(1, 0)", 1, 0)]
	[InlineData("1 ,2  ", 1, 2)]
	public void Test1(string s, int x, int y)
	{
		Point2D<int> p = Point2D<int>.Parse(s);
		Assert.Equal(x, p.X);
		Assert.Equal(y, p.Y);
	}
}
