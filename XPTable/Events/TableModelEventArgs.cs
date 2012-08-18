/*
 * Copyright © 2005, Mathew Hall
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, 
 * are permitted provided that the following conditions are met:
 *
 *    - Redistributions of source code must retain the above copyright notice, 
 *      this list of conditions and the following disclaimer.
 * 
 *    - Redistributions in binary form must reproduce the above copyright notice, 
 *      this list of conditions and the following disclaimer in the documentation 
 *      and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, 
 * OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
 * OF SUCH DAMAGE.
 */


using System;

using XPTable.Models;


namespace XPTable.Events
{
	#region Delegates

	/// <summary>
	/// Represents the methods that will handle the RowAdded and RowRemoved 
	/// events of a TableModel
	/// </summary>
	public delegate void TableModelEventHandler(object sender, TableModelEventArgs e);

	#endregion



	#region TableModelEventArgs

	/// <summary>
	/// Provides data for a TableModel's RowAdded and RowRemoved events
	/// </summary>
	public class TableModelEventArgs : EventArgs
	{
		#region Class Data
		
		/// <summary>
		/// The TableModel that Raised the event
		/// </summary>
		private TableModel source;

		/// <summary>
		/// The affected Row
		/// </summary>
		private Row row;

		/// <summary>
		/// The start index of the affected Row(s)
		/// </summary>
		private int toIndex;

		/// <summary>
		/// The end index of the affected Row(s)
		/// </summary>
		private int fromIndex;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the TableModelEventArgs class with 
		/// the specified TableModel source, start index, end index and affected Column
		/// </summary>
		/// <param name="source">The TableModel that originated the event</param>
		public TableModelEventArgs(TableModel source) : this(source, null, -1, -1)
		{
			
		}

		
		/// <summary>
		/// Initializes a new instance of the TableModelEventArgs class with 
		/// the specified TableModel source, start index, end index and affected Column
		/// </summary>
		/// <param name="source">The TableModel that originated the event</param>
		/// <param name="fromIndex">The start index of the affected Row(s)</param>
		/// <param name="toIndex">The end index of the affected Row(s)</param>
		public TableModelEventArgs(TableModel source, int fromIndex, int toIndex) : this(source, null, fromIndex, toIndex)
		{
			
		}

		
		/// <summary>
		/// Initializes a new instance of the TableModelEventArgs class with 
		/// the specified TableModel source, start index, end index and affected Column
		/// </summary>
		/// <param name="source">The TableModel that originated the event</param>
		/// <param name="row">The affected Row</param>
		/// <param name="fromIndex">The start index of the affected Row(s)</param>
		/// <param name="toIndex">The end index of the affected Row(s)</param>
		public TableModelEventArgs(TableModel source, Row row, int fromIndex, int toIndex)
		{
			this.source = source;
			this.row = row;
			this.fromIndex = fromIndex;
			this.toIndex = toIndex;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets the TableModel that Raised the event
		/// </summary>
		public TableModel TableModel
		{
			get
			{
				return this.source;
			}
		}


		/// <summary>
		/// Gets the affected Row
		/// </summary>
		public Row Row
		{
			get
			{
				return this.row;
			}
		}


		/// <summary>
		/// Gets the start index of the affected Row(s)
		/// </summary>
		public int RowFromIndex
		{
			get
			{
				return this.fromIndex;
			}
		}


		/// <summary>
		/// Gets the end index of the affected Row(s)
		/// </summary>
		public int RowToIndex
		{
			get
			{
				return this.toIndex;
			}
		}

		#endregion
	}

	#endregion
}
