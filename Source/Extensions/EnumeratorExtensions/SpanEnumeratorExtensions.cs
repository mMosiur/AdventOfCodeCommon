using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Common.EnumeratorExtensions;

/// <summary>
/// Provides extension methods for ref struct enumerators not covered in <see cref="EnumeratorExtensions"/>
/// as they do not implement <see cref="IEnumerator{T}"/> interface.
/// </summary>
public static class SpanEnumeratorExtensions
{

	/// <summary>
	/// Advances <paramref name="enumerator"/> to the next line
	/// and throws an exception if it had failed to do so.
	/// </summary>
	/// <remarks>
	/// This method behaves adequately to <see cref="EnumeratorExtensions.EnsureMoveNext{T}"/>.
	/// </remarks>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	/// <exception cref="InvalidOperationException">Thrown when the enumerator can't move to the next item.</exception>
	public static void EnsureMoveNext(this ref SpanLineEnumerator enumerator)
	{
		if (!enumerator.MoveNext())
		{
			throw new InvalidOperationException("The enumerator has reached the end of the collection.");
		}
	}

	/// <summary>
	/// Advances <paramref name="enumerator"/> to the next rune
	/// and throws an exception if it fails to do so (the end had been reached).
	/// </summary>
	/// <remarks>
	/// This method behaves adequately to <see cref="EnumeratorExtensions.EnsureMoveNext{T}"/>.
	/// </remarks>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	/// <exception cref="InvalidOperationException">Thrown when the enumerator can't move to the next item.</exception>
	public static void EnsureMoveNext(this ref SpanRuneEnumerator enumerator)
	{
		if (!enumerator.MoveNext())
		{
			throw new InvalidOperationException("The enumerator has reached the end of the collection.");
		}
	}

	/// <summary>
	/// Advances <paramref name="enumerator"/> to the next element
	/// and throws an exception if it fails to do so (the end had been reached).
	/// </summary>
	/// <remarks>
	/// This method behaves adequately to <see cref="EnumeratorExtensions.EnsureMoveNext{T}"/>.
	/// </remarks>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	/// <exception cref="InvalidOperationException">Thrown when the enumerator can't move to the next item.</exception>
	public static void EnsureMoveNext<T>(this ref ReadOnlySpan<T>.Enumerator enumerator)
	{
		if (!enumerator.MoveNext())
		{
			throw new InvalidOperationException("The enumerator has reached the end of the collection.");
		}
	}

	/// <summary>
	/// Advances <paramref name="enumerator"/> to the next element
	/// and throws an exception if it fails to do so (the end had been reached).
	/// </summary>
	/// <remarks>
	/// This method behaves adequately to <see cref="EnumeratorExtensions.EnsureMoveNext{T}"/>.
	/// </remarks>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	/// <exception cref="InvalidOperationException">Thrown when the enumerator can't move to the next item.</exception>
	public static void EnsureMoveNext<T>(this ref Span<T>.Enumerator enumerator)
	{
		if (!enumerator.MoveNext())
		{
			throw new InvalidOperationException("The enumerator has reached the end of the collection.");
		}
	}

	/// <summary>
	/// Advances <paramref name="enumerator"/> to the next fragment of the split
	/// and throws an exception if it fails to do so (the end had been reached).
	/// </summary>
	/// <remarks>
	/// This method behaves adequately to <see cref="EnumeratorExtensions.EnsureMoveNext{T}"/>.
	/// </remarks>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	/// <exception cref="InvalidOperationException">Thrown when the enumerator can't move to the next item.</exception>
	public static void EnsureMoveNext<T>(this ref MemoryExtensions.SpanSplitEnumerator<char> enumerator) where T : IEquatable<T>
	{
		if (!enumerator.MoveNext())
		{
			throw new InvalidOperationException("The enumerator has reached the end of the collection.");
		}
	}

	/// <summary>
	/// Advances the <paramref name="enumerator"/> to the next non-empty line
	/// (treats whitespace-only lines as empty).
	/// </summary>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	/// <param name="skipCurrent">Whether to skip the current element in enumerator regardless of its content.</param>
	/// <returns>Whether the enumerator has reached non-empty line (did not reach end).</returns>
	public static bool MoveToNextNonEmptyLine(this ref SpanLineEnumerator enumerator, bool skipCurrent = true)
	{
		if (skipCurrent)
		{
			if (!enumerator.MoveNext())
			{
				return false;
			}
		}
		// No need to check for `IsEmpty` because `IsWhiteSpace()` will in that case return false also.
		while (enumerator.Current.IsWhiteSpace())
		{
			if (!enumerator.MoveNext())
			{
				return false;
			}
		}
		return true;
	}

	/// <summary>
	/// Advances the <paramref name="enumerator"/> to the next non-empty line
	/// (treats whitespace-only lines as empty).
	/// Throws an exception if it fails to do so (the end had been reached).
	/// </summary>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	/// <param name="skipCurrent">Whether to skip the current element in enumerator regardless of its content.</param>
	/// <exception cref="InvalidOperationException">Thrown when the enumerator can't move to the next item.</exception>
	public static void EnsureMoveToNextNonEmptyLine(this ref SpanLineEnumerator enumerator, bool skipCurrent = true)
	{
		if (!enumerator.MoveToNextNonEmptyLine(skipCurrent))
		{
			throw new InvalidOperationException("The enumerator has reached the end of the collection.");
		}
	}

	/// <summary>
	/// Advances the <paramref name="enumerator"/> to the next non-empty split part
	/// (treats whitespace-only parts as empty).
	/// </summary>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	/// <param name="spanSplitText">The text span that is being split and enumerated.</param>
	/// <param name="skipCurrent">Whether to skip the current element in enumerator regardless of its content.</param>
	/// <returns>Whether the enumerator has reached non-empty part (did not reach end).</returns>
	public static bool MoveToNextNonEmptyPart(this ref MemoryExtensions.SpanSplitEnumerator<char> enumerator, ReadOnlySpan<char> spanSplitText, bool skipCurrent = true)
	{
		if (skipCurrent)
		{
			if (!enumerator.MoveNext())
			{
				return false;
			}
		}
		// No need to check for `IsEmpty` because `IsWhiteSpace()` will in that case return false also.
		while (spanSplitText[enumerator.Current].IsWhiteSpace())
		{
			if (!enumerator.MoveNext())
			{
				return false;
			}
		}
		return true;
	}

	/// <summary>
	/// Advances the <paramref name="enumerator"/> to the next non-empty split part
	/// (treats whitespace-only lines as empty).
	/// Throws exception if it fails to do so (the end had been reached).
	/// </summary>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	/// <param name="spanSplitText">The text span that is being split and enumerated.</param>
	/// <param name="skipCurrent">Whether to skip the current element in enumerator regardless of its content.</param>
	/// <exception cref="InvalidOperationException">Thrown when the enumerator can't move to the next item.</exception>
	public static void EnsureMoveToNextNonEmptyPart(this ref MemoryExtensions.SpanSplitEnumerator<char> enumerator, ReadOnlySpan<char> spanSplitText, bool skipCurrent = true)
	{
		if (!enumerator.MoveToNextNonEmptyPart(spanSplitText, skipCurrent))
		{
			throw new InvalidOperationException("The enumerator has reached the end of the collection.");
		}
	}
}
