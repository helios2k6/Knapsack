/*
 * The MIT License (MIT)
 * 
 * Copyright (c) 2014 Andrew B. Johnson

 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using Knapsack.Details.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Knapsack.Details
{
	/// <summary>
	/// A DP Knapsack Solver for the unbounded knapsack case
	/// </summary>
	public sealed class UnboundedDPKnapsackSolver : IKnapsackSolver
	{
		/// <summary>
		/// Gets the type of Knapsack Problem this solver solves
		/// </summary>
		public ProblemTrait Trait
		{
			get { return ProblemTrait.Unbounded; }
		}

		/// <summary>
		/// Solves the Knapsack problem
		/// </summary>
		/// <param name="items">The items to put into the knapsack</param>
		/// <param name="maxWeight">The maximum weight the knapsack can hold</param>
		/// <returns>
		/// The items to put into the knapsack
		/// </returns>
		public IEnumerable<IItem> Solve(IEnumerable<IItem> items, long maxWeight)
		{
			IList<IItem> itemList = items.ToList();

			var smallerSolutionList = new OneDimensionalSparseArray<long>();
			var intermediateSolution = new OneDimensionalSparseArray<long>();
			var memoList = new OneDimensionalSparseArray<long>();
			var keepMatrix = new TwoDimensionalSparseMatrix<bool>();

			SolveUnboundedKnapsack(maxWeight, itemList, ref smallerSolutionList, ref intermediateSolution, ref memoList, ref keepMatrix);

			return Package(itemList, keepMatrix, maxWeight);
		}

		/// <summary>
		/// Packages the specified item list.
		/// </summary>
		/// <param name="itemList">The item list.</param>
		/// <param name="keepMatrix">The keep matrix.</param>
		/// <param name="maxWeight">The maximum weight.</param>
		/// <returns>The items packed up into a knapsack</returns>
		private static IEnumerable<IItem> Package(
			IList<IItem> itemList,
			TwoDimensionalSparseMatrix<bool> keepMatrix,
			long maxWeight)
		{
			int itemCount = itemList.Count;
			var knapsackItems = Enumerable.Empty<IItem>();
			long currentWeightToCheck = maxWeight;
			while(true)
			{
				bool foundItem = false;
				for (int itemIndex = 0; itemIndex < itemList.Count; itemIndex++)
				{
					if(keepMatrix[itemIndex, currentWeightToCheck])
					{
						IItem itemToTake = itemList[itemIndex];
						knapsackItems = knapsackItems.Append(itemToTake);
						currentWeightToCheck -= itemToTake.Weight;
						foundItem = true;
						break;
					}
				}

				if (foundItem == false)
				{
					break;
				}
			}

			return knapsackItems;
		}

		/// <summary>
		/// Solves the unbounded knapsack.
		/// </summary>
		/// <param name="maxWeight">The maximum weight.</param>
		/// <param name="itemList">The item list.</param>
		/// <param name="smallerSolutionList">The smaller solution list.</param>
		/// <param name="intermediateSolutionList">The intermediate solution list.</param>
		/// <param name="memoList">The memo list.</param>
		/// <param name="keepMatrix">The keep matrix.</param>
		private static void SolveUnboundedKnapsack(
			long maxWeight,
			IList<IItem> itemList,
			ref OneDimensionalSparseArray<long> smallerSolutionList,
			ref OneDimensionalSparseArray<long> intermediateSolutionList,
			ref OneDimensionalSparseArray<long> memoList,
			ref TwoDimensionalSparseMatrix<bool> keepMatrix)
		{
			for (long weight = 1; weight <= maxWeight; weight++)
			{
				for (int itemIndex = 0; itemIndex < itemList.Count; itemIndex++)
				{
					IItem currentItem = itemList[itemIndex];
					if (weight >= currentItem.Weight)
					{
						smallerSolutionList[itemIndex] = memoList[weight - currentItem.Weight];
					}
					else
					{
						smallerSolutionList[itemIndex] = 0;
					}
				}

				for (int itemIndex = 0; itemIndex < itemList.Count; itemIndex++)
				{
					IItem currentItem = itemList[itemIndex];
					if (weight >= currentItem.Weight)
					{
						intermediateSolutionList[itemIndex] = smallerSolutionList[itemIndex] + currentItem.Value;
					}
					else
					{
						intermediateSolutionList[itemIndex] = 0;
					}
				}

				long fileIndexOfMaxValue = 0;
				memoList[weight] = intermediateSolutionList[0];

				for (int itemIndex = 1; itemIndex < itemList.Count; itemIndex++)
				{
					if (intermediateSolutionList[itemIndex] > memoList[weight])
					{
						memoList[weight] = intermediateSolutionList[itemIndex];
						fileIndexOfMaxValue = itemIndex;
					}
				}

				keepMatrix[fileIndexOfMaxValue, weight] = true;
			}
		}
	}
}
