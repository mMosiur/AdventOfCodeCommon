using System;
using System.Collections.Generic;

namespace AdventOfCode.Common.EnumeratorExtensions;

/// <summary>
/// Provides extension methods for enumerators.
/// </summary>
public static class EnumeratorExtensions
{
	/// <summary>
	/// Advances <paramref name="enumerator"/> to the next element of the collection
	/// and throws an exception if the end of the collection has been reached.
	/// </summary>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	/// <param name="message">Optional message to use as error message when the end of the collection has been reached.</param>
	/// <typeparam name="T">The type of objects to enumerate.</typeparam>
	/// <exception cref="ArgumentNullException">Thrown when the <paramref name="enumerator"/> is <see langword="null"/>.</exception>
	/// <exception cref="InvalidOperationException">Throws when the end of the collection has been reached.</exception>
	public static void EnsureMoveNext<T>(this IEnumerator<T> enumerator, string? message = null)
	{
		ArgumentNullException.ThrowIfNull(enumerator);
		if (!enumerator.MoveNext())
		{
			message ??= "The enumerator has reached the end of the collection.";
			throw new InvalidOperationException(message);
		}
	}

	/// <summary>
	/// Advances the <paramref name="enumerator"/> to the next non-empty <see langword="string"/> of the collection
	/// (treats whitespace-only string as empty).
	/// </summary>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	/// <param name="skipCurrent">Whether to skip the current element in enumerator regardless of its content.</param>
	/// <returns>Whether the enumerator has reached non-empty line (did not reach end).</returns>
	/// <exception cref="ArgumentNullException">Thrown when the <paramref name="enumerator"/> is <see langword="null"/>.</exception>
	public static bool MoveToNextNonEmptyLine(this IEnumerator<string> enumerator, bool skipCurrent = true)
	{
		ArgumentNullException.ThrowIfNull(enumerator);
		if (skipCurrent)
		{
			if (!enumerator.MoveNext())
			{
				return false;
			}
		}
		while (string.IsNullOrWhiteSpace(enumerator.Current))
		{
			if (!enumerator.MoveNext())
			{
				return false;
			}
		}
		return true;
	}

	/// <summary>
	/// Advances the <paramref name="enumerator"/> to the next non-empty <see langword="string"/> line
	/// (treats whitespace-only <see langword="string"/> as empty).
	/// Throws exception if the end has been reached by the <paramref name="enumerator"/>.
	/// </summary>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	/// <param name="message">Optional message to use as error message when the end of the collection has been reached.</param>
	/// <param name="skipCurrent">Whether to skip the current element in enumerator regardless of its content.</param>
	/// <exception cref="ArgumentNullException">Thrown when the <paramref name="enumerator"/> is <see langword="null"/>.</exception>
	/// <exception cref="InvalidOperationException">Throws when the end of the collection has been reached.</exception>
	public static void EnsureMoveToNextNonEmptyLine(this IEnumerator<string> enumerator, string? message = null, bool skipCurrent = true)
	{
		ArgumentNullException.ThrowIfNull(enumerator);
		if (skipCurrent)
		{
			enumerator.EnsureMoveNext(message);
		}
		while (string.IsNullOrWhiteSpace(enumerator.Current))
		{
			enumerator.EnsureMoveNext(message);
		}
	}
}
