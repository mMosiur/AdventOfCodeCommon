using System;
using System.Collections.Generic;

namespace AdventOfCode.Common.EnumeratorExtensions;

/// <summary>
/// Provides extension methods for enumerators.
/// </summary>
public static class EnumeratorExtensions
{
	/// <summary>
	/// Advances the enumerator to the next element of the collection
	/// and throws an exception if the end of the collection has been reached.
	/// </summary>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	/// <param name="message">Optional message to use as a potential error message when the end of the collection has been reached.</param>
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
}
