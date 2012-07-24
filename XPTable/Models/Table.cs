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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

using XPTable.Editors;
using XPTable.Events;
using XPTable.Models;
using XPTable.Renderers;
using XPTable.Sorting;
using XPTable.Themes;
using XPTable.Win32;

namespace XPTable.Models
{
	/// <summary>
	/// A Table is "simple" object in that it doesn't actually contain or know how to draw the data it will display.
	/// </summary>
	[DesignTimeVisible(true),
	ToolboxItem(true),
	ToolboxBitmap(typeof(Table))]
	public class Table : Control, ISupportInitialize
	{
		#region Event Handlers

		#region Cells

		/// <summary>
		/// Occurs when the value of a Cells property changes
		/// </summary>
		public event CellEventHandler CellPropertyChanged;

		#region Focus

		/// <summary>
		/// Occurs when a Cell gains focus
		/// </summary>
		public event CellFocusEventHandler CellGotFocus;

		/// <summary>
		/// Occurs when a Cell loses focus
		/// </summary>
		public event CellFocusEventHandler CellLostFocus;

		#endregion

		#region Keys

		/// <summary>
		/// Occurs when a key is pressed when a Cell has focus
		/// </summary>
		public event CellKeyEventHandler CellKeyDown;

		/// <summary>
		/// Occurs when a key is released when a Cell has focus
		/// </summary>
		public event CellKeyEventHandler CellKeyUp;

		#endregion

		#region Mouse

		/// <summary>
		/// Occurs when the mouse pointer enters a Cell
		/// </summary>
		public event CellMouseEventHandler CellMouseEnter;

		/// <summary>
		/// Occurs when the mouse pointer leaves a Cell
		/// </summary>
		public event CellMouseEventHandler CellMouseLeave;

		/// <summary>
		/// Occurs when a mouse pointer is over a Cell and a mouse button is pressed
		/// </summary>
		public event CellMouseEventHandler CellMouseDown;

		/// <summary>
		/// Occurs when a mouse pointer is over a Cell and a mouse button is released
		/// </summary>
		public event CellMouseEventHandler CellMouseUp;

		/// <summary>
		/// Occurs when a mouse pointer is moved over a Cell
		/// </summary>
		public event CellMouseEventHandler CellMouseMove;

		/// <summary>
		/// Occurs when the mouse pointer hovers over a Cell
		/// </summary>
		public event CellMouseEventHandler CellMouseHover;

		/// <summary>
		/// Occurs when a Cell is clicked
		/// </summary>
		public event CellMouseEventHandler CellClick;

		/// <summary>
		/// Occurs when a Cell is double-clicked
		/// </summary>
		public event CellMouseEventHandler CellDoubleClick;

		#endregion

		#region Buttons

		/// <summary>
		/// Occurs when a Cell's button is clicked
		/// </summary>
		public event CellButtonEventHandler CellButtonClicked;

		#endregion

		#region CheckBox

		/// <summary>
		/// Occurs when a Cell's Checked value changes
		/// </summary>
		public event CellCheckBoxEventHandler CellCheckChanged;

		#endregion

		#endregion

		#region Column

		/// <summary>
		/// Occurs when a Column's property changes
		/// </summary>
		public event ColumnEventHandler ColumnPropertyChanged;

        /// <summary>
        /// Occurs when the column has its width automatically calculated.
        /// </summary>
        public event ColumnEventHandler ColumnAutoResize;

		#endregion

		#region Column Headers

		/// <summary>
		/// Occurs when the mouse pointer enters a Column Header
		/// </summary>
		public event HeaderMouseEventHandler HeaderMouseEnter;

		/// <summary>
		/// Occurs when the mouse pointer leaves a Column Header
		/// </summary>
		public event HeaderMouseEventHandler HeaderMouseLeave;

		/// <summary>
		/// Occurs when a mouse pointer is over a Column Header and a mouse button is pressed
		/// </summary>
		public event HeaderMouseEventHandler HeaderMouseDown;

		/// <summary>
		/// Occurs when a mouse pointer is over a Column Header and a mouse button is released
		/// </summary>
		public event HeaderMouseEventHandler HeaderMouseUp;

		/// <summary>
		/// Occurs when a mouse pointer is moved over a Column Header
		/// </summary>
		public event HeaderMouseEventHandler HeaderMouseMove;

		/// <summary>
		/// Occurs when the mouse pointer hovers over a Column Header
		/// </summary>
		public event HeaderMouseEventHandler HeaderMouseHover;

		/// <summary>
		/// Occurs when a Column Header is clicked
		/// </summary>
		public event HeaderMouseEventHandler HeaderClick;

		/// <summary>
		/// Occurs when a Column Header is double-clicked
		/// </summary>
		public event HeaderMouseEventHandler HeaderDoubleClick;

		/// <summary>
		/// Occurs when the height of the Column Headers changes
		/// </summary>
		public event EventHandler HeaderHeightChanged;

		#endregion

		#region ColumnModel

		/// <summary>
		/// Occurs when the value of the Table's ColumnModel property changes 
		/// </summary>
		public event TableEventHandler ColumnModelChanged; // PJD TEA change

		/// <summary>
		/// Occurs when a Column is added to the ColumnModel
		/// </summary>
		public event ColumnModelEventHandler ColumnAdded;

		/// <summary>
		/// Occurs when a Column is removed from the ColumnModel
		/// </summary>
		public event ColumnModelEventHandler ColumnRemoved;

		#endregion

		#region Editing

		/// <summary>
		/// Occurs when the Table begins editing a Cell
		/// </summary>
		public event CellEditEventHandler BeginEditing;

		/// <summary>
		/// Occurs when the Table stops editing a Cell
		/// </summary>
		public event CellEditEventHandler EditingStopped;

		/// <summary>
		/// Occurs when the editing of a Cell is cancelled
		/// </summary>
		public event CellEditEventHandler EditingCancelled;

		#endregion

		#region Rows

		/// <summary>
		/// Occurs when a Cell is added to a Row
		/// </summary>
		public event RowEventHandler CellAdded;

		/// <summary>
		/// Occurs when a Cell is removed from a Row
		/// </summary>
		public event RowEventHandler CellRemoved;

		/// <summary>
		/// Occurs when the value of a Rows property changes
		/// </summary>
		public event RowEventHandler RowPropertyChanged;


		public event RowEventHandler RowDataRequestAnytime;

		#endregion

		#region Sorting

		/// <summary>
		/// Occurs when a Column is about to be sorted
		/// </summary>
		public event ColumnEventHandler BeginSort;

		/// <summary>
		/// Occurs after a Column has finished sorting
		/// </summary>
		public event ColumnEventHandler EndSort;

		#endregion

		#region Painting

        /// <summary>
        /// Occurs just after the first Paint event occurs
        /// </summary>
        public event EventHandler AfterFirstPaint;

		/// <summary>
		/// Occurs before a Cell is painted
		/// </summary>
		public event PaintCellEventHandler BeforePaintCell;

		/// <summary>
		/// Occurs after a Cell is painted
		/// </summary>
		public event PaintCellEventHandler AfterPaintCell;

		/// <summary>
		/// Occurs before a Column header is painted
		/// </summary>
		public event PaintHeaderEventHandler BeforePaintHeader;

		/// <summary>
		/// Occurs after a Column header is painted
		/// </summary>
		public event PaintHeaderEventHandler AfterPaintHeader;

		#endregion

		#region TableModel

		/// <summary>
		/// Occurs when the value of the Table's TableModel property changes 
		/// </summary>
		public event TableEventHandler TableModelChanged; // PJD TEA change

		/// <summary>
		/// Occurs when a Row is added into the TableModel
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

        public event TableModelEventHandler RowAddedAnytime;
        public event TableModelEventHandler RowRemovedAnytime;


		#endregion

        #region Tooltips
        /// <summary>
        /// Occurs before a cell tooltip is shown.
        /// </summary>
        public event CellToolTipEventHandler CellToolTipPopup;

        /// <summary>
        /// Occurs before a header tooltip is shown.
        /// </summary>
        public event HeaderToolTipEventHandler HeaderToolTipPopup;
        #endregion

        #region DragDrop

        /// <summary>
        /// Occurs when a DragDrop operation contains an unhandled data type.
        /// This should return the required DragDropEffects for the external
        /// type, it is called from the internally handled DragEnter and DragOver
        /// functions.
        /// </summary>
        public event DragDropExternalTypeEffectsHandler DragDropExternalTypeEffect;

        /// <summary>
        /// Occurs when a DragDrop operation contains an unhandled data type.
        /// This should be used to handle the DragDrop functionality for the
        /// external type.
        /// </summary>
        public event DragDropExternalTypeEventHandler DragDropExternalTypeEvent;

        /// <summary>
        /// Occurs following an internally handled row insertion during DragDrop.
        /// It supplies the index of the inserted row.
        /// NOTE this is not trigger if DragDropExternalTypeEvent is triggered.
        /// </summary>
        public event DragDropRowInsertedAtEventHandler DragDropRowInsertedAtEvent;

        /// <summary>
        /// Occurs following an internally handled row move operation during DragDrop.
        /// It supplies the source and destination indexes of the moved row.
        /// NOTE this is not trigger if DragDropExternalTypeEvent is triggered.
        /// </summary>
        public event DragDropRowMovedEventHandler DragDropRowMovedEvent;

        #endregion

        #endregion

        #region Class Data

        /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        /// <summary>
        /// Is false until the first Paint event has been processed
        /// </summary>
        bool painted = false;

		#region Border

		/// <summary>
		/// The style of the Table's border
		/// </summary>
		private BorderStyle borderStyle;

		/// <summary>
		/// The colour of the Table's border
		/// </summary>
		private Color borderColor;

		/// <summary>
		/// The colour of the Table's border when the table does not have the focus
		/// </summary>
		private Color unfocusedBorderColor;
		#endregion

		#region Cells

		/// <summary>
		/// The last known cell position that the mouse was over
		/// </summary>
		private CellPos lastMouseCell;

		/// <summary>
		/// The last known cell position that the mouse's left 
		/// button was pressed in
		/// </summary>
		private CellPos lastMouseDownCell;

		/// <summary>
		/// The position of the Cell that currently has focus
		/// </summary>
		private CellPos focusedCell;

		/// <summary>
		/// The Cell that is currently being edited
		/// </summary>
		private CellPos editingCell;

		/// <summary>
		/// The ICellEditor that is currently being used to edit a Cell
		/// </summary>
		private ICellEditor curentCellEditor;

		/// <summary>
		/// The action that must be performed on a Cell to start editing
		/// </summary>
		private EditStartAction editStartAction;

		/// <summary>
		/// The key that must be pressed for editing to start when 
		/// editStartAction is set to EditStartAction.CustomKey
		/// </summary>
		private Keys customEditKey;

		/// <summary>
		/// The amount of time (in milliseconds) that that the 
		/// mouse pointer must hover over a Cell or Column Header before 
		/// a MouseHover event is raised
		/// </summary>
		private int hoverTime;

		/// <summary>
		/// A TRACKMOUSEEVENT used to set the hoverTime
		/// </summary>
		private TRACKMOUSEEVENT trackMouseEvent;

        /// <summary>
        /// Stop the beep when Enter or Escape keys are pressed when editing
        /// </summary>
        private bool suppressEditorTerminatorBeep;
		#endregion

		#region Columns

		/// <summary>
		/// The ColumnModel of the Table
		/// </summary>
		private ColumnModel columnModel;

		/// <summary>
		/// Whether the Table supports column resizing
		/// </summary>
		private bool columnResizing;

		/// <summary>
		/// The index of the column currently being resized
		/// </summary>
		private int resizingColumnIndex;

		/// <summary>
		/// The x coordinate of the currently resizing column
		/// </summary>
		private int resizingColumnAnchor;

		/// <summary>
		/// The horizontal distance between the resize starting
		/// point and the right edge of the resizing column
		/// </summary>
		private int resizingColumnOffset;

		/// <summary>
		/// The width that the resizing column will be set to 
		/// once column resizing is finished
		/// </summary>
		private int resizingColumnWidth;

		/// <summary>
		/// The index of the current pressed column
		/// </summary>
		private int pressedColumn;

		/// <summary>
		/// The index of the current "hot" column
		/// </summary>
		private int hotColumn;

		/// <summary>
		/// The index of the last sorted column
		/// </summary>
		private int lastSortedColumn;

		/// <summary>
		/// The Color of a sorted Column's background
		/// </summary>
		private Color sortedColumnBackColor;

		#endregion

		#region Grid

		/// <summary>
		/// Indicates whether grid lines appear between the rows and columns 
		/// containing the rows and cells in the Table
		/// </summary>
		private GridLines gridLines;

		/// <summary>
		/// The color of the grid lines
		/// </summary>
		private Color gridColor;

		/// <summary>
		/// The line style of the grid lines
		/// </summary>
		private GridLineStyle gridLineStyle;

		#endregion

		#region Header

		/// <summary>
		/// The styles of the column headers 
		/// </summary>
		private ColumnHeaderStyle headerStyle;

        /// <summary>
        /// Should the header text use the column alignment
        /// </summary>
        private bool headerAlignWithColumn;

		/// <summary>
		/// The Renderer used to paint the column headers
		/// </summary>
		private IHeaderRenderer headerRenderer;

		/// <summary>
		/// The font used to draw the text in the column header
		/// </summary>
		private Font headerFont;

		/// <summary>
		/// The ContextMenu for the column headers
		/// </summary>
		private HeaderContextMenu headerContextMenu;

        private bool includeHeaderInAutoWidth;

		#endregion

		#region Items

		/// <summary>
		/// The TableModel of the Table
		/// </summary>
		private TableModel tableModel;

		#endregion

		#region Scrollbars

		/// <summary>
		/// Indicates whether the Table will allow the user to scroll to any 
		/// columns or rows placed outside of its visible boundaries
		/// </summary>
		private bool scrollable;

		/// <summary>
		/// The Table's horizontal ScrollBar
		/// </summary>
		private HScrollBar hScrollBar;

		/// <summary>
		/// The Table's vertical ScrollBar. The Value property of this scrollbar is not the index of the
		/// topmost row, but the index of the topmost *visible* row.
		/// </summary>
		private VScrollBar vScrollBar;

		/// <summary>
		/// Holds the index of the topmost row.
		/// </summary>
		private int topIndex;

		/// <summary>
		/// Holds the VScroll.Value property. Used to compare with the new VScroll.Value in the
		/// ValueChanged event.
		/// </summary>
		private int lastVScrollValue;

		#endregion

		#region Selection

		/// <summary>
		/// Specifies whether rows and cells can be selected
		/// </summary>
		private bool allowSelection;

		/// <summary>
		/// Specifies whether rows and cells can be selected by right mouse button (RMB)
		/// </summary>
		private bool allowRMBSelection;

		/// <summary>
		/// Specifies whether multiple rows and cells can be selected
		/// </summary>
        private bool multiSelect;

        /// <summary>
        /// Specifies whether all rows in the family are selected (i.e. parent, children and siblings)
        /// </summary>
        private bool familyRowSelect;

		/// <summary>
		/// Specifies whether clicking a row selects all its cells
		/// </summary>
		private bool fullRowSelect;

		/// <summary>
		/// Specifies whether the selected rows and cells in the Table remain 
		/// highlighted when the Table loses focus
		/// </summary>
		private bool hideSelection;

		/// <summary>
		/// The background color of selected rows and cells
		/// </summary>
		private Color selectionBackColor;

		/// <summary>
		/// The foreground color of selected rows and cells
		/// </summary>
		private Color selectionForeColor;

		/// <summary>
		/// The background color of selected rows and cells when the Table 
		/// doesn't have focus
		/// </summary>
		private Color unfocusedSelectionBackColor;

		/// <summary>
		/// The foreground color of selected rows and cells when the Table 
		/// doesn't have focus
		/// </summary>
		private Color unfocusedSelectionForeColor;

		/// <summary>
		/// Determines how selected Cells are hilighted
		/// </summary>
		private SelectionStyle selectionStyle;

		#endregion

		#region Sorting

		private SortType theSortType = SortType.AutoSort;
		private bool theStableSort = true;

		#endregion

		#region Table

		/// <summary>
		/// The state of the table
		/// </summary>
		private TableState tableState;

		/// <summary>
		/// Is the Table currently initialising
		/// </summary>
		private bool init;

		/// <summary>
		/// The number of times BeginUpdate has been called
		/// </summary>
		private int beginUpdateCount;

		/// <summary>
		/// The ToolTip used by the Table to display cell and column tooltips
		/// </summary>
		private ToolTip toolTip;

		/// <summary>
		/// The alternating row background color
		/// </summary>
		private Color alternatingRowColor;

		// Mateusz [PEYN] Adamus (peyn@tlen.pl)
		// span of alternate rows - default 1
		/// <summary>
		/// The span of alternate rows
		/// </summary>
		private int alternatingRowSpan;

        /// <summary>
        /// A value indicating whether all row heights are recalculated after an EndUpdate (only used if WordWrapping is on).
        /// </summary>
        private bool autoCalculateRowHeights;

		/// <summary>
		/// The text displayed in the Table when it has no data to display
		/// </summary>
		private string noItemsText;

		/// <summary>
		/// Specifies whether the Table is being used as a preview Table 
		/// in a ColumnColection editor
		/// </summary>
		private bool preview;

		/*/// <summary>
		/// Specifies whether pressing the Tab key while editing moves the 
		/// editor to the next available cell
		/// </summary>
		private bool tabMovesEditor;*/

		/// <summary>
		/// Specifies whether show selection in grid or not
		/// </summary>
		private bool showSelectionRectangle;

		#endregion

		#region Word wrapping
		/// <summary>
		/// Specifies whether any cells are allowed to word-wrap.
		/// </summary>
		private bool enableWordWrap;
		#endregion

        #region Subrows

        private int _totalHiddenSubRows;
        private int _hiddenSubRowsAboveTop;
        
        /// <summary>
        /// Gets the total number of subrows that are currently not expanded.
        /// </summary>
        public int HiddenSubRows
        {
            get { return _totalHiddenSubRows; }
            set { _totalHiddenSubRows = value; }
        }

        public int HiddenSubRowsAboveTop
        {
            get { return _hiddenSubRowsAboveTop; }
            set { _hiddenSubRowsAboveTop = value; }
        }

        /// <summary>
        /// Count the number of hidden rows before the supplied row.
        /// </summary>
        /// <param name="row">The row to count up to.</param>
        /// <returns>The number of hidden rows.</returns>
        internal int HiddenRowCountBefore(int row)
        {
            int result = 0;

            int start = 0;

            if (row >= this.TopIndex)
            {
                result += HiddenSubRowsAboveTop;
                start = this.TopIndex;
            }
            else if (row > this.TopIndex / 2)
            {
                start = row;
                row = this.TopIndex;
                result -= HiddenSubRowsAboveTop;
            }

            var rows = this.TableModel.Rows;
            for (int i = start; i < row; i++)
            {
                var rowc = rows[i];
                if (rowc == null || (rowc.Parent != null && !rowc.Parent.ExpandSubRows))
                {
                    result += 1;
                }
            }

            return Math.Abs(result);
        }

        #endregion

        /// <summary>
        /// Helper class that provides all drag drop functionality.
        /// </summary>
        DragDropHelper _dragDropHelper;
        private bool useBuiltInDragDrop = true;
        private bool externalDropRemovesRows = true;

        #endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the Table class with default settings
		/// </summary>
		public Table()
		{
            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer, true);
			// starting setup
			this.init = true;

			// This call is required by the Windows.Forms Form Designer.
			components = new System.ComponentModel.Container();

			//
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.ResizeRedraw, true);
			this.SetStyle(ControlStyles.Selectable, true);
			this.TabStop = true;

			this.Size = new Size(150, 150);

			this.BackColor = Color.White;

			//
			this.columnModel = null;
			this.tableModel = null;

			// header
			this.headerStyle = ColumnHeaderStyle.Clickable;
            this.headerAlignWithColumn = false;
			this.headerFont = this.Font;
			this.headerRenderer = new XPHeaderRenderer();
			//this.headerRenderer = new GradientHeaderRenderer();
			//this.headerRenderer = new FlatHeaderRenderer();
			this.headerRenderer.Font = this.headerFont;
			this.headerContextMenu = new HeaderContextMenu();
            this.includeHeaderInAutoWidth = true;

			this.columnResizing = true;
			this.resizingColumnIndex = -1;
			this.resizingColumnWidth = -1;
			this.hotColumn = -1;
			this.pressedColumn = -1;
			this.lastSortedColumn = -1;
			this.sortedColumnBackColor = Color.WhiteSmoke;

			// borders
			this.borderStyle = BorderStyle.Fixed3D;
			this.borderColor = Color.Black;
			this.unfocusedBorderColor = Color.Black;

			// scrolling
			this.scrollable = true;

			this.hScrollBar = new HScrollBar();
			this.hScrollBar.Visible = false;
			this.hScrollBar.Location = new Point(this.BorderWidth, this.Height - this.BorderWidth - SystemInformation.HorizontalScrollBarHeight);
			this.hScrollBar.Width = this.Width - (this.BorderWidth * 2) - SystemInformation.VerticalScrollBarWidth;
			this.hScrollBar.Scroll += new ScrollEventHandler(this.OnHorizontalScroll);
			this.Controls.Add(this.hScrollBar);

			this.vScrollBar = new VScrollBar();
			this.vScrollBar.Visible = false;
			this.vScrollBar.Location = new Point(this.Width - this.BorderWidth - SystemInformation.VerticalScrollBarWidth, this.BorderWidth);
			this.vScrollBar.Height = this.Height - (this.BorderWidth * 2) - SystemInformation.HorizontalScrollBarHeight;
			this.vScrollBar.Scroll += new ScrollEventHandler(this.OnVerticalScroll);
			this.vScrollBar.ValueChanged += new EventHandler(vScrollBar_ValueChanged);
			this.Controls.Add(this.vScrollBar);

			//
			this.gridLines = GridLines.None; ;
			this.gridColor = SystemColors.Control;
			this.gridLineStyle = GridLineStyle.Solid;

			this.allowSelection = true;
			this.allowRMBSelection = false;
			this.multiSelect = false;
			this.fullRowSelect = false;
			this.hideSelection = false;
			this.selectionBackColor = SystemColors.Highlight;
			this.selectionForeColor = SystemColors.HighlightText;
			this.unfocusedSelectionBackColor = SystemColors.Control;
			this.unfocusedSelectionForeColor = SystemColors.ControlText;
			this.selectionStyle = SelectionStyle.ListView;
			this.alternatingRowColor = Color.Transparent;
			this.alternatingRowSpan = 1;

			// current table state
			this.tableState = TableState.Normal;

			this.lastMouseCell = new CellPos(-1, -1);
			this.lastMouseDownCell = new CellPos(-1, -1);
			this.focusedCell = new CellPos(-1, -1);
			this.hoverTime = 1000;
			this.trackMouseEvent = null;
			this.ResetMouseEventArgs();

			this.toolTip = new ToolTip(this.components);
			this.toolTip.Active = false;
			this.toolTip.InitialDelay = 1000;

			this.noItemsText = "There are no items in this view";

			this.editingCell = new CellPos(-1, -1);
			this.curentCellEditor = null;
			this.editStartAction = EditStartAction.DoubleClick;
			this.customEditKey = Keys.F5;
			//this.tabMovesEditor = true;

			// showSelectionRectangle defaults to true
			this.showSelectionRectangle = true;

            // drang and drop
            _dragDropHelper = new DragDropHelper(this);
            _dragDropHelper.DragDropRenderer = new DragDropRenderer();

			// for data binding
			listChangedHandler = new ListChangedEventHandler(dataManager_ListChanged);
			positionChangedHandler = new EventHandler(dataManager_PositionChanged);
			dataSourceColumnBinder = new DataSourceColumnBinder();

			// finished setting up
			this.beginUpdateCount = 0;
			this.init = false;
			this.preview = false;
		}
		#endregion

		#region Methods

		#region Coordinate Translation

        /*/// <summary>
        /// Computes the height of the rows that are not visible (i.e. above the top row currently displayed).
        /// </summary>
        /// <returns></returns>
        private int VScrollOffset()
        {
            return VScrollOffsetCache;
        }*/

		#region ClientToDisplayRect
		/// <summary>
		/// Computes the location of the specified client point into coordinates 
		/// relative to the display rectangle
		/// </summary>
		/// <param name="x">The client x coordinate to convert</param>
		/// <param name="y">The client y coordinate to convert</param>
		/// <returns>A Point that represents the converted coordinates (x, y), 
		/// relative to the display rectangle</returns>
		public Point ClientToDisplayRect(int x, int y)
		{
			int xPos = x - this.BorderWidth;

	        if (this.HScroll)
		        xPos += this.hScrollBar.Value;

			int yPos = y - this.BorderWidth;

            //HACK? - not used anyways
			//if (this.VScroll)
                //yPos += this.VScrollOffset();

			return new Point(xPos, yPos);
		}


		/// <summary>
		/// Computes the location of the specified client point into coordinates 
		/// relative to the display rectangle
		/// </summary>
		/// <param name="p">The client coordinate Point to convert</param>
		/// <returns>A Point that represents the converted Point, p, 
		/// relative to the display rectangle</returns>
		public Point ClientToDisplayRect(Point p)
		{
			return this.ClientToDisplayRect(p.X, p.Y);
		}


		/// <summary>
		/// Converts the location of the specified Rectangle into coordinates 
		/// relative to the display rectangle
		/// </summary>
		/// <param name="rect">The Rectangle to convert whose location is in 
		/// client coordinates</param>
		/// <returns>A Rectangle that represents the converted Rectangle, rect, 
		/// relative to the display rectangle</returns>
		public Rectangle ClientToDisplayRect(Rectangle rect)
		{
			return new Rectangle(this.ClientToDisplayRect(rect.Location), rect.Size);
		}
		#endregion

		#region DisplayRectToClient
		/// <summary>
		/// Computes the location of the specified point relative to the display 
		/// rectangle point into client coordinates 
		/// </summary>
		/// <param name="x">The x coordinate to convert relative to the display rectangle</param>
		/// <param name="y">The y coordinate to convert relative to the display rectangle</param>
		/// <returns>A Point that represents the converted coordinates (x, y) relative to 
		/// the display rectangle in client coordinates</returns>
		public Point DisplayRectToClient(int x, int y)
		{
			int xPos = x + this.BorderWidth;

			if (this.HScroll)
				xPos -= this.hScrollBar.Value;

			int yPos = y + this.BorderWidth;

            //HACK?
			//if (this.VScroll)
                //yPos -= this.VScrollOffset();

			return new Point(xPos, yPos);
		}


		/// <summary>
		/// Computes the location of the specified point relative to the display 
		/// rectangle into client coordinates 
		/// </summary>
		/// <param name="p">The point relative to the display rectangle to convert</param>
		/// <returns>A Point that represents the converted Point relative to 
		/// the display rectangle, p, in client coordinates</returns>
		public Point DisplayRectToClient(Point p)
		{
			return this.DisplayRectToClient(p.X, p.Y);
		}


		/// <summary>
		/// Converts the location of the specified Rectangle relative to the display 
		/// rectangle into client coordinates 
		/// </summary>
		/// <param name="rect">The Rectangle to convert whose location is relative to 
		/// the display rectangle</param>
		/// <returns>A Rectangle that represents the converted Rectangle relative to 
		/// the display rectangle, rect, in client coordinates</returns>
		public Rectangle DisplayRectToClient(Rectangle rect)
		{
			return new Rectangle(this.DisplayRectToClient(rect.Location), rect.Size);
		}
		#endregion

		#region Cells
		/// <summary>
		/// Returns the Cell at the specified client coordinates
		/// </summary>
		/// <param name="x">The client x coordinate of the Cell</param>
		/// <param name="y">The client y coordinate of the Cell</param>
		/// <returns>The Cell at the specified client coordinates, or
		/// null if it does not exist</returns>
		public Cell CellAt(int x, int y)
		{
			int row = this.RowIndexAtClient(x, y);
			int column = this.ColumnIndexAt(x, y);

			// return null if the row or column don't exist
			if (row == -1 || row >= this.TableModel.Rows.Count || column == -1 || column >= this.TableModel.Rows[row].Cells.Count)
				return null;

			return this.TableModel[row, column];
		}

		/// <summary>
		/// Returns the Cell at the specified client Point
		/// </summary>
		/// <param name="p">The point of interest</param>
		/// <returns>The Cell at the specified client Point, 
		/// or null if not found</returns>
		public Cell CellAt(Point p)
		{
			return this.CellAt(p.X, p.Y);
		}

		/// <summary>
		/// Returns a Rectangle that specifies the size and location the cell at 
		/// the specified row and column indexes in client coordinates
		/// </summary>
		/// <param name="row">The index of the row that contains the cell</param>
		/// <param name="column">The index of the column that contains the cell</param>
		/// <returns>A Rectangle that specifies the size and location the cell at 
		/// the specified row and column indexes in client coordinates</returns>
		public Rectangle CellRect(int row, int column)
		{
			// return null if the row or column don't exist
			if (row == -1 || row >= this.TableModel.Rows.Count || column == -1 || column >= this.TableModel.Rows[row].Cells.Count)
				return Rectangle.Empty;

			Rectangle columnRect = this.ColumnRect(column);

			if (columnRect == Rectangle.Empty)
				return columnRect;

			Rectangle rowRect = this.RowRect(row);

			if (rowRect == Rectangle.Empty)
				return rowRect;

            int width = columnRect.Width;
            Cell thisCell = this.TableModel[row, column];
            if (thisCell != null && thisCell.ColSpan > 1)
            {
                width = this.GetColumnWidth(column, thisCell);
            }

			return new Rectangle(columnRect.X, rowRect.Y, width, rowRect.Height);
		}

		/// <summary>
		/// Returns a Rectangle that specifies the size and location the cell at 
		/// the specified cell position in client coordinates
		/// </summary>
		/// <param name="cellPos">The position of the cell</param>
		/// <returns>A Rectangle that specifies the size and location the cell at 
		/// the specified cell position in client coordinates</returns>
		public Rectangle CellRect(CellPos cellPos)
		{
			return this.CellRect(cellPos.Row, cellPos.Column);
		}


		/// <summary>
		///  Returns a Rectangle that specifies the size and location of the 
		///  specified cell in client coordinates
		/// </summary>
		/// <param name="cell">The cell whose bounding rectangle is to be retrieved</param>
		/// <returns>A Rectangle that specifies the size and location the specified 
		/// cell in client coordinates</returns>
		public Rectangle CellRect(Cell cell)
		{
			if (cell == null || cell.Row == null || cell.InternalIndex == -1)
				return Rectangle.Empty;

			if (this.TableModel == null || this.ColumnModel == null)
				return Rectangle.Empty;

			int row = this.TableModel.Rows.IndexOf(cell.Row);
			int col = cell.InternalIndex;

			return this.CellRect(row, col);
		}

		/// <summary>
		/// Returns the position of the actual cell that renders to the given cell pos.
		/// This looks at colspans and returns the cell that colspan overs the given cell (if any)
		/// </summary>
		/// <param name="cellPos"></param>
		/// <returns></returns>
		protected internal CellPos ResolveColspan(CellPos cellPos)
		{
            Row r = null;
            if (cellPos.Row > -1)
                r = this.TableModel.Rows[cellPos.Row];
            if (r == null)
                return cellPos;
            else
			    return new CellPos(cellPos.Row, r.GetRenderedCellIndex(cellPos.Column));
		}

		/// <summary>
		/// Returns whether Cell at the specified row and column indexes 
		/// is not null
		/// </summary>
		/// <param name="row">The row index of the cell</param>
		/// <param name="column">The column index of the cell</param>
		/// <returns>True if the cell at the specified row and column indexes 
		/// is not null, otherwise false</returns>
		protected internal bool IsValidCell(int row, int column)
		{
			if (this.TableModel != null && this.ColumnModel != null)
			{
				if (row >= 0 && row < this.TableModel.Rows.Count)
				{
					if (column >= 0 && column < this.ColumnModel.Columns.Count)
					{
						return (this.TableModel.Rows[row].Cells[column] != null);
					}
				}
			}

			return false;
		}


		/// <summary>
		/// Returns whether Cell at the specified cell position is not null
		/// </summary>
		/// <param name="cellPos">The position of the cell</param>
		/// <returns>True if the cell at the specified cell position is not 
		/// null, otherwise false</returns>
		protected internal bool IsValidCell(CellPos cellPos)
		{
			return this.IsValidCell(cellPos.Row, cellPos.Column);
		}


        /// <summary>
        /// Returns a CellPos that specifies the next Cell that is visible
        /// and enabled from the specified Cell
        /// </summary>
        /// <param name="start">A CellPos that specifies the Cell to start
        /// searching from</param>
        /// <param name="wrap">Specifies whether to move to the start of the
        /// next Row when the end of the current Row is reached</param>
        /// <param name="forward">Specifies whether the search should travel
        /// in a forward direction (top to bottom, left to right) through the Cells</param>
        /// <param name="includeStart">Indicates whether the specified starting
        /// Cell is included in the search</param>
        /// <param name="checkOtherCellsInRow">Specifies whether all Cells in
        /// the Row should be included in the search</param>
        /// <param name="includeDisabledCells">Indicates whether disabled cells should be included in the search.</param>
        /// <returns>
        /// A CellPos that specifies the next Cell that is visible
        /// and enabled, or CellPos.Empty if there are no Cells that are visible
        /// and enabled
        /// </returns>
		protected CellPos FindNextVisibleCell(CellPos start, bool wrap, bool forward, bool includeStart, bool checkOtherCellsInRow, bool includeDisabledCells)
		{
			if (this.ColumnCount == 0 || this.RowCount == 0)
			{
				return CellPos.Empty;
			}

			int startRow = (start.Row != -1) ? start.Row : 0;
			int startCol = (start.Column != -1) ? start.Column : 0;

			bool first = true;

			if (forward)
			{
				for (int i = startRow; i < this.RowCount; i++)
				{
					int j = ((first || !checkOtherCellsInRow) ? startCol : 0);

					for (; j < this.TableModel.Rows[i].Cells.Count; j++)
					{
						if (i == startRow && j == startCol)
						{
							if (!first)
							{
								return CellPos.Empty;
							}

							first = false;

							if (!includeStart)
							{
								if (!checkOtherCellsInRow)
								{
									break;
								}

								continue;
							}
						}

                        if (IsCellVisible(i, j, includeDisabledCells))
						{
							return new CellPos(i, j);
						}

						if (!checkOtherCellsInRow)
						{
							continue;
						}
					}

					if (wrap)
					{
						if (i + 1 == this.TableModel.Rows.Count)
						{
							i = -1;
						}
					}
					else
					{
						break;
					}
				}
			}
			else
			{
				for (int i = startRow; i >= 0; i--)
				{
					int j = ((first || !checkOtherCellsInRow) ? startCol : this.TableModel.Rows[i].Cells.Count);

					for (; j >= 0; j--)
					{
						if (i == startRow && j == startCol)
						{
							if (!first)
							{
								return CellPos.Empty;
							}

							first = false;

							if (!includeStart)
							{
								if (!checkOtherCellsInRow)
								{
									break;
								}

								continue;
							}
						}

                        if (IsCellVisible(i, j, includeDisabledCells))
                        {
                            return new CellPos(i, j);
                        }

						if (!checkOtherCellsInRow)
						{
							continue;
						}
					}

					if (wrap)
					{
						if (i - 1 == -1)
						{
							i = this.TableModel.Rows.Count;
						}
					}
					else
					{
						break;
					}
				}
			}

			return CellPos.Empty;
		}


        private bool IsCellVisible(int row, int column, bool includeDisabledCells)
        {
            bool ok = (this.IsValidCell(row, column) &&
                this.IsValidColumn(column) &&
                (this.TableModel[row, column].Enabled || includeDisabledCells) &&
                this.ColumnModel.Columns[column].Enabled &&
                this.ColumnModel.Columns[column].Visible);

            if (ok)
            {
                // If this cell belongs to a row that is in fact a sub row, and this subrow is hidden, then return false.
                Cell cell = this.TableModel[row, column];
                if (cell.Row.Parent != null)
                {
                    ok = cell.Row.Parent.ExpandSubRows;
                }
            }

            return ok;
        }

		/// <summary>
		/// Returns a CellPos that specifies the next Cell that able to be 
		/// edited from the specified Cell
		/// </summary>
		/// <param name="start">A CellPos that specifies the Cell to start 
		/// searching from</param>
		/// <param name="wrap">Specifies whether to move to the start of the 
		/// next Row when the end of the current Row is reached</param>
		/// <param name="forward">Specifies whether the search should travel 
		/// in a forward direction (top to bottom, left to right) through the Cells</param>
		/// <param name="includeStart">Indicates whether the specified starting 
		/// Cell is included in the search</param>
		/// <returns>A CellPos that specifies the next Cell that is able to
		/// be edited, or CellPos.Empty if there are no Cells that editable</returns>
		protected CellPos FindNextEditableCell(CellPos start, bool wrap, bool forward, bool includeStart)
		{
			if (this.ColumnCount == 0 || this.RowCount == 0)
			{
				return CellPos.Empty;
			}

			int startRow = start.Row != -1 ? start.Row : 0;
			int startCol = start.Column != -1 ? start.Column : 0;

			bool first = true;

			if (forward)
			{
				for (int i = startRow; i < this.RowCount; i++)
				{
					int j = (first ? startCol : 0);

					for (; j < this.TableModel.Rows[i].Cells.Count; j++)
					{
						if (i == startRow && j == startCol)
						{
							if (!first)
							{
								return CellPos.Empty;
							}

							first = false;

							if (!includeStart)
							{
								continue;
							}
						}

						if (this.IsValidCell(i, j) && this.IsValidColumn(j) && this.TableModel[i, j].Editable && this.ColumnModel.Columns[j].Editable)
						{
							return new CellPos(i, j);
						}
					}

					if (wrap)
					{
						if (i + 1 == this.TableModel.Rows.Count)
						{
							i = -1;
						}
					}
					else
					{
						break;
					}
				}
			}
			else
			{
				for (int i = startRow; i >= 0; i--)
				{
					int j = (first ? startCol : this.TableModel.Rows[i].Cells.Count);

					for (; j >= 0; j--)
					{
						if (i == startRow && j == startCol)
						{
							if (!first)
							{
								return CellPos.Empty;
							}

							first = false;

							if (!includeStart)
							{
								continue;
							}
						}

						if (this.IsValidCell(i, j) && this.IsValidColumn(j) && this.TableModel[i, j].Editable && this.ColumnModel.Columns[j].Editable)
						{
							return new CellPos(i, j);
						}
					}

					if (wrap)
					{
						if (i - 1 == -1)
						{
							i = this.TableModel.Rows.Count;
						}
					}
					else
					{
						break;
					}
				}
			}

			return CellPos.Empty;
		}

		#endregion

		#region Columns

		/// <summary>
		/// Returns the index of the Column at the specified client coordinates
		/// </summary>
		/// <param name="x">The client x coordinate of the Column</param>
		/// <param name="y">The client y coordinate of the Column</param>
		/// <returns>The index of the Column at the specified client coordinates, or
		/// -1 if it does not exist</returns>
		public int ColumnIndexAt(int x, int y)
		{
			if (this.ColumnModel == null)
			{
				return -1;
			}

			// convert to DisplayRect coordinates before 
			// sending to the ColumnModel
			return this.ColumnModel.ColumnIndexAtX(this.hScrollBar.Value + x - this.BorderWidth);
		}


		/// <summary>
		/// Returns the index of the Column at the specified client point
		/// </summary>
		/// <param name="p">The point of interest</param>
		/// <returns>The index of the Column at the specified client point, or
		/// -1 if it does not exist</returns>
		public int ColumnIndexAt(Point p)
		{
			return this.ColumnIndexAt(p.X, p.Y);
		}


		/// <summary>
		/// Returns the bounding rectangle of the specified 
		/// column's header in client coordinates
		/// </summary>
		/// <param name="column">The index of the column</param>
		/// <returns>The bounding rectangle of the specified 
		/// column's header</returns>
		public Rectangle ColumnHeaderRect(int column)
		{
			if (this.ColumnModel == null)
			{
				return Rectangle.Empty;
			}

			Rectangle rect = this.ColumnModel.ColumnHeaderRect(column);

			if (rect == Rectangle.Empty)
			{
				return rect;
			}

			rect.X -= this.hScrollBar.Value - this.BorderWidth;
			rect.Y = this.BorderWidth;

			return rect;
		}


		/// <summary>
		/// Returns the bounding rectangle of the specified 
		/// column's header in client coordinates
		/// </summary>
		/// <param name="column">The column</param>
		/// <returns>The bounding rectangle of the specified 
		/// column's header</returns>
		public Rectangle ColumnHeaderRect(Column column)
		{
			if (this.ColumnModel == null)
			{
				return Rectangle.Empty;
			}

			return this.ColumnHeaderRect(this.ColumnModel.Columns.IndexOf(column));
		}


		/// <summary>
		/// Returns the bounding rectangle of the column at the 
		/// specified index in client coordinates
		/// </summary>
		/// <param name="column">The column</param>
		/// <returns>The bounding rectangle of the column at the 
		/// specified index</returns>
		public Rectangle ColumnRect(int column)
		{
			if (this.ColumnModel == null)
			{
				return Rectangle.Empty;
			}

			Rectangle rect = this.ColumnHeaderRect(column);

			if (rect == Rectangle.Empty)
			{
				return rect;
			}

			rect.Y += this.HeaderHeight;
            rect.Height = this.PseudoClientRect.Height - this.HeaderHeight;

			return rect;
		}

		/// <summary>
		/// Returns the actual width that this cell can render over (taking colspan into account).
		/// Normally its just the width of this column from the column model.
		/// </summary>
		/// <param name="column"></param>
		/// <param name="cell"></param>
		/// <returns></returns>
		private int GetColumnWidth(int column, Cell cell)
		{
			int width = this.ColumnModel.Columns[column].Width;

			if (cell.ColSpan > 1)
			{
				// Just in case the colspan goes over the end of the table
				int maxcolindex = Math.Min(cell.ColSpan + column - 1, this.ColumnModel.Columns.Count - 1);

				for (int i = column + 1; i <= maxcolindex; i++)
				{
					width += this.ColumnModel.Columns[i].Width;
				}
			}

			return width;
		}

		/// <summary>
		/// Returns the left position of the given column.
		/// </summary>
		/// <param name="column"></param>
		/// <returns></returns>
		private int GetColumnLeft(int column)
		{
			return this.ColumnRect(column).Left;
		}

        /// <summary>
        /// Resizes all columns to their minimum width that still shows all the cells content.
        /// </summary>
        public void AutoResizeColumnWidths()
        {
            for (int i = 0; i < this.ColumnModel.Columns.Count; i++)
            {
                Column c = this.ColumnModel.Columns[i];
                ColumnEventArgs args = new ColumnEventArgs(c, i, ColumnEventType.WidthChanged, c.Width);
                OnColumnAutoResize(args);
            }
        }

        /// <summary>
        /// Returns the minimum column width that will show all the columns contents. Returns 0
        /// if the column width should not be changed, due to the resize mode.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private int GetAutoColumnWidth(int column)
        {
            RowCollectionForTable rows = this.TableModel.Rows;
            int maxwidth = 0;
            Column c = this.ColumnModel.Columns[column];

            if (this.includeHeaderInAutoWidth)
            {
                maxwidth = c.ContentWidth;
            }

            for (int i = 0; i < rows.Count; i++)
            {
                // Don't count this row if it is currently a hidden subrow
                Row row = rows[i];
                if (row != null && (row.Parent == null || row.Parent.ExpandSubRows) && (row.Cells.Count > column))
                {
                    int w = row.Cells[column].ContentWidth;
                    if (w > maxwidth)
                        maxwidth = w;
                }
            }

            int changedMax = GetAutoColumnWidthWithMode(c, maxwidth);
            return changedMax;
        }

        /// <summary>
        /// Returns the new column width if the columns resize mode allows it to be changed.
        /// Returns 0 if it should not be changed.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="maxwidth"></param>
        /// <returns></returns>
        int GetAutoColumnWidthWithMode(Column column, int maxwidth)
        {
            int changedWidth = 0;
            int oldwidth = column.Width;

            switch (column.AutoResizeMode)
            {
                case ColumnAutoResizeMode.Any:
                    // Always allow the change
                    changedWidth = maxwidth;
                    break;
                case ColumnAutoResizeMode.Shrink:
                    // Only allowed if the new width is smaller
                    if (maxwidth < oldwidth)
                        changedWidth = maxwidth;
                    break;
                case ColumnAutoResizeMode.Grow:
                    // Only allowed if the new width is greater
                    if (maxwidth > oldwidth)
                        changedWidth = maxwidth;
                    break;
            }

            return changedWidth;
        }
		#endregion

		#region Rows

        private int _oldFirstVisibleRow = 0;
        private int _oldLastVisibleRow = 0;

        public void UpdateRowsVisibilityData()
        {
            if (this.TopIndex == -1)
                return;
            var rows = this.TableModel.Rows;
            int lastVisible = CurrentPageRowCount() + topIndex;
            var min = Math.Min(_oldFirstVisibleRow, this.TopIndex);
            var max = Math.Max(_oldLastVisibleRow, lastVisible);

            for (int i = min; i <= max; ++i)
            {
                if (rows[i] == null)
                    continue;
                if (rows[i].MarkedVisible)
                {
                    if (i < this.TopIndex || i > lastVisible || (rows[i].Parent != null && !rows[i].Parent.ExpandSubRows))
                    {
                        rows[i].OnRowBecameInvisible();
                    }
                }
                // is invisible atm
                else
                {
                    if (i >= this.TopIndex && i <= lastVisible && (rows[i].Parent == null || rows[i].Parent.ExpandSubRows))
                    {
                        rows[i].OnRowBecameVisible();
                    }
                }
            }

            _oldFirstVisibleRow = topIndex;
            _oldLastVisibleRow = lastVisible;
        }

		/// <summary>
		/// Returns the index of the Row at the specified client coordinates
		/// </summary>
		/// <param name="x">The client x coordinate of the Row</param>
		/// <param name="y">The client y coordinate of the Row</param>
		/// <returns>The index of the Row at the specified client coordinates, or
		/// -1 if it does not exist</returns>
		public int RowIndexAtClient(int x, int y)
		{
			if (this.TableModel == null)
				return -1;

			if (this.HeaderStyle != ColumnHeaderStyle.None)
				y -= this.HeaderHeight;

			y -= this.BorderWidth;

			if (y < 0)
				return -1;

            int height = 0;
            for (int i = this.TopIndex; i < this.TableModel.Rows.Count; i++)
            {
                Row row = this.TableModel.Rows[i];
                if (row == null)
                    continue;
                if (row.Parent == null || row.Parent.ExpandSubRows)
                {
                    height += row.Height;
                    if (y < height)
                        return i;
                }
            }

            return - 1;
		}

		/// <summary>
		/// Returns the index of the Row at the specified client point
		/// </summary>
		/// <param name="p">The point of interest</param>
		/// <returns>The index of the Row at the specified client point, or
		/// -1 if it does not exist</returns>
		public int RowIndexAt(Point p)
		{
			return this.RowIndexAtClient(p.X, p.Y);
		}


		/// <summary>
		/// Returns the bounding rectangle of the row at the 
		/// specified index in client coordinates
		/// </summary>
		/// <param name="row">The index of the row</param>
		/// <returns>The bounding rectangle of the row at the 
		/// specified index</returns>
		public Rectangle RowRect(int row)
		{
			if (this.TableModel == null || this.ColumnModel == null || row == -1 || row > this.TableModel.Rows.Count)
				return Rectangle.Empty;

			Rectangle rect = new Rectangle();

			rect.X = this.DisplayRectangle.X;

			rect.Y = this.BorderWidth + this.RowYDifference(this.TopIndex, row);
			rect.Height = this.TableModel.Rows[row].Height;

			rect.Width = this.ColumnModel.VisibleColumnsWidth;

			if (this.HeaderStyle != ColumnHeaderStyle.None)
				rect.Y += this.HeaderHeight;

			return rect;
		}

		/// <summary>
		/// Returns the difference in Y-coords between the tops of the two given rows. May return a negative.
		/// </summary>
		/// <param name="row1">Index of first row</param>
		/// <param name="row2">Index of second row</param>
		/// <returns>Is positive if Row2 > Row1</returns>
		public int RowYDifference(int row1, int row2)
		{
			if (row1 == row2)
				return 0;

            if (this.TableModel == null || this.TableModel.Rows == null)
                return 0;

			int r1 = Math.Min(row1, row2);
			int r2 = Math.Max(row1, row2);

			if (r2 > this.TableModel.Rows.Count)
				r2 = this.TableModel.Rows.Count;

			int ydiff = 0;

			RowCollectionForTable rows = this.TableModel.Rows;
			for (int i = r1; i < r2; i++)
			{
				// Don't count this row if it is currently a hidden subrow
				Row row = rows[i];
                if (row != null && (row.Parent == null || row.Parent.ExpandSubRows))
                    ydiff += row.Height;
			}

            if (r1 == row1)
			{
				// Row2 > Row1 so return a +ve
				return ydiff;
			}
			else
			{
				// Row2 < Row1 so return a -ve
				return -ydiff;
			}
		}

        private bool RowsCanReachHeight(int height)
        {
            RowCollectionForTable rows = this.TableModel.Rows;
            int r2 = rows.Count;
            int ydiff = 0;
            for (int i = 0; i < r2; i++)
            {
                // Don't count this row if it is currently a hidden subrow
                Row row = rows[i];
                if (row != null && (row.Parent == null || row.Parent.ExpandSubRows))
                {
                    ydiff += row.Height;
                    if (ydiff >= height)
                        return true;
                }
            }
            return false;
        }

		/// <summary>
		/// Returns the number of visible rows, determined by iterating over all visible rows.
		/// Copes with word-wrapped rows.
		/// </summary>
		/// <returns></returns>
        private int VisibleRowCountExact()
        {
            int a;
            return VisibleRowCountExact(out a);
        }
		private int VisibleRowCountExact(out int currentPageRowCount)
		{
			int ydiff = 0;
			RowCollectionForTable rows = this.TableModel.Rows;
			int visibleHeight = this.CellDataRect.Height;
            int count = 0;
            int i;
			for (i = this.TopIndex; i < rows.Count; i++)
			{
				// Don't count this row if it is currently a hidden subrow
				Row row = rows[i];
				if (row != null && (row.Parent == null || row.Parent.ExpandSubRows))
                {
                    ydiff += row.Height;

                    if (ydiff < visibleHeight)
                        count++;
                    else
                        break;
                }
            }
            currentPageRowCount = Math.Max(i - TopIndex, 0);

            return count;
		}

        private int CurrentPageRowCount()
        {
            int count;
            VisibleRowCountExact(out count);

            return count;
        }

        /// <summary>
        /// For all rows that have a wordwrap cell, calculate the rendered height.
        /// </summary>
        public void CalculateAllRowHeights()
        {
            using (Graphics g = this.CreateGraphics())
            {
                for (int i = 0; i < this.TableModel.Rows.Count; i++)
                {
                    Row row = this.TableModel.Rows[i];
                    if (row != null)
                    {
                        int h = GetRenderedRowHeight(g, row);
                        row.InternalHeight = h;
                    }
                }
            }
        }

        /// <summary>
        /// Returns the actual height for this row when rendered. If there is no word wrapped cell here then
        /// just return the default row height.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        int GetRenderedRowHeight(Graphics g, Row row)
        {
            int height = row.Height;
            if (row.HasWordWrapCell)
            {
                int column = row.WordWrapCellIndex;
                Cell varCell = this.TableModel[row.Index, column];
                if (varCell.WordWrap)
                {
                    // get the renderer for the cells column
                    ICellRenderer renderer = this.ColumnModel.Columns[column].Renderer;
                    if (renderer == null)
                    {
                        // get the default renderer for the column
                        renderer = this.ColumnModel.GetCellRenderer(this.ColumnModel.Columns[column].GetDefaultRendererName());
                    }

                    // When calling renderer.GetCellHeight(), only the width of the bounds is used.
                    int w = this.GetColumnWidth(column, varCell);
                    renderer.Bounds = new Rectangle(this.GetColumnLeft(column), 0, this.GetColumnWidth(column, varCell), 0);

                    // If this comes back zero then we have to go with the default
                    int newheight = renderer.GetCellHeight(g, varCell);
                    //Console.WriteLine("    GetRenderedRowHeight colwidth={0} rowheight={1}", w, newheight);
                    if (newheight == 0)
                        newheight = row.Height;
                    height = newheight;
                }
            }
            return height;
        }
        #endregion

		#region Hit Tests

		/// <summary>
		/// Returns a TableRegions value that represents the table region at 
		/// the specified client coordinates
		/// </summary>
		/// <param name="x">The client x coordinate</param>
		/// <param name="y">The client y coordinate</param>
		/// <returns>A TableRegions value that represents the table region at 
		/// the specified client coordinates</returns>
		public TableRegion HitTest(int x, int y)
		{
			if (this.HeaderStyle != ColumnHeaderStyle.None && this.HeaderRectangle.Contains(x, y))
			{
				return TableRegion.ColumnHeader;
			}
			else if (this.CellDataRect.Contains(x, y))
			{
				return TableRegion.Cells;
			}
			else if (!this.Bounds.Contains(x, y))
			{
				return TableRegion.NoWhere;
			}

			return TableRegion.NonClientArea;
		}


		/// <summary>
		/// Returns a TableRegions value that represents the table region at 
		/// the specified client point
		/// </summary>
		/// <param name="p">The point of interest</param>
		/// <returns>A TableRegions value that represents the table region at 
		/// the specified client point</returns>
		public TableRegion HitTest(Point p)
		{
			return this.HitTest(p.X, p.Y);
		}

		#endregion

		#endregion

		#region Dispose

		/// <summary>
		/// Releases the unmanaged resources used by the Control and optionally 
		/// releases the managed resources
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged 
		/// resources; false to release only unmanaged resources</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
					components.Dispose();
			}

			base.Dispose(disposing);
		}

        /// <summary>
        /// Removes the TableModel from the Table but leaves the ColumnModel unaltered.
        /// </summary>
        public void ClearAllData()
        {
            if (this.TableModel != null)
                this.TableModel.Rows.Clear();
        }

		/// <summary>
		/// Removes the ColumnModel and TableModel from the Table
		/// </summary>
		public void Clear()
		{
			if (this.ColumnModel != null)
				this.ColumnModel = null;

			if (this.TableModel != null)
				this.TableModel = null;

			ClearAllRowControls();
		}

		/// <summary>
		/// Clears all the controls from the Controls collection except the scroll bars
		/// </summary>
		public void ClearAllRowControls()
		{
			int i = 0;
			while (i < this.Controls.Count)
			{
				if ((this.Controls[i] == this.hScrollBar) || (this.Controls[i] == this.vScrollBar))
					i++;
				else
					this.Controls.RemoveAt(i);
			}
		}

		#endregion

		#region Editing

		/// <summary>
		/// Records the Cell that is currently being edited and the 
		/// ICellEditor used to edit the Cell
		/// </summary>
		/// <param name="cell">The Cell that is currently being edited</param>
		/// <param name="editor">The ICellEditor used to edit the Cell</param>
		private void SetEditingCell(Cell cell, ICellEditor editor)
		{
			this.SetEditingCell(new CellPos(cell.Row.InternalIndex, cell.InternalIndex), editor);
		}


		/// <summary>
		/// Records the Cell that is currently being edited and the 
		/// ICellEditor used to edit the Cell
		/// </summary>
		/// <param name="cellPos">The Cell that is currently being edited</param>
		/// <param name="editor">The ICellEditor used to edit the Cell</param>
		private void SetEditingCell(CellPos cellPos, ICellEditor editor)
		{
			this.editingCell = cellPos;
			this.curentCellEditor = editor;
		}


		/// <summary>
		/// Starts editing the Cell at the specified row and column indexes
		/// </summary>
		/// <param name="row">The row index of the Cell to be edited</param>
		/// <param name="column">The column index of the Cell to be edited</param>
		public void EditCell(int row, int column)
		{
			this.EditCell(new CellPos(row, column));
		}

		/// <summary>
		/// Starts editing the Cell at the specified CellPos
		/// </summary>
		/// <param name="cellPos">A CellPos that specifies the Cell to be edited</param>
		public void EditCell(CellPos cellPos)
		{
			// don't bother if the cell doesn't exists or the cell's
			// column is not visible or the cell is not editable
			if (!this.IsValidCell(cellPos) || !this.ColumnModel.Columns[cellPos.Column].Visible || !this.IsCellEditable(cellPos))
				return;

			// check if we're currently editing a cell
			if (this.EditingCell != CellPos.Empty)
			{
				// don't bother if we're already editing the cell.  
				// if we're editing a different cell stop editing
				if (this.EditingCell == cellPos)
					return;
				else
					this.EditingCellEditor.StopEditing();
			}

			Cell cell = this.TableModel[cellPos];
			ICellEditor editor = this.ColumnModel.GetCellEditor(cellPos.Column);

			// make sure we have an editor and that the cell 
			// and the cell's column are editable
			if (editor == null || !cell.Editable || !this.ColumnModel.Columns[cellPos.Column].Editable)
				return;

			if (this.EnsureVisible(cellPos))
				this.Refresh();

			Rectangle cellRect = this.CellRect(cellPos);

			// give anyone subscribed to the table's BeginEditing
			// event the first chance to cancel editing
			CellEditEventArgs e = new CellEditEventArgs(cell, editor, this, cellPos.Row, cellPos.Column, cellRect);

			this.OnBeginEditing(e);

			//
			if (!e.Cancel)
			{
				// get the editor ready for editing.  if PrepareForEditing
				// returns false, someone who subscribed to the editors 
				// BeginEdit event has cancelled editing
				if (!editor.PrepareForEditing(cell, this, cellPos, cellRect, e.Handled))
					return;

				// keep track of the editing cell and editor 
				// and start editing
				this.editingCell = cellPos;
				this.curentCellEditor = editor;

				editor.StartEditing();
			}
		}

		/*/// <summary>
		/// Stops editing the current Cell and starts editing the next editable Cell
		/// </summary>
		/// <param name="forwards">Specifies whether the editor should traverse 
		/// forward when looking for the next editable Cell</param>
		protected internal void EditNextCell(bool forwards)
		{
			if (this.EditingCell == CellPos.Empty)
			{
				return;
			}
				
			CellPos nextCell = this.FindNextEditableCell(this.FocusedCell, true, forwards, false);

			if (nextCell != CellPos.Empty && nextCell != this.EditingCell)
			{
				this.StopEditing();

				this.EditCell(nextCell);
			}
		}*/


		/// <summary>
		/// Stops editing the current Cell and commits any changes
		/// </summary>
		public void StopEditing()
		{
			// don't bother if we're not editing
			if (this.EditingCell == CellPos.Empty)
			{
				return;
			}

			this.EditingCellEditor.StopEditing();

			this.Invalidate(this.RowRect(this.editingCell.Row));

			this.editingCell = CellPos.Empty;
			this.curentCellEditor = null;
		}


		/// <summary>
		/// Cancels editing the current Cell and ignores any changes
		/// </summary>
		public void CancelEditing()
		{
			// don't bother if we're not editing
			if (this.EditingCell == CellPos.Empty)
			{
				return;
			}

			this.EditingCellEditor.CancelEditing();

			this.editingCell = CellPos.Empty;
			this.curentCellEditor = null;
		}


		/// <summary>
		/// Returns whether the Cell at the specified row and column is able 
		/// to be edited by the user
		/// </summary>
		/// <param name="row">The row index of the Cell to check</param>
		/// <param name="column">The column index of the Cell to check</param>
		/// <returns>True if the Cell at the specified row and column is able 
		/// to be edited by the user, false otherwise</returns>
		public bool IsCellEditable(int row, int column)
		{
			return this.IsCellEditable(new CellPos(row, column));
		}


		/// <summary>
		/// Returns whether the Cell at the specified CellPos is able 
		/// to be edited by the user
		/// </summary>
		/// <param name="cellpos">A CellPos that specifies the Cell to check</param>
		/// <returns>True if the Cell at the specified CellPos is able 
		/// to be edited by the user, false otherwise</returns>
		public bool IsCellEditable(CellPos cellpos)
		{
			// don't bother if the cell doesn't exists or the cell's
			// column is not visible
			if (!this.IsValidCell(cellpos) || !this.ColumnModel.Columns[cellpos.Column].Visible)
			{
				return false;
			}

			return (this.TableModel[cellpos].Editable &&
				this.ColumnModel.Columns[cellpos.Column].Editable);
		}


		/// <summary>
		/// Returns whether the Cell at the specified row and column is able 
		/// to respond to user interaction
		/// </summary>
		/// <param name="row">The row index of the Cell to check</param>
		/// <param name="column">The column index of the Cell to check</param>
		/// <returns>True if the Cell at the specified row and column is able 
		/// to respond to user interaction, false otherwise</returns>
		public bool IsCellEnabled(int row, int column)
		{
			return this.IsCellEnabled(new CellPos(row, column));
		}


		/// <summary>
		/// Returns whether the Cell at the specified CellPos is able 
		/// to respond to user interaction
		/// </summary>
		/// <param name="cellpos">A CellPos that specifies the Cell to check</param>
		/// <returns>True if the Cell at the specified CellPos is able 
		/// to respond to user interaction, false otherwise</returns>
		public bool IsCellEnabled(CellPos cellpos)
		{
			// don't bother if the cell doesn't exists or the cell's
			// column is not visible
			if (!this.IsValidCell(cellpos) || !this.ColumnModel.Columns[cellpos.Column].Visible)
			{
				return false;
			}

			return (this.TableModel[cellpos].Enabled &&
				this.ColumnModel.Columns[cellpos.Column].Enabled);
		}

		#endregion

		#region Invalidate

		/// <summary>
		/// Invalidates the specified Cell
		/// </summary>
		/// <param name="cell">The Cell to be invalidated</param>
		public void InvalidateCell(Cell cell)
		{
			this.InvalidateCell(cell.Row.Index, cell.Index);
		}


		/// <summary>
		/// Invalidates the Cell located at the specified row and column indicies
		/// </summary>
		/// <param name="row">The row index of the Cell to be invalidated</param>
		/// <param name="column">The column index of the Cell to be invalidated</param>
		public void InvalidateCell(int row, int column)
		{
			Rectangle cellRect = this.CellRect(row, column);

			if (cellRect == Rectangle.Empty)
			{
				return;
			}

			if (cellRect.IntersectsWith(this.CellDataRect))
			{
				this.Invalidate(Rectangle.Intersect(this.CellDataRect, cellRect), false);
			}
		}


		/// <summary>
		/// Invalidates the Cell located at the specified CellPos
		/// </summary>
		/// <param name="cellPos">A CellPos that specifies the Cell to be invalidated</param>
		public void InvalidateCell(CellPos cellPos)
		{
			this.InvalidateCell(cellPos.Row, cellPos.Column);
		}


		/// <summary>
		/// Invalidates the specified Row
		/// </summary>
		/// <param name="row">The Row to be invalidated</param>
		public void InvalidateRow(Row row)
		{
			this.InvalidateRow(row.Index);
		}


		/// <summary>
		/// Invalidates the Row located at the specified row index
		/// </summary>
		/// <param name="row">The row index of the Row to be invalidated</param>
		public void InvalidateRow(int row)
		{
			Rectangle rowRect = this.RowRect(row);

			if (rowRect == Rectangle.Empty)
			{
				return;
			}

			if (rowRect.IntersectsWith(this.CellDataRect))
			{
				this.Invalidate(Rectangle.Intersect(this.CellDataRect, rowRect), false);
			}
		}


		/// <summary>
		/// Invalidates the Row located at the specified CellPos
		/// </summary>
		/// <param name="cellPos">A CellPos that specifies the Row to be invalidated</param>
		public void InvalidateRow(CellPos cellPos)
		{
			this.InvalidateRow(cellPos.Row);
		}

		/// <summary>
		/// Invalidates the given Rectangle
		/// </summary>
		/// <param name="rect"></param>
		public void InvalidateRect(Rectangle rect)
		{
			this.Invalidate(rect);
		}
		#endregion

		#region Keys

		/// <summary>
		/// Determines whether the specified key is reserved for use by the Table
		/// </summary>
		/// <param name="key">One of the Keys values</param>
		/// <returns>true if the specified key is reserved for use by the Table; 
		/// otherwise, false</returns>
		protected internal bool IsReservedKey(Keys key)
		{
			if ((key & Keys.Alt) != Keys.Alt)
			{
				Keys k = key & Keys.KeyCode;

				return (k == Keys.Up ||
					k == Keys.Down ||
					k == Keys.Left ||
					k == Keys.Right ||
					k == Keys.PageUp ||
					k == Keys.PageDown ||
					k == Keys.Home ||
					k == Keys.End ||
					k == Keys.Tab);
			}

			return false;
		}


		/// <summary>
		/// Determines whether the specified key is a regular input key or a special 
		/// key that requires preprocessing
		/// </summary>
		/// <param name="keyData">One of the Keys values</param>
		/// <returns>true if the specified key is a regular input key; otherwise, false</returns>
		protected override bool IsInputKey(Keys keyData)
		{
			if ((keyData & Keys.Alt) != Keys.Alt)
			{
				Keys key = keyData & Keys.KeyCode;

				switch (key)
				{
					case Keys.Up:
					case Keys.Down:
					case Keys.Left:
					case Keys.Right:
					case Keys.Prior:
					case Keys.Next:
					case Keys.End:
					case Keys.Home:
					{
						return true;
					}
				}

				if (base.IsInputKey(keyData))
				{
					return true;
				}
			}

			return false;
		}

		#endregion

		#region Layout
		/// <summary>
		/// Prevents the Table from drawing until the EndUpdate method is called
		/// </summary>
		public void BeginUpdate()
		{
			if (this.IsHandleCreated)
			{
				if (this.beginUpdateCount == 0)
					NativeMethods.SendMessage(this.Handle, 11, 0, 0);

				this.beginUpdateCount++;
			}
		}

		/// <summary>
		/// Resumes drawing of the Table after drawing is suspended by the 
		/// BeginUpdate method
		/// </summary>
		public void EndUpdate()
		{
			if (this.beginUpdateCount <= 0)
				return;

			this.beginUpdateCount--;

			if (this.beginUpdateCount == 0)
			{
				NativeMethods.SendMessage(this.Handle, 11, -1, 0);

				this.PerformLayout();

                this.ColumnModel.Columns.RecalcWidthCache();

                if (this.EnableWordWrap)
                {
                    if (autoCalculateRowHeights)
                        this.CalculateAllRowHeights();
                }
                this.UpdateScrollBars();   // without this the scolling will have been set up assuming all rows have the default height

                this.Invalidate(true);
            }
		}

		/// <summary>
		/// Signals the object that initialization is starting
		/// </summary>
		public void BeginInit()
		{
			this.init = true;
		}

		/// <summary>
		/// Signals the object that initialization is complete
		/// </summary>
		public void EndInit()
		{
			this.init = false;
			this.PerformLayout();
		}

		/// <summary>
		/// Gets whether the Table is currently initializing
		/// </summary>
		[Browsable(false)]
		public bool Initializing
		{
			get { return this.init; }
		}
		#endregion

		#region Mouse

		/// <summary>
		/// This member supports the .NET Framework infrastructure and is not 
		/// intended to be used directly from your code
		/// </summary>
		public new void ResetMouseEventArgs()
		{
			if (this.trackMouseEvent == null)
			{
				this.trackMouseEvent = new TRACKMOUSEEVENT();
				this.trackMouseEvent.dwFlags = 3;
				this.trackMouseEvent.hwndTrack = base.Handle;
			}

			this.trackMouseEvent.dwHoverTime = this.HoverTime;

			NativeMethods.TrackMouseEvent(this.trackMouseEvent);
		}

		#endregion

		#region Scrolling

		/// <summary>
		/// Updates the scrollbars to reflect any changes made to the Table
		/// </summary>
		public void UpdateScrollBars()
		{
			// fix: Add width/height check as otherwise minimize 
			//      causes a crash
			//      Portia4ever (kangxj@126.com)
			//      13/09/2005
			//      v1.0.1
			if (!this.Scrollable || this.ColumnModel == null || this.Width == 0 || this.Height == 0)
				return;

			bool hscroll = (this.ColumnModel.VisibleColumnsWidth > this.Width - (this.BorderWidth * 2));
            bool vscroll = TopIndex != 0 || RowsCanReachHeight(this.Height - (this.BorderWidth * 2) - (hscroll ? SystemInformation.HorizontalScrollBarHeight : 0));

			if (vscroll)
				hscroll = (this.ColumnModel.VisibleColumnsWidth > this.Width - (this.BorderWidth * 2) - SystemInformation.VerticalScrollBarWidth);

			if (hscroll)
			{
                #region Set up the horizontal scrollbar
				Rectangle hscrollBounds = new Rectangle(this.BorderWidth,
					this.Height - this.BorderWidth - SystemInformation.HorizontalScrollBarHeight,
					this.Width - (this.BorderWidth * 2),
					SystemInformation.HorizontalScrollBarHeight);

				if (vscroll)
				{
					hscrollBounds.Width -= SystemInformation.VerticalScrollBarWidth;
				}

				this.hScrollBar.Visible = true;
				this.hScrollBar.Bounds = hscrollBounds;
				this.hScrollBar.Minimum = 0;
				this.hScrollBar.Maximum = this.ColumnModel.VisibleColumnsWidth;
				// netus fix by Kosmokrat Hismoom  - added check for property Maximum
				// as otherwise resizing could lead to a crash 12/01/06
				this.hScrollBar.Maximum = (this.hScrollBar.Maximum <= 0) ? 0 : this.hScrollBar.Maximum;
				this.hScrollBar.SmallChange = Column.MinimumWidth;
				// fixed by Kosmokrat Hismoom on 7 jan 2006
				this.hScrollBar.LargeChange = (hscrollBounds.Width <= 0) ? 0 : (hscrollBounds.Width - 1);

				if (this.hScrollBar.Value > this.hScrollBar.Maximum - this.hScrollBar.LargeChange)
					this.hScrollBar.Value = this.hScrollBar.Maximum - this.hScrollBar.LargeChange;
                #endregion
			}
			else
			{
				this.hScrollBar.Visible = false;
				this.hScrollBar.Value = 0;
			}

			if (vscroll)
			{
				Rectangle vscrollBounds = new Rectangle(this.Width - this.BorderWidth - SystemInformation.VerticalScrollBarWidth,
					this.BorderWidth,
					SystemInformation.VerticalScrollBarWidth,
					this.Height - (this.BorderWidth * 2));

				if (hscroll)
					vscrollBounds.Height -= SystemInformation.HorizontalScrollBarHeight;

                int visibleRowCount = this.GetVisibleRowCount();

				this.vScrollBar.Visible = true;
				this.vScrollBar.Bounds = vscrollBounds;
				this.vScrollBar.Minimum = 0;
                this.vScrollBar.SmallChange = 1;

                // When at the very bottom,     LargeChange + Value = Maximum + 1

				// This is the total number of rows in the table that are not hidden
                int rowcount = this.RowCount - this.HiddenSubRows;

                vScrollBar.Maximum = rowcount;
				// as otherwise resizing could lead to a crash - 12/01/06
				vScrollBar.Maximum = (this.vScrollBar.Maximum <= 0) ? 0 : this.vScrollBar.Maximum;
                // as otherwise minimising could lead to a crash
                vScrollBar.LargeChange = (visibleRowCount < 0) ? 0 : visibleRowCount + 1;
			}
			else
			{
				this.vScrollBar.Visible = false;
				this.vScrollBar.Value = 0;
			}

            UpdateRowsVisibilityData();
        }

		/// <summary>
		/// Returns the correct new value for the scrollbar.Value property.
		/// The ValueChanged event handler invalidates the control, because if
		/// the thumb track is clicked, then the Value property is changed without coming
		/// through this method.
		/// </summary>
        /// <param name="previousTopRowIndex"></param>
        /// <param name="howMany"></param>
		/// <returns>The index of the row that should be used to set the .Value property of the scrollbar.</returns>
		protected int GetNewTopRowIndex(int previousTopRowIndex, int howMany)
		{
            if (howMany == 0)
                return previousTopRowIndex;
			int visibleRows = this.vScrollBar.LargeChange - 1;
            bool down = (howMany > 0);

            int max = Math.Abs(howMany);
            var rows = this.TableModel.Rows;
            var rowsCount = rows.Count;
            if (down)
            {
                int newTopRowIndex = previousTopRowIndex;
                while (newTopRowIndex < rowsCount)
                {
                    var row = rows[newTopRowIndex];
                    if (row.Parent == null || row.Parent.ExpandSubRows)
                    {
                        --max;
                        if (max == -1)
                            break;
                    }
                    ++newTopRowIndex;
                }
                return newTopRowIndex;
            }
            else
            {
                int newTopRowIndex = previousTopRowIndex;
                while (newTopRowIndex >= 0)
                {
                    var row = rows[newTopRowIndex];
                    if (row.Parent == null || row.Parent.ExpandSubRows)
                    {
                        --max;
                        if (max == -1)
                            break;
                    }
                    --newTopRowIndex;
                }
                return newTopRowIndex;
            }
            /*int max = Math.Abs(howMany);

			int column = this.ColumnModel.NextVisibleColumn(-1);
            CellPos newCell = new CellPos(previousTopRowIndex, column);  // The row currently at the top

			for (int i = 0; i < max; i++)
            {
                // The first cell on the row we are going to (if all is well)
                // Changed to fix scrolling bug (ID - 2848790) reported by Tom Nolan ( lordicarus )
                newCell = this.FindNextVisibleCell(newCell, true, down, false, false, true);
            }

            return newCell.Row;*/
		}

        /// <summary>
        /// Returns a safe value that can be used for the .Value property of the V scrollbar (that is 
        /// within the min and max).
        /// </summary>
        /// <param name="newValue"></param>
        /// <returns></returns>
        private int EnsureSafeVScrollValue(int newValue)
        {
            int newTopRowIndex = newValue;
            int visibleRows = this.vScrollBar.LargeChange - 1;

			if (newTopRowIndex < 0)
			{
				// Can get here with the mousewheel going up
				newTopRowIndex = 0;
			}
			else if (newTopRowIndex > this.vScrollBar.Maximum - visibleRows)
			{
				// Can get here with the mousewheel going down
				newTopRowIndex = this.vScrollBar.Maximum - visibleRows;
            }
            return newTopRowIndex;
        }

        /// <summary>
		/// Scrolls the contents of the Table horizontally to the specified value
		/// </summary>
		/// <param name="value">The value to scroll to</param>
		protected void HorizontalScroll(int value)
		{
			int scrollVal = this.hScrollBar.Value - value;

			if (scrollVal != 0)
			{
				RECT scrollRect = RECT.FromRectangle(this.PseudoClientRect);
				Rectangle invalidateRect = scrollRect.ToRectangle();

				NativeMethods.ScrollWindow(this.Handle, scrollVal, 0, ref scrollRect, ref scrollRect);

				if (scrollVal < 0)
					invalidateRect.X = invalidateRect.Right + scrollVal;

				invalidateRect.Width = Math.Abs(scrollVal);

				this.Invalidate(invalidateRect, false);

				if (this.VScroll)
				{
					this.Invalidate(new Rectangle(this.Width - this.BorderWidth - SystemInformation.VerticalScrollBarWidth,
						this.Height - this.BorderWidth - SystemInformation.HorizontalScrollBarHeight,
						SystemInformation.VerticalScrollBarWidth,
						SystemInformation.HorizontalScrollBarHeight),
						false);
				}
			}
		}

        /// <summary>
		/// Ensures that the Cell at the specified row and column is visible 
		/// within the Table, scrolling the contents of the Table if necessary
		/// </summary>
		/// <param name="row">The zero-based index of the row to scroll into view</param>
		/// <param name="column">The zero-based index of the column to scroll into view</param>
		/// <returns>true if the Table scrolled to the Cell at the specified row 
		/// and column, false otherwise</returns>
		public bool EnsureVisible(int row, int column)
		{
			if (!this.Scrollable || (!this.HScroll && !this.VScroll) || row == -1)
			{
				return false;
			}

			if (column == -1)
			{
				if (this.FocusedCell.Column != -1)
				{
					column = this.FocusedCell.Column;
				}
				else
				{
					column = 0;
				}
			}

			int hscrollVal = this.hScrollBar.Value;
			int vscrollVal = this.vScrollBar.Value;
			bool moved = false;

			if (this.HScroll)
			{
				if (column < 0)
				{
					column = 0;
				}
				else if (column >= this.ColumnCount)
				{
					column = this.ColumnCount - 1;
				}

				if (this.ColumnModel.Columns[column].Visible)
				{
					if (this.ColumnModel.Columns[column].Left < this.hScrollBar.Value)
					{
						hscrollVal = this.ColumnModel.Columns[column].Left;
					}
					else if (this.ColumnModel.Columns[column].Right > this.hScrollBar.Value + this.CellDataRect.Width)
					{
						if (this.ColumnModel.Columns[column].Width > this.CellDataRect.Width)
						{
							hscrollVal = this.ColumnModel.Columns[column].Left;
						}
						else
						{
							hscrollVal = this.ColumnModel.Columns[column].Right - this.CellDataRect.Width;
						}
					}
					if (hscrollVal > this.hScrollBar.Maximum - this.hScrollBar.LargeChange)
					{
						hscrollVal = this.hScrollBar.Maximum - this.hScrollBar.LargeChange;
					}
				}
			}

			if (this.VScroll)
			{
				if (row < 0)
				{
					vscrollVal = 0;
				}
				else if (row >= this.RowCount)
				{
					vscrollVal = this.RowCount - 1;
				}
				else
				{
                    int hidden = HiddenRowCountBefore(row);

                    int rowScrollIndex = row - hidden;

                    if (rowScrollIndex < vscrollVal)
					{
                        vscrollVal = rowScrollIndex;
					}
                    else if (rowScrollIndex > vscrollVal + this.vScrollBar.LargeChange)
					{
                        vscrollVal += rowScrollIndex - (vscrollVal + this.vScrollBar.LargeChange);
					}
				}

				if (vscrollVal > this.vScrollBar.Maximum - this.vScrollBar.LargeChange)
				{
					vscrollVal = (this.vScrollBar.Maximum - this.vScrollBar.LargeChange) + 1;
				}
			}

			if (this.RowRect(row).Bottom > this.CellDataRect.Bottom)
			{
				vscrollVal++;
			}

			moved = (this.hScrollBar.Value != hscrollVal || this.vScrollBar.Value != vscrollVal);

			if (moved)
			{
				this.hScrollBar.Value = hscrollVal;
				this.vScrollBar.Value = vscrollVal;

				this.Invalidate(this.PseudoClientRect);
			}

			return moved;
		}


		/// <summary>
		/// Ensures that the Cell at the specified CellPos is visible within 
		/// the Table, scrolling the contents of the Table if necessary
		/// </summary>
		/// <param name="cellPos">A CellPos that contains the zero-based index 
		/// of the row and column to scroll into view</param>
		/// <returns></returns>
		public bool EnsureVisible(CellPos cellPos)
		{
			return this.EnsureVisible(cellPos.Row, cellPos.Column);
		}


		/// <summary>
		/// Gets the index of the first visible Column currently displayed in the Table
		/// </summary>
		[Browsable(false)]
		public int FirstVisibleColumn
		{
			get
			{
				if (this.ColumnModel == null || this.ColumnModel.VisibleColumnCount == 0)
				{
					return -1;
				}

				return this.ColumnModel.ColumnIndexAtX(this.hScrollBar.Value);
			}
		}


		/// <summary>
		/// Gets the index of the last visible Column currently displayed in the Table
		/// </summary>
		[Browsable(false)]
		public int LastVisibleColumn
		{
			get
			{
				if (this.ColumnModel == null || this.ColumnModel.VisibleColumnCount == 0)
					return -1;

				int rightEdge = this.hScrollBar.Value + this.PseudoClientRect.Right;

				if (this.VScroll)
					rightEdge -= this.vScrollBar.Width;

				int col = this.ColumnModel.ColumnIndexAtX(rightEdge);

				if (col == -1)
					return this.ColumnModel.PreviousVisibleColumn(this.ColumnModel.Columns.Count);
				else if (!this.ColumnModel.Columns[col].Visible)
					return this.ColumnModel.PreviousVisibleColumn(col);

				return col;
			}
		}

		#endregion

		#region Sorting

		/// <summary>
		/// Sorts the last sorted column opposite to its current sort order, 
		/// or sorts the currently focused column in ascending order if no 
		/// columns have been sorted
		/// </summary>
		public void Sort()
		{
			this.Sort(true);
		}


		/// <summary>
		/// Sorts the last sorted column opposite to its current sort order, 
		/// or sorts the currently focused column in ascending order if no 
		/// columns have been sorted
		/// </summary>
		/// <param name="stable">Specifies whether a stable sorting method 
		/// should be used to sort the column</param>
		public void Sort(bool stable)
		{
			// don't allow sorting if we're being used as a 
			// preview table in a ColumnModel editor
			if (this.Preview)
			{
				return;
			}

			// if we don't have a sorted column already, check if 
			// we can use the column of the cell that has focus
			if (!this.IsValidColumn(this.lastSortedColumn))
			{
				if (this.IsValidColumn(this.focusedCell.Column))
				{
					this.lastSortedColumn = this.focusedCell.Column;
				}
			}

			// make sure the last sorted column exists
			if (this.IsValidColumn(this.lastSortedColumn))
			{
				// don't bother if the column won't let us sort
				if (!this.ColumnModel.Columns[this.lastSortedColumn].Sortable)
				{
					return;
				}

				// work out which direction we should sort
				SortOrder newOrder = SortOrder.Ascending;

				Column column = this.ColumnModel.Columns[this.lastSortedColumn];

				if (column.SortOrder == SortOrder.Ascending)
				{
					newOrder = SortOrder.Descending;
				}

				this.Sort(this.lastSortedColumn, column, newOrder, stable);
			}
		}


		/// <summary>
		/// Sorts the specified column opposite to its current sort order, 
		/// or in ascending order if the column is not sorted
		/// </summary>
		/// <param name="column">The index of the column to sort</param>
		public void Sort(int column)
		{
			this.Sort(column, true);
		}


		/// <summary>
		/// Sorts the specified column opposite to its current sort order, 
		/// or in ascending order if the column is not sorted
		/// </summary>
		/// <param name="column">The index of the column to sort</param>
		/// <param name="stable">Specifies whether a stable sorting method 
		/// should be used to sort the column</param>
		public void Sort(int column, bool stable)
		{
			// don't allow sorting if we're being used as a 
			// preview table in a ColumnModel editor
			if (this.Preview)
			{
				return;
			}

			// make sure the column exists
			if (this.IsValidColumn(column))
			{
				// don't bother if the column won't let us sort
				if (!this.ColumnModel.Columns[column].Sortable)
				{
					return;
				}

				// if we already have a different sorted column, set 
				// its sort order to none
				if (column != this.lastSortedColumn)
				{
					if (this.IsValidColumn(this.lastSortedColumn))
					{
						this.ColumnModel.Columns[this.lastSortedColumn].InternalSortOrder = SortOrder.None;
					}
				}

				this.lastSortedColumn = column;

				// work out which direction we should sort
				SortOrder newOrder = SortOrder.Ascending;

				Column col = this.ColumnModel.Columns[column];

				if (col.SortOrder == SortOrder.Ascending)
				{
					newOrder = SortOrder.Descending;
				}

				this.Sort(column, col, newOrder, stable);
			}
		}


		/// <summary>
		/// Sorts the specified column in the specified sort direction
		/// </summary>
		/// <param name="column">The index of the column to sort</param>
		/// <param name="sortOrder">The direction the column is to be sorted</param>
		public void Sort(int column, SortOrder sortOrder)
		{
			this.Sort(column, sortOrder, true);
		}


		/// <summary>
		/// Sorts the specified column in the specified sort direction
		/// </summary>
		/// <param name="column">The index of the column to sort</param>
		/// <param name="sortOrder">The direction the column is to be sorted</param>
		/// <param name="stable">Specifies whether a stable sorting method 
		/// should be used to sort the column</param>
		public void Sort(int column, SortOrder sortOrder, bool stable)
		{
			// don't allow sorting if we're being used as a 
			// preview table in a ColumnModel editor
			if (this.Preview)
			{
				return;
			}

			// make sure the column exists
			if (this.IsValidColumn(column))
			{
				// don't bother if the column won't let us sort
				if (!this.ColumnModel.Columns[column].Sortable)
				{
					return;
				}

				// if we already have a different sorted column, set 
				// its sort order to none
				if (column != this.lastSortedColumn)
				{
					if (this.IsValidColumn(this.lastSortedColumn))
					{
						this.ColumnModel.Columns[this.lastSortedColumn].InternalSortOrder = SortOrder.None;
					}
				}

				this.lastSortedColumn = column;

				this.Sort(column, this.ColumnModel.Columns[column], sortOrder, stable);
			}
		}


		/// <summary>
		/// Sorts the specified column in the specified sort direction
		/// </summary>
		/// <param name="index">The index of the column to sort</param>
		/// <param name="column">The column to sort</param>
		/// <param name="sortOrder">The direction the column is to be sorted</param>
		/// <param name="stable">Specifies whether a stable sorting method 
		/// should be used to sort the column</param>
		private void Sort(int index, Column column, SortOrder sortOrder, bool stable)
		{
			// make sure a null comparer type doesn't sneak past

			ComparerBase comparer = null;

			if (column.Comparer != null)
			{
				comparer = (ComparerBase) Activator.CreateInstance(column.Comparer, new object[] { this.TableModel, index, sortOrder });
			}
			else if (column.DefaultComparerType != null)
			{
				comparer = (ComparerBase) Activator.CreateInstance(column.DefaultComparerType, new object[] { this.TableModel, index, sortOrder });
			}
			else
			{
				return;
			}

			column.InternalSortOrder = sortOrder;

			// create the comparer
			SorterBase sorter = null;

			// work out which sort method to use.
			// - InsertionSort/MergeSort are stable sorts, 
			//   whereas ShellSort/HeapSort are unstable
			// - InsertionSort/ShellSort are faster than 
			//   MergeSort/HeapSort on small lists and slower 
			//   on large lists
			// so we choose based on the size of the list and
			// whether the user wants a stable sort
			if (this.SortType == SortType.AutoSort)
			{
				if (this.TableModel.Rows.Count < 1000)
				{
					if (this.StableSort)
						sorter = new InsertionSorter(this.TableModel, index, comparer, sortOrder);
					else
						sorter = new ShellSorter(this.TableModel, index, comparer, sortOrder);
				}
				else
				{
					if (this.StableSort)
						sorter = new MergeSorter(this.TableModel, index, comparer, sortOrder);
					else
						sorter = new HeapSorter(this.TableModel, index, comparer, sortOrder);
				}
			}
			else
			{
				switch (this.SortType)
				{
					case SortType.HeapSort: { sorter = new HeapSorter(this.TableModel, index, comparer, sortOrder); break; }
					case SortType.InsertionSort: { sorter = new InsertionSorter(this.TableModel, index, comparer, sortOrder); break; }
					case SortType.MergeSort: { sorter = new MergeSorter(this.TableModel, index, comparer, sortOrder); break; }
					case SortType.ShellSort: { sorter = new ShellSorter(this.TableModel, index, comparer, sortOrder); break; }
					default: { throw new ApplicationException("Invalid Sort Type - " + this.SortType.ToString()); }
				}
			}

			sorter.SecondarySortOrders = this.ColumnModel.SecondarySortOrders;
			sorter.SecondaryComparers = this.GetSecondaryComparers(this.ColumnModel.SecondarySortOrders);

			// don't let the table redraw
			this.BeginUpdate();

			this.OnBeginSort(new ColumnEventArgs(column, index, ColumnEventType.Sorting, null));

			// Added by -= Micronn =- on 22 dec 2005
			Row focusedRow = null;

			if (this.FocusedCell != CellPos.Empty)
	            focusedRow = this.tableModel.Rows[ this.FocusedCell.Row ];

			sorter.Sort();

			// Added by -= Micronn =- on 22 dec 2005
			if (focusedRow != null)
			{
				this.FocusedCell = new CellPos(focusedRow.Index, this.FocusedCell.Column);
				this.EnsureVisible(this.FocusedCell);
			}

			this.OnEndSort(new ColumnEventArgs(column, index, ColumnEventType.Sorting, null));

			// redraw any changes
			this.EndUpdate();
		}

		/// <summary>
		/// Gets a collection of comparers for the underlying sort order(s)
		/// </summary>
		/// <param name="secondarySortOrders"></param>
		/// <returns></returns>
		private IComparerCollection GetSecondaryComparers(SortColumnCollection secondarySortOrders)
		{
			IComparerCollection comparers = new IComparerCollection();

			foreach (SortColumn sort in secondarySortOrders)
			{
				ComparerBase comparer = null;
				Column column = this.ColumnModel.Columns[sort.SortColumnIndex];

				if (column.Comparer != null)
				{
					comparer = (ComparerBase) Activator.CreateInstance(column.Comparer, new object[] { this.TableModel, sort.SortColumnIndex, sort.SortOrder });
				}
				else if (column.DefaultComparerType != null)
				{
					comparer = (ComparerBase) Activator.CreateInstance(column.DefaultComparerType, new object[] { this.TableModel, sort.SortColumnIndex, sort.SortOrder });
				}
				if (comparer != null)
					comparers.Add(comparer);
			}

			return comparers;
		}

		/// <summary>
		/// Returns whether a Column exists at the specified index in the 
		/// Table's ColumnModel
		/// </summary>
		/// <param name="column">The index of the column to check</param>
		/// <returns>True if a Column exists at the specified index in the 
		/// Table's ColumnModel, false otherwise</returns>
		public bool IsValidColumn(int column)
		{
			if (this.ColumnModel == null)
			{
				return false;
			}

			return (column >= 0 && column < this.ColumnModel.Columns.Count);
		}

		#endregion

		#region Controls
		/// <summary>
		/// Returns the cell that contains the given Control (in a ControlColumn).
		/// Returns null if the control is not in a valid cell.
		/// </summary>
		/// <param name="control">The control that is part of a Cell.</param>
		/// <exception cref="ArgumentException">If the control is not added to this table.</exception>
		/// <returns></returns>
		public Cell GetContainingCell(Control control)
		{
			if (control.Parent != this)
				throw new ArgumentException("Control is not part of this table.", "control");

			Point p = control.Location;
			CellPos cellPos = new CellPos(this.RowIndexAt(p), this.ColumnIndexAt(p));

			if (this.IsValidCell(cellPos))
			{
				// Adjust this to take colspan into account
				// LastMouseCell may be a cell that is 'under' a colspan cell
				CellPos realCell = this.ResolveColspan(cellPos);
				Cell cell = tableModel[realCell];
				return cell;
			}
			return null;
		}
		#endregion

        #region DragDrop
        internal DragDropEffects DragDropExternalTypeEffectSelector(object sender, DragEventArgs drgevent)
        {
            if (DragDropExternalTypeEffect != null)
                return DragDropExternalTypeEffect(sender, drgevent);
            else
                return DragDropEffects.None;
        }

        internal void DragDropExternalType(object sender, DragEventArgs drgevent)
        {
            if (DragDropExternalTypeEvent != null)
                DragDropExternalTypeEvent(sender, drgevent);
        }

        internal void DragDropRowInsertedAt(int destIndex)
        {
            if (DragDropRowInsertedAtEvent != null)
                DragDropRowInsertedAtEvent(destIndex);
        }

        internal void DragDropRowMoved(int srcIndex, int destIndex)
        {
            if (DragDropRowMovedEvent != null)
                DragDropRowMovedEvent(srcIndex, destIndex);
        }
        #endregion

		#endregion

		#region Properties

		#region Borders

		/// <summary>
		/// Gets or sets the border style for the Table
		/// </summary>
		[Category("Appearance"),
		DefaultValue(BorderStyle.Fixed3D),
		Description("Indicates the border style for the Table")]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.borderStyle;
			}

			set
			{
				if (!Enum.IsDefined(typeof(BorderStyle), value))
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(BorderStyle));
				}

				if (borderStyle != value)
				{
					this.borderStyle = value;

					this.Invalidate(true);
				}
			}
		}

		/// <summary>
		/// Gets or sets the background color of the table.
		/// </summary>
		[Category("Appearance"),
		Description("The background color of the table")]
		public Color BorderColor
		{
			get { return this.borderColor; }
			set
			{
				if (this.borderColor != value)
				{
					this.borderColor = value;

					this.Invalidate(true);
				}
			}
		}

		/// <summary>
		/// Gets or sets the background color of the table when the table does not have focus.
		/// </summary>
		[Category("Appearance"),
		Description("The background color of the table")]
		public Color UnfocusedBorderColor
		{
			get { return this.unfocusedBorderColor; }
			set
			{
				if (this.unfocusedBorderColor != value)
				{
					this.unfocusedBorderColor = value;

					if (!this.Focused)
						this.Invalidate(true);
				}
			}
		}

		/// <summary>
		/// Gets the width of the Tables border
		/// </summary>
		protected int BorderWidth
		{
			get
			{
				if (this.BorderStyle == BorderStyle.Fixed3D)
				{
					return SystemInformation.Border3DSize.Width;
				}
				else if (this.BorderStyle == BorderStyle.FixedSingle)
				{
					return 1;
				}

				return 0;
			}
		}

		#endregion

		#region Cells

		/// <summary>
		/// Gets the last known cell position that the mouse was over
		/// </summary>
		[Browsable(false)]
		public CellPos LastMouseCell
		{
			get
			{
				return this.lastMouseCell;
			}
		}


		/// <summary>
		/// Gets the last known cell position that the mouse's left 
		/// button was pressed in
		/// </summary>
		[Browsable(false)]
		public CellPos LastMouseDownCell
		{
			get
			{
				return this.lastMouseDownCell;
			}
		}


		/// <summary>
		/// Gets or sets the position of the Cell that currently has focus
		/// </summary>
		[Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CellPos FocusedCell
		{
			get
			{
				return this.focusedCell;
			}

			set
			{
				if (!this.IsValidCell(value))
				{
					return;
				}

				if (!this.TableModel[value].Enabled)
				{
					return;
				}

				if (this.focusedCell != value)
				{
					if (!this.focusedCell.IsEmpty)
					{
						this.RaiseCellLostFocus(this.focusedCell);
					}

					this.focusedCell = value;

					if (!value.IsEmpty)
					{
						this.EnsureVisible(value);

						this.RaiseCellGotFocus(value);
					}
				}
			}
		}


		/// <summary>
		/// Gets or sets the amount of time (in milliseconds) that that the 
		/// mouse pointer must hover over a Cell or Column Header before 
		/// a MouseHover event is raised
		/// </summary>
		[Category("Behavior"),
		DefaultValue(1000),
		Description("The amount of time (in milliseconds) that that the mouse pointer must hover over a Cell or Column Header before a MouseHover event is raised")]
		public int HoverTime
		{
			get
			{
				return this.hoverTime;
			}

			set
			{
				if (value < 100)
				{
					throw new ArgumentException("HoverTime cannot be less than 100", "value");
				}

				if (this.hoverTime != value)
				{
					this.hoverTime = value;

					this.ResetMouseEventArgs();
				}
			}
		}

        /// <summary>
        /// Gets or sets the column header alignemnt
        /// </summary>
        [Category("Behavior"),
        DefaultValue(false),
        Description("Stop the beep when Enter or Escape keys are pressed when editing")]
        public bool SuppressEditorTerminatorBeep
        {
            get
            {
                return this.suppressEditorTerminatorBeep;
            }

            set
            {
                if (this.suppressEditorTerminatorBeep != value)
                {
                    this.suppressEditorTerminatorBeep = value;
                }
            }
        }
        
#endregion

		#region ClientRectangle

		/// <summary>
		/// Gets the rectangle that represents the "client area" of the control.
		/// (The rectangle excludes the borders and scrollbars)
		/// </summary>
		[Browsable(false)]
		public Rectangle PseudoClientRect
		{
			get
			{
				Rectangle clientRect = this.InternalBorderRect;

				if (this.HScroll)
				{
					clientRect.Height -= SystemInformation.HorizontalScrollBarHeight;
				}

				if (this.VScroll)
				{
					clientRect.Width -= SystemInformation.VerticalScrollBarWidth;
				}

				return clientRect;
			}
		}


		/// <summary>
		/// Gets the rectangle that represents the "cell data area" of the control.
		/// (The rectangle excludes the borders, column headers and scrollbars)
		/// </summary>
		[Browsable(false)]
		public Rectangle CellDataRect
		{
			get
			{
				Rectangle clientRect = this.PseudoClientRect;

				if (this.HeaderStyle != ColumnHeaderStyle.None && this.ColumnCount > 0)
				{
					clientRect.Y += this.HeaderHeight;
					clientRect.Height -= this.HeaderHeight;
				}

				return clientRect;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		private Rectangle InternalBorderRect
		{
			get
			{
				return new Rectangle(this.BorderWidth,
					this.BorderWidth,
					this.Width - (this.BorderWidth * 2),
					this.Height - (this.BorderWidth * 2));
			}
		}

		#endregion

		#region ColumnModel
		/// <summary>
		/// Gets or sets the ColumnModel that contains all the Columns
		/// displayed in the Table
		/// </summary>
		[Category("Columns"),
		DefaultValue(null),
		Description("Specifies the ColumnModel that contains all the Columns displayed in the Table")]
		public ColumnModel ColumnModel
		{
			get { return this.columnModel; }
			set
			{
				if (this.columnModel != value)
				{
					if (this.columnModel != null && this.columnModel.Table == this)
						this.columnModel.InternalTable = null;

					ColumnModel oldValue = this.columnModel;

					this.columnModel = value;

					if (value != null)
						value.InternalTable = this;

					this.OnColumnModelChanged(new TableEventArgs(this, TableEventType.ColumnModelChanged, oldValue)); // PJD TEA change
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the Table allows users to resize Column widths
		/// </summary>
		[Category("Columns"),
		DefaultValue(true),
		Description("Specifies whether the Table allows users to resize Column widths")]
		public bool ColumnResizing
		{
			get { return this.columnResizing; }
			set
			{
				if (this.columnResizing != value)
					this.columnResizing = value;
			}
		}

		/// <summary>
		/// Returns the number of Columns in the Table
		/// </summary>
		[Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ColumnCount
		{
			get
			{
				if (this.ColumnModel == null)
					return -1;
				return this.ColumnModel.Columns.Count;
			}
		}

		/// <summary>
		/// Returns the index of the currently sorted Column
		/// </summary>
		[Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int SortingColumn
		{
			get { return this.lastSortedColumn; }
		}

		/// <summary>
		/// Gets or sets the background Color for the currently sorted column
		/// </summary>
		[Category("Columns"),
		Description("The background Color for a sorted Column")]
		public Color SortedColumnBackColor
		{
			get { return this.sortedColumnBackColor; }

			set
			{
				if (this.sortedColumnBackColor != value)
				{
					this.sortedColumnBackColor = value;

					if (this.IsValidColumn(this.lastSortedColumn))
					{
						Rectangle columnRect = this.ColumnRect(this.lastSortedColumn);

						if (this.PseudoClientRect.IntersectsWith(columnRect))
							this.Invalidate(Rectangle.Intersect(this.PseudoClientRect, columnRect));
					}
				}
			}
		}

		/// <summary>
		/// Specifies whether the Table's SortedColumnBackColor property 
		/// should be serialized at design time
		/// </summary>
		/// <returns>True if the SortedColumnBackColor property should be 
		/// serialized, False otherwise</returns>
		private bool ShouldSerializeSortedColumnBackColor()
		{
			return this.sortedColumnBackColor != Color.WhiteSmoke;
		}
		#endregion

		#region DisplayRectangle
		/// <summary>
		/// Gets the rectangle that represents the display area of the Table
		/// </summary>
		[Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Rectangle DisplayRectangle
		{
			get
			{
				Rectangle displayRect = this.CellDataRect;

				if (!this.init)
				{
					displayRect.X -= this.hScrollBar.Value;
					displayRect.Y -= this.vScrollBar.Value;
				}

				if (this.ColumnModel == null)
					return displayRect;

				//by netus 2006-02-07
				if (this.ColumnModel.VisibleColumnsWidth <= this.CellDataRect.Width)
					displayRect.Width = this.CellDataRect.Width;
				else
					displayRect.Width = this.ColumnModel.VisibleColumnsWidth;

                //HACK?
				//if (!this.RowsCanReachHeight(this.CellDataRect.Height))
					displayRect.Height = this.CellDataRect.Height;
				//else
					//displayRect.Height = this.TotalRowHeight;

				return displayRect;
			}
		}
		#endregion

		#region Editing
		/// <summary>
		/// Gets whether the Table is currently editing a Cell
		/// </summary>
		[Browsable(false)]
		public bool IsEditing
		{
			get { return !this.EditingCell.IsEmpty; }
		}

		/// <summary>
		/// Gets a CellPos that specifies the position of the Cell that 
		/// is currently being edited
		/// </summary>
		[Browsable(false)]
		public CellPos EditingCell
		{
			get { return this.editingCell; }
		}

		/// <summary>
		/// Gets the ICellEditor that is currently being used to edit a Cell
		/// </summary>
		[Browsable(false)]
		public ICellEditor EditingCellEditor
		{
			get { return this.curentCellEditor; }
		}

		/// <summary>
		/// Gets or sets the action that causes editing to be initiated
		/// </summary>
		[Category("Editing"),
		DefaultValue(EditStartAction.DoubleClick),
		Description("The action that causes editing to be initiated")]
		public EditStartAction EditStartAction
		{
			get { return this.editStartAction; }
			set
			{
				if (!Enum.IsDefined(typeof(EditStartAction), value))
					throw new InvalidEnumArgumentException("value", (int) value, typeof(EditStartAction));

				if (this.editStartAction != value)
					this.editStartAction = value;
			}
		}

		/// <summary>
		/// Gets or sets the custom key used to initiate Cell editing
		/// </summary>
		[Category("Editing"),
		DefaultValue(Keys.F5),
		Description("The custom key used to initiate Cell editing")]
		public Keys CustomEditKey
		{
			get { return this.customEditKey; }
			set
			{
				if (this.IsReservedKey(value))
				{
					throw new ArgumentException("CustomEditKey cannot be one of the Table's reserved keys " +
						"(Up arrow, Down arrow, Left arrow, Right arrow, PageUp, " +
						"PageDown, Home, End, Tab)", "value");
				}

				if (this.customEditKey != value)
					this.customEditKey = value;
			}
		}
		#endregion

		#region Grid
		/// <summary>
		/// Gets or sets how grid lines are displayed around rows and columns
		/// </summary>
		[Category("Grid"),
		DefaultValue(GridLines.None),
		Description("Determines how grid lines are displayed around rows and columns")]
		public GridLines GridLines
		{
			get { return this.gridLines; }
			set
			{
				if (!Enum.IsDefined(typeof(GridLines), value))
					throw new InvalidEnumArgumentException("value", (int) value, typeof(GridLines));

				if (this.gridLines != value)
				{
					this.gridLines = value;
					this.Invalidate(this.PseudoClientRect, true);
				}
			}
		}

		/// <summary>
		/// Gets or sets the style of the lines used to draw the grid
		/// </summary>
		[Category("Grid"),
		DefaultValue(GridLineStyle.Solid),
		Description("The style of the lines used to draw the grid")]
		public GridLineStyle GridLineStyle
		{
			get { return this.gridLineStyle; }
			set
			{
				if (!Enum.IsDefined(typeof(GridLineStyle), value))
					throw new InvalidEnumArgumentException("value", (int) value, typeof(GridLineStyle));

				if (this.gridLineStyle != value)
				{
					this.gridLineStyle = value;

					if (this.GridLines != GridLines.None)
						this.Invalidate(this.PseudoClientRect, false);
				}
			}
		}

        /// <summary>
        /// Indicates whether the grid lines should extend beyond the cells that have data.
        /// </summary>
        bool gridLinesContrainedToData = false;

        /// <summary>
        /// Gets or sets whether the grid lines should extend beyond the cells that have data.
        /// </summary>
        [Category("Grid"),
        DefaultValue(true),
        Description("Indicates whether the grid lines should extend beyond the cells that have data")]
        public bool GridLinesContrainedToData
        {
            get { return this.gridLinesContrainedToData; }
            set
            {
                if (this.gridLinesContrainedToData != value)
                {
                    this.gridLinesContrainedToData = value;
                    this.Invalidate(this.PseudoClientRect, false);
                }
            }
        }

		/// <summary>
		/// Gets or sets the Color of the grid lines
		/// </summary>
		[Category("Grid"),
		Description("The color of the grid lines")]
		public Color GridColor
		{
			get { return this.gridColor; }
			set
			{
				if (this.gridColor != value)
				{
					this.gridColor = value;

					if (this.GridLines != GridLines.None)
						this.Invalidate(this.PseudoClientRect, false);
				}
			}
		}

		/// <summary>
		/// Specifies whether the Table's GridColor property 
		/// should be serialized at design time
		/// </summary>
		/// <returns>True if the GridColor property should be 
		/// serialized, False otherwise</returns>
		private bool ShouldSerializeGridColor()
		{
			return (this.GridColor != SystemColors.Control);
		}

		/// <summary>
		/// 
		/// </summary>
		public override Color BackColor
		{
			get { return base.BackColor; }
			set { base.BackColor = value; }
		}

		/// <summary>
		/// Specifies whether the Table's BackColor property 
		/// should be serialized at design time
		/// </summary>
		/// <returns>True if the BackColor property should be 
		/// serialized, False otherwise</returns>
		private bool ShouldSerializeBackColor()
		{
			return (this.BackColor != Color.White);
		}
		#endregion

		#region Header

		/// <summary>
		/// Gets or sets the column header style
		/// </summary>
		[Category("Columns"),
		DefaultValue(ColumnHeaderStyle.Clickable),
		Description("The style of the column headers")]
		public ColumnHeaderStyle HeaderStyle
		{
			get
			{
				return this.headerStyle;
			}

			set
			{
				if (!Enum.IsDefined(typeof(ColumnHeaderStyle), value))
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(ColumnHeaderStyle));
				}

				if (this.headerStyle != value)
				{
					this.headerStyle = value;

					this.pressedColumn = -1;
					this.hotColumn = -1;

					this.Invalidate();
				}
			}
		}

        /// <summary>
        /// Gets or sets the column header alignemnt
        /// </summary>
        [Category("Columns"),
        DefaultValue(false),
        Description("Defines whether the header text uses the column alignment")]
        public bool HeaderAlignWithColumn
        {
            get
            {
                return this.headerAlignWithColumn;
            }

            set
            {
                if (this.headerAlignWithColumn != value)
                {
                    this.headerAlignWithColumn = value;
                }
            }
        }

		/// <summary>
		/// Gets the height of the column headers
		/// </summary>
		[Browsable(false)]
		public int HeaderHeight
		{
			get
			{
				if (this.ColumnModel == null || this.HeaderStyle == ColumnHeaderStyle.None)
				{
					return 0;
				}

				return this.ColumnModel.HeaderHeight;
			}
		}


		/// <summary>
		/// Gets a Rectangle that specifies the size and location of 
		/// the Table's column header area
		/// </summary>
		[Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Rectangle HeaderRectangle
		{
			get
			{
				return new Rectangle(this.BorderWidth, this.BorderWidth, this.PseudoClientRect.Width, this.HeaderHeight);
			}
		}


		/// <summary>
		/// Gets or sets the font used to draw the text in the column headers
		/// </summary>
		[Category("Columns"),
		Description("The font used to draw the text in the column headers")]
		public Font HeaderFont
		{
			get
			{
				return this.headerFont;
			}

			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("HeaderFont cannot be null");
				}

				if (this.headerFont != value)
				{
					this.headerFont = value;

					this.HeaderRenderer.Font = value;

					this.Invalidate(this.HeaderRectangle, false);
				}
			}
		}


		/// <summary>
		/// Specifies whether the Table's HeaderFont property 
		/// should be serialized at design time
		/// </summary>
		/// <returns>True if the HeaderFont property should be 
		/// serialized, False otherwise</returns>
		private bool ShouldSerializeHeaderFont()
		{
			return this.HeaderFont != this.Font;
		}


		/// <summary>
		/// Gets or sets the HeaderRenderer used to draw the Column headers
		/// </summary>
		[Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IHeaderRenderer HeaderRenderer
		{
			get
			{
				if (this.headerRenderer == null)
				{
					this.headerRenderer = new XPHeaderRenderer();
				}

				return this.headerRenderer;
			}

			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("HeaderRenderer cannot be null");
				}

				if (this.headerRenderer != value)
				{
					this.headerRenderer = value;
					this.headerRenderer.Font = this.HeaderFont;

					this.Invalidate(this.HeaderRectangle, false);
				}
			}
		}


		/// <summary>
		/// Gets the ContextMenu used for Column Headers
		/// </summary>
		[Browsable(false)]
		public HeaderContextMenu HeaderContextMenu
		{
			get
			{
				return this.headerContextMenu;
			}
		}


		/// <summary>
		/// Gets or sets whether the HeaderContextMenu is able to be 
		/// displayed when the user right clicks on a Column Header
		/// </summary>
		[Category("Columns"),
		DefaultValue(true),
		Description("Indicates whether the HeaderContextMenu is able to be displayed when the user right clicks on a Column Header")]
		public bool EnableHeaderContextMenu
		{
			get
			{
				return this.HeaderContextMenu.Enabled;
			}

			set
			{
				this.HeaderContextMenu.Enabled = value;
			}
		}

        /// <summary>
        /// Indicates whether the Column Headers are included when determining the minimim width of a column
        /// </summary>
        [Category("Columns"),
        DefaultValue(true),
        Description("Indicates whether the Column Headers are included when determining the minimim width of a column")]
        public bool IncludeHeaderInAutoWidth
        {
            get { return this.includeHeaderInAutoWidth; }
            set { this.includeHeaderInAutoWidth = value;}
        }
		#endregion

		#region Rows
		/// <summary>
		/// Gets or sets the height of each row
		/// </summary>
		[Browsable(false)]
		public int RowHeight
		{
			get { return this.TableModel == null ? 0 : this.TableModel.RowHeight; }
		}

		/*/// <summary>
		/// Gets the combined height of all the rows in the Table
		/// </summary>
		[Browsable(false)]
		protected int TotalRowHeight
		{
			get
			{
				// v1.1.1 fix (jover) - used to error if no rows were added
				if (this.TableModel == null || this.TableModel.Rows.Count == 0)
					return 0;
                return this.RowYDifference(0, this.TableModel.Rows.Count);
			}
		}

		/// <summary>
		/// Gets the combined height of all the rows in the Table 
		/// plus the height of the column headers
		/// </summary>
		[Browsable(false)]
		public int TotalRowAndHeaderHeight
		{
			get	{ return this.TotalRowHeight + this.HeaderHeight; }
		}

        /// <summary>
        /// Gets the combined height of all the rows in the Table 
        /// plus the height of the column headers and the borders (if there are any).
        /// </summary>
        [Browsable(false)]
        public int TotalHeight
        {
            get { return this.TotalRowHeight + this.HeaderHeight + (2 * this.BorderWidth);  }
        }*/

		/// <summary>
		/// Returns the number of Rows in the Table
		/// </summary>
		[Browsable(false)]
		public int RowCount
		{
			get { return this.TableModel == null ? 0 : this.TableModel.Rows.Count; }
		}

		/// <summary>
		/// Gets the number of whole rows that are visible in the Table
		/// </summary>
		[Browsable(false)]
        public int GetVisibleRowCount()
		{
            return this.VisibleRowCountExact();
        }

		/// <summary>
		/// Gets the index of the first visible row in the Table
		/// </summary>
		[Browsable(false)]
		public int TopIndex
		{
			get
			{
				if (this.TableModel == null || this.TableModel.Rows.Count == 0)
					return -1;

				if (this.VScroll)
                    return topIndex;

				return 0;
			}
		}

		/// <summary>
		/// Gets the first visible row in the Table
		/// </summary>
		[Browsable(false)]
		public Row TopItem
		{
			get
			{
				if (this.TableModel == null || this.TableModel.Rows.Count == 0)
					return null;
                else
				    return this.TableModel.Rows[this.TopIndex];
			}
		}

		/// <summary>
		/// Gets or sets the background color of odd-numbered rows in the Table
		/// </summary>
		[Category("Appearance"),
		DefaultValue(typeof(Color), "Transparent"),
		Description("The background color of odd-numbered rows in the Table")]
		public Color AlternatingRowColor
		{
			get { return this.alternatingRowColor; }
			set
			{
				if (this.alternatingRowColor != value)
				{
					this.alternatingRowColor = value;
					this.Invalidate(this.CellDataRect, false);
				}
			}
		}

		// Mateusz [PEYN] Adamus (peyn@tlen.pl)
		// Span of alternate rows
		/// <summary>
		/// Gets or sets the span of alternate colored rows in the Table
		/// </summary>
		[Category("Appearance"),
	    DefaultValue(1),
	    Description("The span of alternate rows in the Table")]
		public int AlternatingRowSpan
		{
			get { return this.alternatingRowSpan; }
			set
			{
				if (this.alternatingRowSpan != value)
				{
					this.alternatingRowSpan = value >= 1 ? value : 1;
					this.Invalidate(this.CellDataRect, false);
				}
			}
		}

        /// <summary>
        /// Gets or sets a value indicating whether all row heights are recalculated after an EndUpdate (only used if Word Wrapping is on).
        /// </summary>
        [Browsable(true)]
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Indicates whether all row heights are recalculated after an EndUpdate (only used if Word Wrapping is on).")]
        public bool AutoCalculateRowHeights
        {
            get { return this.autoCalculateRowHeights; }
            set { this.autoCalculateRowHeights = value; }
        }
        #endregion

		#region Scrolling
		/// <summary>
		/// Gets or sets a value indicating whether the Table will 
		/// allow the user to scroll to any columns or rows placed 
		/// outside of its visible boundaries
		/// </summary>
		[Category("Behavior"),
		DefaultValue(true),
		Description("Indicates whether the Table will display scroll bars if it contains more items than can fit in the client area")]
		public bool Scrollable
		{
			get { return this.scrollable; }
			set
			{
				if (this.scrollable != value)
				{
					this.scrollable = value;
					this.PerformLayout();
				}
			}
		}

		/// <summary>
		/// Gets a value indicating whether the horizontal 
		/// scroll bar is visible
		/// </summary>
		[Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool HScroll
		{
            get { return this.hScrollBar == null ? false : this.hScrollBar.Visible; }
		}

		/// <summary>
		/// Gets a value indicating whether the vertical 
		/// scroll bar is visible
		/// </summary>
		[Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool VScroll
		{
			get { return this.vScrollBar == null ? false : this.vScrollBar.Visible; }
		}
		#endregion

		#region Selection

		/// <summary>
		/// Gets or sets whether cells are allowed to be selected
		/// </summary>
		[Category("Selection"),
		DefaultValue(true),
		Description("Specifies whether cells are allowed to be selected")]
		public bool AllowSelection
		{
			get
			{
				return this.allowSelection;
			}

			set
			{
				if (this.allowSelection != value)
				{
					this.allowSelection = value;

					if (!value && this.TableModel != null)
					{
						this.TableModel.Selections.Clear();
					}
				}
			}
		}


		/// <summary>
		/// Gets or sets whether cells are allowed to be selected by right mouse button (RMB)
		/// </summary>
		[Category("Selection"),
		DefaultValue(false),
		Description("Specifies whether cells are allowed to be selected by RMB")]
		public bool AllowRMBSelection
		{
			get
			{
				return this.allowRMBSelection;
			}

			set
			{
				if (this.allowRMBSelection != value)
				{
					this.allowRMBSelection = value;

					if (!value && this.TableModel != null)
					{
						this.TableModel.Selections.Clear();
					}
				}
			}
		}


		/// <summary>
		/// Gets or sets how selected Cells are drawn by a Table
		/// </summary>
		[Category("Selection"),
		DefaultValue(SelectionStyle.ListView),
		Description("Determines how selected Cells are drawn by a Table")]
		public SelectionStyle SelectionStyle
		{
			get
			{
				return this.selectionStyle;
			}

			set
			{
				if (!Enum.IsDefined(typeof(SelectionStyle), value))
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(SelectionStyle));
				}

				if (this.selectionStyle != value)
				{
					this.selectionStyle = value;

					if (this.TableModel != null)
					{
						//this.Invalidate(Rectangle.Intersect(this.CellDataRect, this.TableModel.Selections.SelectionBounds), false);
						this.Invalidate(this.CellDataRect, false);
					}
				}
			}
		}


		/// <summary>
		/// Gets or sets whether multiple cells are allowed to be selected
		/// </summary>
		[Category("Selection"),
		DefaultValue(false),
		Description("Specifies whether multiple cells are allowed to be selected")]
		public bool MultiSelect
		{
			get
			{
				return this.multiSelect;
			}

			set
			{
				if (this.multiSelect != value)
				{
					this.multiSelect = value;
				}
			}
		}


        /// <summary>
        /// Gets or sets whether clicking on a cell selects the 'family' of rows (i.e. the parent and all children)
        /// Only if FullRowSelect is also true.
        /// </summary>
        [Category("Selection"),
        DefaultValue(false),
        Description("Specifies whether all rows in the family are selected (i.e. parent, children and siblings)")]
        public bool FamilyRowSelect
        {
            get
            {
                return this.familyRowSelect;
            }

            set
            {
                if (this.familyRowSelect != value)
                    this.familyRowSelect = value;
            }
        }

		/// <summary>
		/// Gets or sets whether all other cells in the row are highlighted 
		/// when a cell is selected
		/// </summary>
		[Category("Selection"),
		DefaultValue(false),
		Description("Specifies whether all other cells in the row are highlighted when a cell is selected")]
		public bool FullRowSelect
		{
			get
			{
				return this.fullRowSelect;
			}

			set
			{
				if (this.fullRowSelect != value)
				{
					this.fullRowSelect = value;

					if (this.TableModel != null)
					{
						//this.Invalidate(Rectangle.Intersect(this.CellDataRect, this.TableModel.Selections.SelectionBounds), false);
						this.Invalidate(this.CellDataRect, false);
					}
				}
			}
		}


		/// <summary>
		/// Gets or sets whether highlighting is removed from the selected 
		/// cells when the Table loses focus
		/// </summary>
		[Category("Selection"),
		DefaultValue(false),
		Description("Specifies whether highlighting is removed from the selected cells when the Table loses focus")]
		public bool HideSelection
		{
			get
			{
				return this.hideSelection;
			}

			set
			{
				if (this.hideSelection != value)
				{
					this.hideSelection = value;

					if (!this.Focused && this.TableModel != null)
					{
						//this.Invalidate(Rectangle.Intersect(this.CellDataRect, this.TableModel.Selections.SelectionBounds), false);
						this.Invalidate(this.CellDataRect, false);
					}
				}
			}
		}


		/// <summary>
		/// Gets or sets whether highlighting rectangle is shown in grid
		/// </summary>
		[Category("Selection"),
	    DefaultValue(true),
	    Description("Specifies whether highlighting rectangle is shown in grid")]
		public bool ShowSelectionRectangle
		{
			get
			{
				return this.showSelectionRectangle;
			}

			set
			{
				if (this.showSelectionRectangle != value)
				{
					this.showSelectionRectangle = value;

					if (this.TableModel != null)
					{
						this.Invalidate(this.CellDataRect, false);
					}
				}
			}
		}


		/// <summary>
		/// Gets or sets the background color of a selected cell
		/// </summary>
		[Category("Selection"),
		Description("The background color of a selected cell")]
		public Color SelectionBackColor
		{
			get
			{
				return this.selectionBackColor;
			}

			set
			{
				if (this.selectionBackColor != value)
				{
					this.selectionBackColor = value;

					if (this.TableModel != null)
					{
						//this.Invalidate(Rectangle.Intersect(this.CellDataRect, this.TableModel.Selections.SelectionBounds), false);
						this.Invalidate(this.CellDataRect, false);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the Table's SelectionBackColor property 
		/// should be serialized at design time
		/// </summary>
		/// <returns>True if the SelectionBackColor property should be 
		/// serialized, False otherwise</returns>
		private bool ShouldSerializeSelectionBackColor()
		{
			return (this.selectionBackColor != SystemColors.Highlight);
		}


		/// <summary>
		/// Gets or sets the foreground color of a selected cell
		/// </summary>
		[Category("Selection"),
		Description("The foreground color of a selected cell")]
		public Color SelectionForeColor
		{
			get
			{
				return this.selectionForeColor;
			}

			set
			{
				if (this.selectionForeColor != value)
				{
					this.selectionForeColor = value;

					if (this.TableModel != null)
					{
						//this.Invalidate(Rectangle.Intersect(this.CellDataRect, this.TableModel.Selections.SelectionBounds), false);
						this.Invalidate(this.CellDataRect, false);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the Table's SelectionForeColor property 
		/// should be serialized at design time
		/// </summary>
		/// <returns>True if the SelectionForeColor property should be 
		/// serialized, False otherwise</returns>
		private bool ShouldSerializeSelectionForeColor()
		{
			return (this.selectionForeColor != SystemColors.HighlightText);
		}


		/// <summary>
		/// Gets or sets the background color of a selected cell when the 
		/// Table doesn't have the focus
		/// </summary>
		[Category("Selection"),
		Description("The background color of a selected cell when the Table doesn't have the focus")]
		public Color UnfocusedSelectionBackColor
		{
			get
			{
				return this.unfocusedSelectionBackColor;
			}

			set
			{
				if (this.unfocusedSelectionBackColor != value)
				{
					this.unfocusedSelectionBackColor = value;

					if (!this.Focused && !this.HideSelection && this.TableModel != null)
					{
						//this.Invalidate(Rectangle.Intersect(this.CellDataRect, this.TableModel.Selections.SelectionBounds), false);
						this.Invalidate(this.CellDataRect, false);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the Table's UnfocusedSelectionBackColor property 
		/// should be serialized at design time
		/// </summary>
		/// <returns>True if the UnfocusedSelectionBackColor property should be 
		/// serialized, False otherwise</returns>
		private bool ShouldSerializeUnfocusedSelectionBackColor()
		{
			return (this.unfocusedSelectionBackColor != SystemColors.Control);
		}


		/// <summary>
		/// Gets or sets the foreground color of a selected cell when the 
		/// Table doesn't have the focus
		/// </summary>
		[Category("Selection"),
		Description("The foreground color of a selected cell when the Table doesn't have the focus")]
		public Color UnfocusedSelectionForeColor
		{
			get
			{
				return this.unfocusedSelectionForeColor;
			}

			set
			{
				if (this.unfocusedSelectionForeColor != value)
				{
					this.unfocusedSelectionForeColor = value;

					if (!this.Focused && !this.HideSelection && this.TableModel != null)
					{
						//this.Invalidate(Rectangle.Intersect(this.CellDataRect, this.TableModel.Selections.SelectionBounds), false);
						this.Invalidate(this.CellDataRect, false);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the Table's UnfocusedSelectionForeColor property 
		/// should be serialized at design time
		/// </summary>
		/// <returns>True if the UnfocusedSelectionForeColor property should be 
		/// serialized, False otherwise</returns>
		private bool ShouldSerializeUnfocusedSelectionForeColor()
		{
			return (this.unfocusedSelectionForeColor != SystemColors.ControlText);
		}


		/// <summary>
		/// Gets an array that contains the currently selected Rows
		/// </summary>
		[Browsable(false)]
		public Row[] SelectedItems
		{
			get
			{
				if (this.TableModel == null)
				{
					return new Row[0];
				}

				return this.TableModel.Selections.SelectedItems;
			}
		}


		/// <summary>
		/// Gets an array that contains the indexes of the currently selected Rows
		/// </summary>
		[Browsable(false)]
		public int[] SelectedIndicies
		{
			get
			{
				if (this.TableModel == null)
				{
					return new int[0];
				}

				return this.TableModel.Selections.SelectedIndicies;
			}
		}
		#endregion

		#region Sorting
		/// <summary>
		/// 
		/// </summary>
        [Browsable(true)]
        [Category("Behavior")]
        [DefaultValue(SortType.AutoSort)]
        //      [DisplayName("Sort Type")]                  // .Net 2.0 only
        [Description("Select the type of sort to be used - use AutoSort to have system determine based on number of rows and whether or not a stable sort is required")]
        public SortType SortType
        {
            get { return this.theSortType; }
            set { this.theSortType = value; }
        }

		/// <summary>
		/// 
		/// </summary>
        [Browsable(true)]
        [Category("Behavior")]
        [DefaultValue(true)]
        //		[DisplayName("Stable Sort Required ?")]     // .Net 2.0 only
        [Description("Only relevant if AutoSort is specified, determines whether or not the system uses a stable sorting alogorithm")]
        public bool StableSort
        {
            get { return this.theStableSort; }
            set { this.theStableSort = value; }
        }
		#endregion

		#region TableModel

		/// <summary>
		/// Gets or sets the TableModel that contains all the Rows
		/// and Cells displayed in the Table
		/// </summary>
		[Category("Items"),
		DefaultValue(null),
		Description("Specifies the TableModel that contains all the Rows and Cells displayed in the Table")]
		public TableModel TableModel
		{
			get
			{
				return this.tableModel;
			}

			set
			{
				if (this.tableModel != value)
				{
					if (this.tableModel != null && this.tableModel.Table == this)
					{
						this.tableModel.InternalTable = null;
					}

					TableModel oldValue = this.tableModel;// PJD TEA add

					this.tableModel = value;

					if (value != null)
					{
						value.InternalTable = this;

						if (this.lastVScrollValue > this.tableModel.Rows.Count)
						{
							this.lastVScrollValue = this.topIndex = 0;
							this.vScrollBar.Value = 0;
						}
					}

					this.OnTableModelChanged(new TableEventArgs(this, TableEventType.TableModelChanged, oldValue));// PJD TEA change
				}
			}
		}


		/// <summary>
		/// Gets or sets the text displayed by the Table when it doesn't 
		/// contain any items
		/// </summary>
		[Category("Appearance"),
		DefaultValue("There are no items in this view"),
		Description("Specifies the text displayed by the Table when it doesn't contain any items"),
		Localizable(true)]
		public string NoItemsText
		{
			get
			{
				return this.noItemsText;
			}

			set
			{
				if (!this.noItemsText.Equals(value))
				{
					this.noItemsText = value;

					if (this.ColumnModel == null || this.TableModel == null || this.TableModel.Rows.Count == 0)
					{
						this.Invalidate(this.PseudoClientRect);
					}
				}
			}
		}

		#endregion

		#region TableState

		/// <summary>
		/// Gets or sets the current state of the Table
		/// </summary>
		protected TableState TableState
		{
            get { return this.tableState; }
            set { this.tableState = value; }
        }

		/// <summary>
		/// Calculates the state of the Table at the specified 
		/// client coordinates
		/// </summary>
		/// <param name="x">The client x coordinate</param>
		/// <param name="y">The client y coordinate</param>
		protected void CalcTableState(int x, int y)
		{
			TableRegion region = this.HitTest(x, y);

			// are we in the header
			if (region == TableRegion.ColumnHeader)
			{
				int column = this.ColumnIndexAt(x, y);

				// get out of here if we aren't in a column
				if (column == -1)
				{
					this.TableState = TableState.Normal;

					return;
				}

				// get the bounding rectangle for the column's header
				Rectangle columnRect = this.ColumnModel.ColumnHeaderRect(column);
				x = this.ClientToDisplayRect(x, y).X;

				// are we in a resizing section on the left
				if (x < columnRect.Left + Column.ResizePadding)
				{
					this.TableState = TableState.ColumnResizing;

					while (column != 0)
					{
						if (this.ColumnModel.Columns[column - 1].Visible)
						{
							break;
						}

						column--;
					}

					// if we are in the first visible column or the next column 
					// to the left is disabled, then we should be potentialy 
					// selecting instead of resizing
					if (column == 0 || !this.ColumnModel.Columns[column - 1].Enabled)
					{
						this.TableState = TableState.ColumnSelecting;
					}

					// Mateusz [PEYN] Adamus (peyn@tlen.pl)
					// If next column to the left has RESIZABLE set to false
					// then we can't resize it
					if ((column != 0)
						&& (!this.ColumnModel.Columns[column - 1].Resizable))
					{
						this.TableState = TableState.ColumnSelecting;
					}
				}
				// or a resizing section on the right
				else if ((x > columnRect.Right - Column.ResizePadding)
					// Mateusz [PEYN] Adamus (peyn@tlen.pl)
					// but only if column can be resized
					&& (this.ColumnModel.Columns[column].Resizable))
				{
					this.TableState = TableState.ColumnResizing;
				}
				// looks like we're somewhere in the middle of 
				// the column header
				else
				{
					this.TableState = TableState.ColumnSelecting;
				}
			}
			else if (region == TableRegion.Cells)
			{
				this.TableState = TableState.Selecting;
			}
			else
			{
				this.TableState = TableState.Normal;
			}

			if (this.TableState == TableState.ColumnResizing && !this.ColumnResizing)
			{
				this.TableState = TableState.ColumnSelecting;
			}
		}

        public bool EventsDisabled = false;
		/// <summary>
		/// Gets whether the Table is able to raise events
		/// </summary>
        protected internal bool CanRaiseEvents
        {
            get { return !EventsDisabled && (this.IsHandleCreated && this.beginUpdateCount == 0); }
        }

        /// <summary>
        /// Gets the value for CanRaiseEvents.
        /// </summary>
        protected internal bool CanRaiseEventsInternal
        {
            get { return this.CanRaiseEvents; }
        }

		/// <summary>
		/// Gets or sets whether the Table is being used as a preview Table in 
		/// a ColumnCollectionEditor
		/// </summary>
		internal bool Preview
		{
            get { return this.preview; }
            set { this.preview = value; }
        }
		#endregion

		#region Word wrapping
		/// <summary>
		/// Gets of sets whether word wrap is allowed in any cell in the table. If false then the WordWrap property on Cells is ignored.
		/// </summary>
		[Category("Appearance"),
		DefaultValue(false),
		Description("Specifies whether any cells are allowed to word-wrap.")]
		public bool EnableWordWrap
		{
			get { return enableWordWrap; }
			set { enableWordWrap = value; }
		}
		#endregion

		#region ToolTips
        /// <summary>
		/// Gets the internal tooltip component
		/// </summary>
		internal ToolTip ToolTip
		{
			get { return this.toolTip; }
		}
        
		/// <summary>
		/// Gets or sets whether ToolTips are currently enabled for the Table
		/// </summary>
		[Category("ToolTips"),
		DefaultValue(false),
		Description("Specifies whether ToolTips are enabled for the Table.")]
		public bool EnableToolTips
		{
			get { return this.toolTip.Active; }
			set { this.toolTip.Active = value; }
		}

		/// <summary>
		/// Gets or sets the automatic delay for the Table's ToolTip
		/// </summary>
		[Category("ToolTips"),
		DefaultValue(500),
		Description("Specifies the automatic delay for the Table's ToolTip.")]
		public int ToolTipAutomaticDelay
		{
			get { return this.toolTip.AutomaticDelay; }
			set
			{
				if (value > 0 && this.toolTip.AutomaticDelay != value)
					this.toolTip.AutomaticDelay = value;
			}
		}

		/// <summary>
		/// Gets or sets the period of time the Table's ToolTip remains visible if 
		/// the mouse pointer is stationary within a Cell with a valid ToolTip text
		/// </summary>
		[Category("ToolTips"),
		DefaultValue(5000),
		Description("Specifies the period of time the Table's ToolTip remains visible if the mouse pointer is stationary within a cell with specified ToolTip text.")]
		public int ToolTipAutoPopDelay
		{
			get { return this.toolTip.AutoPopDelay; }
			set
			{
				if (value > 0 && this.toolTip.AutoPopDelay != value)
					this.toolTip.AutoPopDelay = value;
			}
		}

		/// <summary>
		/// Gets or sets the time that passes before the Table's ToolTip appears
		/// </summary>
		[Category("ToolTips"),
		DefaultValue(1000),
		Description("Specifies the time that passes before the Table's ToolTip appears.")]
		public int ToolTipInitialDelay
		{
			get { return this.toolTip.InitialDelay; }
			set
			{
				if (value > 0 && this.toolTip.InitialDelay != value)
					this.toolTip.InitialDelay = value;
			}
		}

		/// <summary>
		/// Gets or sets whether the Table's ToolTip window is 
		/// displayed even when its parent control is not active
		/// </summary>
		[Category("ToolTips"),
		DefaultValue(false),
		Description("Specifies whether the Table's ToolTip window is displayed even when its parent control is not active.")]
		public bool ToolTipShowAlways
		{
			get { return this.toolTip.ShowAlways; }
			set
			{
				if (this.toolTip.ShowAlways != value)
					this.toolTip.ShowAlways = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void ResetToolTip()
		{
			bool tooltipActive = this.ToolTip.Active;

			if (tooltipActive)
				this.ToolTip.Active = false;

			this.ResetMouseEventArgs();

			this.ToolTip.SetToolTip(this, null);

			if (tooltipActive)
				this.ToolTip.Active = true;
		}
		#endregion

        #region Drag drop
        /// <summary>
        /// Gets or sets the renderer that draws the drag drop hover indicator.
        /// </summary>
        public IDragDropRenderer DragDropRenderer
        {
            get { return _dragDropHelper.DragDropRenderer; }
            set { _dragDropHelper.DragDropRenderer = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use built in drag drop.
        /// NOTE this disables the drag drop rendering functionality, but still 
        /// requires DragDropExternalTypeEffect and DragDropExternalTypeEvent 
        /// to be used.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [use built in drag drop]; otherwise, <c>false</c>.
        /// </value>
        [Category("Behavior"),
        DefaultValue(true),
        Description("Use the built in drag/drop functionality")]
        public bool UseBuiltInDragDrop
        {
            get { return useBuiltInDragDrop; }
            set { useBuiltInDragDrop = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether an external drop removes the dragged rows.
        /// This effectively changes the DragDrop between 2 tables from a Move (the 
        /// default) to a Copy operation.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [external drop removes rows]; otherwise, <c>false</c>.
        /// </value>
        [Category("Behavior"),
        DefaultValue(true),
        Description("If true drag/drop between 2 tables is a move operation")]
        public bool ExternalDropRemovesRows
        {
            get { return externalDropRemovesRows; }
            set { externalDropRemovesRows = value; }
        }
        #endregion

        #endregion

        #region Events

        #region Cells

        /// <summary>
		/// Raises the CellPropertyChanged event
		/// </summary>
		/// <param name="e">A CellEventArgs that contains the event data</param>
		protected internal virtual void OnCellPropertyChanged(CellEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				this.InvalidateCell(e.Row, e.Column);

				if (CellPropertyChanged != null)
				{
					CellPropertyChanged(this, e);
				}

				if (e.EventType == CellEventType.CheckStateChanged)
				{
					this.OnCellCheckChanged(new CellCheckBoxEventArgs(e.Cell, e.Column, e.Row));
				}
			}
		}


		/// <summary>
		/// Handler for a Cells PropertyChanged event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">A CellEventArgs that contains the event data</param>
		private void cell_PropertyChanged(object sender, CellEventArgs e)
		{
			this.OnCellPropertyChanged(e);
		}


		#region Buttons

		/// <summary>
		/// Raises the CellButtonClicked event
		/// </summary>
		/// <param name="e">A CellButtonEventArgs that contains the event data</param>
		protected internal virtual void OnCellButtonClicked(CellButtonEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				if (CellButtonClicked != null)
				{
					CellButtonClicked(this, e);
				}
			}
		}

		#endregion

		#region CheckBox

		/// <summary>
		/// Raises the CellCheckChanged event
		/// </summary>
		/// <param name="e">A CellCheckChanged that contains the event data</param>
		protected internal virtual void OnCellCheckChanged(CellCheckBoxEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				if (CellCheckChanged != null)
				{
					CellCheckChanged(this, e);
				}
			}
		}

		#endregion

		#region Focus

		/// <summary>
		/// Raises the CellGotFocus event
		/// </summary>
		/// <param name="e">A CellFocusEventArgs that contains the event data</param>
		protected virtual void OnCellGotFocus(CellFocusEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				ICellRenderer renderer = this.ColumnModel.GetCellRenderer(e.Column);

				if (renderer != null)
				{
					renderer.OnGotFocus(e);
				}

				if (CellGotFocus != null)
				{
					CellGotFocus(this, e);
				}
			}
		}


		/// <summary>
		/// Raises the GotFocus event for the Cell at the specified position
		/// </summary>
		/// <param name="cellPos">The position of the Cell that gained focus</param>
		protected void RaiseCellGotFocus(CellPos cellPos)
		{
			if (!this.IsValidCell(cellPos))
			{
				return;
			}

			ICellRenderer renderer = this.ColumnModel.GetCellRenderer(cellPos.Column);

			if (renderer != null)
			{
				Cell cell = null;

				if (cellPos.Column < this.TableModel.Rows[cellPos.Row].Cells.Count)
				{
					cell = this.TableModel.Rows[cellPos.Row].Cells[cellPos.Column];
				}

				CellFocusEventArgs cfea = new CellFocusEventArgs(cell, this, cellPos.Row, cellPos.Column, this.CellRect(cellPos.Row, cellPos.Column));

				this.OnCellGotFocus(cfea);
			}
		}


		/// <summary>
		/// Raises the CellLostFocus event
		/// </summary>
		/// <param name="e">A CellFocusEventArgs that contains the event data</param>
		protected virtual void OnCellLostFocus(CellFocusEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				ICellRenderer renderer = this.ColumnModel.GetCellRenderer(e.Column);

				if (renderer != null)
				{
					renderer.OnLostFocus(e);
				}

				if (CellLostFocus != null)
				{
					CellLostFocus(this, e);
				}
			}
		}


		/// <summary>
		/// Raises the LostFocus event for the Cell at the specified position
		/// </summary>
		/// <param name="cellPos">The position of the Cell that lost focus</param>
		protected void RaiseCellLostFocus(CellPos cellPos)
		{
			if (!this.IsValidCell(cellPos))
			{
				return;
			}

			ICellRenderer renderer = this.ColumnModel.GetCellRenderer(cellPos.Column);

			if (renderer != null)
			{
				Cell cell = null;

				if (cellPos.Column < this.TableModel.Rows[cellPos.Row].Cells.Count)
				{
					cell = this.TableModel[cellPos.Row, cellPos.Column];
				}

				CellFocusEventArgs cfea = new CellFocusEventArgs(cell, this, cellPos.Row, cellPos.Column, this.CellRect(cellPos.Row, cellPos.Column));

				this.OnCellLostFocus(cfea);
			}
		}

		#endregion

		#region Keys

		/// <summary>
		/// Raises the CellKeyDown event
		/// </summary>
		/// <param name="e">A CellKeyEventArgs that contains the event data</param>
		protected virtual void OnCellKeyDown(CellKeyEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				ICellRenderer renderer = this.ColumnModel.GetCellRenderer(e.Column);

				if (renderer != null)
				{
					renderer.OnKeyDown(e);
				}

				if (CellKeyDown != null)
				{
					CellKeyDown(e.Cell, e);
				}
			}
		}


		/// <summary>
		/// Raises a KeyDown event for the Cell at the specified cell position
		/// </summary>
		/// <param name="cellPos">The position of the Cell</param>
		/// <param name="e">A KeyEventArgs that contains the event data</param>
		protected void RaiseCellKeyDown(CellPos cellPos, KeyEventArgs e)
		{
			if (!this.IsValidCell(cellPos))
			{
				return;
			}

			if (!this.TableModel[cellPos].Enabled)
			{
				return;
			}

			if (this.ColumnModel.GetCellRenderer(cellPos.Column) != null)
			{
				Cell cell = null;

				if (cellPos.Column < this.TableModel.Rows[cellPos.Row].Cells.Count)
				{
					cell = this.TableModel.Rows[cellPos.Row].Cells[cellPos.Column];
				}

				CellKeyEventArgs ckea = new CellKeyEventArgs(cell, this, cellPos, this.CellRect(cellPos.Row, cellPos.Column), e);

				this.OnCellKeyDown(ckea);
			}
		}


		/// <summary>
		/// Raises the CellKeyUp event
		/// </summary>
		/// <param name="e">A CellKeyEventArgs that contains the event data</param>
		protected virtual void OnCellKeyUp(CellKeyEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				ICellRenderer renderer = this.ColumnModel.GetCellRenderer(e.Column);

				if (renderer != null)
				{
					renderer.OnKeyUp(e);
				}

				if (CellKeyUp != null)
				{
					CellKeyUp(e.Cell, e);
				}
			}
		}


		/// <summary>
		/// Raises a KeyUp event for the Cell at the specified cell position
		/// </summary>
		/// <param name="cellPos">The position of the Cell</param>
		/// <param name="e">A KeyEventArgs that contains the event data</param>
		protected void RaiseCellKeyUp(CellPos cellPos, KeyEventArgs e)
		{
			if (!this.IsValidCell(cellPos))
			{
				return;
			}

			if (!this.TableModel[cellPos].Enabled)
			{
				return;
			}

			if (this.ColumnModel.GetCellRenderer(cellPos.Column) != null)
			{
				Cell cell = null;

				if (cellPos.Column < this.TableModel.Rows[cellPos.Row].Cells.Count)
				{
					cell = this.TableModel.Rows[cellPos.Row].Cells[cellPos.Column];
				}

				CellKeyEventArgs ckea = new CellKeyEventArgs(cell, this, cellPos, this.CellRect(cellPos.Row, cellPos.Column), e);

				this.OnCellKeyUp(ckea);
			}
		}

		#endregion

		#region Mouse

		#region MouseEnter

		/// <summary>
		/// Raises the CellMouseEnter event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		protected virtual void OnCellMouseEnter(CellMouseEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				ICellRenderer renderer = this.ColumnModel.GetCellRenderer(e.Column);

				if (renderer != null)
				{
					renderer.OnMouseEnter(e);
				}

				if (CellMouseEnter != null)
				{
					CellMouseEnter(e.Cell, e);
				}
			}
		}


		/// <summary>
		/// Raises a MouseEnter event for the Cell at the specified cell position
		/// </summary>
		/// <param name="cellPos">The position of the Cell</param>
		protected void RaiseCellMouseEnter(CellPos cellPos)
		{
			if (!this.IsValidCell(cellPos))
			{
				return;
			}

			if (this.ColumnModel.GetCellRenderer(cellPos.Column) != null)
			{
				Cell cell = null;

				if (cellPos.Column < this.TableModel.Rows[cellPos.Row].Cells.Count)
				{
					cell = this.TableModel.Rows[cellPos.Row].Cells[cellPos.Column];
				}

				CellMouseEventArgs mcea = new CellMouseEventArgs(cell, this, cellPos.Row, cellPos.Column, this.CellRect(cellPos.Row, cellPos.Column));

				this.OnCellMouseEnter(mcea);
			}
		}

		#endregion

		#region MouseLeave

		/// <summary>
		/// Raises the CellMouseLeave event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		protected virtual void OnCellMouseLeave(CellMouseEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				ICellRenderer renderer = this.ColumnModel.GetCellRenderer(e.Column);

				if (renderer != null)
				{
					renderer.OnMouseLeave(e);
				}

				if (CellMouseLeave != null)
				{
					CellMouseLeave(e.Cell, e);
				}
			}
		}


		/// <summary>
		/// Raises a MouseLeave event for the Cell at the specified cell position
		/// </summary>
		/// <param name="cellPos">The position of the Cell</param>
		protected internal void RaiseCellMouseLeave(CellPos cellPos)
		{
			if (!this.IsValidCell(cellPos))
			{
				return;
			}

			if (this.ColumnModel.GetCellRenderer(cellPos.Column) != null)
			{
				Cell cell = null;

				if (cellPos.Column < this.TableModel.Rows[cellPos.Row].Cells.Count)
				{
					cell = this.TableModel.Rows[cellPos.Row].Cells[cellPos.Column];
				}

				CellMouseEventArgs mcea = new CellMouseEventArgs(cell, this, cellPos.Row, cellPos.Column, this.CellRect(cellPos.Row, cellPos.Column));

				this.OnCellMouseLeave(mcea);
			}
		}

		#endregion

		#region MouseUp

		/// <summary>
		/// Raises the CellMouseUp event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		protected virtual void OnCellMouseUp(CellMouseEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				ICellRenderer renderer = this.ColumnModel.GetCellRenderer(e.Column);

				if (renderer != null)
				{
					renderer.OnMouseUp(e);
				}

				if (CellMouseUp != null)
				{
					CellMouseUp(e.Cell, e);
				}
			}
		}


		/// <summary>
		/// Raises a MouseUp event for the Cell at the specified cell position
		/// </summary>
		/// <param name="cellPos">The position of the Cell</param>
		/// <param name="e">A MouseEventArgs that contains the event data</param>
		protected void RaiseCellMouseUp(CellPos cellPos, MouseEventArgs e)
		{
			if (!this.IsValidCell(cellPos))
			{
				return;
			}

			if (!this.TableModel[cellPos].Enabled)
			{
				return;
			}

			if (this.ColumnModel.GetCellRenderer(cellPos.Column) != null)
			{
				Cell cell = null;

				if (cellPos.Column < this.TableModel.Rows[cellPos.Row].Cells.Count)
				{
					cell = this.TableModel.Rows[cellPos.Row].Cells[cellPos.Column];
				}

				CellMouseEventArgs mcea = new CellMouseEventArgs(cell, this, cellPos.Row, cellPos.Column, this.CellRect(cellPos.Row, cellPos.Column), e);

				this.OnCellMouseUp(mcea);
			}
		}

		#endregion

		#region MouseDown

		/// <summary>
		/// Raises the CellMouseDown event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		protected virtual void OnCellMouseDown(CellMouseEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				ICellRenderer renderer = this.ColumnModel.GetCellRenderer(e.Column);

				if (renderer != null)
				{
					renderer.OnMouseDown(e);
				}

				if (CellMouseDown != null)
				{
					CellMouseDown(e.Cell, e);
				}
			}
		}


		/// <summary>
		/// Raises a MouseDown event for the Cell at the specified cell position
		/// </summary>
		/// <param name="cellPos">The position of the Cell</param>
		/// <param name="e">A MouseEventArgs that contains the event data</param>
		protected void RaiseCellMouseDown(CellPos cellPos, MouseEventArgs e)
		{
			if (!this.IsValidCell(cellPos))
			{
				return;
			}

			if (!this.TableModel[cellPos].Enabled)
			{
				return;
			}

			if (this.ColumnModel.GetCellRenderer(cellPos.Column) != null)
			{
				Cell cell = null;

				if (cellPos.Column < this.TableModel.Rows[cellPos.Row].Cells.Count)
				{
					cell = this.TableModel.Rows[cellPos.Row].Cells[cellPos.Column];
				}

				CellMouseEventArgs mcea = new CellMouseEventArgs(cell, this, cellPos.Row, cellPos.Column, this.CellRect(cellPos.Row, cellPos.Column), e);

				this.OnCellMouseDown(mcea);
			}
		}

		#endregion

		#region MouseMove

		/// <summary>
		/// Raises the CellMouseMove event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		protected virtual void OnCellMouseMove(CellMouseEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				ICellRenderer renderer = this.ColumnModel.GetCellRenderer(e.Column);

				if (renderer != null)
				{
					renderer.OnMouseMove(e);
				}

				if (CellMouseMove != null)
				{
					CellMouseMove(e.Cell, e);
				}
			}
		}


		/// <summary>
		/// Raises a MouseMove event for the Cell at the specified cell position
		/// </summary>
		/// <param name="cellPos">The position of the Cell</param>
		/// <param name="e">A MouseEventArgs that contains the event data</param>
		protected void RaiseCellMouseMove(CellPos cellPos, MouseEventArgs e)
		{
			if (!this.IsValidCell(cellPos))
			{
				return;
			}

			if (this.ColumnModel.GetCellRenderer(cellPos.Column) != null)
			{
				Cell cell = null;

				if (cellPos.Column < this.TableModel.Rows[cellPos.Row].Cells.Count)
				{
					cell = this.TableModel.Rows[cellPos.Row].Cells[cellPos.Column];
				}

				CellMouseEventArgs mcea = new CellMouseEventArgs(cell, this, cellPos.Row, cellPos.Column, this.CellRect(cellPos.Row, cellPos.Column), e);

				this.OnCellMouseMove(mcea);
			}
		}


		/// <summary>
		/// Resets the last known cell position that the mouse was over to empty
		/// </summary>
		internal void ResetLastMouseCell()
		{
			if (!this.lastMouseCell.IsEmpty)
			{
				this.ResetMouseEventArgs();

				CellPos oldLastMouseCell = this.lastMouseCell;
				this.lastMouseCell = CellPos.Empty;

				this.RaiseCellMouseLeave(oldLastMouseCell);
			}
		}

		#endregion

		#region MouseHover

		/// <summary>
		/// Raises the CellHover event
		/// </summary>
		/// <param name="e">A CellEventArgs that contains the event data</param>
		protected virtual void OnCellMouseHover(CellMouseEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				if (CellMouseHover != null)
				{
					CellMouseHover(e.Cell, e);
				}
			}
		}

		#endregion

		#region Click
		/// <summary>
		/// Raises the CellClick event
		/// </summary>
		/// <param name="e">A CellEventArgs that contains the event data</param>
		protected virtual void OnCellClick(CellMouseEventArgs e)
		{
			if (!this.IsCellEnabled(e.CellPos))
				return;

			if (this.CanRaiseEvents)
			{
				ICellRenderer renderer = this.ColumnModel.GetCellRenderer(this.LastMouseCell.Column);

				if (renderer != null)
					renderer.OnClick(e);

				if (CellClick != null)
					CellClick(e.Cell, e);
			}
		}

		/// <summary>
		/// Raises the CellDoubleClick event
		/// </summary>
		/// <param name="e">A CellEventArgs that contains the event data</param>
		protected virtual void OnCellDoubleClick(CellMouseEventArgs e)
		{
			if (!this.IsCellEnabled(e.CellPos))
				return;

			if (this.CanRaiseEvents)
			{
				ICellRenderer renderer = this.ColumnModel.GetCellRenderer(this.LastMouseCell.Column);

				if (renderer != null)
					renderer.OnDoubleClick(e);

				if (CellDoubleClick != null)
					CellDoubleClick(e.Cell, e);
			}
		}
		#endregion

		#endregion

		#endregion

		#region Columns

		/// <summary>
		/// Raises the ColumnPropertyChanged event
		/// </summary>
		/// <param name="e">A ColumnEventArgs that contains the event data</param>
		protected internal virtual void OnColumnPropertyChanged(ColumnEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				Rectangle columnHeaderRect;

				if (e.Index != -1)
				{
					columnHeaderRect = this.ColumnHeaderRect(e.Index);
				}
				else
				{
					columnHeaderRect = this.ColumnHeaderRect(e.Column);
				}

				switch (e.EventType)
				{
					case ColumnEventType.VisibleChanged:
					case ColumnEventType.WidthChanged:
					{
						if (e.EventType == ColumnEventType.VisibleChanged)
						{
							if (e.Column.Visible && e.Index != this.lastSortedColumn)
							{
								e.Column.InternalSortOrder = SortOrder.None;
							}

							if (e.Index == this.FocusedCell.Column && !e.Column.Visible)
							{
								int index = this.ColumnModel.NextVisibleColumn(e.Index);

								if (index == -1)
								{
									index = this.ColumnModel.PreviousVisibleColumn(e.Index);
								}

								if (index != -1)
								{
									this.FocusedCell = new CellPos(this.FocusedCell.Row, index);
								}
								else
								{
									this.FocusedCell = CellPos.Empty;
								}
							}
						}

						if (columnHeaderRect.X <= 0)
						{
							this.Invalidate(this.PseudoClientRect);
						}
						else if (columnHeaderRect.Left <= this.PseudoClientRect.Right)
						{
							this.Invalidate(new Rectangle(columnHeaderRect.X,
								this.PseudoClientRect.Top,
								this.PseudoClientRect.Right - columnHeaderRect.X,
								this.PseudoClientRect.Height));
						}

						this.UpdateScrollBars();

						break;
					}

					case ColumnEventType.TextChanged:
					case ColumnEventType.StateChanged:
					case ColumnEventType.ImageChanged:
					case ColumnEventType.HeaderAlignmentChanged:
					{
						if (columnHeaderRect.IntersectsWith(this.HeaderRectangle))
						{
							this.Invalidate(columnHeaderRect);
						}

						break;
					}

					case ColumnEventType.AlignmentChanged:
					case ColumnEventType.RendererChanged:
					case ColumnEventType.EnabledChanged:
					{
						if (e.EventType == ColumnEventType.EnabledChanged)
						{
							if (e.Index == this.FocusedCell.Column)
							{
								this.FocusedCell = CellPos.Empty;
							}
						}

						if (columnHeaderRect.IntersectsWith(this.HeaderRectangle))
						{
							this.Invalidate(new Rectangle(columnHeaderRect.X,
								this.PseudoClientRect.Top,
								columnHeaderRect.Width,
								this.PseudoClientRect.Height));
						}

						break;
					}
				}

				if (ColumnPropertyChanged != null)
				{
					ColumnPropertyChanged(e.Column, e);
				}
			}
		}

        /// <summary>
        /// Raises the ColumnAutoResize event.
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnColumnAutoResize(ColumnEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                int w = GetAutoColumnWidth(e.Index);
                if (w > 0)
                {
                    if (e.Column.Width != w + 5)
                        this.Invalidate();
                    e.Column.Width = w + 5;
                }

                if (ColumnAutoResize != null)
                    ColumnAutoResize(e.Column, e);
            }
        }

		#endregion

		#region Column Headers

		#region MouseEnter

		/// <summary>
		/// Raises the HeaderMouseEnter event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		protected virtual void OnHeaderMouseEnter(HeaderMouseEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				if (this.HeaderRenderer != null)
				{
					this.HeaderRenderer.OnMouseEnter(e);
				}

				if (HeaderMouseEnter != null)
				{
					HeaderMouseEnter(e.Column, e);
				}
			}
		}


		/// <summary>
		/// Raises a MouseEnter event for the Column header at the specified colunm 
		/// index position
		/// </summary>
		/// <param name="index">The index of the column to recieve the event</param>
		protected void RaiseHeaderMouseEnter(int index)
		{
			if (index < 0 || this.ColumnModel == null || index >= this.ColumnModel.Columns.Count)
			{
				return;
			}

			if (this.HeaderRenderer != null)
			{
				Column column = this.ColumnModel.Columns[index];

				HeaderMouseEventArgs mhea = new HeaderMouseEventArgs(column, this, index, this.DisplayRectToClient(this.ColumnModel.ColumnHeaderRect(index)));

				this.OnHeaderMouseEnter(mhea);
			}
		}

		#endregion

		#region MouseLeave

		/// <summary>
		/// Raises the HeaderMouseLeave event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		protected virtual void OnHeaderMouseLeave(HeaderMouseEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				if (this.HeaderRenderer != null)
				{
					this.HeaderRenderer.OnMouseLeave(e);
				}

				if (HeaderMouseLeave != null)
				{
					HeaderMouseLeave(e.Column, e);
				}
			}
		}


		/// <summary>
		/// Raises a MouseLeave event for the Column header at the specified colunm 
		/// index position
		/// </summary>
		/// <param name="index">The index of the column to recieve the event</param>
		protected void RaiseHeaderMouseLeave(int index)
		{
			if (index < 0 || this.ColumnModel == null || index >= this.ColumnModel.Columns.Count)
			{
				return;
			}

			if (this.HeaderRenderer != null)
			{
				Column column = this.ColumnModel.Columns[index];

				HeaderMouseEventArgs mhea = new HeaderMouseEventArgs(column, this, index, this.DisplayRectToClient(this.ColumnModel.ColumnHeaderRect(index)));

				this.OnHeaderMouseLeave(mhea);
			}
		}

		#endregion

		#region MouseUp

		/// <summary>
		/// Raises the HeaderMouseUp event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		protected virtual void OnHeaderMouseUp(HeaderMouseEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				if (this.HeaderRenderer != null)
				{
					this.HeaderRenderer.OnMouseUp(e);
				}

				if (HeaderMouseUp != null)
				{
					HeaderMouseUp(e.Column, e);
				}
			}
		}


		/// <summary>
		/// Raises a MouseUp event for the Column header at the specified colunm 
		/// index position
		/// </summary>
		/// <param name="index">The index of the column to recieve the event</param>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		protected void RaiseHeaderMouseUp(int index, MouseEventArgs e)
		{
			if (index < 0 || this.ColumnModel == null || index >= this.ColumnModel.Columns.Count)
			{
				return;
			}

			if (this.HeaderRenderer != null)
			{
				Column column = this.ColumnModel.Columns[index];

				HeaderMouseEventArgs mhea = new HeaderMouseEventArgs(column, this, index, this.DisplayRectToClient(this.ColumnModel.ColumnHeaderRect(index)), e);

				this.OnHeaderMouseUp(mhea);
			}
		}

		#endregion

		#region MouseDown

		/// <summary>
		/// Raises the HeaderMouseDown event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		protected virtual void OnHeaderMouseDown(HeaderMouseEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				if (this.HeaderRenderer != null)
				{
					this.HeaderRenderer.OnMouseDown(e);
				}

				if (HeaderMouseDown != null)
				{
					HeaderMouseDown(e.Column, e);
				}
			}
		}


		/// <summary>
		/// Raises a MouseDown event for the Column header at the specified colunm 
		/// index position
		/// </summary>
		/// <param name="index">The index of the column to recieve the event</param>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		protected void RaiseHeaderMouseDown(int index, MouseEventArgs e)
		{
			if (index < 0 || this.ColumnModel == null || index >= this.ColumnModel.Columns.Count)
			{
				return;
			}

			if (this.HeaderRenderer != null)
			{
				Column column = this.ColumnModel.Columns[index];

				HeaderMouseEventArgs mhea = new HeaderMouseEventArgs(column, this, index, this.DisplayRectToClient(this.ColumnModel.ColumnHeaderRect(index)), e);

				this.OnHeaderMouseDown(mhea);
			}
		}

		#endregion

		#region MouseMove

		/// <summary>
		/// Raises the HeaderMouseMove event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		protected virtual void OnHeaderMouseMove(HeaderMouseEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				if (this.HeaderRenderer != null)
				{
					this.HeaderRenderer.OnMouseMove(e);
				}

				if (HeaderMouseMove != null)
				{
					HeaderMouseMove(e.Column, e);
				}
			}
		}


		/// <summary>
		/// Raises a MouseMove event for the Column header at the specified colunm 
		/// index position
		/// </summary>
		/// <param name="index">The index of the column to recieve the event</param>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		protected void RaiseHeaderMouseMove(int index, MouseEventArgs e)
		{
			if (index < 0 || this.ColumnModel == null || index >= this.ColumnModel.Columns.Count)
			{
				return;
			}

			if (this.HeaderRenderer != null)
			{
				Column column = this.ColumnModel.Columns[index];

				HeaderMouseEventArgs mhea = new HeaderMouseEventArgs(column, this, index, this.DisplayRectToClient(this.ColumnModel.ColumnHeaderRect(index)), e);

				this.OnHeaderMouseMove(mhea);
			}
		}


		/// <summary>
		/// Resets the current "hot" column
		/// </summary>
		internal void ResetHotColumn()
		{
			if (this.hotColumn != -1)
			{
				this.ResetMouseEventArgs();

				int oldHotColumn = this.hotColumn;
				this.hotColumn = -1;

				this.RaiseHeaderMouseLeave(oldHotColumn);
			}
		}

		#endregion

		#region MouseHover

		/// <summary>
		/// Raises the HeaderMouseHover event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		protected virtual void OnHeaderMouseHover(HeaderMouseEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				if (HeaderMouseHover != null)
				{
					HeaderMouseHover(e.Column, e);
				}
			}
		}

		#endregion

		#region Click

		/// <summary>
		/// Raises the HeaderClick event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		protected virtual void OnHeaderClick(HeaderMouseEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				if (this.HeaderRenderer != null)
				{
					this.HeaderRenderer.OnClick(e);
				}

				if (HeaderClick != null)
				{
					HeaderClick(e.Column, e);
				}
			}
		}


		/// <summary>
		/// Raises the HeaderDoubleClick event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		protected virtual void OnHeaderDoubleClick(HeaderMouseEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				if (this.HeaderRenderer != null)
				{
					this.HeaderRenderer.OnDoubleClick(e);
				}

				if (HeaderDoubleClick != null)
				{
					HeaderDoubleClick(e.Column, e);
				}
			}
		}

		#endregion

		#endregion

		#region ColumnModel

		/// <summary>
		/// Raises the ColumnModelChanged event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected virtual void OnColumnModelChanged(TableEventArgs e)	// PJD TEA change
		{
			if (this.CanRaiseEvents)
			{
				this.PerformLayout();
				this.Invalidate();

				if (ColumnModelChanged != null)
				{
					ColumnModelChanged(this, e);
				}
			}
		}


		/// <summary>
		/// Raises the ColumnAdded event
		/// </summary>
		/// <param name="e">A ColumnModelEventArgs that contains the event data</param>
		protected internal virtual void OnColumnAdded(ColumnModelEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				this.PerformLayout();
				this.Invalidate();

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
			if (this.CanRaiseEvents)
			{
				this.PerformLayout();
				this.Invalidate();

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
		protected internal virtual void OnHeaderHeightChanged(EventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				this.PerformLayout();
				this.Invalidate();

				if (HeaderHeightChanged != null)
				{
					HeaderHeightChanged(this, e);
				}
			}
		}

		#endregion

		#region Editing

		/// <summary>
		/// Raises the BeginEditing event
		/// </summary>
		/// <param name="e">A CellEditEventArgs that contains the event data</param>
		protected internal virtual void OnBeginEditing(CellEditEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				if (BeginEditing != null)
				{
					BeginEditing(e.Cell, e);
				}
			}
		}


		/// <summary>
		/// Raises the EditingStopped event
		/// </summary>
		/// <param name="e">A CellEditEventArgs that contains the event data</param>
		protected internal virtual void OnEditingStopped(CellEditEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				if (EditingStopped != null)
				{
					EditingStopped(e.Cell, e);
				}
			}
		}


		/// <summary>
		/// Raises the EditingCancelled event
		/// </summary>
		/// <param name="e">A CellEditEventArgs that contains the event data</param>
		protected internal virtual void OnEditingCancelled(CellEditEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				if (EditingCancelled != null)
				{
					EditingCancelled(e.Cell, e);
				}
			}
		}

		#endregion

		#region Focus

		/// <summary>
		/// Raises the GotFocus event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected override void OnGotFocus(EventArgs e)
		{
			if (this.FocusedCell.IsEmpty)
			{
				CellPos p = this.FindNextVisibleCell(this.FocusedCell, true, true, true, true, true);

				if (this.IsValidCell(p))
				{
					this.FocusedCell = p;
				}
			}
			else
			{
				this.RaiseCellGotFocus(this.FocusedCell);
			}

			if (this.SelectedIndicies.Length > 0)
			{
				this.Invalidate(this.CellDataRect);
			}

			if (this.BorderColor != this.UnfocusedBorderColor)
				this.Invalidate(false);

			base.OnGotFocus(e);
		}


		/// <summary>
		/// Raises the LostFocus event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected override void OnLostFocus(EventArgs e)
		{
			if (!this.FocusedCell.IsEmpty)
			{
				this.RaiseCellLostFocus(this.FocusedCell);
			}

			if (this.SelectedIndicies.Length > 0)
			{
				this.Invalidate(this.CellDataRect);
			}

			if (this.BorderColor != this.UnfocusedBorderColor)
				this.Invalidate(false);

			base.OnLostFocus(e);
		}

		#endregion

		#region Keys

		#region KeyDown

		/// <summary>
		/// Raises the KeyDown event
		/// </summary>
		/// <param name="e">A KeyEventArgs that contains the event data</param>
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);

			if (this.IsValidCell(this.FocusedCell))
			{
				if (this.IsReservedKey(e.KeyData))
				{
					Keys key = e.KeyData & Keys.KeyCode;

					if (key == Keys.Up || key == Keys.Down || key == Keys.Left || key == Keys.Right)
					{
						#region Arrow keys
						CellPos nextCell;

						if (key == Keys.Up)
						{
							nextCell = this.FindNextVisibleCell(this.FocusedCell, this.FocusedCell.Row > 0, false, false, false, true);
						}
						else if (key == Keys.Down)
						{
                            nextCell = this.FindNextVisibleCell(this.FocusedCell, this.FocusedCell.Row < this.RowCount - 1, true, false, false, true);
                            if (nextCell.Row < this.FocusedCell.Row)
                                nextCell.Row = this.FocusedCell.Row;
						}
						else if (key == Keys.Left)
						{
                            nextCell = this.FindNextVisibleCell(this.FocusedCell, false, false, false, true, true);
						}
						else
						{
                            nextCell = this.FindNextVisibleCell(this.FocusedCell, false, true, false, true, true);
                            if (nextCell.Row < this.FocusedCell.Row)
                                nextCell.Row = this.FocusedCell.Row;
						}

						if (nextCell != CellPos.Empty)
						{
                            nextCell = ResolveColspan(nextCell);
							this.FocusedCell = nextCell;

							if ((e.KeyData & Keys.Modifiers) == Keys.Shift && this.MultiSelect)
							{
								this.TableModel.Selections.AddShiftSelectedCell(this.FocusedCell);
							}
							else
							{
								this.TableModel.Selections.SelectCell(this.FocusedCell);
							}
						}
						#endregion
					}
					else if (e.KeyData == Keys.PageUp)
					{
						#region Page Up
						if (this.RowCount > 0)
						{
							int i = GetNewIndexFromPageUp();
							CellPos temp = new CellPos(i, this.FocusedCell.Column);
                            CellPos nextCell = this.FindNextVisibleCell(temp, true, false, true, false, true);

							if (nextCell != CellPos.Empty)
							{
								this.FocusedCell = nextCell;
								this.TableModel.Selections.SelectCell(this.FocusedCell);
							}
                        }
						#endregion
                    }
					else if (e.KeyData == Keys.PageDown)
                    {
						#region Page Down
                        if (this.RowCount > 0)
                        {
							int i = GetNewIndexFromPageDown();
							CellPos temp = new CellPos(i, this.FocusedCell.Column);
                            CellPos nextCell = this.FindNextVisibleCell(temp, true, false, true, false, true);

                            if (nextCell.Row > this.RowCount - 1)
                                nextCell.Row = this.RowCount - 1;
                            if (nextCell != CellPos.Empty)
							{
								this.FocusedCell = nextCell;
								this.TableModel.Selections.SelectCell(this.FocusedCell);
							}
                        }
						#endregion
                    }
                    else if (e.KeyData == Keys.Home || e.KeyData == Keys.End)
                    {
						#region Home, End
                        if (this.RowCount > 0)
						{
							CellPos nextCell;

							if (e.KeyData == Keys.Home)
							{
                                nextCell = this.FindNextVisibleCell(CellPos.Empty, true, true, true, true, true);
							}
							else
							{
                                nextCell = this.FindNextVisibleCell(new CellPos(this.RowCount - 1, this.TableModel.Rows[this.RowCount - 1].Cells.Count), true, false, true, true, true);
							}

							if (nextCell != CellPos.Empty)
							{
								this.FocusedCell = nextCell;

								this.TableModel.Selections.SelectCell(this.FocusedCell);
							}
                        }
						#endregion
                    }
				}
				else
				{
					// check if we can start editing with the custom edit key
					if (e.KeyData == this.CustomEditKey &&
                        ((this.EditStartAction & EditStartAction.CustomKey) == EditStartAction.CustomKey))
					{
						this.EditCell(this.FocusedCell);

						return;
					}

					// send all other key events to the cell's renderer
					// for further processing
					this.RaiseCellKeyDown(this.FocusedCell, e);
				}
			}
			else
			{
				if (this.FocusedCell == CellPos.Empty)
                {
					#region Cell is Empty
                    Keys key = e.KeyData & Keys.KeyCode;

					if (this.IsReservedKey(e.KeyData))
					{
						if (key == Keys.Down || key == Keys.Right)
						{
							CellPos nextCell;

							if (key == Keys.Down)
							{
                                nextCell = this.FindNextVisibleCell(this.FocusedCell, true, true, true, false, true);
							}
							else
							{
                                nextCell = this.FindNextVisibleCell(this.FocusedCell, false, true, true, true, true);
							}

							if (nextCell != CellPos.Empty)
							{
								this.FocusedCell = nextCell;

								if ((e.KeyData & Keys.Modifiers) == Keys.Shift && this.MultiSelect)
								{
									this.TableModel.Selections.AddShiftSelectedCell(this.FocusedCell);
								}
								else
								{
									this.TableModel.Selections.SelectCell(this.FocusedCell);
								}
							}
						}
                    }
					#endregion
                }
			}
		}

		private int GetNewIndexFromPageUp()
		{
			int i;
			if (!this.VScroll)
			{
				// Not enough data to scroll, so go to the top row
				i = 0;
			}
			else
			{
				int x = topIndex;
				//int y = this.vScrollBar.Value - (this.vScrollBar.LargeChange - 1);

				if (this.FocusedCell.Row > topIndex && this.TableModel[topIndex, this.FocusedCell.Column].Enabled)
				{
					// Focus is not on the topmost visible row, so without scrolling, put focus on the topmost row
					i = topIndex;
				}
				else
				{
					// We are already on the topmost visible row, so scroll up by a page
					i = Math.Max(0, this.topIndex - (this.CurrentPageRowCount() - 1));
				}
			}
			return i;
		}

		private int GetNewIndexFromPageDown()
		{
			int i;
			if (!this.VScroll)
			{
				// Not enough data to scroll, so go to the bottom row
				i = this.RowCount - 1;
			}
			else
			{
                var rowCount = this.CurrentPageRowCount();
				int currentRow = this.FocusedCell.Row;
                int bottomRow = topIndex + rowCount;
				if (currentRow < bottomRow)
				{
					i = bottomRow;
				}
				else
				{
                    i = Math.Min(this.RowCount - 1, currentRow + rowCount);
				}
			}
			return i;
		}
		#endregion

		#region KeyUp

		/// <summary>
		/// Raises the KeyUp event
		/// </summary>
		/// <param name="e">A KeyEventArgs that contains the event data</param>
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);

			if (!this.IsReservedKey(e.KeyData))
			{
				// 
				if (e.KeyData == this.CustomEditKey &&
                    ((this.EditStartAction & EditStartAction.CustomKey) == EditStartAction.CustomKey))
				{
					return;
				}

				// send all other key events to the cell's renderer
				// for further processing
				this.RaiseCellKeyUp(this.FocusedCell, e);
			}
		}

		#endregion

        #region KeyPress
        /// <summary>
        /// Adds Auto-Edit support for key press events on texteditors.
        /// </summary>
        /// <param name="e">KeyPressEventArgs that contains the event data</param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            // Auto-Edit Mode upon ascii key press.
            if (this.IsValidCell(this.FocusedCell))
            {
                // Verify a valid ascii character was pressed.
                if ((this.EditStartAction & EditStartAction.KeyPress) == EditStartAction.KeyPress && e.KeyChar >= 32 && e.KeyChar <= 126)
                {
                    // Get the cell editor type and verify it's valid for auto-editing.
                    ICellEditor cellEditor = this.ColumnModel.GetCellEditor(this.FocusedCell.Column);
                    if (cellEditor != null && (cellEditor.GetType() == typeof(TextCellEditor) || cellEditor.GetType() == typeof(ComboBoxCellEditor)))
                    {
                        // Get the active cell.
                        Cell cell = this.TableModel.Rows[this.FocusedCell.Row].Cells[this.FocusedCell.Column];

                        // If the cell is in a sub-row, determine its starting column including ColSpan.
                        if (cell.Row.SubRows == null || cell.Row.SubRows.Count == 0)
                        {
                            // This is a sub-row. Get the actual cell start index (in case it spans multiple columns).
                            int originatingColumn = cell.Row.GetRenderedCellIndex(this.FocusedCell.Column);
                            if (originatingColumn != this.FocusedCell.Column)
                            {
                                // Focus is on a non-visible cell within the ColSpan of a cell (do not allow editing).
                                // If you prefer to allow editing of the originating cell, use: cell = this.TableModel.Rows[this.FocusedCell.Row].Cells[originatingColumn];
                                cell = null;
                            }
                        }

                        if (cell != null && cell.Editable)
                        {
                            // Start editing upon key press.
                            this.EditCell(this.FocusedCell);

                            // Re-send the key code press so it appears in the editor.
                            NativeMethods.PressKey(e.KeyChar);
                        }
                    }
                }
            }
        }
        #endregion

        #endregion

        #region Layout

        /// <summary>
		/// Raises the Layout event
		/// </summary>
		/// <param name="levent">A LayoutEventArgs that contains the event data</param>
		protected override void OnLayout(LayoutEventArgs levent)
		{
			if (!this.IsHandleCreated || this.init)
			{
				return;
			}

			base.OnLayout(levent);

			this.UpdateScrollBars();
		}

		#endregion

		#region Mouse

		#region MouseUp
		/// <summary>
		/// Raises the MouseUp event
		/// </summary>
		/// <param name="e">A MouseEventArgs that contains the event data</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			if (!this.CanRaiseEvents)
				return;

			// work out the current state of  play
			this.CalcTableState(e.X, e.Y);

			TableRegion region = this.HitTest(e.X, e.Y);

			if (e.Button == MouseButtons.Left)
			{
				// if the left mouse button was down for a cell, 
				// Raise a mouse up for that cell
				if (!this.LastMouseDownCell.IsEmpty)
				{
					if (this.IsValidCell(this.LastMouseDownCell))
						this.RaiseCellMouseUp(this.LastMouseDownCell, e);

					// reset the lastMouseDownCell
					this.lastMouseDownCell = CellPos.Empty;
				}

                #region Finish column resizing
                // if we have just finished resizing, it might
                // be a good idea to relayout the table
                if (this.resizingColumnIndex != -1)
				{
					if (this.resizingColumnWidth != -1)
					{
						this.DrawReversibleLine(this.ColumnRect(this.resizingColumnIndex).Left + this.resizingColumnWidth);
						this.ColumnModel.Columns[this.resizingColumnIndex].Width = this.resizingColumnWidth;
					}

					this.resizingColumnIndex = -1;
					this.resizingColumnWidth = -1;

					this.UpdateScrollBars();
					this.Invalidate(this.PseudoClientRect, false);
                }
                #endregion

                // check if the mouse was released in a column header
				if (region == TableRegion.ColumnHeader)
				{
                    #region In column header
                    int column = this.ColumnIndexAt(e.X, e.Y);

					// if we are in the header, check if we are in the pressed column
					if (this.pressedColumn != -1)
					{
						if (this.pressedColumn == column)
						{
							if (this.hotColumn != -1 && this.hotColumn != column)
								this.ColumnModel.Columns[this.hotColumn].InternalColumnState = ColumnState.Normal;

							this.ColumnModel.Columns[this.pressedColumn].InternalColumnState = ColumnState.Hot;
							this.RaiseHeaderMouseUp(column, e);
						}

						this.pressedColumn = -1;

						// only sort the column if we have rows to sort
						if ((this.IsValidColumn(column)) && (this.ColumnModel.Columns[column].Sortable))
						{
							if (this.TableModel != null && this.TableModel.Rows.Count > 0)
								this.Sort(column);
						}

						this.Invalidate(this.HeaderRectangle, false);
					}

					return;
                    #endregion
                }

                // the mouse wasn't released in a column header, so if we 
				// have a pressed column then we need to make it unpressed
				if (this.pressedColumn != -1)
				{
					this.pressedColumn = -1;
					this.Invalidate(this.HeaderRectangle, false);
				}

                _dragDropHelper.MouseUp();

            } // e.Button == MouseButtons.Left
		}
		#endregion

		#region MouseDown
		/// <summary>
		/// Raises the MouseDown event
		/// </summary>
		/// <param name="e">A MouseEventArgs that contains the event data</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!this.CanRaiseEvents)
                return;

            this.CalcTableState(e.X, e.Y);
            TableRegion region = this.HitTest(e.X, e.Y);

            int row = this.RowIndexAtClient(e.X, e.Y);
            int column = this.ColumnIndexAt(e.X, e.Y);

            if (this.IsEditing)
            {
                if (this.EditingCell.Row != row || this.EditingCell.Column != column)
                {
                    this.Focus();

                    if (region == TableRegion.ColumnHeader && e.Button != MouseButtons.Right)
                        return;
                }
            }

            #region ColumnHeader

            if (region == TableRegion.ColumnHeader)
            {
                if (e.Button == MouseButtons.Right && this.HeaderContextMenu.Enabled)
                {
                    this.HeaderContextMenu.Show(this, new Point(e.X, e.Y));

                    return;
                }

                if (column == -1 || !this.ColumnModel.Columns[column].Enabled)
                {
                    return;
                }

                if (e.Button == MouseButtons.Left)
                {
                    this.FocusedCell = new CellPos(-1, -1);

                    // don't bother going any further if the user 
                    // double clicked
                    if (e.Clicks > 1)
                    {
                        return;
                    }

                    this.RaiseHeaderMouseDown(column, e);

                    if (this.TableState == TableState.ColumnResizing)
                    {
                        Rectangle columnRect = this.ColumnModel.ColumnHeaderRect(column);
                        int x = this.ClientToDisplayRect(e.X, e.Y).X;

                        if (x <= columnRect.Left + Column.ResizePadding)
                        {
                            //column--;
                            column = this.ColumnModel.PreviousVisibleColumn(column);
                        }

                        this.resizingColumnIndex = column;

                        if (this.resizingColumnIndex != -1)
                        {
                            this.resizingColumnAnchor = this.ColumnModel.ColumnHeaderRect(column).Left;
                            this.resizingColumnOffset = x - (this.resizingColumnAnchor + this.ColumnModel.Columns[column].Width);
                        }
                    }
                    else
                    {
                        if (this.HeaderStyle != ColumnHeaderStyle.Clickable || !this.ColumnModel.Columns[column].Sortable)
                        {
                            return;
                        }

                        if (column == -1)
                        {
                            return;
                        }

                        if (this.pressedColumn != -1)
                        {
                            this.ColumnModel.Columns[this.pressedColumn].InternalColumnState = ColumnState.Normal;
                        }

                        this.pressedColumn = column;
                        this.ColumnModel.Columns[column].InternalColumnState = ColumnState.Pressed;
                    }

                    return;
                }
            }

            #endregion

            #region Cells
            if (region == TableRegion.Cells)
            {
                #region Checks
                if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right)
                    return;

                if ((!this.AllowRMBSelection) && (e.Button == MouseButtons.Right))
                    return;

                if ((!this.IsValidCell(row, column) || !this.IsCellEnabled(row, column)) && (this.tableModel != null))
                {
                    // clear selections
                    this.TableModel.Selections.Clear();
                    return;
                }
                #endregion

                if (this.tableModel != null)
                {
                    Row r = this.tableModel.Rows[row];
                    int realCol = r.GetRenderedCellIndex(column);
                    column = realCol;

                    this.FocusedCell = new CellPos(row, column);

                    // don't bother going any further if the user 
                    // double clicked or we're not allowed to select
                    if (e.Clicks > 1 || !this.AllowSelection)
                    {
                        // We need to allow 'double clicks' through to the editors so the number change buttons can be used rapidly
                        if (!this.IsEditing)
                            return;
                    }

                    this.lastMouseDownCell.Row = row;
                    this.lastMouseDownCell.Column = column;

                    //
                    this.RaiseCellMouseDown(new CellPos(row, column), e);

                    if (!this.ColumnModel.Columns[column].Selectable)
                        return;

                    //

                    #region Multiselect - shift
                    if ((ModifierKeys & Keys.Shift) == Keys.Shift && this.MultiSelect)
                    {
                        if ((e.Button == MouseButtons.Right)
                            // Mateusz [PEYN] Adamus (peyn@tlen.pl)
                            // and RMB is not allowed to select cells
                            && (!this.AllowRMBSelection))
                        {
                            return;
                        }

                        this.TableModel.Selections.AddShiftSelectedCell(row, column);

                        return;
                    }
                    #endregion

                    #region Multiselect - control
                    if ((ModifierKeys & Keys.Control) == Keys.Control && this.MultiSelect)
                    {
                        if ((e.Button == MouseButtons.Right)
                            // Mateusz [PEYN] Adamus (peyn@tlen.pl)
                            // and RMB is not allowed to select cells
                            && (!this.AllowRMBSelection))
                        {
                            return;
                        }

                        // Mateusz [PEYN] Adamus (peyn@tlen.pl)
                        // If selection selects full rows
                        // we have to find exactly which cell is selected in a row
                        // to deselect it
                        if (this.FullRowSelect)
                        {
                            if (this.TableModel.Selections.IsRowSelected(row))
                            {
                                this.TableModel.Selections.RemoveRow(row);
                                return;
                            }
                        }

                        // Mateusz [PEYN] Adamus (peyn@tlen.pl)
                        // if Table is in ListView style and FullRowSelect = false
                        // clicking on any cell should deselect Row
                        if (this.SelectionStyle == SelectionStyle.ListView)
                        {
                            // if Row we clicked on is selected
                            if (this.TableModel.Selections.IsRowSelected(row))
                            {
                                // we deselect it
                                this.TableModel.Selections.RemoveRow(row);
                                return;
                            }
                        }

                        if (this.TableModel.Selections.IsCellSelected(row, column))
                            this.TableModel.Selections.RemoveCell(row, column);
                        else
                            this.TableModel.Selections.AddCell(row, column);

                        return;
                    }
                    #endregion

                    if (!this.TableModel.Selections.IsCellSelected(row, column))
                    {
                        #region Change the selection
                        if (this.familyRowSelect && this.fullRowSelect)
                        {
                            // family select is where we select all the rows either:
                            // under the clicked (parent) row, or
                            // that are siblings of the clicked (chlid) row
                            if (r.Parent != null)
                            {
                                // this is a child so select all the siblings
                                this.TableModel.Selections.SelectCells(r.Parent.Index, column, r.Parent.SubRows[r.Parent.SubRows.Count - 1].Index, column);
                            }
                            else
                            {
                                // this is not a child, so if it is a parent, select all children
                                if (r.SubRows == null || r.SubRows.Count == 0)
                                {
                                    this.TableModel.Selections.SelectCell(row, column);
                                }
                                else
                                {
                                    this.TableModel.Selections.SelectCells(row, column, r.SubRows[r.SubRows.Count - 1].Index, column);
                                }
                            }
                        }
                        else
                        {
                            // 'normal' secletion mode - just select what was clicked
                            this.TableModel.Selections.SelectCell(row, column);
                        }
                        #endregion
                    }
                }

                // Drag & Drop Code Added - by tankun
                if ((this.AllowDrop) && useBuiltInDragDrop && (e.Button == MouseButtons.Left))
                {
                    if (row > -1)
                        _dragDropHelper.MouseDown(row);
                }
            } //region == TableRegion.Cells
            #endregion
        }
		#endregion

		#region MouseMove
		/// <summary>
		/// Raises the MouseMove event
		/// </summary>
		/// <param name="e">A MouseEventArgs that contains the event data</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			// don't go any further if the table is editing
			if (this.TableState == TableState.Editing)
				return;

            #region Left mouse button
			// if the left mouse button is down, check if the LastMouseDownCell 
			// references a valid cell.  if it does, send the mouse move message 
			// to the cell and then exit (this will stop other cells/headers 
			// from getting the mouse move message even if the mouse is over 
			// them - this seems consistent with the way windows does it for 
			// other controls)
			if (e.Button == MouseButtons.Left)
			{
                _dragDropHelper.MouseMove(e);

				if (!this.LastMouseDownCell.IsEmpty)
				{
					if (this.IsValidCell(this.LastMouseDownCell))
					{
						this.RaiseCellMouseMove(this.LastMouseDownCell, e);
						return;
					}
				}
			}
            #endregion

            #region Column resizing
			// are we resizing a column?
			if (this.resizingColumnIndex != -1)
			{
				if (this.resizingColumnWidth != -1)
				{
					this.DrawReversibleLine(this.ColumnRect(this.resizingColumnIndex).Left + this.resizingColumnWidth);
				}

				// calculate the new width for the column
				int width = this.ClientToDisplayRect(e.X, e.Y).X - this.resizingColumnAnchor - this.resizingColumnOffset;

				// make sure the new width isn't smaller than the minimum allowed
				// column width, or larger than the maximum allowed column width
				if (width < Column.MinimumWidth)
				{
					width = Column.MinimumWidth;
				}
				else if (width > Column.MaximumWidth)
				{
					width = Column.MaximumWidth;
				}

				this.resizingColumnWidth = width;

				//this.ColumnModel.Columns[this.resizingColumnIndex].Width = width;
				this.DrawReversibleLine(this.ColumnRect(this.resizingColumnIndex).Left + this.resizingColumnWidth);

				return;
			}
            #endregion

			// work out the potential state of play
			this.CalcTableState(e.X, e.Y);

			TableRegion hitTest = this.HitTest(e.X, e.Y);

			#region ColumnHeader

			if (hitTest == TableRegion.ColumnHeader)
			{
				// this next bit is pretty complicated. need to work 
				// out which column is displayed as pressed or hot 
				// (so we have the same behaviour as a themed ListView
				// in Windows XP)

				int column = this.ColumnIndexAt(e.X, e.Y);

				// if this isn't the current hot column, reset the
				// hot columns state to normal and set this column
				// to be the hot column
				if (this.hotColumn != column)
				{
					if (this.hotColumn != -1)
					{
						this.ColumnModel.Columns[this.hotColumn].InternalColumnState = ColumnState.Normal;

						this.RaiseHeaderMouseLeave(this.hotColumn);
					}

					if (this.TableState != TableState.ColumnResizing)
					{
						this.hotColumn = column;

						if (this.hotColumn != -1 && this.ColumnModel.Columns[column].Enabled)
						{
							this.ColumnModel.Columns[column].InternalColumnState = ColumnState.Hot;

							this.RaiseHeaderMouseEnter(column);
						}
					}
				}
				else
				{
					if (column != -1 && this.ColumnModel.Columns[column].Enabled)
					{
						this.RaiseHeaderMouseMove(column, e);
					}
				}

				// if this isn't the pressed column, then the pressed columns
				// state should be set back to normal
				if (this.pressedColumn != -1 && this.pressedColumn != column)
				{
					this.ColumnModel.Columns[this.pressedColumn].InternalColumnState = ColumnState.Normal;
				}
				// else if this is the pressed column and its state is not
				// pressed, then we had better set it
				else if (column != -1 && this.pressedColumn == column && this.ColumnModel.Columns[this.pressedColumn].ColumnState != ColumnState.Pressed)
				{
					this.ColumnModel.Columns[this.pressedColumn].InternalColumnState = ColumnState.Pressed;
				}

				// set the cursor to a resizing cursor if necesary
				if (this.TableState == TableState.ColumnResizing)
				{
					Rectangle columnRect = this.ColumnModel.ColumnHeaderRect(column);
					int x = this.ClientToDisplayRect(e.X, e.Y).X;

					this.Cursor = Cursors.VSplit;

					// if the left mouse button is down, we don't want
					// the resizing cursor so set it back to the default
					if (e.Button == MouseButtons.Left)
					{
						this.Cursor = Cursors.Default;
					}

					// if the mouse is in the left side of the column, 
					// the first non-hidden column to the left needs to
					// become the hot column (so the user knows which
					// column would be resized if a resize action were
					// to take place
					if (x < columnRect.Left + Column.ResizePadding)
					{
						int col = column;

						while (col != 0)
						{
							col--;

							if (this.ColumnModel.Columns[col].Visible)
							{
								break;
							}
						}

						if (col != -1)
						{
							if (this.ColumnModel.Columns[col].Enabled)
							{
								if (this.hotColumn != -1)
								{
									this.ColumnModel.Columns[this.hotColumn].InternalColumnState = ColumnState.Normal;
								}

								this.hotColumn = col;
								this.ColumnModel.Columns[this.hotColumn].InternalColumnState = ColumnState.Hot;

								this.RaiseHeaderMouseEnter(col);
							}
							else
							{
								this.Cursor = Cursors.Default;
							}
						}
					}
					else
					{
						if (this.ColumnModel.Columns[column].Enabled)
						{
							// this mouse is in the right side of the column, 
							// so this column needs to be dsiplayed hot
							this.hotColumn = column;
							this.ColumnModel.Columns[this.hotColumn].InternalColumnState = ColumnState.Hot;
						}
						else
						{
							this.Cursor = Cursors.Default;
						}
					}
				}
				else
				{
					// we're not in a resizing area, so make sure the cursor
					// is the default cursor (we may have just come from a
					// resizing area)
					this.Cursor = Cursors.Default;
				}

				// reset the last cell the mouse was over
				this.ResetLastMouseCell();

				return;
			}

			#endregion

			// we're outside of the header, so if there is a hot column,
			// it need to be reset
			if (this.hotColumn != -1)
			{
				this.ColumnModel.Columns[this.hotColumn].InternalColumnState = ColumnState.Normal;

				this.Cursor = Cursors.Default;

				this.ResetHotColumn();
			}

			// if there is a pressed column, its state need to beset to normal
			if (this.pressedColumn != -1)
			{
				this.ColumnModel.Columns[this.pressedColumn].InternalColumnState = ColumnState.Normal;
			}

			#region Cells
			if (hitTest == TableRegion.Cells)
			{
				// find the cell the mouse is over
				CellPos cellPos = new CellPos(this.RowIndexAtClient(e.X, e.Y), this.ColumnIndexAt(e.X, e.Y));

                cellPos = ResolveColspan(cellPos);

				if (!cellPos.IsEmpty)
				{
					if (cellPos != this.lastMouseCell)
					{
						// check if the cell exists (ie is not null)
						if (this.IsValidCell(cellPos))
						{
							CellPos oldLastMouseCell = this.lastMouseCell;

							if (!oldLastMouseCell.IsEmpty)
								this.ResetLastMouseCell();

							this.lastMouseCell = cellPos;

							this.RaiseCellMouseEnter(cellPos);
						}
						else
						{
							this.ResetLastMouseCell();

							// make sure the cursor is the default cursor 
							// (we may have just come from a resizing area in the header)
							this.Cursor = Cursors.Default;
						}
					}
					else
					{
						this.RaiseCellMouseMove(cellPos, e);

						this.Cursor = Cursors.Default;
					}
				}
				else
				{
					this.ResetLastMouseCell();

					if (this.TableModel == null)
						this.ResetToolTip();
				}

				// netus - fix by Kosmokrat Hismoom on 2006-01-29
				// make sure the cursor is the default cursor 
				// (we may have just come from a resizing area in the header)
				this.Cursor = Cursors.Default;
				return;
			}
			else
			{
				this.ResetLastMouseCell();

				if (!this.lastMouseDownCell.IsEmpty)
					this.RaiseCellMouseLeave(this.lastMouseDownCell);

				if (this.TableModel == null)
					this.ResetToolTip();

				// make sure the cursor is the default cursor 
				// (we may have just come from a resizing area in the header)
				this.Cursor = Cursors.Default;
			}

			#endregion
		}
		#endregion

		#region MouseLeave

		/// <summary>
		/// Raises the MouseLeave event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			// we're outside of the header, so if there is a hot column,
			// it needs to be reset (this shouldn't happen, but better 
			// safe than sorry ;)
			if (this.hotColumn != -1)
			{
				this.ColumnModel.Columns[this.hotColumn].InternalColumnState = ColumnState.Normal;

				this.ResetHotColumn();
			}
		}

		#endregion

		#region MouseWheel

		/// <summary>
		/// Raises the MouseWheel event
		/// </summary>
		/// <param name="e">A MouseEventArgs that contains the event data</param>
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);

			if (!this.Scrollable || (!this.HScroll && !this.VScroll))
			{
				return;
			}

			if (this.VScroll)
			{
				int newVal = this.vScrollBar.Value - ((e.Delta / 120) * SystemInformation.MouseWheelScrollLines);

                this.vScrollBar.Value = this.EnsureSafeVScrollValue(newVal);
			}
			else if (this.HScroll)
			{
				int newVal = this.hScrollBar.Value - ((e.Delta / 120) * Column.MinimumWidth);

				if (newVal < 0)
				{
					newVal = 0;
				}
				else if (newVal > this.hScrollBar.Maximum - this.hScrollBar.LargeChange)
				{
					newVal = this.hScrollBar.Maximum - this.hScrollBar.LargeChange;
				}

				this.HorizontalScroll(newVal);
				this.hScrollBar.Value = newVal;
			}
		}

		#endregion

		#region MouseHover

		/// <summary>
		/// Raises the MouseHover event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected override void OnMouseHover(EventArgs e)
		{
			base.OnMouseHover(e);

			if (this.IsValidCell(this.LastMouseCell))
			{
				this.OnCellMouseHover(new CellMouseEventArgs(this.TableModel[this.LastMouseCell], this, this.LastMouseCell, this.CellRect(this.LastMouseCell)));
			}
			else if (this.hotColumn != -1)
			{
				this.OnHeaderMouseHover(new HeaderMouseEventArgs(this.ColumnModel.Columns[this.hotColumn], this, this.hotColumn, this.DisplayRectToClient(this.ColumnModel.ColumnHeaderRect(this.hotColumn))));
			}
		}

		#endregion

		#region Click
		/// <summary>
		/// Raises the Click event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);

			if (this.IsValidCell(this.LastMouseCell))
			{
				// Adjust this to take colspan into account
				// LastMouseCell may be a cell that is 'under' a colspan cell
				CellPos realCell = this.ResolveColspan(this.LastMouseCell);

				this.OnCellClick(new CellMouseEventArgs(this.TableModel[realCell], this, realCell, this.CellRect(realCell)));
			}
			else if (this.hotColumn != -1)
			{
				this.OnHeaderClick(new HeaderMouseEventArgs(this.ColumnModel.Columns[this.hotColumn], this, this.hotColumn, this.DisplayRectToClient(this.ColumnModel.ColumnHeaderRect(this.hotColumn))));
			}
		}

		/// <summary>
		/// Raises the DoubleClick event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected override void OnDoubleClick(EventArgs e)
		{
			base.OnDoubleClick(e);

			if (this.IsValidCell(this.LastMouseCell))
			{
				// Adjust this to take colspan into account
				// LastMouseCell may be a cell that is 'under' a colspan cell
				CellPos realCell = this.ResolveColspan(this.LastMouseCell);

				this.OnCellDoubleClick(new CellMouseEventArgs(this.TableModel[realCell], this, realCell, this.CellRect(realCell)));
			}
			else if (this.hotColumn != -1)
			{
                if (this.TableState == TableState.ColumnResizing)
                {
                    Column column = this.ColumnModel.Columns[this.hotColumn];
                    this.OnColumnAutoResize(new ColumnEventArgs(column, this.hotColumn, ColumnEventType.WidthChanged, column.Width));
                }
                else
                {
                    this.OnHeaderDoubleClick(new HeaderMouseEventArgs(this.ColumnModel.Columns[this.hotColumn], this, this.hotColumn, this.DisplayRectToClient(this.ColumnModel.ColumnHeaderRect(this.hotColumn))));
                }
			}
		}

		#endregion

        #endregion

        #region Paint
        /// <summary>
		/// Raises the PaintBackground event
		/// </summary>
		/// <param name="e">A PaintEventArgs that contains the event data</param>
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
		}

		/// <summary>
		/// Raises the Paint event
		/// </summary>
		/// <param name="e">A PaintEventArgs that contains the event data</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			// we'll do our own painting thanks
			//base.OnPaint(e);

			// check if we actually need to paint
			if (this.Width == 0 || this.Height == 0)
				return;

			if (this.ColumnModel != null)
			{
				// keep a record of the current clip region
				Region clip = e.Graphics.Clip;

				if (this.TableModel != null && this.TableModel.Rows.Count > 0)
				{
					this.OnPaintRows(e);

					// reset the clipping region
					e.Graphics.Clip = clip;
				}

				if (this.GridLines != GridLines.None)
					this.OnPaintGrid(e);

				if (this.HeaderStyle != ColumnHeaderStyle.None && this.ColumnModel.Columns.Count > 0)
				{
					if (this.HeaderRectangle.IntersectsWith(e.ClipRectangle))
						this.OnPaintHeader(e);
				}

				// reset the clipping region
				e.Graphics.Clip = clip;
			}

			this.OnPaintEmptyTableText(e);

			this.OnPaintBorder(e);

            if (!painted)
            {
                OnAfterFirstPaint(EventArgs.Empty);
                painted = true;
            }
		}

		/// <summary>
		/// Draws a reversible line at the specified screen x-coordinate 
		/// that is the height of the PseudoClientRect
		/// </summary>
		/// <param name="x">The screen x-coordinate of the reversible line 
		/// to be drawn</param>
		private void DrawReversibleLine(int x)
		{
			Point start = this.PointToScreen(new Point(x, this.PseudoClientRect.Top));

			ControlPaint.DrawReversibleLine(start, new Point(start.X, start.Y + this.PseudoClientRect.Height), this.BackColor);
		}

		#region Border

		/// <summary>
		/// Paints the Table's border
		/// </summary>
		/// <param name="e">A PaintEventArgs that contains the event data</param>
		protected void OnPaintBorder(PaintEventArgs e)
		{
			//e.Graphics.SetClip(e.ClipRectangle);

			if (this.BorderStyle == BorderStyle.Fixed3D)
			{
				if (ThemeManager.VisualStylesEnabled)
				{
					TextBoxState state = TextBoxState.Normal;
					if (!this.Enabled)
					{
						state = TextBoxState.Disabled;
					}

					// draw the left border
					Rectangle clipRect = new Rectangle(0, 0, SystemInformation.Border3DSize.Width, this.Height);
					if (clipRect.IntersectsWith(e.ClipRectangle))
					{
						ThemeManager.DrawTextBox(e.Graphics, this.ClientRectangle, clipRect, state);
					}

					// draw the top border
					clipRect = new Rectangle(0, 0, this.Width, SystemInformation.Border3DSize.Height);
					if (clipRect.IntersectsWith(e.ClipRectangle))
					{
						ThemeManager.DrawTextBox(e.Graphics, this.ClientRectangle, clipRect, state);
					}

					// draw the right border
					clipRect = new Rectangle(this.Width - SystemInformation.Border3DSize.Width, 0, this.Width, this.Height);
					if (clipRect.IntersectsWith(e.ClipRectangle))
					{
						ThemeManager.DrawTextBox(e.Graphics, this.ClientRectangle, clipRect, state);
					}

					// draw the bottom border
					clipRect = new Rectangle(0, this.Height - SystemInformation.Border3DSize.Height, this.Width, SystemInformation.Border3DSize.Height);
					if (clipRect.IntersectsWith(e.ClipRectangle))
					{
						ThemeManager.DrawTextBox(e.Graphics, this.ClientRectangle, clipRect, state);
					}
				}
				else
				{
					ControlPaint.DrawBorder3D(e.Graphics, 0, 0, this.Width, this.Height, Border3DStyle.Sunken);
				}
			}
			else if (this.BorderStyle == BorderStyle.FixedSingle)
			{
				Color color = this.Focused ? color = this.BorderColor : color = this.UnfocusedBorderColor;

				using (Pen borderPen = new Pen(color))
				{
					e.Graphics.DrawRectangle(borderPen, 0, 0, this.Width - 1, this.Height - 1);
				}
			}

			if (this.HScroll && this.VScroll)
			{
				Rectangle rect = new Rectangle(this.Width - this.BorderWidth - SystemInformation.VerticalScrollBarWidth,
					this.Height - this.BorderWidth - SystemInformation.HorizontalScrollBarHeight,
					SystemInformation.VerticalScrollBarWidth,
					SystemInformation.HorizontalScrollBarHeight);

				if (rect.IntersectsWith(e.ClipRectangle))
				{
					e.Graphics.FillRectangle(SystemBrushes.Control, rect);
				}
			}
		}

		#endregion

		#region Cells

		/// <summary>
		/// Paints the Cell at the specified row and column indexes
		/// </summary>
		/// <param name="e">A PaintEventArgs that contains the event data</param>
		/// <param name="row">The index of the row that contains the cell to be painted</param>
		/// <param name="column">The index of the column that contains the cell to be painted</param>
		/// <param name="cellRect">The bounding Rectangle of the Cell</param>
		protected void OnPaintCell(PaintEventArgs e, int row, int column, Rectangle cellRect)
		{
			if (row == 0 && column == 1)
			{
				column = 1;
			}

			// get the renderer for the cells column
			ICellRenderer renderer = this.ColumnModel.Columns[column].Renderer;
			if (renderer == null)
			{
				// get the default renderer for the column
				renderer = this.ColumnModel.GetCellRenderer(this.ColumnModel.Columns[column].GetDefaultRendererName());
			}

			// if the renderer is still null (which it shouldn't)
			// the get out of here
			if (renderer == null)
			{
				return;
			}

			////////////
			// Adjust the rectangle for this cell to include any cells that it colspans over
			Rectangle realRect = cellRect;
			Cell thisCell = this.TableModel[row, column];
			if (thisCell != null && thisCell.ColSpan > 1)
			{
				int width = this.GetColumnWidth(column, thisCell);
				realRect = new Rectangle(cellRect.X, cellRect.Y, width, cellRect.Height);
			}
			////////////

            // For the last column, extend the width of the cell to fill the grid, including the vertical scrollbar (to avoid a blank space where the scrollbar would be).
            if (column == this.ColumnModel.Columns.Count - 1)
            {
                //realRect.Width += SystemInformation.VerticalScrollBarWidth;
            }

			PaintCellEventArgs pcea = new PaintCellEventArgs(e.Graphics, realRect);

            if (thisCell.ImageSizeMode == ImageSizeMode.NoClip)
            {
                pcea.Graphics.SetClip(e.ClipRectangle);
            }
            else
            {
                pcea.Graphics.SetClip(Rectangle.Intersect(e.ClipRectangle, realRect));
            }

            if (column < this.TableModel.Rows[row].Cells.Count)
			{
				// is the cell selected
				bool selected = false;

				if (this.FullRowSelect)
				{
					selected = this.TableModel.Selections.IsRowSelected(row);
				}
				else
				{
					if (this.SelectionStyle == SelectionStyle.ListView)
					{
						if (this.TableModel.Selections.IsRowSelected(row) && this.ColumnModel.PreviousVisibleColumn(column) == -1)
						{
							selected = true;
						}
					}
					else if (this.SelectionStyle == SelectionStyle.Grid)
					{
						if (this.TableModel.Selections.IsCellSelected(row, column))
						{
							selected = true;
						}
					}
				}

				//
				bool editable = this.TableModel[row, column].Editable && this.TableModel.Rows[row].Editable && this.ColumnModel.Columns[column].Editable;
				bool enabled = this.TableModel[row, column].Enabled && this.TableModel.Rows[row].Enabled && this.ColumnModel.Columns[column].Enabled;

				// draw the cell
				pcea.SetCell(this.TableModel[row, column]);
				pcea.SetRow(row);
				pcea.SetColumn(column);
				pcea.SetTable(this);
				pcea.SetSelected(selected);
				pcea.SetFocused(this.Focused && this.FocusedCell.Row == row && this.FocusedCell.Column == column);
				pcea.SetSorted(column == this.lastSortedColumn);
				pcea.SetEditable(editable);
				pcea.SetEnabled(enabled);
				pcea.SetCellRect(realRect);
			}
			else
			{
				// there isn't a cell for this column, so send a 
				// null value for the cell and the renderer will 
				// take care of the rest (it should draw an empty cell)

				pcea.SetCell(null);
				pcea.SetRow(row);
				pcea.SetColumn(column);
				pcea.SetTable(this);
				pcea.SetSelected(false);
				pcea.SetFocused(false);
				pcea.SetSorted(false);
				pcea.SetEditable(false);
				pcea.SetEnabled(false);
				pcea.SetCellRect(realRect);
			}

			// let the user get the first crack at painting the cell
			this.OnBeforePaintCell(pcea);

			// only send to the renderer if the user hasn't 
			// set the handled property
			if (!pcea.Handled)
			{
				renderer.OnPaintCell(pcea);
			}

			// let the user have another go
			this.OnAfterPaintCell(pcea);
		}


		/// <summary>
		/// Raises the BeforePaintCell event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		protected virtual void OnBeforePaintCell(PaintCellEventArgs e)
		{
			if (BeforePaintCell != null)
			{
				BeforePaintCell(this, e);
			}
		}


		/// <summary>
		/// Raises the AfterPaintCell event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		protected virtual void OnAfterPaintCell(PaintCellEventArgs e)
		{
			if (AfterPaintCell != null)
			{
				AfterPaintCell(this, e);
			}
		}

        /// <summary>
        /// Raises the AfterFirstPaint event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnAfterFirstPaint(EventArgs e)
        {
            if (AfterFirstPaint != null)
            {
                AfterFirstPaint(this, e);
            }
        }

		#endregion

		#region Grid
		/// <summary>
		/// Paints the Table's grid
		/// </summary>
		/// <param name="e">A PaintEventArgs that contains the event data</param>
        protected void OnPaintGrid(PaintEventArgs e)
        {
            if (this.GridLines == GridLines.None)
                return;

            if (this.ColumnModel == null || this.ColumnModel.Columns.Count == 0)
                return;

            if (this.ColumnModel != null)
            {
                using (Pen gridPen = new Pen(this.GridColor))
                {
                    gridPen.DashStyle = (DashStyle)this.GridLineStyle;

                    // check if we can draw column lines
                    // kbomb987 - Fix for painting grid lines on parent rows and columns
                    if (this.GridLines == GridLines.Columns || this.GridLines == GridLines.Both)
                    {
                        PaintGridColumns(e, gridPen);
                    }

                    if (this.TableModel != null)
                    {
                        switch (this.GridLines)
                        {
                            case GridLines.RowsOnlyParent:
                                PaintGridRowsOnlyParent(e, gridPen);
                                break;
                            case GridLines.RowsColumnsOnlyParent:
                                // kbomb987 - Fix for painting grid lines on parent rows and columns
                                PaintGridRowsColumnsOnlyParent(e, gridPen);
                                break;
                            case GridLines.Both:
                            case GridLines.Rows:
                                PaintGridAllRows(e, gridPen);
                                break;
                            case GridLines.None:
                            case GridLines.Columns:
                                break;
                        }
                    }
                }
            }
        }

        void PaintGridRowsColumnsOnlyParent(PaintEventArgs e, Pen gridPen)
        {
            if (this.TopIndex > -1)
            {
                int yline = this.CellDataRect.Y - 1;

                int rowright = GetGridlineYMax(e);

                // Need to draw each row grid at its correct height
                for (int irow = this.TopIndex; irow < this.TableModel.Rows.Count; irow++)
                {
                    if (yline > e.ClipRectangle.Bottom)
                        break;
                    if (yline >= this.CellDataRect.Top)
                        e.Graphics.DrawLine(gridPen, e.ClipRectangle.Left, yline, rowright, yline);

                    // Only draw columns on parent.
                    if (this.tableModel.Rows[irow].Parent == null)
                    {
                        int right = this.DisplayRectangle.X;

                        // Draw columns
                        int columns = this.ColumnModel.Columns.Count;
                        for (int i = 0; i < columns; i++)
                        {
                            if (this.ColumnModel.Columns[i].Visible)
                            {
                                BitArray flags = this.TableModel.Rows[irow].InternalGridLineFlags;

                                right += this.ColumnModel.Columns[i].Width;

                                if (flags[i] || i == columns - 1)
                                {
                                    if (right >= e.ClipRectangle.Left && right <= e.ClipRectangle.Right)
                                        e.Graphics.DrawLine(gridPen, right - 1, yline, right - 1, yline + this.tableModel.Rows[irow].Height);
                                }
                            }
                        }
                    }

                    yline += this.TableModel.Rows[irow].Height;
                }

                // Now draw the final gridline under the last row (if visible)
                // TODO Make this option selectable via a parameter?
                if (yline < e.ClipRectangle.Bottom)
                    e.Graphics.DrawLine(gridPen, e.ClipRectangle.Left, yline, rowright, yline);
            }
        }

        int GetGridlineYMax(PaintEventArgs e)
        {
            int rightOfLastCol = this.ColumnModel.ColumnHeaderRect(this.ColumnCount - 1).Right;
            int right = 0;
            if (this.GridLinesContrainedToData && (e.ClipRectangle.Right > rightOfLastCol))
                right = rightOfLastCol;
            else
                right = e.ClipRectangle.Right;
            return right;
        }

        int GetGridlineXMax(PaintEventArgs e)
        {
            int bottomRow = RowIndexAtClient(0, this.CellDataRect.Bottom);
            Rectangle rect = RowRect(bottomRow);
            int bottom = e.ClipRectangle.Bottom;
            if (this.GridLinesContrainedToData && (e.ClipRectangle.Bottom > rect.Bottom))
                bottom = rect.Bottom - 1;
            return bottom;
        }

        void PaintGridRowsOnlyParent(PaintEventArgs e, Pen gridPen)
        {
            if (this.TopIndex > -1)
            {
                int yline = this.CellDataRect.Y - 1;

                int rowright = GetGridlineYMax(e);

                // Need to draw each row grid at its correct height
                for (int irow = this.TopIndex; irow < this.TableModel.Rows.Count; irow++)
                {
                    if (!(this.tableModel.Rows[irow].Parent != null))
                    {
                        //if row is not a subrow:drawline and increment yline
                        if (yline > e.ClipRectangle.Bottom)
                            break;
                        if (yline >= this.CellDataRect.Top)
                            e.Graphics.DrawLine(gridPen, e.ClipRectangle.Left, yline, rowright, yline);

                        yline += this.TableModel.Rows[irow].Height;
                    }
                    else
                    {
                        // if row is a subrow,if is visible then increment yline	
                        if (this.tableModel.Rows[irow].Parent.ExpandSubRows)
                            yline += this.TableModel.Rows[irow].Height;
                    }
                    //	e.Graphics.DrawLine(gridPen, e.ClipRectangle.Left, yline, e.ClipRectangle.Right, yline);	
                }
                // Now draw the final gridline under the last row (if visible)
                // TODO Make this option selectable via a parameter?
                if (yline < e.ClipRectangle.Bottom)
                    e.Graphics.DrawLine(gridPen, e.ClipRectangle.Left, yline, rowright, yline);
            }
        }

        void PaintGridAllRows(PaintEventArgs e, Pen gridPen)
        {
            if (this.TopIndex > -1)
            {
                int yline = this.CellDataRect.Y - 1;

                int right = GetGridlineYMax(e);

                // Need to draw each row grid at its correct height
                for (int irow = this.TopIndex; irow < this.TableModel.Rows.Count; irow++)
                {
                    if (yline > e.ClipRectangle.Bottom)
                        break;
                    if (yline >= this.CellDataRect.Top)
                        e.Graphics.DrawLine(gridPen, e.ClipRectangle.Left, yline, right, yline);
                    yline += this.TableModel.Rows[irow].Height;
                }
                // Now draw the final gridline under the last row (if visible)
                // TODO Make this option selectable via a parameter?
                if (yline < e.ClipRectangle.Bottom)
                    e.Graphics.DrawLine(gridPen, e.ClipRectangle.Left, yline, right, yline);
            }
        }

        void PaintGridColumns(PaintEventArgs e, Pen gridPen)
        {
            int right = this.DisplayRectangle.X;

            int columns = this.ColumnModel.Columns.Count;

            List<bool> wholeLineFlags = GetWholeLineFlags(columns);

            int bottom = GetGridlineXMax(e);

            for (int i = 0; i < columns; i++)
            {
                if (this.ColumnModel.Columns[i].Visible)
                {
                    right += this.ColumnModel.Columns[i].Width;

                    // We only draw a single, full-height line if the flags tell us there are no colspans or if it is the 
                    // right hand edge of the last column.
                    if (wholeLineFlags[i] || i == (columns - 1))
                    {
                        if (right >= e.ClipRectangle.Left && right <= e.ClipRectangle.Right)
                            e.Graphics.DrawLine(gridPen, right - 1, e.ClipRectangle.Top, right - 1, bottom);
                    }
                    else
                    {
                        // We need to draw the vertical line for each row separately to cope with colspans
                        PaintBrokenGridColumn(e, gridPen, i, right);
                    }
                }
            }
        }

        /// <summary>
        /// Draws a vertical grid line that is broken by colspans.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="gridPen"></param>
        /// <param name="column"></param>
        /// <param name="x"></param>
        void PaintBrokenGridColumn(PaintEventArgs e, Pen gridPen, int column, int x)
        {
            if (x >= e.ClipRectangle.Left && x <= e.ClipRectangle.Right)
            {
                int topRow = TopIndex;
                int bottomRow = RowIndexAtClient(0, this.CellDataRect.Bottom) + 1;

                // Go through each row, and see if it has any colspans that mean we can't draw 
                // the vertical gridline as one long line
                int lastRowBottom = 0;
                for (int irow = topRow; irow < bottomRow; irow++)
                {
                    Row row = this.TableModel.Rows[irow];
                    BitArray flags = row.InternalGridLineFlags;

                    Rectangle rect = RowRect(irow);
                    if (flags[column])
                    {
                        e.Graphics.DrawLine(gridPen, x - 1, rect.Top, x - 1, rect.Bottom - 1);
                    }
                    lastRowBottom = rect.Bottom;
                }

                // The column line underneath the data cells
                if (!this.GridLinesContrainedToData && (e.ClipRectangle.Bottom > lastRowBottom))
                    e.Graphics.DrawLine(gridPen, x - 1, lastRowBottom, x - 1, e.ClipRectangle.Bottom - 1);
            }
        }

        /// <summary>
        /// Returns a set of flags, one per column, that indicate whether the column
        /// can have its RHS vertical gridline drawn as an unbroken line.
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        private List<bool> GetWholeLineFlags(int columns)
        {
            // For each column, can we show the entire vertical gridline?
            List<bool> wholeLineFlags = CreateNewBoolList(columns);

            int topRow = TopIndex;
            int bottomRow = RowIndexAtClient(0, this.CellDataRect.Bottom);

            // Go through each row, and see if it has any colspans that mean we can't draw 
            // the vertical gridline as one long line
            for (int irow = topRow; irow < bottomRow; irow++)
            {
                Row row = this.TableModel.Rows[irow];
                BitArray flags = row.InternalGridLineFlags;

                for (int col = 0; col < columns; col++)
                {
                    if (!flags[col])
                        wholeLineFlags[col] = false;
                }
            }
            return wholeLineFlags;
        }

        /// <summary>
        /// Create a new List with the values initialised to true.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        List<bool> CreateNewBoolList(int count)
        {
            List<bool> wholeLineFlags = new List<bool>();
            for (int i = 0; i < count; i++)
            {
                wholeLineFlags.Add(true);
            }
            return wholeLineFlags;
        }
		#endregion

		#region Header

		/// <summary>
		/// Paints the Table's Column headers
		/// </summary>
		/// <param name="e">A PaintEventArgs that contains the event data</param>
		protected void OnPaintHeader(PaintEventArgs e)
		{
			// only bother if we actually get to paint something
			if (!this.HeaderRectangle.IntersectsWith(e.ClipRectangle))
			{
				return;
			}

			int xPos = this.DisplayRectangle.Left;
			bool needDummyHeader = true;

			//
			PaintHeaderEventArgs phea = new PaintHeaderEventArgs(e.Graphics, e.ClipRectangle);

			for (int i = 0; i < this.ColumnModel.Columns.Count; i++)
			{
				// check that the column isn't hidden
				if (this.ColumnModel.Columns[i].Visible)
				{
					Rectangle colHeaderRect = new Rectangle(xPos, this.BorderWidth, this.ColumnModel.Columns[i].Width, this.HeaderHeight);

					// check that the column intersects with the clipping rect
					if (e.ClipRectangle.IntersectsWith(colHeaderRect))
					{
						// move and resize the headerRenderer
						this.headerRenderer.Bounds = new Rectangle(xPos, this.BorderWidth, this.ColumnModel.Columns[i].Width, this.HeaderHeight);

						// set the clipping area to the header renderers bounds
						phea.Graphics.SetClip(Rectangle.Intersect(e.ClipRectangle, this.headerRenderer.Bounds));

						// draw the column header
						phea.SetColumn(this.ColumnModel.Columns[i]);
						phea.SetColumnIndex(i);
						phea.SetTable(this);
						phea.SetHeaderStyle(this.HeaderStyle);
						phea.SetHeaderRect(this.headerRenderer.Bounds);

						// let the user get the first crack at painting the header
						this.OnBeforePaintHeader(phea);

						// only send to the renderer if the user hasn't 
						// set the handled property
						if (!phea.Handled)
						{
							this.headerRenderer.OnPaintHeader(phea);
						}

						// let the user have another go
						this.OnAfterPaintHeader(phea);
					}

					// set the next column start position
					xPos += this.ColumnModel.Columns[i].Width;

					// if the next start poition is past the right edge
					// of the clipping rectangle then we don't need to
					// draw anymore
					if (xPos >= e.ClipRectangle.Right)
					{
						return;
					}

					// check is the next column position is past the
					// right edge of the table.  if it is, get out of
					// here as we don't need to draw anymore columns
					if (xPos >= this.ClientRectangle.Width)
					{
						needDummyHeader = false;

						break;
					}
				}
			}

			if (needDummyHeader)
			{
				// move and resize the headerRenderer
				this.headerRenderer.Bounds = new Rectangle(xPos, this.BorderWidth, this.ClientRectangle.Width - xPos + 2, this.HeaderHeight);

				phea.Graphics.SetClip(Rectangle.Intersect(e.ClipRectangle, this.headerRenderer.Bounds));

				phea.SetColumn(null);
				phea.SetColumnIndex(-1);
				phea.SetTable(this);
				phea.SetHeaderStyle(this.HeaderStyle);
				phea.SetHeaderRect(this.headerRenderer.Bounds);

				// let the user get the first crack at painting the header
				this.OnBeforePaintHeader(phea);

				// only send to the renderer if the user hasn't 
				// set the handled property
				if (!phea.Handled)
				{
					this.headerRenderer.OnPaintHeader(phea);
				}

				// let the user have another go
				this.OnAfterPaintHeader(phea);
			}
		}


		/// <summary>
		/// Raises the BeforePaintHeader event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		protected virtual void OnBeforePaintHeader(PaintHeaderEventArgs e)
		{
			if (BeforePaintHeader != null)
			{
				BeforePaintHeader(this, e);
			}
		}


		/// <summary>
		/// Raises the AfterPaintHeader event
		/// </summary>
		/// <param name="e">A PaintHeaderEventArgs that contains the event data</param>
		protected virtual void OnAfterPaintHeader(PaintHeaderEventArgs e)
		{
			if (AfterPaintHeader != null)
			{
				AfterPaintHeader(this, e);
			}
		}

		#endregion

		#region Rows

		/// <summary>
		/// Paints the Table's Rows
		/// </summary>
		/// <param name="e">A PaintEventArgs that contains the event data</param>
		protected void OnPaintRows(PaintEventArgs e)
		{
			int xPos = this.DisplayRectangle.Left;
			int yPos = this.PseudoClientRect.Top;

			if (this.HeaderStyle != ColumnHeaderStyle.None)
			{
				yPos += this.HeaderHeight;
			}

			bool wordWrapOn = this.EnableWordWrap;

			Rectangle rowRect = new Rectangle(xPos, yPos, this.ColumnModel.TotalColumnWidth, this.RowHeight);

			for (int i = this.TopIndex; i < this.TableModel.Rows.Count; i++)
			{
				Row row = this.TableModel.Rows[i];
				if (row != null && (row.Parent == null || row.Parent.ExpandSubRows))
				{
                    if (wordWrapOn)
					{
                        row.InternalHeight = GetRenderedRowHeight(e.Graphics, row);
					}
                    rowRect.Height = row.Height;

					if (rowRect.IntersectsWith(e.ClipRectangle))
					{
						this.OnPaintRow(e, i, rowRect);
					}
					else if (rowRect.Top > e.ClipRectangle.Bottom)
					{
						break;
					}

					// move to the next row
					rowRect.Y += rowRect.Height;
				}
			}

			#region Set the background colour of the sorted column
			if (this.IsValidColumn(this.lastSortedColumn))
			{
				if (rowRect.Y < this.PseudoClientRect.Bottom)
				{
					Rectangle columnRect = this.ColumnRect(this.lastSortedColumn);
					columnRect.Y = rowRect.Y;
					columnRect.Height = this.PseudoClientRect.Bottom - rowRect.Y;

					if (columnRect.IntersectsWith(e.ClipRectangle))
					{
						columnRect.Intersect(e.ClipRectangle);

						e.Graphics.SetClip(columnRect);

						using (SolidBrush brush = new SolidBrush(this.SortedColumnBackColor))
						{
							e.Graphics.FillRectangle(brush, columnRect);
						}
					}
				}
			}
			#endregion
		}

		/// <summary>
		/// Paints the Row at the specified index
		/// </summary>
		/// <param name="e">A PaintEventArgs that contains the event data</param>
		/// <param name="row">The index of the Row to be painted</param>
		/// <param name="rowRect">The bounding Rectangle of the Row to be painted</param>
		protected void OnPaintRow(PaintEventArgs e, int row, Rectangle rowRect)
		{
			Rectangle cellRect = new Rectangle(rowRect.X, rowRect.Y, 0, rowRect.Height);

			int colsToIgnore = 0;       // Used to skip cells that are ignored because of a colspan

            BitArray gridLineFlags = new BitArray(this.ColumnModel.Columns.Count);

            for (int i = 0; i < this.ColumnModel.Columns.Count; i++)
			{
                gridLineFlags[i] = false;

				if (this.ColumnModel.Columns[i].Visible)
				{
					//////////
					Cell thisCell = TableModel[row, i];
					if (colsToIgnore == 0)
					{
						//////////

						cellRect.Width = this.ColumnModel.Columns[i].Width;

                        // Cope with missing cells in a row
                        if (thisCell == null)
                            thisCell = new Cell();

                        // If this cell spans other columns, then the width (above) needs to take into account
                        // the spanned columns too.
                        if (thisCell.ColSpan > 1)
                        {
                            for (int span = 1; span < thisCell.ColSpan; span++)
                            {
                                cellRect.Width += this.ColumnModel.Columns[i + span].Width;
                            }
                        }

						if (cellRect.IntersectsWith(e.ClipRectangle))
							this.OnPaintCell(e, row, i, cellRect);
						else if (cellRect.Left > e.ClipRectangle.Right)
							break;

						//////////
						if (thisCell != null && thisCell.ColSpan > 1)
							colsToIgnore = thisCell.ColSpan - 1;        // Ignore the cells that this cell span over
						/////////
					}
					else
					{
						colsToIgnore--;     // Skip over this cell and count down
					}

                    gridLineFlags[i] = (colsToIgnore < 1);

					cellRect.X += this.ColumnModel.Columns[i].Width;
				}
			}

            Row r = this.TableModel.Rows[row];
            if (r.InternalGridLineFlags == null)
                r.InternalGridLineFlags = gridLineFlags;
		}

		#endregion

		#region Empty Table Text

		/// <summary>
		/// Paints the message that is displayed when the Table doen't 
		/// contain any items
		/// </summary>
		/// <param name="e">A PaintEventArgs that contains the event data</param>
		protected void OnPaintEmptyTableText(PaintEventArgs e)
		{
			if (this.ColumnModel == null || this.RowCount == 0)
			{
				Rectangle client = this.CellDataRect;

				client.Y += 10;
				client.Height -= 10;

				StringFormat format = new StringFormat();
				format.Alignment = StringAlignment.Center;

				using (SolidBrush brush = new SolidBrush(this.ForeColor))
				{
					if (this.DesignMode)
					{
						if (this.ColumnModel == null || this.TableModel == null)
						{
							string text = null;

							if (this.ColumnModel == null)
							{
								if (this.TableModel == null)
								{
									text = "Table does not have a ColumnModel or TableModel";
								}
								else
								{
									text = "Table does not have a ColumnModel";
								}
							}
							else if (this.TableModel == null)
							{
								text = "Table does not have a TableModel";
							}

							e.Graphics.DrawString(text, this.Font, brush, client, format);
						}
						else if (this.TableModel != null && this.TableModel.Rows.Count == 0)
						{
							if (this.NoItemsText != null && this.NoItemsText.Length > 0)
							{
								e.Graphics.DrawString(this.NoItemsText, this.Font, brush, client, format);
							}
						}
					}
					else
					{
						if (this.NoItemsText != null && this.NoItemsText.Length > 0)
						{
							e.Graphics.DrawString(this.NoItemsText, this.Font, brush, client, format);
						}
					}
				}
			}
		}

		#endregion

		#endregion

		#region Rows

		/// <summary>
		/// Raises the RowPropertyChanged event
		/// </summary>
		/// <param name="e">A RowEventArgs that contains the event data</param>
		protected internal virtual void OnRowPropertyChanged(RowEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				if (e.EventType == RowEventType.ExpandSubRowsChanged)
				{
					// This changes the whole table
					this.Invalidate();

					UpdateScrollBars();
				}
				else
				{
					// These events just change the row itself
					this.InvalidateRow(e.Index);

					if (RowPropertyChanged != null)
					{
						RowPropertyChanged(e.Row, e);
					}
				}
            }
        }

        protected internal virtual void OnRowDataRequest(RowEventArgs e)
        {
            if (RowDataRequestAnytime != null)
			{
				RowDataRequestAnytime(e.Row, e);
			}
        }


		/// <summary>
		/// Raises the CellAdded event
		/// </summary>
		/// <param name="e">A RowEventArgs that contains the event data</param>
		protected internal virtual void OnCellAdded(RowEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				this.InvalidateRow(e.Index);

				if (CellAdded != null)
				{
					CellAdded(e.Row, e);
				}
			}
		}


		/// <summary>
		/// Raises the CellRemoved event
		/// </summary>
		/// <param name="e">A RowEventArgs that contains the event data</param>
		protected internal virtual void OnCellRemoved(RowEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				this.InvalidateRow(e.Index);

				if (CellRemoved != null)
				{
					CellRemoved(this, e);
				}

				if (e.CellFromIndex == -1 && e.CellToIndex == -1)
				{
					if (this.FocusedCell.Row == e.Index)
					{
						this.focusedCell = CellPos.Empty;
					}
				}
				else
				{
					for (int i = e.CellFromIndex; i <= e.CellToIndex; i++)
					{
						if (this.FocusedCell.Row == e.Index && this.FocusedCell.Column == i)
						{
							this.focusedCell = CellPos.Empty;

							break;
						}
					}
				}
			}
		}

		#endregion

		#region Scrollbars

		/// <summary>
		/// Occurs when the Table's horizontal scrollbar is scrolled
		/// </summary>
		/// <param name="sender">The object that Raised the event</param>
		/// <param name="e">A ScrollEventArgs that contains the event data</param>
		protected void OnHorizontalScroll(object sender, ScrollEventArgs e)
		{
			// stop editing as the editor doesn't move while 
			// the table scrolls
			if (this.IsEditing)
			{
				this.StopEditing();
			}

			if (this.CanRaiseEvents)
			{
				// non-solid row lines develop artifacts while scrolling 
				// with the thumb so we invalidate the table once thumb 
				// scrolling has finished to make them look nice again
				if (e.Type == ScrollEventType.ThumbPosition)
				{
					if (this.GridLineStyle != GridLineStyle.Solid)
					{
						if (this.GridLines == GridLines.Rows || this.GridLines == GridLines.Both)
						{
							this.Invalidate(this.CellDataRect, false);
						}
					}

					// same with the focus rect
					if (this.FocusedCell != CellPos.Empty)
					{
						this.Invalidate(this.CellRect(this.FocusedCell), false);
					}
				}
				else
				{
					this.HorizontalScroll(e.NewValue);
				}
			}
		}


		/// <summary>
		/// Occurs when the Table's vertical scrollbar is scrolled
		/// </summary>
		/// <param name="sender">The object that Raised the event</param>
		/// <param name="e">A ScrollEventArgs that contains the event data</param>
		protected void OnVerticalScroll(object sender, ScrollEventArgs e)
		{
			// stop editing as the editor doesn't move while 
			// the table scrolls
			if (this.IsEditing)
				this.StopEditing();

			if (this.CanRaiseEvents)
			{
                if (e.Type == ScrollEventType.EndScroll
                    || e.Type == ScrollEventType.SmallIncrement
                    || e.Type == ScrollEventType.SmallDecrement)
                {
                    int i = EnsureSafeVScrollValue(e.NewValue);
                    e.NewValue = i;
                }
            }
        }


		private void vScrollBar_ValueChanged(object sender, EventArgs e)
		{
            int newtopIndex = GetNewTopRowIndex(topIndex, vScrollBar.Value - lastVScrollValue);

            HiddenSubRowsAboveTop = HiddenRowCountBefore(newtopIndex);

            topIndex = newtopIndex;

		    UpdateScrollBars();

            this.Invalidate(true);

            lastVScrollValue = vScrollBar.Value;
		}

		/// <summary>
		/// Handler for a ScrollBars GotFocus event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">An EventArgs that contains the event data</param>
		private void scrollBar_GotFocus(object sender, EventArgs e)
		{
			// don't let the scrollbars have focus 
			// (appears to slow scroll speed otherwise)
			this.Focus();
		}

		#endregion

		#region Sorting

		/// <summary>
		/// Raises the BeginSort event
		/// </summary>
		/// <param name="e">A ColumnEventArgs that contains the event data</param>
		protected virtual void OnBeginSort(ColumnEventArgs e)
		{
			if (BeginSort != null)
			{
				BeginSort(this, e);
			}
		}


		/// <summary>
		/// Raises the EndSort event
		/// </summary>
		/// <param name="e">A ColumnEventArgs that contains the event data</param>
		protected virtual void OnEndSort(ColumnEventArgs e)
		{
			if (EndSort != null)
			{
				EndSort(this, e);
			}
		}

		#endregion

		#region TableModel

		/// <summary>
		/// Raises the TableModelChanged event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected internal virtual void OnTableModelChanged(TableEventArgs e) // PJD TEA change
		{
			if (this.CanRaiseEvents)
			{
				this.PerformLayout();
				this.Invalidate();

				if (TableModelChanged != null)
				{
					TableModelChanged(this, e);
				}
			}
		}


		/// <summary>
		/// Raises the SelectionChanged event
		/// </summary>
		/// <param name="e">A TableModelEventArgs that contains the event data</param>
		protected internal virtual void OnSelectionChanged(SelectionEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				if (e.OldSelectionBounds != Rectangle.Empty)
				{
					Rectangle invalidateRect = new Rectangle(this.DisplayRectToClient(e.OldSelectionBounds.Location), e.OldSelectionBounds.Size);

					if (this.HeaderStyle != ColumnHeaderStyle.None)
					{
						invalidateRect.Y += this.HeaderHeight;
					}

                    this.InvalidateRect(invalidateRect);
				}

				if (e.NewSelectionBounds != Rectangle.Empty)
				{
					Rectangle invalidateRect = new Rectangle(this.DisplayRectToClient(e.NewSelectionBounds.Location), e.NewSelectionBounds.Size);

					if (this.HeaderStyle != ColumnHeaderStyle.None)
					{
						invalidateRect.Y += this.HeaderHeight;
					}

                    this.InvalidateRect(invalidateRect);
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
		protected internal virtual void OnRowHeightChanged(EventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				this.PerformLayout();
				this.Invalidate();

				if (RowHeightChanged != null)
				{
					RowHeightChanged(this, e);
				}
			}
		}


		/// <summary>
		/// Raises the RowAdded event
		/// </summary>
		/// <param name="e">A TableModelEventArgs that contains the event data</param>
		protected internal virtual void OnRowAdded(TableModelEventArgs e)
		{
			// tunned by Kosmokrat Hismoom on 9 jan 2006
			if (this.CanRaiseEvents)
			{
				this.PerformLayout();
				Rectangle rowRect = this.RowRect(e.Row.Index);
				if ((rowRect != Rectangle.Empty)
					&& (rowRect.IntersectsWith(this.CellDataRect)))
				{
					this.Invalidate();
				}
				if (RowAdded != null)
				{
					RowAdded(e.TableModel, e);
				}
			}
            if (RowAddedAnytime != null)
            {
                RowAddedAnytime(e.TableModel, e);
            }
		}


		/// <summary>
		/// Raises the RowRemoved event
		/// </summary>
		/// <param name="e">A TableModelEventArgs that contains the event data</param>
		protected internal virtual void OnRowRemoved(TableModelEventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				this.PerformLayout();
				this.Invalidate();

				// Removing a parent row should also remove the child rows...
				// Fix (Colby Dillion)
				if (e.Row != null && e.Row.SubRows != null)
				{
					for(int i = 0; i < e.Row.SubRows.Count; ++i)
                    {
                        e.TableModel.Rows.Remove(e.Row.SubRows[i]);
					}
				}

				if (RowRemoved != null)
				{
					RowRemoved(e.TableModel, e);
				}
			}
            if (RowRemovedAnytime != null)
            {
                RowRemovedAnytime(e.TableModel, e);
            }
		}

		#endregion

        #region Tooltips
        /// <summary>
        /// Raises the CellToolTipPopup event
        /// </summary>
        /// <param name="e">A CellToolTipEventArgs that contains the event data</param>
        protected internal virtual void OnCellToolTipPopup(CellToolTipEventArgs e)
        {
            if (this.CanRaiseEvents && CellToolTipPopup != null)
                CellToolTipPopup(this, e);
        }

        /// <summary>
        /// Raises the HeaderToolTipPopup event
        /// </summary>
        /// <param name="e">A HeaderToolTipEventArgs that contains the event data</param>
        protected internal virtual void OnHeaderToolTipPopup(HeaderToolTipEventArgs e)
        {
            if (this.CanRaiseEvents && HeaderToolTipPopup != null)
                HeaderToolTipPopup(this, e);
        }
        #endregion

        #endregion

        #region Data binding

        // Taken directly from http://www.codeproject.com/cs/database/DataBindCustomControls.asp

		#region Class data

		/// <summary>
		/// Manages the data bindings.
		/// </summary>
		private CurrencyManager dataManager;

		/// <summary>
		/// Delegate for the handler of the ListChanged event.
		/// </summary>
		private ListChangedEventHandler listChangedHandler;

		/// <summary>
		/// Delegate for the handler of the PositionChanged event.
		/// </summary>
		private EventHandler positionChangedHandler;

		/// <summary>
		/// Provides mapping from the data source to the XPTable.
		/// </summary>
		private DataSourceColumnBinder dataSourceColumnBinder;

		/// <summary>
		/// The data source to bind to.
		/// </summary>
		private object dataSource;

		/// <summary>
		/// The member to use in the data source.
		/// </summary>
		private string dataMember;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the binder that provides mapping from the data source to the XPTable.
		/// </summary>
		[Browsable(false)]
		public DataSourceColumnBinder DataSourceColumnBinder
		{
			get { return dataSourceColumnBinder; }
			set { dataSourceColumnBinder = value; }
		}

		/// <summary>
		/// Gets or sets the data source to bind to.
		/// </summary>
		[TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design")]
		[Category("Data")]
		[DefaultValue(null)]
		public object DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				if (this.dataSource != value)
				{
					this.dataSource = value;
					TryDataBinding();
				}
			}
		}

		/// <summary>
		/// Gets or sets the member to use in the data source.
		/// </summary>
		[Category("Data")]
		[Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
		[DefaultValue("")]
		public string DataMember
		{
			get
			{
				return this.dataMember;
			}
			set
			{
				if (this.dataMember != value)
				{
					this.dataMember = value;
					TryDataBinding();
				}
			}
		}

		#endregion

		#region Event handlers
		/// <summary>
		/// Fires the BindingContextChanged event.
		/// Called when something has changed
		/// </summary>
		/// <param name="e"></param>
		protected override void OnBindingContextChanged(EventArgs e)
		{
			this.TryDataBinding();
			base.OnBindingContextChanged(e);
		}

		/// <summary>
		/// Fired when any data is changed, removed or added to the data source.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataManager_ListChanged(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.Reset ||
				e.ListChangedType == ListChangedType.ItemMoved)
			{
				// Update all data
				UpdateAllData();
			}
			else if (e.ListChangedType == ListChangedType.ItemAdded)
			{
				// Add new Item
				AddItem(e.NewIndex);
			}
			else if (e.ListChangedType == ListChangedType.ItemChanged)
			{
				// Change Item
				UpdateItem(e.NewIndex);
			}
			else if (e.ListChangedType == ListChangedType.ItemDeleted)
			{
				// Delete Item
				DeleteItem(e.NewIndex);
			}
			else
			{
				// Update metadata and all data
				CalculateColumns();
				UpdateAllData();
			}
		}

		/// <summary>
		/// Called when the selected row in the data source changes.
		/// Ensures the Table keeps this row in view.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataManager_PositionChanged(object sender, EventArgs e)
		{
			if (this.TableModel.Rows.Count > dataManager.Position)
			{
				this.TableModel.Selections.SelectCell(dataManager.Position, 0);
				this.EnsureVisible(dataManager.Position, 0);
			}
		}
		#endregion

		#region Methods
		/// <summary>
		/// Gets the CurrencyManager by the BindingContext, unwires the old CurrencyManager (if needed), 
		/// and wires the new CurrencyManager. 
		/// Then it calls calculateColumns and updateAllData.
		/// </summary>
		private void TryDataBinding()
		{
			if (this.DataSource == null || base.BindingContext == null)
				return;

			CurrencyManager cm;
			try
			{
				cm = (CurrencyManager) base.BindingContext[this.DataSource, this.DataMember];
			}
			catch (System.ArgumentException)
			{
				// If no CurrencyManager was found
				return;
			}

			if (this.dataManager != cm)
			{
				// Unwire the old CurrencyManager
				if (this.dataManager != null)
				{
					//                    this.dataManager.ListChanged -= listChangedHandler;	// only in .Net 2.0
					this.dataManager.PositionChanged -= positionChangedHandler;
					if (this.dataManager.List is IBindingList)
						((IBindingList) this.dataManager.List).ListChanged -= listChangedHandler;	// OK for .Net 1.1
				}
				this.dataManager = cm;

				// Wire the new CurrencyManager
				if (this.dataManager != null)
				{
					//                    this.dataManager.ListChanged += listChangedHandler;	// only in .Net 2.0
					if (this.dataManager.List is IBindingList)
						((IBindingList) this.dataManager.List).ListChanged += listChangedHandler;	// OK for .Net 1.1
					this.dataManager.PositionChanged += positionChangedHandler;
				}

				// Update metadata and data
				CalculateColumns();

				UpdateAllData();
			}
		}

		/// <summary>
		/// Creates a ColumnModel for the columns the data source provides and assigns it to the Table.
		/// </summary>
		private void CalculateColumns()
		{
			if (dataManager == null)
				return;

            ColumnModel columns = this.DataSourceColumnBinder.GetColumnModel(dataManager.GetItemProperties());

			this.ColumnModel = columns;
		}

		/// <summary>
		/// Clears and re-adds all data from the data source.
		/// </summary>
		private void UpdateAllData()
		{
			if (this.TableModel == null)
				this.TableModel = new TableModel();

			this.TableModel.Rows.Clear();
			for (int i = 0; i < dataManager.Count; i++)
			{
				AddItem(i);
			}
		}

		/// <summary>
		/// Returns a row (ready to be added into the TableModel) derived from the given index in the data source.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		private Row GetRowFromDataSource(int index)
		{
			object row = dataManager.List[index];
			PropertyDescriptorCollection propColl = dataManager.GetItemProperties();

			Cell[] cells = new Cell[this.ColumnModel.Columns.Count];

			// Fill value for each column
			int i = 0;
			foreach (Column column in this.ColumnModel.Columns)
			{
                PropertyDescriptor prop = propColl.Find(column.Text, false);
                if (prop == null)
                    throw new ApplicationException(string.Format("Cannot find property '{0}' in datasource.", column.Text));

				object val = prop.GetValue(row);
				Cell cell = this.DataSourceColumnBinder.GetCell(column, val);
				cells.SetValue(cell, i);
				i++;
			}

			return new Row(cells);
		}

		/// <summary>
		/// Inserts the item at the given index from the data source.
		/// </summary>
		/// <param name="index"></param>
		private void AddItem(int index)
		{
			Row row = this.GetRowFromDataSource(index);
			this.TableModel.Rows.Insert(index, row);
		}

		/// <summary>
		/// Refreshes the given item in the TableModel.
		/// </summary>
		/// <param name="index"></param>
		private void UpdateItem(int index)
		{
			if (index >= 0 && index < this.TableModel.Rows.Count)
			{
				Row item = GetRowFromDataSource(index);
				this.TableModel.Rows.SetRow(index, item);
			}
		}

		/// <summary>
		/// Removes the given item from the TableModel.
		/// </summary>
		/// <param name="index"></param>
		private void DeleteItem(int index)
		{
			if (index >= 0 && index < this.TableModel.Rows.Count)
				this.TableModel.Rows.RemoveAt(index);
		}

		#endregion

		#endregion
	}
}
