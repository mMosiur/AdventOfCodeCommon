using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;

namespace AdventOfCode.Common;

public static partial class MathExtended
{
	[Pure]
	public static T LeastCommonMultiple<T>(IEnumerable<T> values)
		where T : struct, IBinaryInteger<T>
	{
		return values.Aggregate(LeastCommonMultiple);
	}

	[Pure]
	public static TResult LeastCommonMultiple<TSource, TResult>(IEnumerable<TSource> values)
		where TSource : struct, IBinaryInteger<TSource>
		where TResult : struct, IBinaryInteger<TResult>
	{
		return values.Select(TResult.CreateChecked).Aggregate(LeastCommonMultiple);
	}

	[Pure]
	public static T LeastCommonMultiple<T>(T a, T b)
		where T : struct, IBinaryInteger<T>
	{
		a = T.Abs(a);
		b = T.Abs(b);
		T div = b / GreatestCommonDivisorInternal(a, b);
		return checked(a * div);
	}

	[Pure]
	public static TResult LeastCommonMultiple<TSource, TResult>(TSource a, TSource b)
		where TSource : struct, IBinaryInteger<TSource>
		where TResult : struct, IBinaryInteger<TResult>
	{
		a = TSource.Abs(a);
		b = TSource.Abs(b);
		TSource div = b / GreatestCommonDivisorInternal(a, b);
		return checked(TResult.CreateChecked(a) * TResult.CreateChecked(div));
	}
}
