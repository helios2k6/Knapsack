using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Knapsack.Details;
using System.Collections.Generic;
using System.Linq;
using Knapsack;

namespace UnitTests
{
    /// <summary>
    /// Tests Zero-One Knapsack solvers
    /// </summary>
    [TestClass]
    public sealed class ZeroOneKnapsack
    {
        /// <summary>
        /// Tests the zero one dp solver.
        /// </summary>
        [TestMethod]
        public void AllItemsTest()
        {
            var solver = new ZeroOneDPKnapsackSolver();
            IEnumerable<int> seq = Enumerable.Range(1, 5);
            IEnumerable<IItem> items = seq.Select(item => new IntItem(item));
            IEnumerable<IItem> knapsack = solver.Solve(items, 15);

            Assert.AreEqual(5, knapsack.Count());

            var itemSet = new HashSet<IItem>(items);

            foreach (var item in itemSet)
            {
                var query = from result in knapsack
                            where result.Value == item.Value && result.Weight == item.Weight
                            select result;

                //Make sure we have the item in the result set
                Assert.IsTrue(query.Any());
            }
        }

        /// <summary>
        /// Tests an expected empty knapsack
        /// </summary>
        [TestMethod]
        public void NoItemsTest()
        {
            var solver = new ZeroOneDPKnapsackSolver();
            IEnumerable<int> seq = Enumerable.Range(10, 10);
            IEnumerable<IItem> items = seq.Select(item => new IntItem(item));
            IEnumerable<IItem> knapsack = solver.Solve(items, 0);

            Assert.AreEqual(0, knapsack.Count());
        }

        /// <summary>
        /// Tests a tricky and common mistake in naive implementations. This test requires the solver to choose the best 
        /// subset by not choosing the largest item. It ensures that a greedy algorithm is not used
        /// </summary>
        [TestMethod]
        public void SomeItemsTest()
        {
            var solver = new ZeroOneDPKnapsackSolver();
            IEnumerable<int> seq = new[] { 2, 3, 4 };
            IEnumerable<IItem> items = seq.Select(item => new IntItem(item));
            IEnumerable<IItem> knapsack = solver.Solve(items, 5);

            Assert.AreEqual(2, knapsack.Count());

            var twoQuery = from result in knapsack
                           where result.Weight == 2 && result.Value == 2
                           select result;

            var threeQuery = from result in knapsack
                             where result.Weight == 3 && result.Value == 3
                             select result;

            Assert.IsTrue(twoQuery.Any());
            Assert.IsTrue(threeQuery.Any());
        }
    }
}
