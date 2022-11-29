using AdventOfCode.Common.Geometry;

namespace AdventOfCode.Common.Tests.Geometry;

public class Vector2DParsing
{
	[Theory]
	[InlineData("1, 2", 1, 2)]
	[InlineData("1, -2", 1, -2)]
	[InlineData("[1, 0]", 1, 0)]
	[InlineData("1 ,2  ", 1, 2)]
	public void Test1(string s, int x, int y)
	{
		Vector2D<int> p = Vector2D<int>.Parse(s);
		Assert.Equal(x, p.X);
		Assert.Equal(y, p.Y);
	}
}
