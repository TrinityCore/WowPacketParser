using System;
using System.ComponentModel;
using System.Drawing;

using XPTable.Themes;

namespace XPTable.Renderers
{
	/// <summary>
	/// Contains information about the current state of a grouping Cell.
	/// </summary>
	public class GroupRendererData
	{
		#region Class Data

		/// <summary>
		/// The current state of the Cell
		/// </summary>
		private bool grouped;

		#endregion

		/// <summary>
		/// Initializes a new instance of the GroupRendererData class
		/// </summary>
		public GroupRendererData()
		{
		}

		#region Properties
		/// <summary>
		/// Gets or sets the current state of the Cell
		/// </summary>
		public bool Grouped
		{
			get { return grouped; }
			set { grouped = value; }
		}
		#endregion
	}
}
