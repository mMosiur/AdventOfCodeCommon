using AdventOfCode.Common.Geometry;

namespace AdventOfCode.Common.Tests.Geometry;

public class PointVector2DTests
{
	[Theory]
	[InlineData(1, 2, 1, 2, 2, 4)]
	[InlineData(1, 2, 3, 4, 4, 6)]
	[InlineData(0, 0, 10, -2, 10, -2)]
	public void TestIntPointVectorAdditionCorrect(int pointX, int pointY, int vectorX, int vectorY, int resultX, int resultY)
	{
		Point2D<int> p1 = new(pointX, pointY);
		Vector2D<int> v1 = new(vectorX, vectorY);
		Point2D<int> p2 = p1 + v1;
		Assert.Equal(resultX, p2.X);
		Assert.Equal(resultY, p2.Y);
		Point2D<int> result = new(resultX, resultY);
		Assert.Equal(result, p2);
	}

	[Theory]
	[InlineData(1, 2, 1, 2, 2, 5)]
	[InlineData(1, 0, 3, 4, 4, 6)]
	[InlineData(0, 0, 10, int.MaxValue, 10, -2)]
	public void TestIntPointVectorAdditionFailed(int pointX, int pointY, int vectorX, int vectorY, int resultX, int resultY)
	{
		Point2D<int> p1 = new(pointX, pointY);
		Vector2D<int> v1 = new(vectorX, vectorY);
		Point2D<int> p2 = p1 + v1;
		Point2D<int> result = new(resultX, resultY);
		Assert.NotEqual(result, p2);
	}

	[Theory]
	[InlineData(0.1, 2.0, 1.1, 2.0, 1.2, 4.0)]
	[InlineData(1.0, 0.2, 0.3, 4.0, 1.3, 4.2)]
	[InlineData(0.0, 0.0, 10.0, -0.2, 10.0, -0.2)]
	public void TestFloatPointVectorAdditionCorrect(float pointX, float pointY, float vectorX, float vectorY, float resultX, float resultY)
	{
		Point2D<float> p1 = new(pointX, pointY);
		Vector2D<float> v1 = new(vectorX, vectorY);
		Point2D<float> p2 = p1 + v1;
		Assert.Equal(resultX, p2.X);
		Assert.Equal(resultY, p2.Y);
		Point2D<float> result = new(resultX, resultY);
		Assert.Equal(result, p2);
	}

	[Theory]
	[InlineData(1.0, 2.0, 0.1, 0.2, 2.0, 5.0)]
	[InlineData(0.1, 0.0, 3.0, float.NaN, 4.0, 6.0)]
	[InlineData(0.0, 0.0, 1.0, float.NegativeInfinity, 1.0, -0.2)]
	public void TestFloatPointVectorAdditionFailed(float pointX, float pointY, float vectorX, float vectorY, float resultX, float resultY)
	{
		Point2D<float> p1 = new(pointX, pointY);
		Vector2D<float> v1 = new(vectorX, vectorY);
		Point2D<float> p2 = p1 + v1;
		Point2D<float> result = new(resultX, resultY);
		Assert.NotEqual(result, p2);
	}
}
