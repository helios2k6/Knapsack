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
using System.Collections.Generic;
using System.Linq;

namespace Knapsack.Details
{
	/// <summary>
	/// A DP Knapsack Solver for the unbounded knapsack case
	/// </summary>
	public sealed class UnboundedDPKnapsackSolver : DPKnapsackSolverBase
	{
		/// <summary>
		/// Gets the type of Knapsack Problem this solver solves
		/// </summary>
		public override ProblemTrait Trait
		{
			get { return ProblemTrait.Unbounded; }
		}

		/// <summary>
		/// Package the results into an IEnumerable{T}
		/// </summary>
		/// <param name="items">The items to package</param>
		/// <param name="valueMatrix">The value matrix</param>
		/// <param name="keepMatrix">The keep matrix</param>
		/// <param name="maxWeight">The max weight</param>
		/// <returns>
		/// The IEnumerable{T} of items
		/// </returns>
		protected override IEnumerable<IItem> Package(
			IList<IItem> items,
			TwoDimensionalSparseMatrix<long> valueMatrix,
			TwoDimensionalSparseMatrix<bool> keepMatrix,
			long maxWeight)
		{
			IEnumerable<IItem> package = Enumerable.Empty<IItem>();

			/*
			 * 1. Find the max value at the max weight
			 * 2. Figure out which items added to the max value
			 * 3. Count them
			 * 4. Add them to the bag
			 */
			int itemCount = items.Count;
			for (var i = 1; i <= itemCount; i++)
			{
				if (keepMatrix[i, maxWeight])
				{
					//Count how many times we should add it to the sack
					for (var j = 0; j <= maxWeight; j++)
					{
						if (keepMatrix[i, j])
						{
							package = package.Append(items[i - 1]); //The index for the item array is off by one 
						}
					}
				}
			}

			return package;
		}
	}
}
