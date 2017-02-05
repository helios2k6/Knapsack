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
    /// The 0/1 Knapsack solver using DP
    /// </summary>
    public sealed class ZeroOneDPKnapsackSolver : IKnapsackSolver
    {
        /// <summary>
        /// Gets the type of Knapsack Problem this solver solves
        /// </summary>
        public ProblemTrait Trait
        {
            get { return ProblemTrait.ZeroOne; }
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
            if (items.Any() == false)
            {
                return Enumerable.Empty<IItem>();
            }

            IList<IItem> itemList = items.ToList();

            var valueMatrix = new TwoDimensionalSparseMatrix<long>();
            var keepMatrix = new TwoDimensionalSparseMatrix<bool>();

            int itemCount = itemList.Count;

            SolveKnapsackProblem(maxWeight, itemList, valueMatrix, keepMatrix, itemCount);

            return Package(itemList, keepMatrix, maxWeight);
        }

        /// <summary>
        /// Solves the knapsack problem.
        /// </summary>
        /// <param name="maxWeight">The maximum weight.</param>
        /// <param name="itemList">The item list.</param>
        /// <param name="valueMatrix">The value matrix.</param>
        /// <param name="keepMatrix">The keep matrix.</param>
        /// <param name="itemCount">The item count.</param>
        private static void SolveKnapsackProblem(
            long maxWeight,
            IList<IItem> itemList,
            TwoDimensionalSparseMatrix<long> valueMatrix,
            TwoDimensionalSparseMatrix<bool> keepMatrix,
            int itemCount)
        {
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
            long upperBound = maxWeight;
            for (int i = itemCount; i > 0; i--)
            {
                if (keepMatrix[i, upperBound])
                {
                    IItem currentItem = itemList[i - 1];
                    knapsackItems = knapsackItems.Append(currentItem);
                    upperBound -= currentItem.Weight;
                }
            }

            return knapsackItems;
        }
    }
}
