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
	/// Represents the methods that will handle the PropertyChanged event of a Column, 
	/// or a Table's BeginSort and EndSort events
	/// </summary>
	public delegate void ColumnEventHandler(object sender, ColumnEventArgs e);

	#endregion
	
	
	
	#region ColumnEventArgs

	/// <summary>
	/// Provides data for a Column's PropertyChanged event, or a Table's 
	/// BeginSort and EndSort events
	/// </summary>
	public class ColumnEventArgs
	{
		#region Class Data

		/// <summary>
		/// The Column that Raised the event
		/// </summary>
		private Column source;

		/// <summary>
		/// The index of the Column in the ColumnModel
		/// </summary>
		private int index;

		/// <summary>
		/// The old value of the property that changed
		/// </summary>
		private object oldValue;

		/// <summary>
		/// The type of event
		/// </summary>
		private ColumnEventType eventType;

		#endregion


		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the ColumnEventArgs class with 
		/// the specified Column source, column index and event type
		/// </summary>
		/// <param name="source">The Column that Raised the event</param>
		/// <param name="eventType">The type of event</param>
		/// <param name="oldValue">The old value of the changed property</param>
		public ColumnEventArgs(Column source, ColumnEventType eventType, object oldValue) : this(source, -1, eventType, oldValue)
		{

		}

		
		/// <summary>
		/// Initializes a new instance of the ColumnEventArgs class with 
		/// the specified Column source, column index and event type
		/// </summary>
		/// <param name="source">The Column that Raised the event</param>
		/// <param name="index">The index of the Column</param>
		/// <param name="eventType">The type of event</param>
		/// <param name="oldValue">The old value of the changed property</param>
		public ColumnEventArgs(Column source, int index, ColumnEventType eventType, object oldValue) : base()
		{
			this.source = source;
			this.index = index;
			this.eventType = eventType;
			this.oldValue = oldValue;
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
				return this.source;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="column"></param>
		internal void SetColumn(Column column)
		{
			this.source = column;
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
		/// 
		/// </summary>
		/// <param name="index"></param>
		internal void SetIndex(int index)
		{
			this.index = index;
		}


		/// <summary>
		/// Gets the type of event
		/// </summary>
		public ColumnEventType EventType
		{
			get
			{
				return this.eventType;
			}
		}


		/// <summary>
		/// Gets the old value of the Columns changed property
		/// </summary>
		public object OldValue
		{
			get
			{
				return this.oldValue;
			}
		}

		#endregion
	}

	#endregion
}
