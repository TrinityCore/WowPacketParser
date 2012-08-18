using System;

using XPTable.Models;

namespace XPTable.Events
{
	#region Delegates
	/// <summary>
	/// The delegate definition for PropertyChanged event of a Table
	/// </summary>
	public delegate void TableEventHandler(object sender, TableEventArgs e);
	#endregion

	#region TableEventArgs
	/// <summary>
	/// Provides data for a Tables's PropertyChanged event
	/// </summary>
	public class TableEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the Column that Raised the event
		/// </summary>
		public Table Table{get{return this.theTable;}}
		
		/// <summary>
		/// Gets the type of event
		/// </summary>
		public TableEventType EventType{get{return this.theEventType;}}
		
		/// <summary>
		/// Gets the old value of the Columns changed property
		/// </summary>
		public object OldValue{get{return this.theOldValue;}}

		/// <summary>
		/// Initializes a new instance of the TableEventArgs class with the specified table, event type and old value
		/// </summary>
		/// <param name="_table">The table on which the event occured</param>
		/// <param name="_eventType">The type of event (principally the property that changed)</param>
		/// <param name="_oldValue">The old value of a changed property 
		/// <para>would be null for non property change events e.g. if table was being used as a matrix then there might be an inversion event)</para></param>
		public TableEventArgs(Table _table, TableEventType _eventType, object _oldValue) 
			: base()
		{
			this.theTable = _table;
			this.theEventType = _eventType;
			this.theOldValue = _oldValue;
		}

		private Table theTable;
		private object theOldValue;
		private TableEventType theEventType;
	}
	#endregion
}
