using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using AdventOfCode.Common.Numerics;

namespace AdventOfCode.Common.Geometry;

/// <summary>
/// Represents geometrical cuboid (specifically rectangular cuboid).
/// </summary>
/// <remarks>
/// The cuboid is grid-like, that is uses integer based coordinates.
/// </remarks>
/// <seealso href="https://en.wikipedia.org/wiki/Cuboid#Rectangular_cuboid">Rectangular cuboid</seealso>
public readonly struct Cuboid<T>
	: IEquatable<Cuboid<T>>
	where T : unmanaged, IBinaryInteger<T>
{

	/// <summary>The range of X values in the cuboid.</summary>
	public required Interval<T> XRange { get; init; }

	/// <summary>The range of Y values in the cuboid.</summary>
	public required Interval<T> YRange { get; init; }

	/// <summary>The range of Z values in the cuboid.</summary>
	public required Interval<T> ZRange { get; init; }

	/// <summary>The volume of the cuboid.</summary>
	public T Volume => XRange.Count * YRange.Count * ZRange.Count;

	/// <summary>
	/// Initializes cuboid with the specified ranges.
	/// </summary>
	/// <param name="xRange">The X range of the values to initialize the cuboid with.</param>
	/// <param name="yRange">The Y range of the values to initialize the cuboid with.</param>
	/// <param name="zRange">The Z range of the values to initialize the cuboid with.</param>
	[SetsRequiredMembers]
	public Cuboid(Interval<T> xRange, Interval<T> yRange, Interval<T> zRange)
	{
		XRange = xRange;
		YRange = yRange;
		ZRange = zRange;
	}

	/// <summary>
	/// Initializes cuboid with ranges specified using their start and end values.
	/// </summary>
	/// <param name="xStart">The start value of the X range.</param>
	/// <param name="xEnd">The end value of the X range (inclusive).</param>
	/// <param name="yStart" > The start value of the Y range.</param>
	/// <param name="yEnd" > The end value of the Y range (inclusive).</param>
	/// <param name="zStart" > The start value of the Z range.</param>
	/// <param name="zEnd" > The end value of the Z range (inclusive).</param>
	[SetsRequiredMembers]
	public Cuboid(T xStart, T xEnd, T yStart, T yEnd, T zStart, T zEnd)
		: this(new(xStart, xEnd), new(yStart, yEnd), new(zStart, zEnd))
	{
	}

	/// <summary>
	/// Initializes cuboid as a copy of the other specified <paramref name="cuboid"/>.
	/// </summary>
	/// <param name="cuboid">The cuboid to be used as a base for copy initialization.</param>
	[SetsRequiredMembers]
	public Cuboid(Cuboid<T> cuboid)
		: this(cuboid.XRange, cuboid.YRange, cuboid.ZRange)
	{
	}

	/// <summary>
	/// Enumerates all point in the cuboid.
	/// </summary>
	public IEnumerable<Point3D<T>> Points
	{
		get
		{
			foreach (T x in XRange)
			{
				foreach (T y in YRange)
				{
					foreach (T z in ZRange)
					{
						yield return new(x, y, z);
					}
				}
			}
		}
	}

	/// <summary>
	/// Enumerate all corner points of the cuboid.
	/// </summary>
	public IEnumerable<Point3D<T>> CornerPoint
	{
		get
		{
			yield return new(XRange.Start, YRange.Start, ZRange.Start);
			yield return new(XRange.Start, YRange.Start, ZRange.End);
			yield return new(XRange.Start, YRange.End, ZRange.Start);
			yield return new(XRange.Start, YRange.End, ZRange.End);
			yield return new(XRange.End, YRange.Start, ZRange.Start);
			yield return new(XRange.End, YRange.Start, ZRange.End);
			yield return new(XRange.End, YRange.End, ZRange.Start);
			yield return new(XRange.End, YRange.End, ZRange.End);
		}
	}

	/// <summary>
	/// Determines whether the specified point is inside the cuboid.
	/// </summary>
	/// <param name="point">The point to be checked.</param>
	public bool Contains(Point3D<T> point)
	{
		return XRange.Contains(point.X) && YRange.Contains(point.Y) && ZRange.Contains(point.Z);
	}

	/// <summary>
	/// Determines whether the specified cuboid is contained inside the current cuboid.
	/// </summary>
	/// <param name="cuboid">The cuboid to be checked.</param>
	public bool Contains(Cuboid<T> cuboid)
	{
		return XRange.Contains(cuboid.XRange) && YRange.Contains(cuboid.YRange) && ZRange.Contains(cuboid.ZRange);
	}

	/// <summary>
	/// Determines whether the specified <paramref name="other"/> cuboid is equal to the current cuboid.
	/// </summary>
	/// <param name="other">The other cuboid to be compared.</param>
	public bool Equals(Cuboid<T> other)
	{
		return XRange == other.XRange && YRange == other.YRange && ZRange == other.ZRange;
	}

	/// <inheritdoc/>
	public override bool Equals(object? obj)
	{
		return obj is Cuboid<T> cuboid && Equals(cuboid);
	}

	/// <inheritdoc/>
	public override int GetHashCode()
	{
		return HashCode.Combine(XRange, YRange, ZRange);
	}

	/// <inheritdoc/>
	public static bool operator ==(Cuboid<T> left, Cuboid<T> right) => left.Equals(right);

	/// <inheritdoc/>
	public static bool operator !=(Cuboid<T> left, Cuboid<T> right) => !(left == right);

}
