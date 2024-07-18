using AdventOfCode.Common.SpanExtensions;

namespace AdventOfCode.Common.Tests.Extensions;

public class SpanSplitExtensionsTests
{
	[Fact]
	public void SplitWithSingleSeparator_ReturnsExpectedPartsUsingForeach()
	{
		ReadOnlySpan<char> input = "a,b,c";
		var separator = ',';
		var result = input.Split(separator);

		var expectedParts = new[] { "a", "b", "c" };

		int index = 0;
		foreach (var part in result)
		{
			Assert.Equal(expectedParts[index], part.ToString());
			index++;
		}

		Assert.Equal(expectedParts.Length, index); // Ensure all parts were iterated
	}

	[Fact]
	public void SplitWithSpanSeparator_ReturnsExpectedPartsUsingForeach()
	{
		ReadOnlySpan<char> input = "a--b--c";
		ReadOnlySpan<char> separator = "--";
		var result = input.Split(separator);

		var expectedParts = new[] { "a", "b", "c" };

		int index = 0;
		foreach (var part in result)
		{
			Assert.Equal(expectedParts[index], part.ToString());
			index++;
		}

		Assert.Equal(expectedParts.Length, index); // Ensure all parts were iterated
	}

}
