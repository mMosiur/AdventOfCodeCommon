using AdventOfCode.Common.SpanExtensions;

namespace AdventOfCode.Common.Tests.Extensions;

public class SpanSplitExtensionsTests
{
	[Fact]
	public void SplitWithSingleSeparator_ReturnsExpectedPartsUsingForeach()
	{
		ReadOnlySpan<char> input = "a,b,c";
		char separator = ',';
		string[] expectedParts = ["a", "b", "c"];

		int index = 0;
		foreach (ReadOnlySpan<char> part in input.SplitAsSpans(separator))
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
		string[] expectedParts = ["a", "b", "c"];

		int index = 0;
		foreach (ReadOnlySpan<char> part in input.SplitAsSpans(separator))
		{
			Assert.Equal(expectedParts[index], part.ToString());
			index++;
		}

		Assert.Equal(expectedParts.Length, index); // Ensure all parts were iterated
	}

}
