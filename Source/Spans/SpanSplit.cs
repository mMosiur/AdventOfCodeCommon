using System;

namespace AdventOfCode.Common;

/// <summary>
/// A struct that allows enumeration of a span split into parts.
/// </summary>
public readonly ref struct SpanSplit<T> where T : IEquatable<T>
{
	/// <summary>The span being split.</summary>
	private readonly ReadOnlySpan<T> _span;
	/// <summary>The separator value used in split (if <see cref="_useSeparatorSpan"/> is false).</summary>
	private readonly T _separator;
	/// <summary>The separator sequence used in split (if <see cref="_useSeparatorSpan"/> is true).</summary>
	private readonly ReadOnlySpan<T> _separatorSpan;

	/// <summary>A flag to signalize weather the separator is a sequence instead of a single value.</summary>
	private readonly bool _useSeparatorSpan;

	/// <summary>Initialize the SpanSplit for a single value separator.</summary>
	/// <param name="span">The span to be split.</param>
	/// <param name="separator">The separator value to be used in split.</param>
	/// <remarks>
	/// This constructor is used when the separator is a single value and leaves
	/// the <see cref="_separatorSpan"/> empty and <see cref="_useSeparatorSpan"/> flag set to false.
	/// </remarks>
	internal SpanSplit(ReadOnlySpan<T> span, T separator)
	{
		_span = span;
		_separator = separator;
		_separatorSpan = default!;
		_useSeparatorSpan = false;
	}

	/// <summary>Initialize the SpanSplit for a sequence separator.</summary>
	/// <param name="span">The span to be split.</param>
	/// <param name="separator">The separator sequence to be used in split.</param>
	/// <remarks>
	/// This constructor is used when the separator is a span sequence and leaves
	/// the <see cref="_separator"/> defaulted and <see cref="_useSeparatorSpan"/> flag set to true.
	/// </remarks>
	internal SpanSplit(ReadOnlySpan<T> span, ReadOnlySpan<T> separator)
	{
		_span = span;
		_separator = default!;
		_separatorSpan = separator;
		_useSeparatorSpan = true;
	}

	/// <summary>Gets an enumerator for this split.</summary>
	public Enumerator GetEnumerator() => new(_span, _separator, _separatorSpan, _useSeparatorSpan);

	/// <summary>Enumerates the parts of a span using separator provided in <see cref="SpanSplit{T}"/>.</summary>
	public ref struct Enumerator
	{
		/// <summary>The whole span being split.</summary>
		private readonly ReadOnlySpan<T> _span;
		/// <summary>The current part of the span split.</summary>
		private ReadOnlySpan<T> _current;
		/// <summary>The rest of the span to be split.</summary>
		private ReadOnlySpan<T> _rest;
		/// <summary>A flag to signalize that the enumeration has been completed.</summary>
		private bool _finished;
		/// <summary>The separator value used in split (if <see cref="_useSeparatorSpan"/> is set to false).</summary>
		private readonly T _separator;
		/// <summary>The separator sequence used in split (if <see cref="_useSeparatorSpan"/> is set to true).</summary>
		private readonly ReadOnlySpan<T> _separatorSpan;
		/// <summary>A flag to signalize weather the separator is a sequence instead of a single value.</summary>
		private readonly bool _useSeparatorSpan;

		/// <summary>Initializes the enumerator.</summary>
		/// <param name="span">The span to be split.</param>
		/// <param name="separator">The separator value to be used in split (if <paramref name="useSeparatorSpan"/> is set to false).</param>
		/// <param name="separatorSpan">The separator sequence to be used in split (if <paramref name="useSeparatorSpan"/> is set to true).</param>
		/// <param name="useSeparatorSpan">Whether to use <paramref name="separator"/> value or <paramref name="separatorSpan"/> sequence as a split separator.</param>
		internal Enumerator(ReadOnlySpan<T> span, T separator, ReadOnlySpan<T> separatorSpan, bool useSeparatorSpan)
		{
			_span = span;
			_current = default;
			_rest = span;
			_finished = false;
			_separator = separator;
			_separatorSpan = separatorSpan;
			_useSeparatorSpan = useSeparatorSpan;
		}

		/// <summary>Advanced the enumerator to the next part of the split.</summary>
		public bool MoveNext()
		{
			if (_finished) return false;
			int sepIndex = _useSeparatorSpan ? _rest.IndexOf(_separatorSpan) : _rest.IndexOf(_separator);
			if (sepIndex == -1)
			{
				_current = _rest;
				_rest = default;
				_finished = true;
				return true;
			}
			_current = _rest[..sepIndex];
			int separatorWidth = _useSeparatorSpan ? _separatorSpan.Length : 1;
			_rest = _rest[(sepIndex + separatorWidth)..];
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
