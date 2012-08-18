using System;

namespace XPTable.Events
{
	/// <summary>
	/// Things that happen to tables, essentially the properties that can change, 
	/// Sort Start and End should be here rather than in Column Events
	/// </summary>
	public enum TableEventType
	{
		/// <summary>
		/// The Row Store Changed
		/// </summary>
		TableModelChanged,
		/// <summary>
		/// The Column Store Changed
		/// </summary>
		ColumnModelChanged,
	}
}
