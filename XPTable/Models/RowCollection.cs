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
	/// Represents a collection of Row objects
	/// </summary>
	public class RowCollection : CollectionBase
	{
		#region Class Data

		/// <summary>
		/// The TableModel that owns the RowCollection
		/// </summary>
		private TableModel owner;

        /// <summary>
        /// A Row that owns this row
        /// </summary>
        private Row rowowner;

        private RowEventHandler propertyChangedEventHandler;
		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the RowCollection class 
		/// that belongs to the specified TableModel
		/// </summary>
		/// <param name="owner">A TableModel representing the tableModel that owns 
		/// the RowCollection</param>
		public RowCollection(TableModel owner) : base()
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
				
			this.owner = owner;

            propertyChangedEventHandler = new RowEventHandler(row_PropertyChanged);
		}

        /// <summary>
        /// Initializes a new instance of the RowCollection class 
        /// that belongs to the specified Row
        /// </summary>
        /// <param name="owner"></param>
        public RowCollection(Row owner)
            : base()
        {
            if (owner == null)
            {
                throw new ArgumentNullException("owner");
            }

            this.rowowner = owner;
        }

		#endregion
		

		#region Methods
		
		/// <summary>
		/// Adds the specified Row to the end of the collection
		/// </summary>
		/// <param name="row">The Row to add</param>
		public int Add(Row row)
		{
			if (row == null) 
				throw new System.ArgumentNullException("Row is null");

			int index = this.List.Add(row);

            if (owner != null)
            {
                // this RowCollection is the collection of toplevel rows
                this.OnRowAdded(new TableModelEventArgs(this.owner, row, index, index));
            }
            else if (rowowner != null)
            {
                // this is a sub row, so it needs a parent
                row.Parent = rowowner;
                row.ChildIndex = this.List.Count;
                this.OnRowAdded(new RowEventArgs(row, RowEventType.SubRowAdded, rowowner));
            }

            row.PropertyChanged += propertyChangedEventHandler;

			return index;
		}

		private int _totalHiddenSubRows = 0;

        /// <summary>
        /// Gets the total number of subrows that are currently not expanded.
        /// </summary>
        public int HiddenSubRows
        {
            get { return _totalHiddenSubRows; }
        }
        
        /// <summary>
        /// Count the number of hidden rows before the supplied row.
        /// </summary>
        /// <param name="row">The row to count up to.</param>
        /// <returns>The number of hidden rows.</returns>
        internal int HiddenRowCountBefore(int row)
        {
            int result = 0;

            int skip = 0;
            for (int i = 0; i < row; i++)
            {
                if (skip > 0)
                    skip--;
                else if ((skip == 0) && (!this[i].ExpandSubRows))
                {
                    skip = this[i].SubRows.Count;
                    result += skip;
                }
                else
                    skip = this[i].SubRows.Count;
            }

            return result;
        }

        /// <summary>
        /// Count the number of hidden rows before the supplied row.
        /// </summary>
        /// <param name="row">The row to count up to.</param>
        /// <returns>The number of hidden rows.</returns>
        internal int HiddenRowCountBefore(Row row)
        {
            return HiddenRowCountBefore(IndexOf(row));
        }

        /// <summary>
        /// Collapses all sub rows.
        /// </summary>
        public void CollapseAllSubRows()
        {
            int i = 0;
            while (i < this.Count)
            {
                if (this[i].SubRows.Count > 0)
                    this[i].ExpandSubRows = false;
                i++;
            }
        }

        /// <summary>
        /// Expands all sub rows.
        /// </summary>
        public void ExpandAllSubRows()
        {
            int i = 0;
            while (i < this.Count)
            {
                if (this[i].Parent == null)
                {
                    this[i].ExpandSubRows = true;
                    i += this[i].SubRows.Count;
                }
            }
        }

        private void row_PropertyChanged(object sender, RowEventArgs e)
        {
            if (e.EventType == RowEventType.ExpandSubRowsChanged)
            {
                if (!e.Row.ExpandSubRows)
                    _totalHiddenSubRows += e.Row.SubRows.Count;
                else
                    _totalHiddenSubRows -= e.Row.SubRows.Count;

            }
		}


		/// <summary>
		/// Adds an array of Row objects to the collection
		/// </summary>
		/// <param name="rows">An array of Row objects to add 
		/// to the collection</param>
		public void AddRange(Row[] rows)
		{
			if (rows == null) 
			{
				throw new System.ArgumentNullException("Row[] is null");
			}

			for (int i=0; i<rows.Length; i++)
			{
				this.Add(rows[i]);
			}
		}


		/// <summary>
		/// Removes the specified Row from the model
		/// </summary>
		/// <param name="row">The Row to remove</param>
		public void Remove(Row row)
		{
			int rowIndex = this.IndexOf(row);

			if (rowIndex != -1) 
			{
				this.RemoveAt(rowIndex);
			}
		}


		/// <summary>
		/// Removes an array of Row objects from the collection
		/// </summary>
		/// <param name="rows">An array of Row objects to remove 
		/// from the collection</param>
		public void RemoveRange(Row[] rows)
		{
			if (rows == null) 
			{
				throw new System.ArgumentNullException("Row[] is null");
			}

			for (int i=0; i<rows.Length; i++)
			{
				this.Remove(rows[i]);
			}
		}


		/// <summary>
		/// Removes the Row at the specified index from the collection
		/// </summary>
		/// <param name="index">The index of the Row to remove</param>
		public new void RemoveAt(int index)
		{
			if (index >= 0 && index < this.Count) 
			{
				Row row = this[index];
			
				RemoveControlIfRequired(index);
				this.List.RemoveAt(index);

                if (owner != null)
    				this.OnRowRemoved(new TableModelEventArgs(this.owner, row, index, index));

                else if (rowowner != null)
                    this.OnRowRemoved(new RowEventArgs(row, RowEventType.SubRowRemoved, rowowner));

                row.PropertyChanged -= propertyChangedEventHandler;
            }
		}

        private void RemoveControlIfRequired(int index)
        {
            for (int i = 0; i < this[index].Cells.Count; i++)
            {
                Cell cell = this[index].Cells[i];
                if (cell.RendererData is XPTable.Renderers.ControlRendererData)
                {
                    if ((cell.RendererData as XPTable.Renderers.ControlRendererData).Control != null)
                        cell.Row.TableModel.Table.Controls.Remove((cell.RendererData as XPTable.Renderers.ControlRendererData).Control);
                }
            }
        }

		/// <summary>
		/// Removes all Rows from the collection
		/// </summary>
		public new void Clear()
		{
			if (this.Count == 0)
			{
				return;
			}

			this[0].InternalTableModel.Table.ClearAllRowControls();
			for (int i=0; i<this.Count; i++)
			{
				this[i].InternalTableModel = null;
			}

			base.Clear();
			this.InnerList.Capacity = 0;

            if (owner != null)
                this.owner.OnRowRemoved(new TableModelEventArgs(this.owner, null, -1, -1));

            else if (rowowner != null)
                this.OnRowRemoved(new RowEventArgs(null, RowEventType.SubRowRemoved, rowowner));
        }

		/// <summary>
		/// Inserts a Row into the collection at the specified index
		/// </summary>
		/// <param name="index">The zero-based index at which the Row 
		/// should be inserted</param>
		/// <param name="row">The Row to insert</param>
		public void Insert(int index, Row row)
		{
			if (row == null)
			{
				return;
			}

			if (index < 0)
			{
				throw new IndexOutOfRangeException();
			}
			
			if (index >= this.Count)
			{
				this.Add(row);
			}
			else
			{
				base.List.Insert(index, row);

                if (owner != null)
                    this.owner.OnRowAdded(new TableModelEventArgs(this.owner, row, index, index));

                else if (rowowner != null)
                {
                    RowEventArgs args = new RowEventArgs(row, RowEventType.SubRowAdded, rowowner);
                    args.SetRowIndex(index);
                    this.OnRowAdded(args);
                }
            }
		}


		/// <summary>
		/// Inserts an array of Rows into the collection at the specified 
		/// index
		/// </summary>
		/// <param name="index">The zero-based index at which the rows 
		/// should be inserted</param>
		/// <param name="rows">The array of Rows to be inserted into 
		/// the collection</param>
		public void InsertRange(int index, Row[] rows)
		{
			if (rows == null) 
			{
				throw new System.ArgumentNullException("Row[] is null");
			}

			if (index < 0)
			{
				throw new IndexOutOfRangeException();
			}

			if (index >= this.Count)
			{
				this.AddRange(rows);
			}
			else
			{
				for (int i=rows.Length-1; i>=0; i--)
				{
					this.Insert(index, rows[i]);
				}
			}
		}


		/// <summary>
		///	Returns the index of the specified Row in the model
		/// </summary>
		/// <param name="row">The Row to look for</param>
		/// <returns>The index of the specified Row in the model</returns>
		public int IndexOf(Row row)
		{
			for (int i=0; i<this.Count; i++)
			{
				if (this[i] == row)
				{
					return i;
				}
			}

			return -1;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets the Row at the specified index
		/// </summary>
		public Row this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					return null;
				}
					
				return this.List[index] as Row;
			}
		}


		/// <summary>
		/// Replaces the Row at the specified index to the specified Row
		/// </summary>
		/// <param name="index">The index of the Row to be replaced</param>
		/// <param name="row">The Row to be placed at the specified index</param>
		internal void SetRow(int index, Row row)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("value");
			}

			if (row == null)
			{
				throw new ArgumentNullException("row cannot be null");
			}
					
			this.List[index] = row;

			row.InternalIndex = index;
		}

		#endregion


		#region Events

		/// <summary>
		/// Raises the RowAdded event
		/// </summary>
		/// <param name="e">A TableModelEventArgs that contains the event data</param>
		protected virtual void OnRowAdded(TableModelEventArgs e)
		{
			this.owner.OnRowAdded(e);
		}


		/// <summary>
		/// Raises the RowRemoved event
		/// </summary>
		/// <param name="e">A TableModelEventArgs that contains the event data</param>
		protected virtual void OnRowRemoved(TableModelEventArgs e)
		{
			this.owner.OnRowRemoved(e);
		}

		/// <summary>
		/// Raises the RowAdded event
		/// </summary>
		/// <param name="e">A TableModelEventArgs that contains the event data</param>
		protected virtual void OnRowAdded(RowEventArgs e)
		{
			this.rowowner.OnSubRowAdded(e);
		}


		/// <summary>
		/// Raises the RowRemoved event
		/// </summary>
		/// <param name="e">A TableModelEventArgs that contains the event data</param>
        protected virtual void OnRowRemoved(RowEventArgs e)
		{
            this.rowowner.OnSubRowRemoved(e);
		}
		#endregion
	}
}
