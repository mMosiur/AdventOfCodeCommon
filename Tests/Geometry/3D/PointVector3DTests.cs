using AdventOfCode.Common.Geometry;

namespace AdventOfCode.Common.Tests.Geometry;

public class PointVector3DTests
{
	[Theory]
	[InlineData(1, 2, 3, 1, 2, 3, 2, 4, 6)]
	[InlineData(1, 2, 3, 4, 5, 6, 5, 7, 9)]
	[InlineData(0, 0, 0, 10, -2, 6, 10, -2, 6)]
	public void TestIntPointVectorAdditionCorrect(int pointX, int pointY, int pointZ, int vectorX, int vectorY, int vectorZ, int resultX, int resultY, int resultZ)
	{
		Point3D<int> p1 = new(pointX, pointY, pointZ);
		Vector3D<int> v1 = new(vectorX, vectorY, vectorZ);
		Point3D<int> p2 = p1 + v1;
		Assert.Equal(resultX, p2.X);
		Assert.Equal(resultY, p2.Y);
		Assert.Equal(resultZ, p2.Z);
		Point3D<int> result = new(resultX, resultY, resultZ);
		Assert.Equal(result, p2);
	}

	[Theory]
	[InlineData(1, 2, 3, 1, 2, 3, 2, 4, 5)]
	[InlineData(1, 0, 1, 3, 1, 4, 4, 6, 9)]
	[InlineData(0, 0, 0, 10, int.MaxValue, int.MinValue, 10, -2, 0)]
	public void TestIntPointVectorAdditionFailed(int pointX, int pointY, int pointZ, int vectorX, int vectorY, int vectorZ, int resultX, int resultY, int resultZ)
	{
		Point3D<int> p1 = new(pointX, pointY, pointZ);
		Vector3D<int> v1 = new(vectorX, vectorY, vectorZ);
		Point3D<int> p2 = p1 + v1;
		Point3D<int> result = new(resultX, resultY, resultZ);
		Assert.NotEqual(result, p2);
	}

	[Theory]
	[InlineData(0.1, 2.0, 3.0, 1.1, 2.0, 3.0, 1.2, 4.0, 6.0)]
	[InlineData(1.0, 0.2, 0.3, 4.0, 5.0, 0.3, 5.0, 5.2, 0.6)]
	[InlineData(0.0, 0.0, 0.0, 10.0, -0.2, 0.2, 10.0, -0.2, 0.2)]
	public void TestFloatPointVectorAdditionCorrect(float pointX, float pointY, float pointZ, float vectorX, float vectorY, float vectorZ, float resultX, float resultY, float resultZ)
	{
		Point3D<float> p1 = new(pointX, pointY, pointZ);
		Vector3D<float> v1 = new(vectorX, vectorY, vectorZ);
		Point3D<float> p2 = p1 + v1;
		Assert.Equal(resultX, p2.X);
		Assert.Equal(resultY, p2.Y);
		Assert.Equal(resultZ, p2.Z);
		Point3D<float> result = new(resultX, resultY, resultZ);
		Assert.Equal(result, p2);
	}

	[Theory]
	[InlineData(1.0, 2.0, 3.0, 0.1, 0.2, 2.0, 2.0, 5.0, 5.0)]
	[InlineData(0.1, 0.0, 3.0, 3.0, float.NaN, 2.0, 4.0, 6.0, 5.0)]
	[InlineData(0.0, 0.0, 3.0, 1.0, float.NegativeInfinity, 2.0, 1.0, -0.2, 5.0)]
	public void TestFloatPointVectorAdditionFailed(float pointX, float pointY, float pointZ, float vectorX, float vectorY, float vectorZ, float resultX, float resultY, float resultZ)
	{
		Point3D<float> p1 = new(pointX, pointY, pointZ);
		Vector3D<float> v1 = new(vectorX, vectorY, vectorZ);
		Point3D<float> p2 = p1 + v1;
		Point3D<float> result = new(resultX, resultY, resultZ);
		Assert.NotEqual(result, p2);
	}
}
