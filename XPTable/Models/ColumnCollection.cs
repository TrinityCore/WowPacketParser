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
using System.Collections;

using XPTable.Events;


namespace XPTable.Models
{
	/// <summary>
	/// Represents a collection of Column objects
	/// </summary>
	public class ColumnCollection : CollectionBase
	{
		#region Class Data
		/// <summary>
		/// The ColumnModel that owns the CollumnCollection
		/// </summary>
		private ColumnModel owner;

		/// <summary>
		/// A local cache of the combined width of all columns
		/// </summary>
		private int totalColumnWidth;

		/// <summary>
		/// A local cache of the combined width of all visible columns
		/// </summary>
		private int visibleColumnsWidth;

		/// <summary>
		/// A local cache of the number of visible columns
		/// </summary>
		private int visibleColumnCount;

		/// <summary>
		/// A local cache of the last visible column in the collection
		/// </summary>
		private int lastVisibleColumn;
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the ColumnModel.ColumnCollection class 
		/// that belongs to the specified ColumnModel
		/// </summary>
		/// <param name="owner">A ColumnModel representing the columnModel that owns 
		/// the Column collection</param>
		public ColumnCollection(ColumnModel owner) : base()
		{
			if (owner == null)
				throw new ArgumentNullException("owner");
				
			this.owner = owner;
			this.totalColumnWidth = 0;
			this.visibleColumnsWidth = 0;
			this.visibleColumnCount = 0;
			this.lastVisibleColumn = -1;
		}
		#endregion
		
		#region Methods
		/// <summary>
		/// Adds the specified Column to the end of the collection
		/// </summary>
		/// <param name="column">The Column to add</param>
		public int Add(Column column)
		{
			if (column == null) 
				throw new System.ArgumentNullException("Column is null");

			int index = this.List.Add(column);
			
			this.RecalcWidthCache();

			this.OnColumnAdded(new ColumnModelEventArgs(this.owner, column, index, index));

			return index;
		}

		/// <summary>
		/// Adds an array of Column objects to the collection
		/// </summary>
		/// <param name="columns">An array of Column objects to add 
		/// to the collection</param>
		public void AddRange(Column[] columns)
		{
			if (columns == null) 
				throw new System.ArgumentNullException("Column[] is null");

			for (int i=0; i<columns.Length; i++)
			{
				this.Add(columns[i]);
			}
		}

		/// <summary>
		/// Removes the specified Column from the model
		/// </summary>
		/// <param name="column">The Column to remove</param>
		public void Remove(Column column)
		{
			int columnIndex = this.IndexOf(column);

			if (columnIndex != -1) 
				this.RemoveAt(columnIndex);
		}
        
		/// <summary>
		/// Removes an array of Column objects from the collection
		/// </summary>
		/// <param name="columns">An array of Column objects to remove 
		/// from the collection</param>
		public void RemoveRange(Column[] columns)
		{
			if (columns == null) 
				throw new System.ArgumentNullException("Column[] is null");

			for (int i=0; i<columns.Length; i++)
			{
				this.Remove(columns[i]);
			}
		}

		/// <summary>
		/// Removes the Column at the specified index from the collection
		/// </summary>
		/// <param name="index">The index of the Column to remove</param>
		public new void RemoveAt(int index)
		{
			if (index >= 0 && index < this.Count) 
			{
				Column column = this[index];

				this.RemoveControlIfRequired(index);
				this.List.RemoveAt(index);

				this.RecalcWidthCache();

				this.OnColumnRemoved(new ColumnModelEventArgs(this.owner, column, index, index));
			}
		}

		/// <summary>
		/// Removes all Columns from the collection
		/// </summary>
		public new void Clear()
		{
			if (this.Count == 0)
				return;

			for (int i=0; i<this.Count; i++)
			{
				this.RemoveControlIfRequired(i);
				this[i].ColumnModel = null;
			}

			base.Clear();

			this.InnerList.Capacity = 0;

			this.RecalcWidthCache();

			this.OnColumnRemoved(new ColumnModelEventArgs(this.owner, null, -1, -1));
		}

		private void RemoveControlIfRequired(int index)
		{
			for (int i = 0; i < this.owner.Table.RowCount; i++)
			{
				Cell cell = this.owner.Table.TableModel.Rows[i].Cells[index];
				if (cell.RendererData is XPTable.Renderers.ControlRendererData)
				{
					if ((cell.RendererData as XPTable.Renderers.ControlRendererData).Control != null)
						cell.Row.TableModel.Table.Controls.Remove((cell.RendererData as XPTable.Renderers.ControlRendererData).Control);
				}
			}
		}

		/// <summary>
		///	Returns the index of the specified Column in the model
		/// </summary>
		/// <param name="column">The Column to look for</param>
		/// <returns>The index of the specified Column in the model</returns>
		public int IndexOf(Column column)
		{
			for (int i=0; i<this.Count; i++)
			{
				if (this[i] == column)
					return i;
			}

			return -1;
		}

        /// <summary>
        /// Returns the index of the named Column in the model
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int IndexOf(string name)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Text == name)
                    return i;
            }

            return -1;
        }

		/// <summary>
		/// Recalculates the total combined width of all columns
		/// </summary>
		protected internal void RecalcWidthCache() 
		{
			int total = 0;
			int visibleWidth = 0;
			int visibleCount = 0;
			int lastVisible = -1;

			for (int i=0; i<this.Count; i++)
			{
				total += this[i].Width;
					
				if (this[i].Visible)
				{
					this[i].X = visibleWidth;
					visibleWidth += this[i].Width;
					visibleCount++;
					lastVisible = i;
				}
			}

			this.totalColumnWidth = total;
			this.visibleColumnsWidth = visibleWidth;
			this.visibleColumnCount = visibleCount;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the Column at the specified index
		/// </summary>
		public Column this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
					return null;
					
				return this.List[index] as Column;
			}
		}

        /// <summary>
        /// Gets the Column with the specified name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Column this[string name]
        {
            get
            {
                int index = IndexOf(name);
                return this[index];
            }
        }

		/// <summary>
		/// Gets the ColumnModel that owns this ColumnCollection
		/// </summary>
		public ColumnModel ColumnModel
		{
			get { return this.owner; }
		}

		/// <summary>
		/// Returns the total width of all the Columns in the model
		/// </summary>
		internal int TotalColumnWidth
		{
			get { return this.totalColumnWidth; }
		}

		/// <summary>
		/// Returns the total width of all the visible Columns in the model
		/// </summary>
		internal int VisibleColumnsWidth
		{
			get { return this.visibleColumnsWidth; }
		}

		/// <summary>
		/// Returns the number of visible Columns in the model
		/// </summary>
		internal int VisibleColumnCount
		{
			get { return this.visibleColumnCount; }
		}

		/// <summary>
		/// Returns the index of the last visible Column in the model
		/// </summary>
		internal int LastVisibleColumn
		{
			get { return this.lastVisibleColumn; }
		}
		#endregion

		#region Events
		/// <summary>
		/// Raises the ColumnAdded event
		/// </summary>
		/// <param name="e">A ColumnModelEventArgs that contains the event data</param>
		protected virtual void OnColumnAdded(ColumnModelEventArgs e)
		{
			this.owner.OnColumnAdded(e);
		}

		/// <summary>
		/// Raises the ColumnRemoved event
		/// </summary>
		/// <param name="e">A ColumnModelEventArgs that contains the event data</param>
		protected virtual void OnColumnRemoved(ColumnModelEventArgs e)
		{
			this.owner.OnColumnRemoved(e);
		}
		#endregion
	}
}
