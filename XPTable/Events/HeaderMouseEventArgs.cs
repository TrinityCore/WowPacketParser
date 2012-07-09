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
using System.Windows.Forms;

using XPTable.Models;


namespace XPTable.Events
{
	#region Delegates

	/// <summary>
	/// Represents the method that will handle the HeaderMouseEnter, HeaderMouseLeave, 
	/// HeaderMouseDown, HeaderMouseUp, HeaderMouseMove, HeaderClick and HeaderDoubleClick 
	/// events of a Table
	/// </summary>
	public delegate void HeaderMouseEventHandler(object sender, HeaderMouseEventArgs e);

	#endregion



	#region HeaderMouseEventArgs
	
	/// <summary>
	/// Provides data for the HeaderMouseEnter, HeaderMouseLeave, HeaderMouseDown, 
	/// HeaderMouseUp, HeaderMouseMove, HeaderClick and HeaderDoubleClick events of a Table
	/// </summary>
	public class HeaderMouseEventArgs : MouseEventArgs
	{
		#region Class Data

		/// <summary>
		/// The Column that raised the event
		/// </summary>
		private Column column;
		
		/// <summary>
		/// The Table the Column belongs to
		/// </summary>
		private Table table;
		
		/// <summary>
		/// The index of the Column
		/// </summary>
		private int index;
		
		/// <summary>
		/// The column header's bounding rectangle
		/// </summary>
		private Rectangle headerRect;

		#endregion
		
		
		#region Constructor

		/// <summary>
		/// Initializes a new instance of the HeaderMouseEventArgs class with 
		/// the specified source Column, Table, column index and column header bounds
		/// </summary>
		/// <param name="column">The Column that Raised the event</param>
		/// <param name="table">The Table the Column belongs to</param>
		/// <param name="index">The index of the Column</param>
		/// <param name="headerRect">The column header's bounding rectangle</param>
		public HeaderMouseEventArgs(Column column, Table table, int index, Rectangle headerRect) : base(MouseButtons.None, 0, -1, -1, 0)
		{
			this.column = column;
			this.table = table;
			this.index = index;
			this.headerRect = headerRect;
		} 

		
		/// <summary>
		/// Initializes a new instance of the HeaderMouseEventArgs class with 
		/// the specified source Column, Table, column index, column header bounds 
		/// and MouseEventArgs
		/// </summary>
		/// <param name="column">The Column that Raised the event</param>
		/// <param name="table">The Table the Column belongs to</param>
		/// <param name="index">The index of the Column</param>
		/// <param name="headerRect">The column header's bounding rectangle</param>
		/// <param name="mea">The MouseEventArgs that contains data about the 
		/// mouse event</param>
		public HeaderMouseEventArgs(Column column, Table table, int index, Rectangle headerRect, MouseEventArgs mea) : base(mea.Button, mea.Clicks, mea.X, mea.Y, mea.Delta)
		{
			this.column = column;
			this.table = table;
			this.index = index;
			this.headerRect = headerRect;
		} 

		#endregion


		#region Properties

		/// <summary>
		/// Gets the Column that Raised the event
		/// </summary>
		public Column Column
		{
			get
			{
				return this.column;
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
		/// Gets the index of the Column
		/// </summary>
		public int Index
		{
			get
			{
				return this.index;
			}
		}


		/// <summary>
		/// Gets the column header's bounding rectangle
		/// </summary>
		public Rectangle HeaderRect
		{
			get
			{
				return this.headerRect;
			}
		}

		#endregion
	}

	#endregion
}
