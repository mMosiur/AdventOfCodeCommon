using System;
using System.Text;

namespace AdventOfCode.Common.EnumeratorExtensions;

/// <summary>
/// Provides extension methods for ref struct enumerators not covered in <see cref="EnumeratorExtensions"/>
/// as they do not implement <see cref="IEnumerator{T}"/> interface.
/// </summary>
public static class SpanEnumeratorExtensions
{

	/// <summary>
	/// Advances <paramref name="enumerator"/> to the next <see cref="ReadOnlySpan{char}"/> line
	/// and throws an exception if the end of the collection has been reached.
	/// </summary>
	/// <remarks>
	/// This method behaves adequately to <see cref="EnumeratorExtensions.EnsureMoveNext{T}"/>.
	/// </remarks>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	/// <param name="message">Optional message to use as error message when the end of the collection has been reached.</param>
	/// <exception cref="InvalidOperationException">Throws when the end of the collection has been reached.</exception>
	public static void EnsureMoveNext(this SpanLineEnumerator enumerator, string? message = null)
	{
		if (!enumerator.MoveNext())
		{
			message ??= "The enumerator has reached the end of the collection.";
			throw new InvalidOperationException(message);
		}
	}

	/// <summary>
	/// Advances <paramref name="enumerator"/> to the next <see cref="Rune"/> element
	/// and throws an exception if the end of the collection has been reached.
	/// </summary>
	/// <remarks>
	/// This method behaves adequately to <see cref="EnumeratorExtensions.EnsureMoveNext{T}"/>.
	/// </remarks>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	/// <param name="message">Optional message to use as error message when the end of the collection has been reached.</param>
	/// <exception cref="InvalidOperationException">Throws when the end of the collection has been reached.</exception>
	public static void EnsureMoveNext(this SpanRuneEnumerator enumerator, string? message = null)
	{
		if (!enumerator.MoveNext())
		{
			message ??= "The enumerator has reached the end of the collection.";
			throw new InvalidOperationException(message);
		}
	}

	/// <summary>
	/// Advances <paramref name="enumerator"/> to the next <typeparamref name="T"/> element
	/// and throws an exception if the end of the collection has been reached.
	/// </summary>
	/// <remarks>
	/// This method behaves adequately to <see cref="EnumeratorExtensions.EnsureMoveNext{T}"/>.
	/// </remarks>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	/// <param name="message">Optional message to use as error message when the end of the collection has been reached.</param>
	/// <exception cref="InvalidOperationException">Throws when the end of the collection has been reached.</exception>
	public static void EnsureMoveNext<T>(this ReadOnlySpan<T>.Enumerator enumerator, string? message = null)
	{
		if (!enumerator.MoveNext())
		{
			message ??= "The enumerator has reached the end of the collection.";
			throw new InvalidOperationException(message);
		}
	}

	/// <summary>
	/// Advances <paramref name="enumerator"/> to the next <typeparamref name="T"/> element
	/// and throws an exception if the end of the collection has been reached.
	/// </summary>
	/// <remarks>
	/// This method behaves adequately to <see cref="EnumeratorExtensions.EnsureMoveNext{T}"/>.
	/// </remarks>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	/// <param name="message">Optional message to use as error message when the end of the collection has been reached.</param>
	/// <exception cref="InvalidOperationException">Throws when the end of the collection has been reached.</exception>
	public static void EnsureMoveNext<T>(this Span<T>.Enumerator enumerator, string? message = null)
	{
		if (!enumerator.MoveNext())
		{
			message ??= "The enumerator has reached the end of the collection.";
			throw new InvalidOperationException(message);
		}
	}

	/// <summary>
	/// Advances <paramref name="enumerator"/> to the next <see cref="ReadOnlySpan{T}"/> fragment of the split
	/// and throws an exception if the end of the collection has been reached.
	/// </summary>
	/// <remarks>
	/// This method behaves adequately to <see cref="EnumeratorExtensions.EnsureMoveNext{T}"/>.
	/// </remarks>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	/// <param name="message">Optional message to use as error message when the end of the collection has been reached.</param>
	/// <exception cref="InvalidOperationException">Throws when the end of the collection has been reached.</exception>
	public static void EnsureMoveNext<T>(this SpanSplit<T>.Enumerator enumerator, string? message = null) where T : IEquatable<T>
	{
		if (!enumerator.MoveNext())
		{
			message ??= "The enumerator has reached the end of the collection.";
			throw new InvalidOperationException(message);
		}
	}

	/// <summary>
	/// Advances the <paramref name="enumerator"/> to the next non-empty <see cref="ReadOnlySpan{char}"/> line
	/// (treats whitespace-only <see cref="ReadOnlySpan{char}"/> as empty).
	/// </summary>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	/// <param name="skipCurrent">Whether to skip the current element in enumerator regardless of its content.</param>
	/// <returns>Whether the enumerator has reached non-empty line (did not reach end).</returns>
	public static bool MoveToNextNonEmptyLine(this SpanLineEnumerator enumerator, bool skipCurrent = true)
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
	/// Advances the <paramref name="enumerator"/> to the next non-empty <see cref="ReadOnlySpan{char}"/> line
	/// (treats whitespace-only <see cref="ReadOnlySpan{char}"/> as empty).
	/// Throws exception if the end has been reached by the <paramref name="enumerator"/>.
	/// </summary>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	/// <param name="message">Optional message to use as error message when the end of the collection has been reached.</param>
	/// <param name="skipCurrent">Whether to skip the current element in enumerator regardless of its content.</param>
	/// <exception cref="InvalidOperationException">Throws when the end of the collection has been reached.</exception>
	public static void EnsureMoveToNextNonEmptyLine(this SpanLineEnumerator enumerator, string? message = null, bool skipCurrent = true)
	{
		if (skipCurrent)
		{
			enumerator.EnsureMoveNext(message);
		}
		// No need to check for `IsEmpty` because `IsWhiteSpace()` will in that case return false also.
		while (enumerator.Current.IsWhiteSpace())
		{
			enumerator.EnsureMoveNext(message);
		}
	}

	/// <summary>
	/// Advances the <paramref name="enumerator"/> to the next non-empty <see cref="ReadOnlySpan{char}"/> split part
	/// (treats whitespace-only <see cref="ReadOnlySpan{char}"/> as empty).
	/// </summary>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	/// <param name="skipCurrent">Whether to skip the current element in enumerator regardless of its content.</param>
	/// <returns>Whether the enumerator has reached non-empty line (did not reach end).</returns>
	public static bool MoveToNextNonEmptyPart(this SpanSplit<char>.Enumerator enumerator, bool skipCurrent = true)
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
	/// Advances the <paramref name="enumerator"/> to the next non-empty <see cref="ReadOnlySpan{char}"/> split part
	/// (treats whitespace-only <see cref="ReadOnlySpan{char}"/> as empty).
	/// Throws exception if the end has been reached by the <paramref name="enumerator"/>.
	/// </summary>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	/// <param name="message">Optional message to use as error message when the end of the collection has been reached.</param>
	/// <param name="skipCurrent">Whether to skip the current element in enumerator regardless of its content.</param>
	/// <exception cref="InvalidOperationException">Throws when the end of the collection has been reached.</exception>
	public static void EnsureMoveToNextNonEmptyPart(this SpanSplit<char>.Enumerator enumerator, string? message = null, bool skipCurrent = true)
	{
		if (skipCurrent)
		{
			enumerator.EnsureMoveNext(message);
		}
		// No need to check for `IsEmpty` because `IsWhiteSpace()` will in that case return false also.
		while (enumerator.Current.IsWhiteSpace())
		{
			enumerator.EnsureMoveNext(message);
		}
	}

}
