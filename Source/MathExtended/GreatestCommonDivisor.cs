using System.Diagnostics.Contracts;
using System.Numerics;

namespace AdventOfCode.Common;

// Mathematical functions for Greatest Common Divisor calculation.
public static partial class MathExtended
{
	/// <summary>
	/// Calculates the greatest common divisor of two numbers.
	/// </summary>
	/// <param name="a">The first number for GCD calculation.</param>
	/// <param name="b">The second number for GCD calculation.</param>
	/// <typeparam name="T">The numeric type of the numbers used.</typeparam>
	/// <returns>The greatest common divisor for provided two numbers.</returns>
	[Pure]
	public static T GreatestCommonDivisor<T>(T a, T b)
		where T : struct, IBinaryInteger<T>
	{
		return GreatestCommonDivisorInternal(T.Abs(a), T.Abs(b));
	}

	/// <summary>
	/// Calculates the greatest common divisor of two numbers assuming both numbers are non-negative.
	/// </summary>
	/// <param name="a">The first number for GCD calculation.</param>
	/// <param name="b">The second number for GCD calculation.</param>
	/// <typeparam name="T">The numeric type of the numbers used.</typeparam>
	/// <returns>The greatest common divisor for provided two numbers.</returns>
	[Pure]
	private static T GreatestCommonDivisorInternal<T>(T a, T b)
		where T : struct, IBinaryInteger<T>
	{
		if (T.IsZero(a))
		{
			return b;
		}

		if (T.IsZero(b))
		{
			return a;
		}

		int shift = int.CreateTruncating(T.TrailingZeroCount(a | b));
		a >>= shift;
		b >>= shift;
		a >>= int.CreateTruncating(T.TrailingZeroCount(a));

		while (true)
		{
			b >>= int.CreateTruncating(T.TrailingZeroCount(b));

			if (a > b)
			{
				(a, b) = (b, a);
			}

			b -= a;

			if (T.IsZero(b))
			{
				break;
			}
		}

		return a << shift;
	}
}
