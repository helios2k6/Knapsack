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

using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Knapsack.Details
{
	/// <summary>
	/// Represents a sparse two dimensional matrix that returns a default value whenever an element
	/// does not exist
	/// </summary>
	/// <typeparam name="T">The type of the matrix</typeparam>
	public sealed class TwoDimensionalSparseMatrix<T>
	{
		#region private fields
		private readonly IDictionary<long, IDictionary<long, T>> _weightMatrix = new Dictionary<long, IDictionary<long, T>>();
		private long _virtualRowCount = 0;
		private long _virtualColCount = 0;
		#endregion

		#region public interface
		/// <summary>
		/// Gets the number of rows
		/// </summary>
		/// <value>
		/// The number of rows
		/// </value>
		public long NumRows
		{
			get { return _virtualRowCount; }
		}

		/// <summary>
		/// Gets the number of columns
		/// </summary>
		/// <value>
		/// The number of columes
		/// </value>
		public long NumCols
		{
			get { return _virtualColCount; }
		}

		/// <summary>
		/// The index into this 2-Dimensional Matrix
		/// </summary>
		/// <param name="i">The row</param>
		/// <param name="j">The column</param>
		/// <returns>The T that is at the specified indices, or default(T) if it doesn't exist</returns>
		public T this[long i, long j]
		{
			get
			{
				IDictionary<long, T> matrixRow;
				T rowValue;
				if (_weightMatrix.TryGetValue(i, out matrixRow) && matrixRow.TryGetValue(j, out rowValue))
				{
					return rowValue;
				}
				return default(T);
			}
			set
			{
				//Check row count
				if (_virtualRowCount <= i) { _virtualRowCount = i + 1; }

				//Check col count
				if (_virtualColCount <= j) { _virtualColCount = j + 1; }

				IDictionary<long, T> row;
				if (!_weightMatrix.TryGetValue(i, out row))
				{
					row = new Dictionary<long, T>();
					_weightMatrix[i] = row;
				}

				row[j] = value;
			}
		}

		/// <summary>
		/// Generate the string representation of this matrix
		/// </summary>
		/// <returns>The string representation</returns>
		public override string ToString()
		{
			var stringBuilder = new StringBuilder();

			for (var currentRow = 0; currentRow < _virtualRowCount; currentRow++)
			{
				for (var currentCol = 0; currentCol < _virtualColCount; currentCol++)
				{
					stringBuilder.Append(this[currentRow, currentCol]);
					if (currentCol + 1 != _virtualColCount)
					{
						stringBuilder.Append(", ");
					}
				}
				stringBuilder.AppendLine();
			}

			return stringBuilder.ToString();
		} 
		#endregion
	}
}
