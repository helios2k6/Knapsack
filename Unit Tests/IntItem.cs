using Knapsack;

namespace UnitTests
{
	/// <summary>
	/// Wraps an Integer in an IItem
	/// </summary>
	internal sealed class IntItem : IItem
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IntItem"/> class.
		/// </summary>
		/// <param name="item">The item.</param>
		public IntItem(int item)
		{
			Item = item;
		}

		/// <summary>
		/// Gets or sets the item.
		/// </summary>
		/// <value>
		/// The item.
		/// </value>
		public int Item { get; set; }

		/// <summary>
		/// Gets the value of the item
		/// </summary>
		public long Value
		{
			get { return Item; }
		}

		/// <summary>
		/// Gets the weight of the item
		/// </summary>
		public long Weight
		{
			get { return Item; }
		}
	}
}
