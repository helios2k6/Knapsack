using Knapsack;
using Knapsack.Details;
using Knapsack.Details.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
	/// <summary>
	/// Tests Unbounded knapsack solvers
	/// </summary>
	[TestClass]
	public sealed class UnboundedKnapsack
	{
		/// <summary>
		/// Ensures the duplication to reach the max capacity
		/// </summary>
		[TestMethod]
		public void EnsureDuplication()
		{
			var solver = new UnboundedDPKnapsackSolver();
			IEnumerable<int> seq = 5.AsEnumerable();
			IEnumerable<IItem> items = seq.Select(item => new IntItem(item));
			IEnumerable<IItem> knapsack = solver.Solve(items, 15);

			Assert.AreEqual(3, knapsack.Count());

			foreach (var result in knapsack)
			{
				Assert.AreEqual(5, result.Value);
				Assert.AreEqual(5, result.Weight);
			}
		}
	}
}