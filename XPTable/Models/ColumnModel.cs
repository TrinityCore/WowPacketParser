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
using System.Drawing;

using XPTable.Editors;
using XPTable.Events;
using XPTable.Models.Design;
using XPTable.Renderers;
using XPTable.Sorting;

namespace XPTable.Models
{
	/// <summary>
	/// A ColumnModel contains a collection of Columns that will be displayed in a Table. It also keeps track of whether a 
	/// CellRenderer or CellEditor has been created for a particular Column. 
	/// </summary>
	[DesignTimeVisible(true),
	ToolboxItem(true), 
	ToolboxBitmap(typeof(ColumnModel))]
	public class ColumnModel : Component
	{
		#region EventHandlers

		/// <summary>
		/// Occurs when a Column has been added to the ColumnModel
		/// </summary>
		public event ColumnModelEventHandler ColumnAdded;

		/// <summary>
		/// Occurs when a Column is removed from the ColumnModel
		/// </summary>
		public event ColumnModelEventHandler ColumnRemoved;

		/// <summary>
		/// Occurs when the value of the HeaderHeight property changes
		/// </summary>
		public event EventHandler HeaderHeightChanged;

		#endregion
		
		#region Class Data

		/// <summary>
		/// The default height of a column header
		/// </summary>
		public static readonly int DefaultHeaderHeight = 20;

		/// <summary>
		/// The minimum height of a column header
		/// </summary>
		public static readonly int MinimumHeaderHeight = 16;

		/// <summary>
		/// The maximum height of a column header
		/// </summary>
		public static readonly int MaximumHeaderHeight = 128;
		
		/// <summary>
		/// The collection of Column's contained in the ColumnModel
		/// </summary>
		private ColumnCollection columns;

		/// <summary>
		/// The list of all default CellRenderers used by the Columns in the ColumnModel
		/// </summary>
		private Hashtable cellRenderers;

		/// <summary>
		/// The list of all default CellEditors used by the Columns in the ColumnModel
		/// </summary>
		private Hashtable cellEditors;

		/// <summary>
		/// The Table that the ColumnModel belongs to
		/// </summary>
		private Table table;

		/// <summary>
		/// The height of the column headers
		/// </summary>
		private int headerHeight;

        /// <summary>
        /// Specifies a collection of underlying sort order(s)
        /// </summary>
        private SortColumnCollection secondarySortOrder;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the ColumnModel class with default settings
		/// </summary>
		public ColumnModel()
		{
			this.Init();
		}


		/// <summary>
		/// Initializes a new instance of the ColumnModel class with an array of strings 
		/// representing TextColumns
		/// </summary>
		/// <param name="columns">An array of strings that represent the Columns of 
		/// the ColumnModel</param>
		public ColumnModel(string[] columns)
		{
			if (columns == null)
			{
				throw new ArgumentNullException("columns", "string[] cannot be null");
			}
			
			this.Init();

			if (columns.Length > 0)
			{
				Column[] cols = new Column[columns.Length];

				for (int i=0; i<columns.Length; i++)
				{
					cols[i] = new TextColumn(columns[i]);
				}

				this.Columns.AddRange(cols);
			}
		}


		/// <summary>
		/// Initializes a new instance of the Row class with an array of Column objects
		/// </summary>
		/// <param name="columns">An array of Cell objects that represent the Columns 
		/// of the ColumnModel</param>
		public ColumnModel(Column[] columns)
		{
			if (columns == null)
			{
				throw new ArgumentNullException("columns", "Column[] cannot be null");
			}
			
			this.Init();

			if (columns.Length > 0)
			{
				this.Columns.AddRange(columns);
			}
		}


		/// <summary>
		/// Initialise default settings
		/// </summary>
		private void Init()
		{
			this.columns = null;
			
			this.table = null;
			this.headerHeight = ColumnModel.DefaultHeaderHeight;

			this.cellRenderers = new Hashtable();
			this.SetCellRenderer("TEXT", new TextCellRenderer());

			this.cellEditors = new Hashtable();
			this.SetCellEditor("TEXT", new TextCellEditor());

            this.secondarySortOrder = new SortColumnCollection();
		}

		#endregion

		#region Methods

		#region Coordinate Translation

		/// <summary>
		/// Returns the index of the Column that lies on the specified position
		/// </summary>
		/// <param name="xPosition">The x-coordinate to check</param>
		/// <returns>The index of the Column or -1 if no Column is found</returns>
		public int ColumnIndexAtX(int xPosition) 
		{
			if (xPosition < 0 || xPosition > this.VisibleColumnsWidth)
			{
				return -1;
			}

			for (int i=0; i<this.Columns.Count; i++)
			{
				if (this.Columns[i].Visible && xPosition < this.Columns[i].Right)
				{
					return i;
				}
			}

			return -1;
		}


		/// <summary>
		/// Returns the Column that lies on the specified position
		/// </summary>
		/// <param name="xPosition">The x-coordinate to check</param>
		/// <returns>The Column that lies on the specified position, 
		/// or null if not found</returns>
		public Column ColumnAtX(int xPosition) 
		{
			if (xPosition < 0 || xPosition > this.VisibleColumnsWidth)
			{
				return null;
			}
			
			int index = this.ColumnIndexAtX(xPosition);

			if (index != -1)
			{
				return this.Columns[index];
			}

			return null;
		}

		/// <summary>
		/// Returns a rectangle that contains the header of the column 
		/// at the specified index in the ColumnModel
		/// </summary>
		/// <param name="index">The index of the column</param>
		/// <returns>that countains the header of the specified column</returns>
		public Rectangle ColumnHeaderRect(int index)
		{
			// make sure the index is valid and the column is not hidden
			if (index < 0 || index >= this.Columns.Count || !this.Columns[index].Visible)
				return Rectangle.Empty;

			return new Rectangle(this.Columns[index].Left, 0, this.Columns[index].Width, this.HeaderHeight);
		}

		/// <summary>
		/// Returns a rectangle that contains the header of the specified column
		/// </summary>
		/// <param name="column">The column</param>
		/// <returns>A rectangle that countains the header of the specified column</returns>
		public Rectangle ColumnHeaderRect(Column column)
		{
			// check if we actually own the column
			int index = this.Columns.IndexOf(column);
			
			if (index == -1)
				return Rectangle.Empty;

			return this.ColumnHeaderRect(index);
		}
		#endregion

		#region Dispose

		/// <summary> 
		/// Releases the unmanaged resources used by the ColumnModel and optionally 
		/// releases the managed resources
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				
			}

			base.Dispose(disposing);
		}

		#endregion

		#region Editors

		/// <summary>
		/// Returns the ICellEditor that is associated with the specified name
		/// </summary>
		/// <param name="name">The name thst is associated with an ICellEditor</param>
		/// <returns>The ICellEditor that is associated with the specified name, 
		/// or null if the name or ICellEditor do not exist</returns>
		public ICellEditor GetCellEditor(string name)
		{
			if (name == null || name.Length == 0)
			{
				return null;
			}
			
			name = name.ToUpper();
			
			if (!this.cellEditors.ContainsKey(name))
			{
				if (this.cellEditors.Count == 0)
				{
					this.SetCellEditor("TEXT", new TextCellEditor());
				}
				
				return null;
			}

			return (ICellEditor) this.cellEditors[name];
		}


		/// <summary>
		/// Gets the ICellEditor for the Column at the specified index in the 
		/// ColumnModel
		/// </summary>
		/// <param name="column">The index of the Column in the ColumnModel for 
		/// which an ICellEditor will be retrieved</param>
		/// <returns>The ICellEditor for the Column at the specified index, or 
		/// null if the editor does not exist</returns>
		public ICellEditor GetCellEditor(int column)
		{
			if (column < 0 || column >= this.Columns.Count)
			{
				return null;
			}

			//
			if (this.Columns[column].Editor != null)
			{
				return this.Columns[column].Editor;
			}

			return this.GetCellEditor(this.Columns[column].GetDefaultEditorName());
		}


		/// <summary>
		/// Associates the specified ICellRenderer with the specified name
		/// </summary>
		/// <param name="name">The name to be associated with the specified ICellEditor</param>
		/// <param name="editor">The ICellEditor to be added to the ColumnModel</param>
		public void SetCellEditor(string name, ICellEditor editor)
		{
			if (name == null || name.Length == 0 || editor == null)
			{
				return;
			}
			
			name = name.ToUpper();
			
			if (this.cellEditors.ContainsKey(name))
			{	
				this.cellEditors.Remove(name);
				
				this.cellEditors[name] = editor;
			}
			else
			{
				this.cellEditors.Add(name, editor);
			}
		}


		/// <summary>
		/// Gets whether the ColumnModel contains an ICellEditor with the 
		/// specified name
		/// </summary>
		/// <param name="name">The name associated with the ICellEditor</param>
		/// <returns>true if the ColumnModel contains an ICellEditor with the 
		/// specified name, false otherwise</returns>
		public bool ContainsCellEditor(string name)
		{
			if (name == null)
			{
				return false;
			}

			return this.cellEditors.ContainsKey(name);
		}


		/// <summary>
		/// Gets the number of ICellEditors contained in the ColumnModel
		/// </summary>
		internal int EditorCount
		{
			get
			{
				return this.cellEditors.Count;
			}
		}

		#endregion

		#region Renderers
		/// <summary>
		/// Returns the ICellRenderer that is associated with the specified name
		/// </summary>
		/// <param name="name">The name thst is associated with an ICellEditor</param>
		/// <returns>The ICellRenderer that is associated with the specified name, 
		/// or null if the name or ICellRenderer do not exist</returns>
		public ICellRenderer GetCellRenderer(string name)
		{
			if (name == null)
				name = "TEXT";
			
			name = name.ToUpper();
			
			if (!this.cellRenderers.ContainsKey(name))
			{
				if (this.cellRenderers.Count == 0)
					this.SetCellRenderer("TEXT", new TextCellRenderer());
				
				return (ICellRenderer) this.cellRenderers["TEXT"];
			}

			return (ICellRenderer) this.cellRenderers[name];
		}

		/// <summary>
		/// Gets the ICellRenderer for the Column at the specified index in the 
		/// ColumnModel
		/// </summary>
		/// <param name="column">The index of the Column in the ColumnModel for 
		/// which an ICellRenderer will be retrieved</param>
		/// <returns>The ICellRenderer for the Column at the specified index, or 
		/// null if the renderer does not exist</returns>
		public ICellRenderer GetCellRenderer(int column)
		{
			if (column < 0 || column >= this.Columns.Count)
				return null;

			if (this.Columns[column].Renderer != null)
				return this.Columns[column].Renderer;

			return this.GetCellRenderer(this.Columns[column].GetDefaultRendererName());
		}

		/// <summary>
		/// Associates the specified ICellRenderer with the specified name
		/// </summary>
		/// <param name="name">The name to be associated with the specified ICellRenderer</param>
		/// <param name="renderer">The ICellRenderer to be added to the ColumnModel</param>
		public void SetCellRenderer(string name, ICellRenderer renderer)
		{
			if (name == null || renderer == null)
				return;
			
			name = name.ToUpper();
			
			if (this.cellRenderers.ContainsKey(name))
			{	
				this.cellRenderers.Remove(name);
				this.cellRenderers[name] = renderer;
			}
			else
			{
				this.cellRenderers.Add(name, renderer);
			}
		}

		/// <summary>
		/// Gets whether the ColumnModel contains an ICellRenderer with the 
		/// specified name
		/// </summary>
		/// <param name="name">The name associated with the ICellRenderer</param>
		/// <returns>true if the ColumnModel contains an ICellRenderer with the 
		/// specified name, false otherwise</returns>
		public bool ContainsCellRenderer(string name)
		{
			if (name == null)
			{
				return false;
			}

			return this.cellRenderers.ContainsKey(name);
		}


		/// <summary>
		/// Gets the number of ICellRenderers contained in the ColumnModel
		/// </summary>
		internal int RendererCount
		{
			get
			{
				return this.cellRenderers.Count;
			}
		}

		#endregion

		#region Utility Methods

		/// <summary>
		/// Returns the index of the first visible Column that is to the 
		/// left of the Column at the specified index in the ColumnModel
		/// </summary>
		/// <param name="index">The index of the Column for which the first 
		/// visible Column that is to the left of the specified Column is to 
		/// be found</param>
		/// <returns>the index of the first visible Column that is to the 
		/// left of the Column at the specified index in the ColumnModel, or 
		/// -1 if the Column at the specified index is the first visible column, 
		/// or there are no Columns in the Column model</returns>
		public int PreviousVisibleColumn(int index)
		{
			if (this.Columns.Count == 0)
			{
				return -1;
			}
			
			if (index <= 0)
			{
				return -1;
			}

			if (index >= this.Columns.Count)
			{
				if (this.Columns[this.Columns.Count-1].Visible)
				{
					return this.Columns.Count - 1;
				}
				
				index = this.Columns.Count - 1;
			}

			for (int i=index; i>0; i--)
			{
				if (this.Columns[i-1].Visible)
				{
					return i - 1;
				}
			}

			return -1;
		}


		/// <summary>
		/// Returns the index of the first visible Column that is to the 
		/// right of the Column at the specified index in the ColumnModel
		/// </summary>
		/// <param name="index">The index of the Column for which the first 
		/// visible Column that is to the right of the specified Column is to 
		/// be found</param>
		/// <returns>the index of the first visible Column that is to the 
		/// right of the Column at the specified index in the ColumnModel, or 
		/// -1 if the Column at the specified index is the last visible column, 
		/// or there are no Columns in the Column model</returns>
		public int NextVisibleColumn(int index)
		{
			if (this.Columns.Count == 0)
			{
				return -1;
			}
			
			if (index >= this.Columns.Count - 1)
			{
				return -1;
			}

			for (int i=index; i<this.Columns.Count-1; i++)
			{
				if (this.Columns[i+1].Visible)
				{
					return i + 1;
				}
			}

			return -1;
		}

		#endregion

		#endregion

		#region Properties
		/// <summary>
		/// A ColumnCollection representing the collection of 
		/// Columns contained within the ColumnModel
		/// </summary>
		[Category("Behavior"),
		Description("Column Collection"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		Editor(typeof(ColumnCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public ColumnCollection Columns
		{
			get
			{
				if (this.columns == null)
					this.columns = new ColumnCollection(this);
				
				return this.columns;
			}
		}

		/// <summary>
		/// Gets or sets the height of the column headers
		/// </summary>
		[Category("Appearance"),
		Description("The height of the column headers")]
		public int HeaderHeight
		{
			get { return this.headerHeight; }

			set
			{
				if (value < ColumnModel.MinimumHeaderHeight)
					value = ColumnModel.MinimumHeaderHeight;
				else if (value > ColumnModel.MaximumHeaderHeight)
					value = ColumnModel.MaximumHeaderHeight;
				
				if (this.headerHeight != value)
				{
					this.headerHeight = value;
					this.OnHeaderHeightChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// Specifies whether the HeaderHeight property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the HeaderHeight property should be serialized, 
		/// false otherwise</returns>
        private bool ShouldSerializeHeaderHeight()
        {
            return this.headerHeight != ColumnModel.DefaultHeaderHeight;
        }

		/// <summary>
		/// Gets a rectangle that specifies the width and height of all the 
		/// visible column headers in the model
		/// </summary>
		[Browsable(false)]
		public Rectangle HeaderRect
		{
			get
			{
				if (this.VisibleColumnCount == 0)
					return Rectangle.Empty;
				
				return new Rectangle(0, 0, this.VisibleColumnsWidth, this.HeaderHeight);
			}
		}

		/// <summary>
		/// Gets the total width of all the Columns in the model
		/// </summary>
		[Browsable(false)]
		public int TotalColumnWidth
		{
			get { return this.Columns.TotalColumnWidth; }
		}

		/// <summary>
		/// Gets the total width of all the visible Columns in the model
		/// </summary>
		[Browsable(false)]
		public int VisibleColumnsWidth
		{
			get { return this.Columns.VisibleColumnsWidth; }
		}

		/// <summary>
		/// Gets the index of the last Column that is not hidden
		/// </summary>
		[Browsable(false)]
		public int LastVisibleColumnIndex
		{
			get { return this.Columns.LastVisibleColumn; }
		}

		/// <summary>
		/// Gets the number of Columns in the ColumnModel that are visible
		/// </summary>
		[Browsable(false)]
		public int VisibleColumnCount
		{
			get { return this.Columns.VisibleColumnCount; }
		}

		/// <summary>
		/// Gets the Table the ColumnModel belongs to
		/// </summary>
		[Browsable(false)]
		public Table Table
		{
			get { return this.table; }
		}

        /// <summary>
        /// Gets or sets a collection of underlying sort order(s)
        /// </summary>
        [Browsable(false)]
        public SortColumnCollection SecondarySortOrders
        {
            get { return this.secondarySortOrder; }
            set { this.secondarySortOrder = value; }
        }

		/// <summary>
		/// Gets or sets the Table the ColumnModel belongs to
		/// </summary>
		internal Table InternalTable
		{
			get { return this.table; }
			set { this.table = value; }
		}

		/// <summary>
		/// Gets whether the ColumnModel is able to raise events
		/// </summary>
        protected override bool CanRaiseEvents
		{
			get
			{
				// check if the Table that the ColumModel belongs to is able to 
				// raise events (if it can't, the ColumModel shouldn't raise 
				// events either)
				if (this.Table != null)
					return this.Table.CanRaiseEventsInternal;

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
		/// Gets whether the ColumnModel is enabled
		/// </summary>
		internal bool Enabled
		{
			get
			{
				if (this.Table == null)
					return true;

				return this.Table.Enabled;
			}
		}
		#endregion

        #region Events
        /// <summary>
		/// Raises the ColumnAdded event
		/// </summary>
		/// <param name="e">A ColumnModelEventArgs that contains the event data</param>
		protected internal virtual void OnColumnAdded(ColumnModelEventArgs e)
		{
			e.Column.ColumnModel = this;

			if (!this.ContainsCellRenderer(e.Column.GetDefaultRendererName()))
			{
				this.SetCellRenderer(e.Column.GetDefaultRendererName(), e.Column.CreateDefaultRenderer());
			}

			if (!this.ContainsCellEditor(e.Column.GetDefaultEditorName()))
			{
				this.SetCellEditor(e.Column.GetDefaultEditorName(), e.Column.CreateDefaultEditor());
			}

			if (this.CanRaiseEvents)
			{
				if (this.Table != null)
				{
					this.Table.OnColumnAdded(e);
				}
				
				if (ColumnAdded != null)
				{
					ColumnAdded(this, e);
				}
			}
		}


		/// <summary>
		/// Raises the ColumnRemoved event
		/// </summary>
		/// <param name="e">A ColumnModelEventArgs that contains the event data</param>
		protected internal virtual void OnColumnRemoved(ColumnModelEventArgs e)
		{
			if (e.Column != null && e.Column.ColumnModel == this)
			{
				e.Column.ColumnModel = null;
			}
			
			if (this.CanRaiseEvents)
			{
				if (this.Table != null)
				{
					this.Table.OnColumnRemoved(e);
				}
				
				if (ColumnRemoved != null)
				{
					ColumnRemoved(this, e);
				}
			}
		}


		/// <summary>
		/// Raises the HeaderHeightChanged event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected virtual void OnHeaderHeightChanged(EventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				if (this.Table != null)
				{
					this.Table.OnHeaderHeightChanged(e);
				}
				
				if (HeaderHeightChanged != null)
				{
					HeaderHeightChanged(this, e);
				}
			}
		}


		/// <summary>
		/// Raises the ColumnPropertyChanged event
		/// </summary>
		/// <param name="e">A ColumnEventArgs that contains the event data</param>
		internal void OnColumnPropertyChanged(ColumnEventArgs e)
		{
			if (e.EventType == ColumnEventType.WidthChanged || e.EventType == ColumnEventType.VisibleChanged)
			{
				this.Columns.RecalcWidthCache();
			}
			
			if (this.CanRaiseEvents)
			{
				if (this.Table != null)
				{
					this.Table.OnColumnPropertyChanged(e);
				}
			}
		}

		#endregion
	}
}
