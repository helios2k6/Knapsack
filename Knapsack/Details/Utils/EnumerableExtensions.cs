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

namespace Knapsack.Details.Utils
{
	/// <summary>
	/// IEnumerable extensions
	/// </summary>
	public static class EnumerableExtensions
	{
		/// <summary>
		/// Append an item to the end of an IEnumerable{T}
		/// </summary>
		/// <typeparam name="T">The type of item to append</typeparam>
		/// <param name="this">The IEnumerable{T}</param>
		/// <param name="item">The item itself</param>
		/// <returns>An IEnumerable{T} with the appended item</returns>
		public static IEnumerable<T> Append<T>(this IEnumerable<T> @this, T item)
		{
			if (@this == null)
			{
				throw new ArgumentNullException();
			}

			return @this.Concat(item.AsEnumerable());
		}

		/// <summary>
		/// Turn an item into an IEnumerable{T}
		/// </summary>
		/// <typeparam name="T">The type of item</typeparam>
		/// <param name="t">The item to turn into an IEnumerable{T}</param>
		/// <returns>An IEnumerable{T} with the passed item as its only element</returns>
		public static IEnumerable<T> AsEnumerable<T>(this T t)
		{
			if(EqualityComparer<T>.Default.Equals(t, default(T)))
			{
				throw new ArgumentNullException();
			}

			yield return t;
		}
	}
}
