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
	/// <see cref="SpanLineEnumerator"/> extensions.
	/// </summary>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	extension(ref SpanLineEnumerator enumerator)
	{
		/// <summary>
		/// Advances the enumerator to the next line
		/// and throws an exception if it had failed to do so.
		/// </summary>
		/// <remarks>
		/// This method behaves adequately to <see cref="EnumeratorExtensions.EnsureMoveNext{T}"/>.
		/// </remarks>
		/// <exception cref="InvalidOperationException">Thrown when the enumerator can't move to the next item.</exception>
		public void EnsureMoveNext()
		{
			if (!enumerator.MoveNext())
			{
				throw new InvalidOperationException("The enumerator has reached the end of the collection.");
			}
		}
	}

	/// <summary>
	/// <see cref="SpanRuneEnumerator"/> extensions.
	///	 </summary>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	extension(ref SpanRuneEnumerator enumerator)
	{
		/// <summary>
		/// Advances the enumerator to the next rune
		/// and throws an exception if it fails to do so (the end had been reached).
		/// </summary>
		/// <remarks>
		/// This method behaves adequately to <see cref="EnumeratorExtensions.EnsureMoveNext{T}"/>.
		/// </remarks>
		/// <exception cref="InvalidOperationException">Thrown when the enumerator can't move to the next item.</exception>
		public void EnsureMoveNext()
		{
			if (!enumerator.MoveNext())
			{
				throw new InvalidOperationException("The enumerator has reached the end of the collection.");
			}
		}
	}

	/// <summary>
	/// <see cref="ReadOnlySpan{T}.Enumerator"/> extensions.
	///	 </summary>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	extension<T>(ref ReadOnlySpan<T>.Enumerator enumerator)
	{
		/// <summary>
		/// Advances the enumerator to the next element
		/// and throws an exception if it fails to do so (the end had been reached).
		/// </summary>
		/// <remarks>
		/// This method behaves adequately to <see cref="EnumeratorExtensions.EnsureMoveNext{T}"/>.
		/// </remarks>
		/// <exception cref="InvalidOperationException">Thrown when the enumerator can't move to the next item.</exception>
		public void EnsureMoveNext()
		{
			if (!enumerator.MoveNext())
			{
				throw new InvalidOperationException("The enumerator has reached the end of the collection.");
			}
		}
	}

	/// <summary>
	/// <see cref="Span{T}.Enumerator"/> extensions.
	///	 </summary>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	extension<T>(ref Span<T>.Enumerator enumerator)
	{
		/// <summary>
		/// Advances the enumerator to the next element
		/// and throws an exception if it fails to do so (the end had been reached).
		/// </summary>
		/// <remarks>
		/// This method behaves adequately to <see cref="EnumeratorExtensions.EnsureMoveNext{T}"/>.
		/// </remarks>
		/// <exception cref="InvalidOperationException">Thrown when the enumerator can't move to the next item.</exception>
		public void EnsureMoveNext()
		{
			if (!enumerator.MoveNext())
			{
				throw new InvalidOperationException("The enumerator has reached the end of the collection.");
			}
		}
	}

	/// <summary>
	/// <see cref="SpanSplit{T}.Enumerator"/> extensions.
	///	 </summary>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	extension<T>(ref SpanSplit<T>.Enumerator enumerator) where T : IEquatable<T>
	{
		/// <summary>
		/// Advances the enumerator to the next fragment of the split
		/// and throws an exception if it fails to do so (the end had been reached).
		/// </summary>
		/// <remarks>
		/// This method behaves adequately to <see cref="EnumeratorExtensions.EnsureMoveNext{T}"/>.
		/// </remarks>
		/// <exception cref="InvalidOperationException">Thrown when the enumerator can't move to the next item.</exception>
		public void EnsureMoveNext()
		{
			if (!enumerator.MoveNext())
			{
				throw new InvalidOperationException("The enumerator has reached the end of the collection.");
			}
		}
	}
	/// <summary>
	/// <see cref="SpanLineEnumerator"/> extensions.
	///	</summary>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	extension(ref SpanLineEnumerator enumerator)
	{
		/// <summary>
		/// Advances the <paramref name="enumerator"/> to the next non-empty line
		/// (treats whitespace-only lines as empty).
		/// </summary>
		/// <param name="skipCurrent">Whether to skip the current element in enumerator regardless of its content.</param>
		/// <returns>Whether the enumerator has reached non-empty line (did not reach end).</returns>
		public bool MoveToNextNonEmptyLine(bool skipCurrent = true)
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
		/// <param name="skipCurrent">Whether to skip the current element in enumerator regardless of its content.</param>
		/// <exception cref="InvalidOperationException">Thrown when the enumerator can't move to the next item.</exception>
		public void EnsureMoveToNextNonEmptyLine(bool skipCurrent = true)
		{
			if (!enumerator.MoveToNextNonEmptyLine(skipCurrent))
			{
				throw new InvalidOperationException("The enumerator has reached the end of the collection.");
			}
		}
	}

	/// <summary>
	/// <see cref="char"/>-specialized <see cref="SpanSplit{T}.Enumerator"/> extensions.
	///	</summary>
	/// <param name="enumerator">The enumerator to be advanced.</param>
	extension(ref SpanSplit<char>.Enumerator enumerator)
	{
		/// <summary>
		/// Advances the <paramref name="enumerator"/> to the next non-empty split part
		/// (treats whitespace-only parts as empty).
		/// </summary>
		/// <param name="skipCurrent">Whether to skip the current element in enumerator regardless of its content.</param>
		/// <returns>Whether the enumerator has reached non-empty part (did not reach end).</returns>
		public bool MoveToNextNonEmptyPart(bool skipCurrent = true)
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
		/// Advances the <paramref name="enumerator"/> to the next non-empty split part
		/// (treats whitespace-only lines as empty).
		/// Throws exception if it fails to do so (the end had been reached).
		/// </summary>
		/// <param name="skipCurrent">Whether to skip the current element in enumerator regardless of its content.</param>
		/// <exception cref="InvalidOperationException">Thrown when the enumerator can't move to the next item.</exception>
		public void EnsureMoveToNextNonEmptyPart(bool skipCurrent = true)
		{
			if (!enumerator.MoveToNextNonEmptyPart(skipCurrent))
			{
				throw new InvalidOperationException("The enumerator has reached the end of the collection.");
			}
		}
	}

}
