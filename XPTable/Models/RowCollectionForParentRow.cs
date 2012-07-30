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
using XPTable.Renderers;


namespace XPTable.Models
{
    /// <summary>
    /// Represents a collection of Row objects
    /// </summary>
    public class RowCollectionForParentRow : RowCollection
    {
        #region Class Data

        /// <summary>
        /// A Row that owns this row
        /// </summary>
        private Row rowowner;

        private Row subRow = null;

        #endregion


        #region Constructor


        /// <summary>
        /// Initializes a new instance of the RowCollection class 
        /// that belongs to the specified Row
        /// </summary>
        /// <param name="owner"></param>
        public RowCollectionForParentRow(Row owner)
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

            if (Count >= 1)
                return 0;
            subRow = row;

            if (rowowner != null)
            {
                // this is a sub row, so it needs a parent
                row.Parent = rowowner;
                row.ChildIndex = 1;
                this.OnRowAdded(new RowEventArgs(row, RowEventType.SubRowAdded, rowowner));
            }

            return 0;
        }

        /// <summary>
        /// Collapses all sub rows.
        /// </summary>
        public void CollapseAllSubRows()
        {
            int i = 0;
            while (i < this.Count)
            {
                if (this[i].SubRows != null && this[i].SubRows.Count > 0)
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
                    i += (this[i].SubRows != null) ? this[i].SubRows.Count : 0;
                }
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

            for (int i = 0; i < rows.Length; i++)
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

            for (int i = 0; i < rows.Length; i++)
            {
                this.Remove(rows[i]);
            }
        }


        /// <summary>
        /// Removes the Row at the specified index from the collection
        /// </summary>
        /// <param name="index">The index of the Row to remove</param>
        public void RemoveAt(int index)
        {
            if (index >= 0 && index < this.Count)
            {
                Row row = this[index];

                RemoveControlIfRequired(index);
                subRow = null;

                if (rowowner != null)
                    this.OnRowRemoved(new RowEventArgs(row, RowEventType.SubRowRemoved, rowowner));
            }
        }

        public void RemoveCacheAt(int index)
        {
            if (index >= 0 && index < this.Count)
            {
                Row row = this[index];
                if (row.cells == null)
                    return;
                RemoveControlIfRequired(index);
                row.cells = null;
            }
        }

        private void RemoveControlIfRequired(int index)
        {
            for (int i = 0; i < this[index].cells.Count; i++)
            {
                Cell cell = this[index].cells[i];
                if (cell.RendererData is XPTable.Renderers.ControlRendererData)
                {
                    ControlCellRenderer.RemoveControlRenderData(cell);
                }
            }
        }

        /// <summary>
        /// Removes all Rows from the collection
        /// </summary>
        public void Clear()
        {
            if (this.Count == 0)
            {
                return;
            }

            subRow = null;

            if (rowowner != null)
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

            if (index < 0 || index > 0)
            {
                throw new IndexOutOfRangeException();
            }

            if (index >= this.Count)
            {
                this.Add(row);
            }
            else
            {
                subRow = row;

                if (rowowner != null)
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
                for (int i = rows.Length - 1; i >= 0; i--)
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
            for (int i = 0; i < this.Count; i++)
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

        public int Count
        {
            get
            {
                return (subRow == null) ? 0 : 1;
            }
        }

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

                return subRow as Row;
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

            subRow = row;

            row.InternalIndex = index;
        }

        #endregion


        #region Events

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