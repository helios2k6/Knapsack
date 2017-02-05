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

using System;
using System.Collections.Generic;
using System.Linq;

namespace Knapsack.Details
{
    /// <summary>
    /// An infinite matrix that can be used to hold as many elements as your system can reasonably handle
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class InfiniteTwoDimensionalSparseMatrix<T>
    {
        #region private fields
        private static readonly long MaxLengthOfMatrix = int.MaxValue;

        private readonly IDictionary<Tuple<int, int>, TwoDimensionalSparseMatrix<T>> _matrixQuadrants;
        #endregion

        #region ctor
        public InfiniteTwoDimensionalSparseMatrix()
        {
            _matrixQuadrants = new Dictionary<Tuple<int, int>, TwoDimensionalSparseMatrix<T>>();
        }
        #endregion

        #region public properties
        /// <summary>
        /// Gets the number of rows
        /// </summary>
        /// <value>
        /// The number of rows
        /// </value>
        public long NumRows
        {
            get { return _matrixQuadrants.Aggregate(0L, (acc, e) => e.Value.NumRows + acc); }
        }

        /// <summary>
        /// Gets the number of columns
        /// </summary>
        /// <value>
        /// The number of columes
        /// </value>
        public long NumCols
        {
            get { return _matrixQuadrants.Aggregate(0L, (acc, e) => e.Value.NumCols + acc); }
        }
        #endregion

        #region public methods
        public T this[long rowIndex, long colIndex]
        {
            get
            {
                TwoDimensionalSparseMatrix<T> selectedMatrix = GetMatrixForQuadrantAtCoordinate(rowIndex, colIndex);
                if (selectedMatrix == null)
                {
                    return default(T);
                }

                return selectedMatrix[rowIndex, colIndex];
            }
            set
            {
                TwoDimensionalSparseMatrix<T> selectedMatrix = GetMatrixForQuadrantAtCoordinate(rowIndex, colIndex);
                if (selectedMatrix == null)
                {
                    Tuple<int, int> quadrant = GetQuadrant(rowIndex, colIndex);
                    selectedMatrix = new TwoDimensionalSparseMatrix<T>();
                    _matrixQuadrants.Add(quadrant, selectedMatrix);
                }

                selectedMatrix[rowIndex, colIndex] = value;
            }
        }
        #endregion

        #region private methods
        private static Tuple<int, int> GetQuadrant(long rowIndex, long colIndex)
        {
            int rowQuad = (int)(rowIndex % MaxLengthOfMatrix);
            int colQuad = (int)(colIndex % MaxLengthOfMatrix);
            return Tuple.Create(rowQuad, colQuad);
        }

        private TwoDimensionalSparseMatrix<T> GetMatrixForQuadrantAtCoordinate(long rowIndex, long colIndex)
        {
            Tuple<int, int> quadrant = GetQuadrant(rowIndex, colIndex);
            TwoDimensionalSparseMatrix<T> matrix;
            return _matrixQuadrants.TryGetValue(quadrant, out matrix)
                ? matrix
                : null;
        }

        #endregion
    }
}
