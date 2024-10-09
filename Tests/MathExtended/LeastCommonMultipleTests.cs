namespace AdventOfCode.Common.Tests.MathExtended;

public class LeastCommonMultipleTests
{
	[Theory]
	[InlineData(1, 0, 0)]
	[InlineData(0, 1, 0)]
	[InlineData(1, 1, 1)]
	[InlineData(-1, 1, 1)]
	[InlineData(1, -1, 1)]
	[InlineData(-1, -1, 1)]
	[InlineData(8, 9, 72)]
	[InlineData(11, 5, 55)]
	[InlineData(15, 17, 255)]
	public void BasicTests(int a, int b, int expectedResult)
	{
		int result = Common.MathExtended.LeastCommonMultiple(a, b);
		Assert.Equal(expectedResult, result);
	}
}
