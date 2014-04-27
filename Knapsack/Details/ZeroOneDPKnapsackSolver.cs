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

using System.Collections.Generic;

namespace Knapsack.Details
{
	public sealed class ZeroOneDPKnapsackSolver : DPKnapsackSolverBase
	{
		public override ProblemTrait Trait
		{
			get { return ProblemTrait.ZeroOne; }
		}

		protected override IEnumerable<IItem> Package(
			IList<IItem> itemList, 
			TwoDimensionalSparseMatrix<long> valueMatrix, 
			TwoDimensionalSparseMatrix<bool> keepMatrix, 
			long maxWeight)
		{
			int itemCount = itemList.Count;
			var setOfItems = new HashSet<IItem>();
			long upperBound = maxWeight;
			for (int i = itemCount; i > 0; i--)
			{
				if (keepMatrix[i, upperBound])
				{
					IItem currentItem = itemList[i - 1];
					setOfItems.Add(currentItem);
					upperBound -= currentItem.Weight;
				}
			}

			return setOfItems;
		}
	}
}
