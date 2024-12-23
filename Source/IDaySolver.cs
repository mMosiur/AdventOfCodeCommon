namespace AdventOfCode.Common;

/// <summary>
/// Interface that represents a solver for a specific day puzzle of Advent of Code.
/// </summary>
public interface IDaySolver
{
	/// <summary>
	/// Property <c>Year</c> represents the year of the Advent of Code event the puzzle of the <see cref="DaySolver{TDaySolverOptions}"/> belongs to.
	/// </summary>
	int Year { get; }

	/// <summary>
	/// Property <c>Day</c> represents the day of the Advent of Code event the puzzle of the <see cref="DaySolver{TDaySolverOptions}"/> belongs to.
	/// </summary>
	int Day { get; }

	/// <summary>
	/// Property <c>Title</c> represents the title of the day of the Advent of Code event the <see cref="DaySolver{TDaySolverOptions}"/> belongs to.
	/// </summary>
	string Title { get; }

	/// <summary>
	/// A method that solves the first part of the day puzzle.
	/// </summary>
	/// <returns>The solution of the first part of the day puzzle.</returns>
	string SolvePart1();

	/// <summary>
	/// A method that solves the second part of the day puzzle.
	/// </summary>
	/// <returns>The solution of the second part of the day puzzle.</returns>
	string SolvePart2();
}
