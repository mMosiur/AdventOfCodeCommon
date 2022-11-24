using System;

namespace AdventOfCode.Common;

/// <summary>
/// A struct that allows enumeration of a span splitted into parts.
/// </summary>
public readonly ref struct SpanSplit<T> where T : IEquatable<T>
{
	/// <summary>The span being splitted.</summary>
	private readonly ReadOnlySpan<T> _span;
	/// <summary>The separator used in split.</summary>
	private readonly T _separator;

	/// <summary>Initialize the struct.</summary>
	/// <param name="span">The span to be splitted.</param>
	/// <param name="separator">The separator to be used in split.</param>
	internal SpanSplit(ReadOnlySpan<T> span, T separator)
	{
		_span = span;
		_separator = separator;
	}

	/// <summary>Gets an enumerator for this split.</summary>
	public Enumerator GetEnumerator() => new(_span, _separator);

	/// <summary>Enumerates the parts of a span using separator provided in <see cref="SpanSplit{T}"/>.</summary>
	public ref struct Enumerator
	{
		/// <summary>The whole span being splitted.</summary>
		private readonly ReadOnlySpan<T> _span;
		/// <summary>The current part of the span split.</summary>
		private ReadOnlySpan<T> _current;
		/// <summary>The rest of the span to be split.</summary>
		private ReadOnlySpan<T> _rest;
		/// <summary>A flag to signalize that the enumeration has been completed.</summary>
		private bool _finished;
		/// <summary>The separator used in split.</summary>
		private readonly T _separator;

		/// <summary>Initializes the enumerator.</summary>
		/// <param name="span">The span to be splitted.</param>
		/// <param name="separator">The separator to be used in split.</param>
		internal Enumerator(ReadOnlySpan<T> span, T separator)
		{
			_span = span;
			_current = default;
			_rest = span;
			_finished = false;
			_separator = separator;
		}

		/// <summary>Advanced the enumerator to the next part of the split.</summary>
		public bool MoveNext()
		{
			if (_finished) return false;
			int sepIndex = _rest.IndexOf(_separator);
			if (sepIndex == -1)
			{
				_current = _rest;
				_rest = default;
				_finished = true;
				return true;
			}
			_current = _rest[..sepIndex];
			_rest = _rest[(sepIndex + 1)..];
			return true;
		}

		/// <summary>Resets the enumerator to the fresh state.</summary>
		public void Reset()
		{
			_current = default;
			_rest = _span;
			_finished = false;
		}

		/// <summary>Get the current part of the split.</summary>
		public ReadOnlySpan<T> Current => _current;
	}
}
