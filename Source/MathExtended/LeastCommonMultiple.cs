using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;

namespace AdventOfCode.Common;

// Mathematical functions for Least Common Multiple calculation.
public static partial class MathExtended
{
	/// <summary>
	/// Calculates the <a href="https://en.wikipedia.org/wiki/Least_common_multiple">least common multiple</a> of a collection of numbers.
	/// </summary>
	/// <param name="values">The collection of numbers for LCM calculation.</param>
	/// <typeparam name="T">The numeric type of the numbers used.</typeparam>
	/// <returns>The least common multiple for provided collection of numbers.</returns>
	/// <exception cref="ArgumentNullException">Thrown when the provided collection is null.</exception>
	[Pure]
	public static T LeastCommonMultiple<T>(IEnumerable<T> values)
		where T : struct, IBinaryInteger<T>
	{
		ArgumentNullException.ThrowIfNull(values);
		return values.Aggregate(LeastCommonMultiple);
	}

	/// <summary>
	/// Calculates the <a href="https://en.wikipedia.org/wiki/Least_common_multiple">least common multiple</a> of a collection of numbers.
	/// </summary>
	/// <param name="values">The collection of numbers for LCM calculation.</param>
	/// <typeparam name="TSource">The numeric type of the input numbers.</typeparam>
	/// <typeparam name="TResult">The numeric type of the result numbers.</typeparam>
	/// <returns>The least common multiple for provided collection of numbers.</returns>
	/// <exception cref="ArgumentNullException">Thrown when the provided collection is null.</exception>
	/// <remarks>
	/// Type <typeparamref name="TResult"/> is supposed tom help with situations where the LCM of two numbers is too
	/// large to fit into <typeparamref name="TSource"/> (e.g. when LCM of two <see cref="int"/> numbers only fits in a <see cref="long"/>).
	/// </remarks>
	[Pure]

	public static TResult LeastCommonMultiple<TSource, TResult>(IEnumerable<TSource> values)
		where TSource : struct, IBinaryInteger<TSource>
		where TResult : struct, IBinaryInteger<TResult>
	{
		ArgumentNullException.ThrowIfNull(values);
		return values.Select(TResult.CreateChecked).Aggregate(LeastCommonMultiple);
	}

	/// <summary>
	/// Calculates the <a href="https://en.wikipedia.org/wiki/Least_common_multiple">least common multiple</a> of two numbers.
	/// </summary>
	/// <param name="a">The first number for LCM calculation.</param>
	/// <param name="b">The second number for LCM calculation.</param>
	/// <typeparam name="T">The numeric type of the numbers used.</typeparam>
	/// <returns>The least common multiple for provided two numbers.</returns>
	[Pure]
	public static T LeastCommonMultiple<T>(T a, T b)
		where T : struct, IBinaryInteger<T>
	{
		if (T.IsZero(a) || T.IsZero(b))
		{
			throw new ArgumentException("Least common multiple is not defined for zero values.");
		}
		a = T.Abs(a);
		b = T.Abs(b);
		T gcd = GreatestCommonDivisorInternal(a, b);
		T div = b / gcd; // Will work with integers also as by definition gcd of b divides b.
		return checked(a * div);
	}

	/// <summary>
	/// Calculates the <a href="https://en.wikipedia.org/wiki/Least_common_multiple">least common multiple</a> of two numbers.
	/// </summary>
	/// <param name="a">The first number for LCM calculation.</param>
	/// <param name="b">The second number for LCM calculation.</param>
	/// <typeparam name="TSource">The numeric type of the input numbers.</typeparam>
	/// <typeparam name="TResult">The numeric type of the result numbers.</typeparam>
	/// <returns>The least common multiple for provided two numbers.</returns>
	/// <remarks>
	/// Type <typeparamref name="TResult"/> is supposed tom help with situations where the LCM of two numbers is too
	/// large to fit into <typeparamref name="TSource"/> (e.g. when LCM of two <see cref="int"/> numbers only fits in a <see cref="long"/>).
	/// </remarks>
	[Pure]
	public static TResult LeastCommonMultiple<TSource, TResult>(TSource a, TSource b)
		where TSource : struct, IBinaryInteger<TSource>
		where TResult : struct, IBinaryInteger<TResult>
	{
		return LeastCommonMultiple(TResult.CreateChecked(a), TResult.CreateChecked(b));
	}
}
