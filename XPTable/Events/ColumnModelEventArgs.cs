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
	/// Represents the methods that will handle the ColumnInserted, ColumnRemoved 
	/// and HeaderHeightChanged event of a ColumnModel
	/// </summary>
	public delegate void ColumnModelEventHandler(object sender, ColumnModelEventArgs e);

	#endregion
	
	
	
	#region ColumnModelEventArgs

	/// <summary>
	/// Provides data for a ColumnModel's ColumnAdded, ColumnRemoved, 
	/// and HeaderHeightChanged events
	/// </summary>
	public class ColumnModelEventArgs
	{
		#region Class Data
		
		/// <summary>
		/// The ColumnModel that Raised the event
		/// </summary>
		private ColumnModel source;

		/// <summary>
		/// The affected Column
		/// </summary>
		private Column column;

		/// <summary>
		/// The start index of the affected Column(s)
		/// </summary>
		private int fromIndex;

		/// <summary>
		/// The end index of the affected Column(s)
		/// </summary>
		private int toIndex;

		#endregion


		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the ColumnModelEventArgs class with 
		/// the specified ColumnModel source, start index, end index and affected Column
		/// </summary>
		/// <param name="source">The ColumnModel that originated the event</param>
		/// <param name="column">The affected Column</param>
		/// <param name="fromIndex">The start index of the affected Column(s)</param>
		/// <param name="toIndex">The end index of the affected Column(s)</param>
		public ColumnModelEventArgs(ColumnModel source, Column column, int fromIndex, int toIndex) : base()
		{
			this.source = source;
			this.column = column;
			this.fromIndex = fromIndex;
			this.toIndex = toIndex;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets the ColumnModel that Raised the event
		/// </summary>
		public ColumnModel ColumnModel
		{
			get
			{
				return this.source;
			}
		}


		/// <summary>
		/// Gets the affected Column
		/// </summary>
		public Column Column
		{
			get
			{
				return this.column;
			}
		}


		/// <summary>
		/// Gets the start index of the affected Column(s)
		/// </summary>
		public int FromIndex
		{
			get
			{
				return this.fromIndex;
			}
		}


		/// <summary>
		/// Gets the end index of the affected Column(s)
		/// </summary>
		public int ToIndex
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
