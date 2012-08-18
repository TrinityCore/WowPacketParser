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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using XPTable.Editors;
using XPTable.Events;
using XPTable.Renderers;

namespace XPTable.Models
{
    /// <summary>
    /// Abstract class used as a base for all specific column types.
    /// </summary>
    [DesignTimeVisible(false),
    ToolboxItem(false)]
    public abstract class Column : Component
    {
        #region Event Handlers
        /// <summary>
        /// Occurs when one of the Column's properties changes
        /// </summary>
        public event ColumnEventHandler PropertyChanged;
        #endregion

        #region Class Data
        // Column state flags
        private readonly static int STATE_EDITABLE = 1;
        private readonly static int STATE_ENABLED = 2;
        private readonly static int STATE_VISIBLE = 4;
        private readonly static int STATE_SELECTABLE = 8;
        private readonly static int STATE_SORTABLE = 16;

        // Mateusz [PEYN] Adamus (peyn@tlen.pl)
        // Determines whether the column is able to be resized
        private readonly static int STATE_RESIZABLE = 32;
        
        /// <summary>
        /// The amount of space on each side of the Column that can 
        /// be used as a resizing handle
        /// </summary>
        public static readonly int ResizePadding = 8;

        /// <summary>
        /// The default width of a Column
        /// </summary>
        public static readonly int DefaultWidth = 75;
        
        /// <summary>
        /// The maximum width of a Column
        /// </summary>
        public static readonly int MaximumWidth = 1024;
        
        /// <summary>
        /// The minimum width of a Column
        /// </summary>
        public static readonly int MinimumWidth = ResizePadding * 2;

        /// <summary>
        /// Contains the current state of the the Column
        /// </summary>
        public byte state;

        /// <summary>
        /// The text displayed in the Column's header
        /// </summary>
        private string text;
        
        /// <summary>
        /// A string that specifies how a Column's Cell contents are formatted
        /// </summary>
        private string format;

        /// <summary>
        /// The alignment of the text displayed in the Column's Cells
        /// </summary>
        private ColumnAlignment alignment;

        /// <summary>
        /// Specifies how the column behaves when it is auto-resized.
        /// </summary>
        private ColumnAutoResizeMode resizeMode;

        /// <summary>
        /// The width of the Column
        /// </summary>
        private int width;

        /// <summary>
        /// The Image displayed on the Column's header
        /// </summary>
        private Image image;

        /// <summary>
        /// Specifies whether the Image displayed on the Column's header should 
        /// be draw on the right hand side of the Column
        /// </summary>
        private bool imageOnRight;

        /// <summary>
        /// The current state of the Column
        /// </summary>
        private ColumnState columnState;

        /// <summary>
        /// The text displayed when a ToolTip is shown for the Column's header
        /// </summary>
        private string tooltipText;

        /// <summary>
        /// The ColumnModel that the Column belongs to
        /// </summary>
        private ColumnModel columnModel;

        /// <summary>
        /// The x-coordinate of the column's left edge in pixels
        /// </summary>
        private int x;

        /// <summary>
        /// The current SortOrder of the Column
        /// </summary>
        private SortOrder sortOrder;

        /// <summary>
        /// The CellRenderer used to draw the Column's Cells
        /// </summary>
        private ICellRenderer renderer;

        /// <summary>
        /// The CellEditor used to edit the Column's Cells
        /// </summary>
        private ICellEditor editor;

        /// <summary>
        /// The Type of the IComparer used to compare the Column's Cells
        /// </summary>
        private Type comparer;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new Column with default values
        /// </summary>
        public Column() : base()
        {
            this.Init();
        }

        /// <summary>
        /// Creates a new Column with the specified header text
        /// </summary>
        /// <param name="text">The text displayed in the column's header</param>
        public Column(string text) : base()
        {
            this.Init();
            this.text = text;
        }

        /// <summary>
        /// Creates a new Column with the specified header text and width
        /// </summary>
        /// <param name="text">The text displayed in the column's header</param>
        /// <param name="width">The column's width</param>
        public Column(string text, int width) : base()
        {
            this.Init();

            this.text = text;
            this.width = width;
        }

        /// <summary>
        /// Creates a new Column with the specified header text, width and visibility
        /// </summary>
        /// <param name="text">The text displayed in the column's header</param>
        /// <param name="width">The column's width</param>
        /// <param name="visible">Specifies whether the column is visible</param>
        public Column(string text, int width, bool visible) : base()
        {
            this.Init();

            this.text = text;
            this.width = width;
            this.Visible = visible;
        }

        /// <summary>
        /// Creates a new Column with the specified header text and image
        /// </summary>
        /// <param name="text">The text displayed in the column's header</param>
        /// <param name="image">The image displayed on the column's header</param>
        public Column(string text, Image image) : base()
        {
            this.Init();

            this.text = text;
            this.image = image;
        }

        /// <summary>
        /// Creates a new Column with the specified header text, image and width
        /// </summary>
        /// <param name="text">The text displayed in the column's header</param>
        /// <param name="image">The image displayed on the column's header</param>
        /// <param name="width">The column's width</param>
        public Column(string text, Image image, int width) : base()
        {
            this.Init();

            this.text = text;
            this.image = image;
            this.width = width;
        }

        /// <summary>
        /// Creates a new Column with the specified header text, image, width and visibility
        /// </summary>
        /// <param name="text">The text displayed in the column's header</param>
        /// <param name="image">The image displayed on the column's header</param>
        /// <param name="width">The column's width</param>
        /// <param name="visible">Specifies whether the column is visible</param>
        public Column(string text, Image image, int width, bool visible) : base()
        {
            this.Init();

            this.text = text;
            this.image = image;
            this.width = width;
            this.Visible = visible;
        }

        /// <summary>
        /// Initialise default values
        /// </summary>
        private void Init()
        {
            this.text = null;
            this.width = Column.DefaultWidth;
            this.columnState = ColumnState.Normal;
            this.alignment = ColumnAlignment.Left;
            this.image = null;
            this.imageOnRight = false;
            this.columnModel = null;
            this.x = 0;
            this.tooltipText = null;
            this.format = "";
            this.sortOrder = SortOrder.None;
            this.renderer = null;
            this.editor = null;
            this.comparer = null;

            // Mateusz [PEYN] Adamus (peyn@tlen.pl)
            // Added STATE_RESIZABLE to column's initialization
            this.state = (byte) (STATE_ENABLED | STATE_EDITABLE | STATE_VISIBLE | STATE_SELECTABLE | STATE_SORTABLE | STATE_RESIZABLE );
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets a string that specifies the name of the Column's default CellRenderer
        /// </summary>
        /// <returns>A string that specifies the name of the Column's default 
        /// CellRenderer</returns>
        public abstract string GetDefaultRendererName();

        /// <summary>
        /// Gets the Column's default CellRenderer
        /// </summary>
        /// <returns>The Column's default CellRenderer</returns>
        public abstract ICellRenderer CreateDefaultRenderer();

        /// <summary>
        /// Gets a string that specifies the name of the Column's default CellEditor
        /// </summary>
        /// <returns>A string that specifies the name of the Column's default 
        /// CellEditor</returns>
        public abstract string GetDefaultEditorName();

        /// <summary>
        /// Gets the Column's default CellEditor
        /// </summary>
        /// <returns>The Column's default CellEditor</returns>
        public abstract ICellEditor CreateDefaultEditor();

        /// <summary>
        /// Returns the state represented by the specified state flag
        /// </summary>
        /// <param name="flag">A flag that represents the state to return</param>
        /// <returns>The state represented by the specified state flag</returns>
        internal bool GetState(int flag)
        {
            return ((this.state & flag) != 0);
        }

        /// <summary>
        /// Sets the state represented by the specified state flag to the specified value
        /// </summary>
        /// <param name="flag">A flag that represents the state to be set</param>
        /// <param name="value">The new value of the state</param>
        internal void SetState(int flag, bool value)
        {
            this.state = (byte) (value ? (this.state | flag) : (this.state & ~flag));
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the text displayed on the Column header
        /// </summary>
        [Category("Appearance"),
        DefaultValue(null),
        Description("The text displayed in the column's header."),
        Localizable(true)]
        public string Text
        {
            get { return this.text; }
            set
            {
                if (value == null)
                    value = "";
                
                if (!value.Equals(this.text))
                {
                    string oldText = this.text;
                    this.text = value;
                    this.OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.TextChanged, oldText));
                }
            }
        }

        /// <summary>
        /// Gets or sets the string that specifies how a Column's Cell contents 
        /// are formatted
        /// </summary>
        [Category("Appearance"),
        DefaultValue(""),
        Description("A string that specifies how a column's cell contents are formatted."),
        Localizable(true)]
        public virtual string Format
        {
            get { return this.format; }
            set
            {
                if (value == null)
                    value = "";

                if (!value.Equals(this.format))
                {
                    string oldFormat = this.format;
                    this.format = value;
                    this.OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.FormatChanged, oldFormat));
                }
            }
        }

        /// <summary>
        /// Gets or sets the horizontal alignment of the Column's Cell contents
        /// </summary>
        [Category("Appearance"),
        DefaultValue(ColumnAlignment.Left),
        Description("The horizontal alignment of the column's cell contents."),
        Localizable(true)]
        public virtual ColumnAlignment Alignment
        {
            get { return this.alignment; }
            set
            {
                if (!Enum.IsDefined(typeof(ColumnAlignment), value)) 
                    throw new InvalidEnumArgumentException("value", (int) value, typeof(ColumnAlignment));
                    
                if (this.alignment != value)
                {
                    ColumnAlignment oldAlignment = this.alignment;
                    this.alignment = value;
                    this.OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.AlignmentChanged, oldAlignment));
                }
            }
        }

        /// <summary>
        /// Gets or sets how the column behaves when it is auto-resized.
        /// </summary>
        [Category("Appearance"),
        DefaultValue(ColumnAutoResizeMode.Any),
        Description("Specifies how the column behaves when it is auto-resized."),
        Localizable(true)]
        public virtual ColumnAutoResizeMode AutoResizeMode
        {
            get { return this.resizeMode; }
            set
            {
                if (!Enum.IsDefined(typeof(ColumnAutoResizeMode), value))
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(ColumnAutoResizeMode));

                if (this.resizeMode != value)
                {
                    ColumnAutoResizeMode oldValue = this.resizeMode;
                    this.resizeMode = value;
                    this.OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.AutoResizeModeChanged, oldValue));
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the Column
        /// </summary>
        [Category("Appearance"),
        Description("The width of the column."),
        Localizable(true)]
        public int Width
        {
            get { return this.width; }
            set
            {
                if (this.width != value)
                {
                    int oldWidth = this.Width;
                    // Set the width, and check min & max
                    this.width = Math.Min(Math.Max(value, MinimumWidth), MaximumWidth);
                    this.OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.WidthChanged, oldWidth));
                }
            }
        }

        /// <summary>
        /// Specifies whether the Width property should be serialized at 
        /// design time
        /// </summary>
        /// <returns>true if the Width property should be serialized, 
        /// false otherwise</returns>
        private bool ShouldSerializeWidth()
        {
            return this.Width != Column.DefaultWidth;
        }

        /// <summary>
        /// Indicates whether the text has all been shown when rendered.
        /// </summary>
        private bool _isTextTrimmed = false;

        /// <summary>
        /// Gets or sets a value that indicates whether the text has all been shown when rendered.
        /// </summary>
        [Browsable(false)]
        public bool IsTextTrimmed
        {
            get { return _isTextTrimmed; }
            set { _isTextTrimmed = value; }
        }

        private int _internalContentWidth;

        /// <summary>
        /// Gets or sets the minimum width required to display this column header.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ContentWidth
        {
            get { return _internalContentWidth; }
            set
            {
                _internalContentWidth = value;
                if (value > 0)
                    _internalWidthSet = true;
            }
        }

        private bool _internalWidthSet = false;

        /// <summary>
        /// Returns true if the cells width property has been assigned.
        /// </summary>
        [Browsable(false)]
        public bool WidthNotSet
        {
            get { return !_internalWidthSet; }
        }

        /// <summary>
        /// Gets or sets the Image displayed in the Column's header
        /// </summary>
        [Category("Appearance"),
        DefaultValue(null),
        Description("Ihe image displayed in the column's header"),
        Localizable(true)]
        public Image Image
        {
            get { return this.image; }
            set
            {
                if (this.image != value)
                {
                    Image oldImage = this.Image;
                    this.image = value;
                    this.OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.ImageChanged, oldImage));
                }
            }
        }

        /// <summary>
        /// Gets or sets whether the Image displayed on the Column's header should 
        /// be draw on the right hand side of the Column
        /// </summary>
        [Category("Appearance"),
        DefaultValue(false),
        Description("Specifies whether the image displayed on the column's header should be drawn on the right hand side of the column"),
        Localizable(true)]
        public bool ImageOnRight
        {
            get { return this.imageOnRight; }
            set
            {
                if (this.imageOnRight != value)
                {
                    this.imageOnRight = value;
                    this.OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.ImageChanged, null));
                }
            }
        }

        /// <summary>
        /// Gets the state of the Column
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ColumnState ColumnState
        {
            get { return this.columnState; }
        }

        /// <summary>
        /// Gets or sets the state of the Column
        /// </summary>
        internal ColumnState InternalColumnState
        {
            get { return this.ColumnState; }
            set
            {
                if (!Enum.IsDefined(typeof(ColumnState), value)) 
                    throw new InvalidEnumArgumentException("value", (int) value, typeof(ColumnState));
                if (this.columnState != value)
                {
                    ColumnState oldState = this.columnState;
                    this.columnState = value;
                    this.OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.StateChanged, oldState));
                }
            }
        }

        /// <summary>
        /// Gets or sets the whether the Column is displayed
        /// </summary>
        [Category("Appearance"),
        DefaultValue(true),
        Description("Determines whether the column is visible or hidden.")]
        public bool Visible
        {
            get { return this.GetState(STATE_VISIBLE); }
            set
            {
                bool visible = this.Visible;
                this.SetState(STATE_VISIBLE, value);
                if (visible != value)
                    this.OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.VisibleChanged, visible));
            }
        }

        /// <summary>
        /// Gets or sets whether the Column is able to be sorted
        /// </summary>
        [Category("Appearance"),
        DefaultValue(true),
        Description("Determines whether the column is able to be sorted.")]
        public virtual bool Sortable
        {
            get { return this.GetState(STATE_SORTABLE); }
            set
            {
                bool sortable = this.Sortable;
                this.SetState(STATE_SORTABLE, value);
                if (sortable != value)
                    this.OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.SortableChanged, sortable));
            }
        }

        // Mateusz [PEYN] Adamus (peyn@tlen.pl)
        // Determines whether the column is able to be resized
        /// <summary>
        /// Gets or sets whether the Column is able to be resized
        /// </summary>
        [ Category( "Appearance" ),
        DefaultValue( true ),
        Description( "Determines whether the column is able to be resized." ) ]
        public virtual bool Resizable
        {
            get { return this.GetState( STATE_RESIZABLE ); }
            set
            {
                bool resizable = this.Resizable;
                this.SetState( STATE_RESIZABLE, value );
                if( resizable != value)
                    this.OnPropertyChanged( new ColumnEventArgs( this, ColumnEventType.ResizableChanged, resizable ) );
            }
        }

        /// <summary>
        /// Gets or sets the user specified ICellRenderer that is used to draw the 
        /// Column's Cells
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ICellRenderer Renderer
        {
            get { return this.renderer; }
            set
            {
                if (this.renderer != value)
                {
                    this.renderer = value;
                    this.OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.RendererChanged, null));
                }
            }
        }

        /// <summary>
        /// Gets or sets the user specified ICellEditor that is used to edit the 
        /// Column's Cells
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ICellEditor Editor
        {
            get { return this.editor; }
            set
            {
                if (this.editor != value)
                {
                    this.editor = value;
                    this.OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.EditorChanged, null));
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the user specified Comparer type that is used to edit the 
        /// Column's Cells
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type Comparer
         {
             get { return this.comparer; }
             set
             {
                 if (this.comparer != value)
                 {
                     this.comparer = value;
                     this.OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.ComparerChanged, null));
                 }
             }
         }

        /// <summary>
        /// Gets the Type of the default Comparer used to compare the Column's Cells when 
        /// the Column is sorting
        /// </summary>
        [Browsable(false)]
        public abstract Type DefaultComparerType
        {
            get;
        }

        /// <summary>
        /// Gets the current SortOrder of the Column
        /// </summary>
        [Browsable(false)]
        public SortOrder SortOrder
        {
            get { return this.sortOrder; }
        }

        /// <summary>
        /// Gets or sets the current SortOrder of the Column
        /// </summary>
        internal SortOrder InternalSortOrder
        {
            get { return this.SortOrder; }
            set
            {
                if (!Enum.IsDefined(typeof(SortOrder), value)) 
                    throw new InvalidEnumArgumentException("value", (int) value, typeof(SortOrder));
                
                if (this.sortOrder != value)
                {
                    SortOrder oldOrder = this.sortOrder;
                    this.sortOrder = value;
                    this._internalWidthSet = false; // Need to re-calc width with/without the arrow
                    this.OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.SortOrderChanged, oldOrder));
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Column's Cells contents 
        /// are able to be edited
        /// </summary>
        [Category("Appearance"),
        Description("Controls whether the column's cell contents are able to be changed by the user")]
        public virtual bool Editable
        {
            get
            {
                if (!this.GetState(STATE_EDITABLE))
                    return false;
                else
                    return this.Visible && this.Enabled;
            }

            set
            {
                bool editable = this.GetState(STATE_EDITABLE);
                this.SetState(STATE_EDITABLE, value);
                
                if (editable != value)
                    this.OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.EditableChanged, editable));
            }
        }

        /// <summary>
        /// Specifies whether the Editable property should be serialized at 
        /// design time
        /// </summary>
        /// <returns>true if the Editable property should be serialized, 
        /// false otherwise</returns>
        private bool ShouldSerializeEditable()
        {
            return !this.GetState(STATE_EDITABLE);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Column's Cells can respond to 
        /// user interaction
        /// </summary>
        [Category("Appearance"),
        Description("Indicates whether the column's cells can respond to user interaction")]
        public bool Enabled
        {
            get
            {
                if (!this.GetState(STATE_ENABLED))
                    return false;

                if (this.ColumnModel == null)
                    return true;

                return this.ColumnModel.Enabled;
            }

            set
            {
                bool enabled = this.GetState(STATE_ENABLED);
                
                this.SetState(STATE_ENABLED, value);
                
                if (enabled != value)
                    this.OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.EnabledChanged, enabled));
            }
        }

        /// <summary>
        /// Specifies whether the Enabled property should be serialized at 
        /// design time
        /// </summary>
        /// <returns>true if the Enabled property should be serialized, 
        /// false otherwise</returns>
        private bool ShouldSerializeEnabled()
        {
            return !this.GetState(STATE_ENABLED);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Column's Cells can be selected
        /// </summary>
        [Category("Appearance"),
        DefaultValue(true),
        Description("Indicates whether the column's cells can be selected")]
        public virtual bool Selectable
        {
            get { return this.GetState(STATE_SELECTABLE); }

            set
            {
                bool selectable = this.Selectable;
                
                this.SetState(STATE_SELECTABLE, value);
                
                if (selectable != value)
                    this.OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.SelectableChanged, selectable));
            }
        }

        /// <summary>
        /// Gets or sets the ToolTip text associated with the Column
        /// </summary>
        [Category("Appearance"),
        DefaultValue(null),
        Description("The ToolTip text associated with the Column"),
        Localizable(true)]
        public string ToolTipText
        {
            get { return this.tooltipText; }

            set
            {
                if (value == null)
                    value = "";
                
                if (!value.Equals(this.tooltipText))
                {
                    string oldTip = this.tooltipText;
                    this.tooltipText = value;
                    this.OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.ToolTipTextChanged, oldTip));
                }
            }
        }

        /// <summary>
        /// Gets the x-coordinate of the column's left edge in pixels
        /// </summary>
        internal int X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        /// <summary>
        /// Gets the x-coordinate of the column's left edge in pixels
        /// </summary>
        [Browsable(false)]
        public int Left
        {
            get { return this.X; }
        }

        /// <summary>
        /// Gets the x-coordinate of the column's right edge in pixels
        /// </summary>
        [Browsable(false)]
        public int Right
        {
            get { return this.Left + this.Width; }
        }

        /// <summary>
        /// Gets or sets the ColumnModel the Column belongs to
        /// </summary>
        protected internal ColumnModel ColumnModel
        {
            get { return this.columnModel; }
            set { this.columnModel = value; }
        }

        /// <summary>
        /// Gets the ColumnModel the Column belongs to.  This member is not 
        /// intended to be used directly from your code
        /// </summary>
        [Browsable(false)]
        public ColumnModel Parent
        {
            get { return this.ColumnModel; }
        }

        /// <summary>
        /// Gets whether the Column is able to raise events
        /// </summary>
		protected override bool CanRaiseEvents
        {
            get
            {
                // check if the ColumnModel that the Colum belongs to is able to 
                // raise events (if it can't, the Colum shouldn't raise events either)
                if (this.ColumnModel != null)
					return this.ColumnModel.CanRaiseEventsInternal;
                else
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
        #endregion

        #region Events
        /// <summary>
        /// Raises the PropertyChanged event
        /// </summary>
        /// <param name="e">A ColumnEventArgs that contains the event data</param>
        protected virtual void OnPropertyChanged(ColumnEventArgs e)
        {
            if (this.ColumnModel != null)
                e.SetIndex(this.ColumnModel.Columns.IndexOf(this)); 
            
            if (this.CanRaiseEvents)
            {
                if (this.ColumnModel != null)
                    this.ColumnModel.OnColumnPropertyChanged(e);
                
                if (PropertyChanged != null)
                    PropertyChanged(this, e);
            }
        }
        #endregion
    }
}
