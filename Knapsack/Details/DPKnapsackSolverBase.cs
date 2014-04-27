using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack.Details
{
	/// <summary>
	/// Base class for DP Knapsack Solvers
	/// </summary>
	public abstract class DPKnapsackSolverBase : IKnapsackSolver
	{
		/// <summary>
		/// Gets the type of Knapsack Problem this solver solves
		/// </summary>
		public abstract ProblemTrait Trait { get; }

		/// <summary>
		/// Solves the Knapsack problem 
		/// </summary>
		/// <param name="items">The items to put into the knapsack</param>
		/// <param name="maxWeight">The maximum weight the knapsack can hold</param>
		/// <returns>The items to put into the knapsack</returns>
		public IEnumerable<IItem> Solve(IEnumerable<IItem> items, long maxWeight)
		{
			if (items.Any() == false)
			{
				return Enumerable.Empty<IItem>();
			}

			IList<IItem> itemList = items.ToList();

			var valueMatrix = new TwoDimensionalSparseMatrix<long>();
			var keepMatrix = new TwoDimensionalSparseMatrix<bool>();

			int itemCount = itemList.Count;

			for (var currentFileIndex = 1; currentFileIndex <= itemCount; currentFileIndex++)
			{
				var weightAtPreviousIndex = itemList[currentFileIndex - 1].Weight;
				var valueAtPreviousIndex = itemList[currentFileIndex - 1].Value;

				for (var currentWeight = 0; currentWeight <= maxWeight; currentWeight++)
				{
					var newProspectiveValue = valueAtPreviousIndex + valueMatrix[currentFileIndex - 1, currentWeight - weightAtPreviousIndex];
					var oldValue = valueMatrix[currentFileIndex - 1, currentWeight];
					if (weightAtPreviousIndex <= currentWeight && newProspectiveValue > oldValue)
					{
						valueMatrix[currentFileIndex, currentWeight] = newProspectiveValue;
						keepMatrix[currentFileIndex, currentWeight] = true;
					}
					else
					{
						valueMatrix[currentFileIndex, currentWeight] = oldValue;
					}
				}
			}

			return Package(itemList, valueMatrix, keepMatrix, maxWeight);
		}
		
		/// <summary>
		/// Package the results into an IEnumerable{T}
		/// </summary>
		/// <param name="items">The items to package</param>
		/// <param name="valueMatrix">The value matrix</param>
		/// <param name="keepMatrix">The keep matrix</param>
		/// <param name="maxWeight">The max weight</param>
		/// <returns>The IEnumerable{T} of items</returns>
		protected abstract IEnumerable<IItem> Package(
			IList<IItem> items, 
			TwoDimensionalSparseMatrix<long> valueMatrix, 
			TwoDimensionalSparseMatrix<bool> keepMatrix, 
			long maxWeight);
	}
}
