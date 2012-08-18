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
using System.Drawing;

using XPTable.Editors;
using XPTable.Models;


namespace XPTable.Events
{
	#region Delegates

	/// <summary>
	/// Represents the methods that will handle the BeginEdit, StopEdit and 
	/// CancelEdit events of a Table
	/// </summary>
	public delegate void CellEditEventHandler(object sender, CellEditEventArgs e);

	#endregion

	
	
	#region CellEditEventArgs
	
	/// <summary>
	/// Provides data for the BeginEdit, StopEdit and CancelEdit events of a Table
	/// </summary>
	public class CellEditEventArgs : CellEventArgsBase
	{
		#region Class Data

		/// <summary>
		/// The CellEditor used to edit the Cell
		/// </summary>
		private ICellEditor editor;
		
		/// <summary>
		/// The Table the Cell belongs to
		/// </summary>
		private Table table;

		/// <summary>
		/// The Cells bounding Rectangle
		/// </summary>
		private Rectangle cellRect;

		/// <summary>
		/// Specifies whether the event should be cancelled
		/// </summary>
		private bool cancel;

		/// <summary>
		/// Indicates whether the event was handled
		/// </summary>
		private bool handled;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the CellEventArgs class with 
		/// the specified Cell source, column index and row index
		/// </summary>
		/// <param name="source">The Cell that Raised the event</param>
		/// <param name="editor">The CellEditor used to edit the Cell</param>
		/// <param name="table">The Table that the Cell belongs to</param>
		private CellEditEventArgs(Cell source, ICellEditor editor, Table table) 
			: this(source, editor, table, -1, -1, Rectangle.Empty)
		{
			
		}


		/// <summary>
		/// Initializes a new instance of the CellEventArgs class with 
		/// the specified Cell source, column index and row index
		/// </summary>
		/// <param name="source">The Cell that Raised the event</param>
		/// <param name="editor">The CellEditor used to edit the Cell</param>
		/// <param name="table">The Table that the Cell belongs to</param>
		/// <param name="row">The Column index of the Cell</param>
		/// <param name="column">The Row index of the Cell</param>
		/// <param name="cellRect"></param>
		public CellEditEventArgs(Cell source, ICellEditor editor, Table table, int row, int column, Rectangle cellRect) 
			: base(source, column, row)
		{
			this.editor = editor;
			this.table = table;
			this.cellRect = cellRect;

			this.cancel = false;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets the CellEditor used to edit the Cell
		/// </summary>
		public ICellEditor Editor
		{
			get
			{
				return this.editor;
			}
		}


		/// <summary>
		/// Gets the Table the Cell belongs to
		/// </summary>
		public Table Table
		{
			get
			{
				return this.table;
			}
		}


		/// <summary>
		/// Gets the Cells bounding Rectangle
		/// </summary>
		public Rectangle CellRect
		{
			get
			{
				return this.cellRect;
			}
		}


		/// <summary>
		/// Gets or sets whether the event should be cancelled
		/// </summary>
		public bool Cancel
		{
			get
			{
				return this.cancel;
			}

			set
			{
				this.cancel = value;
			}
		}


		/// <summary>
		/// Gets or sets a value indicating whether the event was handled
		/// </summary>
		public bool Handled
		{
			get
			{
				return this.handled;
			}

			set
			{
				this.handled = value;
			}
		}

		#endregion
	}

	#endregion
}
