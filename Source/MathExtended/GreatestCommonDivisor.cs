using System;
using System.Diagnostics.Contracts;
using System.Numerics;

namespace AdventOfCode.Common;

public static partial class MathExtended
{
	[Pure]
	public static T GreatestCommonDivisor<T>(T a, T b)
		where T : struct, IBinaryInteger<T>
	{
		return GreatestCommonDivisorInternal(T.Abs(a), T.Abs(b));
	}

	[Pure]
	private static T GreatestCommonDivisorInternal<T>(T a, T b)
		where T : struct, IBinaryInteger<T>
	{
		// Base cases: gcd(n, 0) = gcd(0, n) = n
		if (T.IsZero(a))
			return b;
		if (T.IsZero(b))
			return a;

		// Using identities 2 and 3:
		// gcd(2ⁱ a, 2ʲ v) = 2ᵏ gcd(u, v) with u, v odd and k = min(i, j)
		// 2ᵏ is the greatest power of two that divides both 2ⁱ u and 2ʲ v
		int i = int.CreateTruncating(T.TrailingZeroCount(a));
		a >>= i;
		int j = int.CreateTruncating(T.TrailingZeroCount(b));
		b >>= j;
		int k = Math.Min(i, j);

		while (true)
		{
			// Swap if necessary so u ≤ v
			if (a > b)
			{
				(a, b) = (b, a);
			}

			// Identity 4: gcd(u, v) = gcd(u, v-u) as u ≤ v and u, v are both odd
			b -= a;
			// v is now even

			if (T.IsZero(b))
			{
				// Identity 1: gcd(u, 0) = u
				// The shift by k is necessary to add back the 2ᵏ factor that was removed before the loop
				return a << k;
			}

			// Identity 3: gcd(u, 2ʲ v) = gcd(u, v) as u is odd
			b >>= int.CreateTruncating(T.TrailingZeroCount(b));
		}
	}
}
