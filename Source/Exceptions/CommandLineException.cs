using System;

namespace AdventOfCode.Common;

/// <summary>
/// Exception thrown when the command line arguments are invalid.
/// </summary>
public class CommandLineException : AdventOfCodeException
{
	/// <inheritdoc />
	public CommandLineException() { }

	/// <inheritdoc />
	public CommandLineException(string message) : base(message) { }

	/// <inheritdoc />
	public CommandLineException(string message, Exception innerException) : base(message, innerException) { }
}
