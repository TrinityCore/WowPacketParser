using System;
using System.Drawing;

using XPTable.Editors;
using XPTable.Models;

namespace XPTable.Events
{

	/// <summary>
	/// Represents the methods that will handle the BeginEdit, StopEdit and 
	/// CancelEdit events of a Table
	/// </summary>
	public delegate void NumericCellEditEventHandler(object sender, NumericCellEditEventArgs e);

	/// <summary>
	/// Provides data for the BeforeChange event of a Table
	/// </summary>
    public class NumericCellEditEventArgs : CellEditEventArgs
    {
		/// <summary>
		/// Initializes a new instance of the NumericCellEditEventArgs class with 
		/// the specified Cell source, column index and row index
		/// </summary>
		/// <param name="source"></param>
		/// <param name="editor"></param>
		/// <param name="table"></param>
		/// <param name="row"></param>
		/// <param name="column"></param>
		/// <param name="cellRect"></param>
		/// <param name="oldValue"></param>
        public NumericCellEditEventArgs(Cell source, ICellEditor editor, Table table, int row, int column, Rectangle cellRect, decimal oldValue)
			: base(source, editor, table, row, column, cellRect)
        {
			this.oldValue = oldValue;
			this.newValue = oldValue;
        }

		/// <summary>
		/// The old value of the editor
		/// </summary>
		private decimal oldValue;
	
		/// <summary>
		/// The new value of the editor
		/// </summary>
		private decimal newValue;

		/// <summary>
		/// Gets the editors old value
		/// </summary>
		public decimal OldValue
		{
			get { return oldValue; }
		}

		/// <summary>
		/// Gets or sets the editors new value
		/// </summary>
		public decimal NewValue
		{
			get { return newValue; }
			set { newValue = value; }
		}

	}
}
