using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using AdventOfCode.Common.Numerics;

namespace AdventOfCode.Common.Geometry;

/// <summary>
/// Represents geometrical rectangle.
/// </summary>
/// <remarks>
/// The rectangle is grid-like, that is uses integer based coordinates.
/// </remarks>
/// <seealso href="https://en.wikipedia.org/wiki/Rectangle">Rectangle</seealso>
public readonly struct Rectangle<T>
	: IEquatable<Rectangle<T>>
	where T : unmanaged, IBinaryInteger<T>
{

	/// <summary>The range of X values in the rectangle.</summary>
	public required Interval<T> XRange { get; init; }

	/// <summary>The range of Y values in the rectangle.</summary>
	public required Interval<T> YRange { get; init; }

	/// <summary>The area of the rectangle.</summary>
	public T Area => XRange.Count * YRange.Count;

	/// <summary>
	/// Initializes rectangle with the specified ranges.
	/// </summary>
	/// <param name="xRange">The X range of the values to initialize the rectangle with.</param>
	/// <param name="yRange">The Y range of the values to initialize the rectangle with.</param>
	[SetsRequiredMembers]
	public Rectangle(Interval<T> xRange, Interval<T> yRange)
	{
		XRange = xRange;
		YRange = yRange;
	}

	/// <summary>
	/// Initializes rectangle with ranges specified using their start and end values.
	/// </summary>
	/// <param name="xStart">The start value of the X range.</param>
	/// <param name="xEnd">The end value of the X range (inclusive).</param>
	/// <param name="yStart">The start value of the Y range.</param>
	/// <param name="yEnd">The end value of the Y range (inclusive).</param>
	[SetsRequiredMembers]
	public Rectangle(T xStart, T xEnd, T yStart, T yEnd)
		: this(new(xStart, xEnd), new(yStart, yEnd))
	{
	}

	/// <summary>
	/// Initializes rectangle as a copy of the other specified <paramref name="rectangle"/>.
	/// </summary>
	/// <param name="rectangle">The rectangle to be used as a base for copy initialization.</param>
	[SetsRequiredMembers]
	public Rectangle(Rectangle<T> rectangle)
		: this(rectangle.XRange, rectangle.YRange)
	{
	}

	/// <summary>
	/// Enumerates all point in the rectangle.
	/// </summary>
	public IEnumerable<Point2D<T>> Points
	{
		get
		{
			foreach (T x in XRange)
			{
				foreach (T y in YRange)
				{
					yield return new(x, y);
				}
			}
		}
	}

	/// <summary>
	/// Enumerate all corner points of the rectangle.
	/// </summary>
	public IEnumerable<Point2D<T>> CornerPoints
	{
		get
		{
			yield return new(XRange.Start, YRange.Start);
			yield return new(XRange.Start, YRange.End);
			yield return new(XRange.End, YRange.Start);
			yield return new(XRange.End, YRange.End);
		}
	}

	/// <summary>
	/// Determines whether the specified point is inside the rectangle.
	/// </summary>
	/// <param name="point">The point to be checked.</param>
	public bool Contains(Point2D<T> point)
	{
		return XRange.Contains(point.X) && YRange.Contains(point.Y);
	}

	/// <summary>
	/// Determines whether the specified rectangle is contained inside the current rectangle.
	/// </summary>
	/// <param name="cuboid">The rectangle to be checked.</param>
	public bool Contains(Rectangle<T> rectangle)
	{
		return XRange.Contains(rectangle.XRange) && YRange.Contains(rectangle.YRange);
	}

	/// <summary>
	/// Determines whether the specified <paramref name="other"/> rectangle is equal to the current rectangle.
	/// </summary>
	/// <param name="other">The other rectangle to be compared.</param>
	public bool Equals(Rectangle<T> other)
	{
		return XRange == other.XRange && YRange == other.YRange;
	}

	/// <inheritdoc/>
	public override bool Equals(object? obj)
	{
		return obj is Rectangle<T> rectangle && Equals(rectangle);
	}

	/// <inheritdoc/>
	public override int GetHashCode()
	{
		return HashCode.Combine(XRange, YRange);
	}

	/// <inheritdoc/>
	public static bool operator ==(Rectangle<T> left, Rectangle<T> right) => left.Equals(right);

	/// <inheritdoc/>
	public static bool operator !=(Rectangle<T> left, Rectangle<T> right) => !(left == right);

}
