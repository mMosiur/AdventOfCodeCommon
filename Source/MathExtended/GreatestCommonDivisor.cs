using System.Diagnostics.Contracts;
using System.Numerics;

namespace AdventOfCode.Common;

public static partial class MathExtended
{
	/// <summary>
	/// Calculates the greatest common divisor of two numbers.
	/// </summary>
	/// <remarks>
	/// Based on <a href="https://docs.rs/gcd/latest/src/gcd/lib.rs.html">Rust's implementation</a> of the binary GCD algorithm.
	/// </remarks>
	[Pure]
	public static T GreatestCommonDivisor<T>(T a, T b)
		where T : struct, IBinaryInteger<T>
	{
		return GreatestCommonDivisorInternal(T.Abs(a), T.Abs(b));
	}

	/// <summary>
	/// Calculates the greatest common divisor of two numbers.
	/// Assumes both numbers are non-negative at this point.
	/// </summary>
	/// <remarks>
	/// Based on <a href="https://docs.rs/gcd/latest/src/gcd/lib.rs.html">Rust's implementation</a> of the binary GCD algorithm.
	/// </remarks>
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
