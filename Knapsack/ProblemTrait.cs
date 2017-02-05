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

namespace Knapsack
{
    /// <summary>
    /// Represents the type of Knapsack problem
    /// </summary>
    public sealed class ProblemTrait
    {
        #region private fields
        private readonly string _description;
        #endregion

        #region private ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ProblemTrait"/> class.
        /// </summary>
        /// <param name="description">The description.</param>
        private ProblemTrait(string description)
        {
            _description = description;
        }
        #endregion

        #region public static readonly fields
        /// <summary>
        /// The 0/1 Knapsack Problem Trait
        /// </summary>
        public static readonly ProblemTrait ZeroOne = new ProblemTrait("0/1 Knapsack Problem");

        /// <summary>
        /// The Unbounded Knapsack Problem Trait
        /// </summary>
        public static readonly ProblemTrait Unbounded = new ProblemTrait("Unbounded Knapsack Problem");
        #endregion

        #region public methods
        /// <summary>
        /// Return the string representation of this object
        /// </summary>
        /// <returns>The string representation</returns>
        public override string ToString()
        {
            return _description;
        }
        #endregion
    }
}
