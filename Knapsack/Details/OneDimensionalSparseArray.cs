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
    /// A 1-dimensional sparse array implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class OneDimensionalSparseArray<T> : IEnumerable<T>
    {
        /// <summary>
        /// The enumerator for this sparse array
        /// </summary>
        private sealed class Enumerator : IEnumerator<T>
        {
            private readonly OneDimensionalSparseArray<T> _parent;
            private long _index = -1;

            /// <summary>
            /// Initializes a new instance of the <see cref="Enumerator"/> class.
            /// </summary>
            /// <param name="parent">The parent.</param>
            public Enumerator(OneDimensionalSparseArray<T> parent)
            {
                _parent = parent;
            }

            /// <summary>
            /// Gets the element in the collection at the current position of the enumerator.
            /// </summary>
            /// <returns>The element in the collection at the current position of the enumerator.</returns>
            public T Current { get { return _parent._backingMatrix[0, _index]; } }

            public void Dispose()
            {
            }

            /// <summary>
            /// Gets the element in the collection at the current position of the enumerator.
            /// </summary>
            /// <returns>The element in the collection at the current position of the enumerator.</returns>
            object IEnumerator.Current
            {
                get { return Current; }
            }

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns>
            /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
            /// </returns>
            public bool MoveNext()
            {
                if (_index + 1 >= _parent._backingMatrix.NumCols)
                {
                    return false;
                }

                _index++;
                return true;
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            public void Reset()
            {
                _index = -1;
            }
        }

        private readonly TwoDimensionalSparseMatrix<T> _backingMatrix = new TwoDimensionalSparseMatrix<T>();

        /// <summary>
        /// Gets or sets the <see cref="T"/> with the specified index i
        /// </summary>
        /// <value>
        /// The <see cref="T"/>.
        /// </value>
        /// <param name="i">The index</param>
        /// <returns>The item at index i or the default(T)</returns>
        public T this[long i]
        {
            get { return _backingMatrix[0, i]; }
            set { _backingMatrix[0, i] = value; }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("[");

            for (long i = 0; i < _backingMatrix.NumCols; i++)
            {
                builder.Append(i);
                if (i + 1 != _backingMatrix.NumCols)
                {
                    builder.Append(", ");
                }
            }

            builder.Append("]").AppendLine();
            return builder.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }
    }
}
