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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;

using XPTable.Events;
using XPTable.Models.Design;


namespace XPTable.Models
{
	/// <summary>
	/// Represents a collection of Rows and Cells displayed in a Table.
	/// </summary>
	[DesignTimeVisible(true),
	ToolboxItem(true), 
	ToolboxBitmap(typeof(TableModel))]
	public class TableModel : Component
	{
		#region Event Handlers

		/// <summary>
		/// Occurs when a Row is added to the TableModel
		/// </summary>
		public event TableModelEventHandler RowAdded;

		/// <summary>
		/// Occurs when a Row is removed from the TableModel
		/// </summary>
		public event TableModelEventHandler RowRemoved;

		/// <summary>
		/// Occurs when the value of the TableModel Selection property changes
		/// </summary>
		public event SelectionEventHandler SelectionChanged;

		/// <summary>
		/// Occurs when the value of the RowHeight property changes
		/// </summary>
		public event EventHandler RowHeightChanged;

		#endregion
		
		
		#region Class Data

		/// <summary>
		/// The default height of a Row
		/// </summary>
		public static readonly int DefaultRowHeight = 15;
		
		/// <summary>
		/// The minimum height of a Row
		/// </summary>
		public static readonly int MinimumRowHeight = 14;

		/// <summary>
		/// The maximum height of a Row
		/// </summary>
		public static readonly int MaximumRowHeight = 1024;
		
		/// <summary>
		/// The collection of Rows's contained in the TableModel
		/// </summary>
		private RowCollection rows;

		/// <summary>
		/// The Table that the TableModel belongs to
		/// </summary>
		private Table table;

		/// <summary>
		/// The currently selected Rows and Cells
		/// </summary>
		private Selection selection;

		/// <summary>
		/// The height of each Row in the TableModel
		/// </summary>
		private int rowHeight;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the TableModel class with default settings
		/// </summary>
		public TableModel()
		{
			this.Init();
		}


		/// <summary>
		/// Initializes a new instance of the TableModel class with an array of Row objects
		/// </summary>
		/// <param name="rows">An array of Row objects that represent the Rows 
		/// of the TableModel</param>
		public TableModel(Row[] rows)
		{
			if (rows == null)
			{
				throw new ArgumentNullException("rows", "Row[] cannot be null");
			}
			
			this.Init();

			if (rows.Length > 0)
			{
				this.Rows.AddRange(rows);
			}
		}


		/// <summary>
		/// Initialise default settings
		/// </summary>
		private void Init()
		{
			this.rows = null;

			this.selection = new Selection(this);
			this.table = null;
			this.rowHeight = TableModel.DefaultRowHeight;
		}

		#endregion


		#region Methods

		/// <summary> 
		/// Releases the unmanaged resources used by the TableModel and optionally 
		/// releases the managed resources
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				
			}

			base.Dispose(disposing);
		}


		/// <summary>
		/// Returns the index of the Row that lies on the specified position
		/// </summary>
		/// <param name="yPosition">The y-coordinate to check</param>
		/// <returns>The index of the Row at the specified position or -1 if 
		/// no Row is found</returns>
		public int RowIndexAt(int yPosition)
		{
            int row = 0;
            if (this.Table.EnableWordWrap)
                row = this.RowIndexAtExact(yPosition);
            else
                row = yPosition / this.RowHeight;

			if (row < 0 || row > this.Rows.Count - 1)
				return -1;

			return row;
		}

        /// <summary>
        /// Returns the index of the Row that lies on the specified position.
        /// Found by iterating through all rows (i.e. copes with variable height rows).
        /// </summary>
        /// <param name="yPosition"></param>
        /// <returns></returns>
        private int RowIndexAtExact(int yPosition)
        {
            int height = 0;
            for (int i = 0; i < this.Rows.Count; i++)
            {
				Row row = this.Rows[i];
				if (row.Parent == null || row.Parent.ExpandSubRows)
				{
					height += row.Height;
					if (yPosition < height)
						return i;
				}
            }

            // If we've got this far then its the last row
            return this.Rows.Count - 1;
        }
		#endregion


		#region Properties
		/// <summary>
		/// Gets the Cell located at the specified row index and column index
		/// </summary>
		/// <param name="row">The row index of the Cell</param>
		/// <param name="column">The column index of the Cell</param>
		[Browsable(false)]
		public Cell this[int row, int column]
		{
			get
			{
				if (row < 0 || row >= this.Rows.Count)
					return null;

				if (column < 0 || column >= this.Rows[row].Cells.Count)
					return null;

				return this.Rows[row].Cells[column];
			}
		}

		/// <summary>
		/// Gets the Cell located at the specified cell position
		/// </summary>
		/// <param name="cellPos">The position of the Cell</param>
		[Browsable(false)]
		public Cell this[CellPos cellPos]
		{
			get { return this[cellPos.Row, cellPos.Column]; }
		}
		
		/// <summary>
		/// A TableModel.RowCollection representing the collection of 
		/// Rows contained within the TableModel
		/// </summary>
		[Category("Behavior"),
		Description("Row Collection"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		Editor(typeof(RowCollectionEditor), typeof(UITypeEditor))]
		public RowCollection Rows
		{
			get
			{
				if (this.rows == null)
				{
					this.rows = new RowCollection(this);
				}
				
				return this.rows;
			}
		}


		/// <summary>
		/// A TableModel.Selection representing the collection of selected
		/// Rows and Cells contained within the TableModel
		/// </summary>
		[Browsable(false)]
		public Selection Selections
		{
			get
			{
				if (this.selection == null)
				{
					this.selection = new Selection(this);
				}
				
				return this.selection;
			}
		}


		/// <summary>
		/// Gets or sets the height of each Row in the TableModel
		/// </summary>
		[Category("Appearance"),
		Description("The height of each row")]
		public int RowHeight
		{
			get
			{
				return this.rowHeight;
			}

			set
			{
				if (value < TableModel.MinimumRowHeight)
				{
					value = TableModel.MinimumRowHeight;
				}
				else if (value > TableModel.MaximumRowHeight)
				{
					value = TableModel.MaximumRowHeight;
				}
				
				if (this.rowHeight != value)
				{
					this.rowHeight = value;

					this.OnRowHeightChanged(EventArgs.Empty);
				}
			}
		}


		/// <summary>
		/// Specifies whether the RowHeight property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the RowHeight property should be serialized, 
		/// false otherwise</returns>
		private bool ShouldSerializeRowHeight()
		{
			return this.rowHeight != TableModel.DefaultRowHeight;
		}


		/// <summary>
		/// Gets the Table the TableModel belongs to
		/// </summary>
		[Browsable(false)]
		public Table Table
		{
			get
			{
				return this.table;
			}
		}


		/// <summary>
		/// Gets or sets the Table the TableModel belongs to
		/// </summary>
		internal Table InternalTable
		{
			get
			{
				return this.table;
			}

			set
			{
				this.table = value;
			}
		}


		/// <summary>
		/// Gets whether the TableModel is able to raise events
		/// </summary>
        protected override bool CanRaiseEvents
		{
			get
			{
				// check if the Table that the TableModel belongs to is able to 
				// raise events (if it can't, the TableModel shouldn't raise 
				// events either)
				if (this.Table != null)
				{
					return this.Table.CanRaiseEventsInternal;
				}

				return true;
			}
		}

        /// <summary>
        /// Gets the value for CanRaiseEvents.
        /// </summary>
        protected internal bool CanRaiseEventsInternal
        {
            get { return this.CanRaiseEvents; }
        }

		/// <summary>
		/// Gets whether the TableModel is enabled
		/// </summary>
		internal bool Enabled
		{
			get
			{
				if (this.Table == null)
				{
					return true;
				}

				return this.Table.Enabled;
			}
		}


		/// <summary>
		/// Updates the Row's Index property so that it matches the Rows 
		/// position in the RowCollection
		/// </summary>
		/// <param name="start">The index to start updating from</param>
		internal void UpdateRowIndicies(int start)
		{
			if (start == -1)
			{
				start = 0;
			}
			
			for (int i=start; i<this.Rows.Count; i++)
			{
				this.Rows[i].InternalIndex = i;
			}
		}

		#endregion


		#region Events

		/// <summary>
		/// Raises the RowAdded event
		/// </summary>
		/// <param name="e">A TableModelEventArgs that contains the event data</param>
		protected internal virtual void OnRowAdded(TableModelEventArgs e)
		{
			e.Row.InternalTableModel = this;
			e.Row.InternalIndex = e.RowFromIndex;
			e.Row.ClearSelection();

			this.UpdateRowIndicies(e.RowFromIndex);
			
			if (this.CanRaiseEvents)
			{
				if (this.Table != null)
				{
					this.Table.OnRowAdded(e);
				}

				if (RowAdded != null)
				{
					RowAdded(this, e);
				}
			}
		}


		/// <summary>
		/// Raises the RowRemoved event
		/// </summary>
		/// <param name="e">A TableModelEventArgs that contains the event data</param>
		protected internal virtual void OnRowRemoved(TableModelEventArgs e)
		{
			if (e.Row != null && e.Row.TableModel == this)
			{
				e.Row.InternalTableModel = null;
				e.Row.InternalIndex = -1;
				
				if (e.Row.AnyCellsSelected)
				{
					e.Row.ClearSelection();

					this.Selections.RemoveRow(e.Row);
				}
			}

			this.UpdateRowIndicies(e.RowFromIndex);
			
			if (this.CanRaiseEvents)
			{
				if (this.Table != null)
				{
					this.Table.OnRowRemoved(e);
				}

				if (RowRemoved != null)
				{
					RowRemoved(this, e);
				}
			}
		}


		/// <summary>
		/// Raises the SelectionChanged event
		/// </summary>
		/// <param name="e">A SelectionEventArgs that contains the event data</param>
		protected virtual void OnSelectionChanged(SelectionEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				if (this.Table != null)
				{
					this.Table.OnSelectionChanged(e);
				}
				
				if (SelectionChanged != null)
				{
					SelectionChanged(this, e);
				}
			}
		}


		/// <summary>
		/// Raises the RowHeightChanged event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected virtual void OnRowHeightChanged(EventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				if (this.Table != null)
				{
					this.Table.OnRowHeightChanged(e);
				}
				
				if (RowHeightChanged != null)
				{
					RowHeightChanged(this, e);
				}
			}
		}


		/// <summary>
		/// Raises the RowPropertyChanged event
		/// </summary>
		/// <param name="e">A RowEventArgs that contains the event data</param>
		internal void OnRowPropertyChanged(RowEventArgs e)
		{
			if (this.Table != null)
			{
				this.Table.OnRowPropertyChanged(e);
			}
		}


		/// <summary>
		/// Raises the CellAdded event
		/// </summary>
		/// <param name="e">A RowEventArgs that contains the event data</param>
		internal void OnCellAdded(RowEventArgs e)
		{
			if (this.Table != null)
			{
				this.Table.OnCellAdded(e);
			}
		}


		/// <summary>
		/// Raises the CellRemoved event
		/// </summary>
		/// <param name="e">A RowEventArgs that contains the event data</param>
		internal void OnCellRemoved(RowEventArgs e)
		{
			if (this.Table != null)
			{
				this.Table.OnCellRemoved(e);
			}
		}


		/// <summary>
		/// Raises the CellPropertyChanged event
		/// </summary>
		/// <param name="e">A CellEventArgs that contains the event data</param>
		internal void OnCellPropertyChanged(CellEventArgs e)
		{
			if (this.Table != null)
			{
				this.Table.OnCellPropertyChanged(e);
			}
		}

		#endregion


		#region Selection

		/// <summary>
		/// Represents the collection of selected Rows and Cells in a TableModel.
		/// </summary>
		public class Selection
		{
			#region Class Data

			/// <summary>
			/// The TableModel that owns the Selection
			/// </summary>
			private TableModel owner;

			/// <summary>
			/// The list of Rows that have selected Cells
			/// </summary>
			private ArrayList rows;

			/// <summary>
			/// The starting cell of a selection that uses the shift key
			/// </summary>
			private CellPos shiftSelectStart;

			/// <summary>
			/// The ending cell of a selection that uses the shift key
			/// </summary>
			private CellPos shiftSelectEnd;

			#endregion

			#region Constructor

			/// <summary>
			/// Initializes a new instance of the TableModel.Selection class 
			/// that belongs to the specified TableModel
			/// </summary>
			/// <param name="owner">A TableModel representing the tableModel that owns 
			/// the Selection</param>
			public Selection(TableModel owner)
			{
				if (owner == null)
				{
					throw new ArgumentNullException("owner", "owner cannot be null");
				}
				
				this.owner = owner;
				this.rows = new ArrayList();

				this.shiftSelectStart = CellPos.Empty;
				this.shiftSelectEnd = CellPos.Empty;
			}

			#endregion

			#region Methods

			#region Add

			/// <summary>
			/// Replaces the currently selected Cells with the Cell at the specified 
			/// row and column indexes
			/// </summary>
			/// <param name="row">The row index of the Cell to be selected</param>
			/// <param name="column">The column index of the Cell to be selected</param>
			public void SelectCell(int row, int column)
			{
				// don't bother going any further if the cell 
				// is already selected
				if (this.rows.Count == 1)
				{
					Row r = (Row) this.rows[0];
					
					if (r.InternalIndex == row && r.SelectedCellCount == 1)
					{
						if (column >= 0 && column < r.Cells.Count)
						{
							if (r.Cells[column].Selected)
							{
								return;
							}
						}
					}
				}
				
				this.SelectCells(row, column, row, column);
			}


			/// <summary>
			/// Replaces the currently selected Cells with the Cell at the specified CellPos
			/// </summary>
			/// <param name="cellPos">A CellPos thst specifies the row and column indicies of 
			/// the Cell to be selected</param>
			public void SelectCell(CellPos cellPos)
			{
				this.SelectCell(cellPos.Row, cellPos.Column);
			}


			/// <summary>
			/// Replaces the currently selected Cells with the Cells located between the specified 
			/// start and end row/column indicies
			/// </summary>
			/// <param name="startRow">The row index of the start Cell</param>
			/// <param name="startColumn">The column index of the start Cell</param>
			/// <param name="endRow">The row index of the end Cell</param>
			/// <param name="endColumn">The column index of the end Cell</param>
			public void SelectCells(int startRow, int startColumn, int endRow, int endColumn)
			{
				int[] oldSelectedIndicies = this.SelectedIndicies;
				
				this.InternalClear();

				if (this.InternalAddCells(startRow, startColumn, endRow, endColumn))
				{
					this.owner.OnSelectionChanged(new SelectionEventArgs(this.owner, oldSelectedIndicies, this.SelectedIndicies));
				}

				this.shiftSelectStart = new CellPos(startRow, startColumn);
				this.shiftSelectEnd = new CellPos(endRow, endColumn);
			}


			/// <summary>
			/// Replaces the currently selected Cells with the Cells located between the specified 
			/// start and end CellPos
			/// </summary>
			/// <param name="start">A CellPos that specifies the start Cell</param>
			/// <param name="end">A CellPos that specifies the end Cell</param>
			public void SelectCells(CellPos start, CellPos end)
			{
				this.SelectCells(start.Row, start.Column, end.Row, end.Column);
			}

			
			/// <summary>
			/// Adds the Cell at the specified row and column indicies to the current selection
			/// </summary>
			/// <param name="row">The row index of the Cell to add to the selection</param>
			/// <param name="column">The column index of the Cell to add to the selection</param>
			public void AddCell(int row, int column)
			{
				this.AddCells(row, column, row, column);
			}


			/// <summary>
			/// Adds the Cell at the specified row and column indicies to the current selection
			/// </summary>
			/// <param name="cellPos">A CellPos that specifies the Cell to add to the selection</param>
			public void AddCell(CellPos cellPos)
			{
				this.AddCell(cellPos.Row, cellPos.Column);
			}


			/// <summary>
			/// Adds the Cells located between the specified start and end row/column indicies 
			/// to the current selection
			/// </summary>
			/// <param name="startRow">The row index of the start Cell</param>
			/// <param name="startColumn">The column index of the start Cell</param>
			/// <param name="endRow">The row index of the end Cell</param>
			/// <param name="endColumn">The column index of the end Cell</param>
			public void AddCells(int startRow, int startColumn, int endRow, int endColumn)
			{
				int[] oldSelectedIndicies = this.SelectedIndicies;
				
				if (InternalAddCells(startRow, startColumn, endRow, endColumn))
				{
					this.owner.OnSelectionChanged(new SelectionEventArgs(this.owner, oldSelectedIndicies, this.SelectedIndicies));
				}
				
				this.shiftSelectStart = new CellPos(startRow, startColumn);
				this.shiftSelectEnd = new CellPos(endRow, endColumn);
			}


			/// <summary>
			/// Adds the Cells located between the specified start and end CellPos to the
			/// current selection
			/// </summary>
			/// <param name="start">A CellPos that specifies the start Cell</param>
			/// <param name="end">A CellPos that specifies the end Cell</param>
			public void AddCells(CellPos start, CellPos end)
			{
				this.AddCells(start.Row, start.Column, end.Row, end.Column);
			}


			/// <summary>
			/// Adds the Cells located between the specified start and end CellPos to the
			/// current selection without raising an event
			/// </summary>
			/// <param name="start">A CellPos that specifies the start Cell</param>
			/// <param name="end">A CellPos that specifies the end Cell</param>
			/// <returns>true if any Cells were added, false otherwise</returns>
			private bool InternalAddCells(CellPos start, CellPos end)
			{
				return this.InternalAddCells(start.Row, start.Column, end.Row, end.Column);
			}


			/// <summary>
			/// Adds the Cells located between the specified start and end row/column indicies 
			/// to the current selection without raising an event
			/// </summary>
			/// <param name="startRow">The row index of the start Cell</param>
			/// <param name="startColumn">The column index of the start Cell</param>
			/// <param name="endRow">The row index of the end Cell</param>
			/// <param name="endColumn">The column index of the end Cell</param>
			/// <returns>true if any Cells were added, false otherwise</returns>
			private bool InternalAddCells(int startRow, int startColumn, int endRow, int endColumn)
			{
				this.Normalise(ref startRow, ref endRow);
				this.Normalise(ref startColumn, ref endColumn);

				bool anyAdded = false;
				bool anyAddedInRow = false;

				for (int i=startRow; i<=endRow; i++)
				{
					if (i >= this.owner.Rows.Count)
					{
						break;
					}
					
					Row r = this.owner.Rows[i];
					
					for (int j=startColumn; j<=endColumn; j++)
					{
						if (j >= r.Cells.Count)
						{
							break;
						}

						if (!r.Cells[j].Selected && r.Cells[j].Enabled)
						{
							if (this.owner.Table != null && !this.owner.Table.IsCellEnabled(i, j))
							{
								continue;
							}

							r.Cells[j].SetSelected(true);
							r.InternalSelectedCellCount++;
							
							anyAdded = true;
							anyAddedInRow = true;
						}
					}
					
					if (anyAddedInRow && !this.rows.Contains(r))
					{
						this.rows.Add(r);
					}

					anyAddedInRow = false;
				}

				return anyAdded;
			}


			/// <summary>
			/// Adds the Cells between the last selection start Cell and the Cell at the 
			/// specified row/column indicies to the current selection.  Any Cells that are 
			/// between the last start and end Cells that are not in the new area are 
			/// removed from the current selection
			/// </summary>
			/// <param name="row">The row index of the shift selected Cell</param>
			/// <param name="column">The column index of the shift selected Cell</param>
			public void AddShiftSelectedCell(int row, int column)
			{
				int[] oldSelectedIndicies = this.SelectedIndicies;
				
				if (this.shiftSelectStart == CellPos.Empty)
				{
					this.shiftSelectStart = new CellPos(0, 0);
				}

				bool changed = false;
					
				if (this.shiftSelectEnd != CellPos.Empty)
				{
					changed = this.InternalRemoveCells(this.shiftSelectStart, this.shiftSelectEnd);
					changed |= this.InternalAddCells(this.shiftSelectStart, new CellPos(row, column));
				}
				else
				{
					changed = this.InternalAddCells(0, 0, row, column);
				}

				if (changed)
				{
					this.owner.OnSelectionChanged(new SelectionEventArgs(this.owner, oldSelectedIndicies, this.SelectedIndicies));
				}

				this.shiftSelectEnd = new CellPos(row, column);
			}


			/// <summary>
			/// Adds the Cells between the last selection start Cell and the Cell at the 
			/// specified CellPas to the current selection.  Any Cells that are 
			/// between the last start and end Cells that are not in the new area are 
			/// removed from the current selection
			/// </summary>
			/// <param name="cellPos">A CellPos that specifies the shift selected Cell</param>
			public void AddShiftSelectedCell(CellPos cellPos)
			{
				this.AddShiftSelectedCell(cellPos.Row, cellPos.Column);
			}


			/// <summary>
			/// Ensures that the first index is smaller than the second index, 
			/// performing a swap if necessary
			/// </summary>
			/// <param name="a">The first index</param>
			/// <param name="b">The second index</param>
			private void Normalise(ref int a, ref int b)
			{
				if (a < 0)
				{
					a = 0;
				}

				if (b < 0)
				{
					b = 0;
				}
				
				if (b < a)
				{
					int temp = a;
					a = b;
					b = temp;
				}
			}

			#endregion

			#region Clear

			/// <summary>
			/// Removes all selected Rows and Cells from the selection
			/// </summary>
			public void Clear()
			{
				if (this.rows.Count > 0)
				{
					int[] oldSelectedIndicies = this.SelectedIndicies;

					this.InternalClear();

					this.shiftSelectStart = CellPos.Empty;
					this.shiftSelectEnd = CellPos.Empty;

					this.owner.OnSelectionChanged(new SelectionEventArgs(this.owner, oldSelectedIndicies, this.SelectedIndicies));
				}
			}

			
			/// <summary>
			/// Removes all selected Rows and Cells from the selection without raising an event
			/// </summary>
			private void InternalClear()
			{
				if (this.rows.Count > 0)
				{
					for (int i=0; i<this.rows.Count; i++)
					{
						((Row) this.rows[i]).ClearSelection();
					}
					
					this.rows.Clear();
					this.rows.Capacity = 0;
				}
			}

			#endregion

			#region Remove

			/// <summary>
			/// Removes the Cell at the specified row and column indicies from the current selection
			/// </summary>
			/// <param name="row">The row index of the Cell to remove from the selection</param>
			/// <param name="column">The column index of the Cell to remove from the selection</param>
			public void RemoveCell(int row, int column)
			{
				this.RemoveCells(row, column, row, column);
			}


			/// <summary>
			/// Removes the Cell at the specified row and column indicies from the current selection
			/// </summary>
			/// <param name="cellPos">A CellPos that specifies the Cell to remove from the selection</param>
			public void RemoveCell(CellPos cellPos)
			{
				this.RemoveCell(cellPos.Row, cellPos.Column);
			}


			/// <summary>
			/// Removes the Cells located between the specified start and end row/column indicies 
			/// from the current selection
			/// </summary>
			/// <param name="startRow">The row index of the start Cell</param>
			/// <param name="startColumn">The column index of the start Cell</param>
			/// <param name="endRow">The row index of the end Cell</param>
			/// <param name="endColumn">The column index of the end Cell</param>
			public void RemoveCells(int startRow, int startColumn, int endRow, int endColumn)
			{
				if (this.rows.Count > 0)
				{
					int[] oldSelectedIndicies = this.SelectedIndicies;

					if (this.InternalRemoveCells(startRow, startColumn, endRow, endColumn))
					{
						this.owner.OnSelectionChanged(new SelectionEventArgs(this.owner, oldSelectedIndicies, this.SelectedIndicies));
					}

					this.shiftSelectStart = new CellPos(startRow, startColumn);
					this.shiftSelectEnd = new CellPos(endRow, endColumn);
				}
			}


			/// <summary>
			/// Removes the Cells located between the specified start and end CellPos from the
			/// current selection
			/// </summary>
			/// <param name="start">A CellPos that specifies the start Cell</param>
			/// <param name="end">A CellPos that specifies the end Cell</param>
			public void RemoveCells(CellPos start, CellPos end)
			{
				this.RemoveCells(start.Row, start.Column, end.Row, end.Column);
			}


			/// <summary>
			/// Removes the Cells located between the specified start and end CellPos from the
			/// current selection without raising an event
			/// </summary>
			/// <param name="start">A CellPos that specifies the start Cell</param>
			/// <param name="end">A CellPos that specifies the end Cell</param>
			/// <returns>true if any Cells were added, false otherwise</returns>
			private bool InternalRemoveCells(CellPos start, CellPos end)
			{
				return this.InternalRemoveCells(start.Row, start.Column, end.Row, end.Column);
			}


			/// <summary>
			/// Removes the Cells located between the specified start and end row/column indicies 
			/// from the current selection without raising an event
			/// </summary>
			/// <param name="startRow">The row index of the start Cell</param>
			/// <param name="startColumn">The column index of the start Cell</param>
			/// <param name="endRow">The row index of the end Cell</param>
			/// <param name="endColumn">The column index of the end Cell</param>
			/// <returns>true if any Cells were added, false otherwise</returns>
			private bool InternalRemoveCells(int startRow, int startColumn, int endRow, int endColumn)
			{
				this.Normalise(ref startRow, ref endRow);
				this.Normalise(ref startColumn, ref endColumn);

				bool anyRemoved = false;

				for (int i=startRow; i<=endRow; i++)
				{
					if (i >= this.owner.Rows.Count)
					{
						break;
					}
					
					Row r = this.owner.Rows[i];
					
					for (int j=startColumn; j<=endColumn; j++)
					{
						if (j >= r.Cells.Count)
						{
							break;
						}

						if (r.Cells[j].Selected)
						{
							r.Cells[j].SetSelected(false);
							r.InternalSelectedCellCount--;

							anyRemoved = true;
						}
					}
					
					if (!r.AnyCellsSelected)
					{
						if (this.rows.Contains(r))
						{
							this.rows.Remove(r);
						}
					}
				}

				return anyRemoved;
			}


			/// <summary>
			/// Removes the specified Row from the selection
			/// </summary>
			/// <param name="row">The Row to be removed from the selection</param>
			internal void RemoveRow(Row row)
			{
				// Mateusz [PEYN] Adamus (peyn@tlen.pl)
				// old method didn't work well
				// it removed rows from selection but didn't refreshed table's view
				if (this.rows.Contains(row))
				{
					// if Row has already been removed from the table then just remove it from selection
					if( this.owner.Rows.IndexOf( row ) == -1 )
					{
						int[] oldSelectedIndicies = this.SelectedIndicies;
						this.rows.Remove( row );
						this.owner.OnSelectionChanged( new SelectionEventArgs( this.owner, oldSelectedIndicies, this.SelectedIndicies ) );

						return;
					}

					// remove from selection every cell that is in the Row we want to remove
					// - remove Row
					this.RemoveCells( row.Index, 0, row.Index, row.Cells.Count - 1 );
				}
			}

			// Mateusz [PEYN] Adamus (peyn@tlen.pl)
			// New override of RemoveRow - now you can remove Row specified by it's index
			/// <summary>
			/// Removes the specified Row from selection
			/// </summary>
			/// <param name="row">Index specifing which Row we want to remove from selection</param>
			internal void RemoveRow( int row )
			{
				// if specified row isn't in the rows collection
				if( ( row < 0 ) || ( row >= this.owner.Rows.Count ) )
				{
					// then do nothing and exit
					return;
				}

				// remove specified Row
				this.RemoveRow( this.owner.Rows[ row ] );
			}

			#endregion

			#region Queries

			/// <summary>
			/// Returns whether the Cell at the specified row and column indicies is 
			/// currently selected
			/// </summary>
			/// <param name="row">The row index of the specified Cell</param>
			/// <param name="column">The column index of the specified Cell</param>
			/// <returns>true if the Cell at the specified row and column indicies is 
			/// selected, false otherwise</returns>
			public bool IsCellSelected(int row, int column)
			{
				if (row < 0 || row >= this.owner.Rows.Count)
				{
					return false;
				}

				return this.owner.Rows[row].IsCellSelected(column);
			}


			/// <summary>
			/// Returns whether the Cell at the specified CellPos is currently selected
			/// </summary>
			/// <param name="cellPos">A CellPos the represents the row and column indicies 
			/// of the Cell to check</param>
			/// <returns>true if the Cell at the specified CellPos is currently selected, 
			/// false otherwise</returns>
			public bool IsCellSelected(CellPos cellPos)
			{
				return this.IsCellSelected(cellPos.Row, cellPos.Column);
			}


			/// <summary>
			/// Returns whether the Row at the specified index in th TableModel is 
			/// currently selected
			/// </summary>
			/// <param name="index">The index of the Row to check</param>
			/// <returns>true if the Row at the specified index is currently selected, 
			/// false otherwise</returns>
			public bool IsRowSelected(int index)
			{
				if (index < 0 || index >= this.owner.Rows.Count)
				{
					return false;
				}

				return this.owner.Rows[index].AnyCellsSelected;
			}

			#endregion

			#endregion

			#region Properties
			/// <summary>
			/// Gets an array that contains the currently selected Rows
			/// </summary>
			public Row[] SelectedItems
			{
				get
				{
					if (this.rows.Count == 0)
						return new Row[0];

					this.rows.Sort(new RowComparer());

					return (Row[]) this.rows.ToArray(typeof(Row));
				}
			}


			/// <summary>
			/// Gets an array that contains the indexes of the currently selected Rows
			/// </summary>
			public int[] SelectedIndicies
			{
				get
				{
					if (this.rows.Count == 0)
						return new int[0];

					this.rows.Sort(new RowComparer());

					int[] indicies = new int[this.rows.Count];

					for (int i=0; i<this.rows.Count; i++)
					{
						indicies[i] = ((Row) this.rows[i]).InternalIndex;
					}

					return indicies;
				}
			}


			/// <summary>
			/// Returns a Rectange that bounds the currently selected Rows
			/// </summary>
			public Rectangle SelectionBounds
			{
				get
				{
					if (this.rows.Count == 0)
						return Rectangle.Empty;

					int[] indicies = this.SelectedIndicies;

					return this.CalcSelectionBounds(indicies[0], indicies[indicies.Length-1]);
				}
			}


			/// <summary>
            /// Returns a Rectange that bounds the currently selected Rows
			/// </summary>
			/// <param name="start">First row index</param>
            /// <param name="end">Last row index</param>
			/// <returns></returns>
			internal Rectangle CalcSelectionBounds(int start, int end)
			{
				this.Normalise(ref start, ref end);
				
				Rectangle bounds = new Rectangle();

				if (this.owner.Table != null && this.owner.Table.ColumnModel != null)
					bounds.Width = this.owner.Table.ColumnModel.VisibleColumnsWidth;

                if (this.owner.Table.EnableWordWrap)
                {
					// v1.1.1 fix - this Y value used to include the border + header height

                    bounds.Y = this.owner.Table.RowYDifference(0, start);

                    if (start == end)
                    {
                        // no object when using subrows here
                        // fix by CINAMON
                        if (this.owner.rows[start] != null)
                            bounds.Height = this.owner.Rows[start].Height;
                        else
                            bounds.Height = this.owner.RowHeight;
                    }
                    else
                    {
                        bounds.Height = this.owner.Table.RowYDifference(start, end) + this.owner.Rows[end].Height;
                    }
                }
                else
                {
                    bounds.Y = start * this.owner.RowHeight;

                    if (start == end)
                        bounds.Height = this.owner.RowHeight;
                    else
                        bounds.Height = ((end + 1) * this.owner.RowHeight) - bounds.Y;
                }

				return bounds;
			}
			#endregion
		}
		#endregion
	}
}
