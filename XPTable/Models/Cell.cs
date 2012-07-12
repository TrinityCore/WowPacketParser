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

using XPTable.Events;
using XPTable.Models.Design;
using XPTable.Renderers;


namespace XPTable.Models
{
	/// <summary>
	/// Represents a Cell that is displayed in a Table
	/// </summary>
	[DesignTimeVisible(true),
	TypeConverter(typeof(CellConverter))]
	public class Cell : IDisposable
	{
		#region EventHandlers

		/// <summary>
		/// Occurs when the value of a Cells property changes
		/// </summary>
		public event CellEventHandler PropertyChanged;

		#endregion


		#region Class Data

		// Cell state flags
		private static readonly int STATE_EDITABLE = 1;
		private static readonly int STATE_ENABLED = 2;
		private static readonly int STATE_SELECTED = 4;
        private static readonly int STATE_DISPOSED = 8;
        private static readonly int STATE_WIDTH_SET = 16;
        private static readonly int STATE_IS_TEXT_TRIMMED = 32;

		/// <summary>
		/// The text displayed in the Cell
		/// </summary>
        protected virtual string text
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

		/// <summary>
		/// An object that contains data to be displayed in the Cell
		/// </summary>
        protected virtual object data
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

		/// <summary>
		/// An object that contains data about the Cell
		/// </summary>
		protected virtual object tag
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

		/// <summary>
		/// Stores information used by CellRenderers to record the current 
		/// state of the Cell
		/// </summary>
		private object rendererData;

		/// <summary>
		/// The Row that the Cell belongs to
		/// </summary>
		private Row row;

        private sbyte _index;

		/// <summary>
		/// The index of the Cell
		/// </summary>
		private int index
        {
            get { return _index; }
            set { _index = (sbyte)value; }
        }

        private byte _state = 0;

		/// <summary>
		/// Contains the current state of the the Cell
		/// </summary>
        private byte state
        {
            get
            {
                return (byte)((int)_state & 7);
            }
            set
            {
                _state = (byte)(((int)_state & ~7) | ((int)value) & 7);
            }
        }
		
		/// <summary>
		/// The Cells CellStyle settings
		/// </summary>
        protected virtual CellStyle cellStyle
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

		/// <summary>
		/// The Cells CellCheckStyle settings
		/// </summary>
        private CellCheckStyle checkStyle
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

		/// <summary>
		/// The Cells CellImageStyle settings
		/// </summary>
		private CellImageStyle imageStyle
        {
            get 
            {
                return null;
            }
            set
            {
            }
        }

		/// <summary>
		/// The text displayed in the Cells tooltip
		/// </summary>
		private string tooltipText
        {
            get
            {
                return null;
            }
            set
            {
            }
        }


		/// <summary>
		/// Specifies whether the Cell has been disposed
		/// </summary>
        private bool disposed
        {
            get
            {
                return (_state & STATE_DISPOSED)!=0;
            }
            set
            {
                if (value)
                    _state |= (byte)STATE_DISPOSED;
                else
                    _state &= (byte)~STATE_DISPOSED;
            }
        }

        /// <summary>
        /// Specifies how many columns this cell occupies.
        /// </summary>
        protected virtual int colspan
        {
            get
            {
                return 1;
            }
            set
            {
            }
        }

		#endregion


		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the Cell class with default settings
		/// </summary>
		public Cell() : base()
		{
			this.Init();
		}


		/// <summary>
		/// Initializes a new instance of the Cell class with the specified text
		/// </summary>
		/// <param name="text">The text displayed in the Cell</param>
		public Cell(string text)
		{	
			this.Init();

			this.text = text;
		}


		/// <summary>
		/// Initializes a new instance of the Cell class with the specified object
		/// </summary>
		/// <param name="value">The object displayed in the Cell</param>
		public Cell(object value)
		{
			this.Init();

			this.data = value;
		}


		/// <summary>
		/// Initializes a new instance of the Cell class with the specified text 
		/// and object
		/// </summary>
		/// <param name="text">The text displayed in the Cell</param>
		/// <param name="value">The object displayed in the Cell</param>
		public Cell(string text, object value)
		{
			this.Init();

			this.text = text;
			this.data = value;
		}


		/// <summary>
		/// Initializes a new instance of the Cell class with the specified text 
		/// and check value
		/// </summary>
		/// <param name="text">The text displayed in the Cell</param>
		/// <param name="check">Specifies whether the Cell is Checked</param>
		public Cell(string text, bool check)
		{
			this.Init();

			this.text = text;
			this.Checked = check;
		}
		
		
		/// <summary>
		/// Initializes a new instance of the Cell class with the specified text 
		/// and Image value
		/// </summary>
		/// <param name="text">The text displayed in the Cell</param>
		/// <param name="image">The Image displayed in the Cell</param>
		public Cell(string text, Image image)
		{
			this.Init();

			this.text = text;
			this.Image = image;
		}
		
		
		/// <summary>
		/// Initializes a new instance of the Cell class with the specified text, 
		/// fore Color, back Color and Font
		/// </summary>
		/// <param name="text">The text displayed in the Cell</param>
		/// <param name="foreColor">The foreground Color of the Cell</param>
		/// <param name="backColor">The background Color of the Cell</param>
		/// <param name="font">The Font used to draw the text in the Cell</param>
		public Cell(string text, Color foreColor, Color backColor, Font font)
		{
			this.Init();

			this.text = text;
			this.ForeColor = foreColor;
			this.BackColor = backColor;
			this.Font = font;
		}


		/// <summary>
		/// Initializes a new instance of the Cell class with the specified text 
		/// and CellStyle
		/// </summary>
		/// <param name="text">The text displayed in the Cell</param>
		/// <param name="cellStyle">A CellStyle that specifies the visual appearance 
		/// of the Cell</param>
		public Cell(string text, CellStyle cellStyle)
		{
			this.Init();

			this.text = text;
			this.cellStyle = cellStyle;
		}
		
		
		/// <summary>
		/// Initializes a new instance of the Cell class with the specified object, 
		/// fore Color, back Color and Font
		/// </summary>
		/// <param name="value">The object displayed in the Cell</param>
		/// <param name="foreColor">The foreground Color of the Cell</param>
		/// <param name="backColor">The background Color of the Cell</param>
		/// <param name="font">The Font used to draw the text in the Cell</param>
		public Cell(object value, Color foreColor, Color backColor, Font font)
		{
			this.Init();

			this.data = value;
			this.ForeColor = foreColor;
			this.BackColor = backColor;
			this.Font = font;
		}


		/// <summary>
		/// Initializes a new instance of the Cell class with the specified text 
		/// and CellStyle
		/// </summary>
		/// <param name="value">The object displayed in the Cell</param>
		/// <param name="cellStyle">A CellStyle that specifies the visual appearance 
		/// of the Cell</param>
		public Cell(object value, CellStyle cellStyle)
		{
			this.Init();

			this.data = value;
			this.cellStyle = cellStyle;
		}


		/// <summary>
		/// Initializes a new instance of the Cell class with the specified text, 
		/// object, fore Color, back Color and Font
		/// </summary>
		/// <param name="text">The text displayed in the Cell</param>
		/// <param name="value">The object displayed in the Cell</param>
		/// <param name="foreColor">The foreground Color of the Cell</param>
		/// <param name="backColor">The background Color of the Cell</param>
		/// <param name="font">The Font used to draw the text in the Cell</param>
		public Cell(string text, object value, Color foreColor, Color backColor, Font font)
		{
			this.Init();

			this.text = text;
			this.data = value;
			this.ForeColor = foreColor;
			this.BackColor = backColor;
			this.Font = font;
		}


		/// <summary>
		/// Initializes a new instance of the Cell class with the specified text, 
		/// object and CellStyle
		/// </summary>
		/// <param name="text">The text displayed in the Cell</param>
		/// <param name="value">The object displayed in the Cell</param>
		/// <param name="cellStyle">A CellStyle that specifies the visual appearance 
		/// of the Cell</param>
		public Cell(string text, object value, CellStyle cellStyle)
		{
			this.Init();

			this.text = text;
			this.data = value;
			this.cellStyle = cellStyle;
		}


		/// <summary>
		/// Initializes a new instance of the Cell class with the specified text, 
		/// check value, fore Color, back Color and Font
		/// </summary>
		/// <param name="text">The text displayed in the Cell</param>
		/// <param name="check">Specifies whether the Cell is Checked</param>
		/// <param name="foreColor">The foreground Color of the Cell</param>
		/// <param name="backColor">The background Color of the Cell</param>
		/// <param name="font">The Font used to draw the text in the Cell</param>
		public Cell(string text, bool check, Color foreColor, Color backColor, Font font)
		{
			this.Init();

			this.text = text;
			this.Checked = check;
			this.ForeColor = foreColor;
			this.BackColor = backColor;
			this.Font = font;
		}


		/// <summary>
		/// Initializes a new instance of the Cell class with the specified text, 
		/// check value and CellStyle
		/// </summary>
		/// <param name="text">The text displayed in the Cell</param>
		/// <param name="check">Specifies whether the Cell is Checked</param>
		/// <param name="cellStyle">A CellStyle that specifies the visual appearance 
		/// of the Cell</param>
		public Cell(string text, bool check, CellStyle cellStyle)
		{
			this.Init();

			this.text = text;
			this.Checked = check;
			this.cellStyle = cellStyle;
		}
		
		
		/// <summary>
		/// Initializes a new instance of the Cell class with the specified text, 
		/// Image, fore Color, back Color and Font
		/// </summary>
		/// <param name="text">The text displayed in the Cell</param>
		/// <param name="image">The Image displayed in the Cell</param>
		/// <param name="foreColor">The foreground Color of the Cell</param>
		/// <param name="backColor">The background Color of the Cell</param>
		/// <param name="font">The Font used to draw the text in the Cell</param>
		public Cell(string text, Image image, Color foreColor, Color backColor, Font font)
		{
			this.Init();

			this.text = text;
			this.Image = image;
			this.ForeColor = foreColor;
			this.BackColor = backColor;
			this.Font = font;
		}
		
		
		/// <summary>
		/// Initializes a new instance of the Cell class with the specified text, 
		/// Image and CellStyle
		/// </summary>
		/// <param name="text">The text displayed in the Cell</param>
		/// <param name="image">The Image displayed in the Cell</param>
		/// <param name="cellStyle">A CellStyle that specifies the visual appearance 
		/// of the Cell</param>
		public Cell(string text, Image image, CellStyle cellStyle)
		{
			this.Init();

			this.text = text;
			this.Image = image;
			this.cellStyle = cellStyle;
		}


		/// <summary>
		/// Initialise default values
		/// </summary>
		private void Init()
		{
			this.text = null;
			this.data = null;
			this.rendererData = null;
			this.tag = null;
			this.row = null;
			this.index = -1;
			this.cellStyle = null;
			this.checkStyle = null;
			this.imageStyle = null;
			this.tooltipText = null;
            this.colspan = 1;

			this.state = (byte) (STATE_EDITABLE | STATE_ENABLED);
		}

		#endregion


		#region Methods

		/// <summary>
		/// Releases all resources used by the Cell
		/// </summary>
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.text = null;
				this.data = null;
				this.tag = null;
				this.rendererData = null;

				if (this.row != null)
				{
					this.row.Cells.Remove(this);
				}

				this.row = null;
				this.index = -1;
				this.cellStyle = null;
				this.checkStyle = null;
				this.imageStyle = null;
				this.tooltipText = null;

				this.state = (byte) 0;
				
				this.disposed = true;
			}
		}


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
		/// Gets or sets the text displayed by the Cell
		/// </summary>
		[Category("Appearance"),
		DefaultValue(null),
		Description("The text displayed by the cell")]
		public string Text
		{
			get
			{
				return this.text;
			}

			set
			{
				if (this.text == null || !this.text.Equals(value))
				{
					string oldText = this.Text;
					
					this.text = value;

                    this._widthSet = false; // Need to re-calc the width

					this.OnPropertyChanged(new CellEventArgs(this, CellEventType.ValueChanged, oldText));
				}
			}
		}


		/// <summary>
		/// Gets or sets the Cells non-text data
		/// </summary>
		[Category("Appearance"),
		DefaultValue(null),
		Description("The non-text data displayed by the cell"),
		TypeConverter(typeof(StringConverter))]
		public object Data
		{
			get
			{
				return this.data;
			}

			set
			{
				if (this.data != value)
				{
					object oldData = this.Data;
					
					this.data = value;

                    this._widthSet = false; // Need to re-calc the width

					this.OnPropertyChanged(new CellEventArgs(this, CellEventType.ValueChanged, oldData));
				}
			}
		}


		/// <summary>
		/// Gets or sets the object that contains data about the Cell
		/// </summary>
		[Category("Appearance"),
		DefaultValue(null),
		Description("User defined data associated with the cell"),
		TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.tag;
			}

			set
			{
				this.tag = value;
			}
		}


		/// <summary>
		/// Gets or sets the CellStyle used by the Cell
		/// </summary>
		[Browsable(false),
		DefaultValue(null)]
		public CellStyle CellStyle
		{
			get
			{
				return this.cellStyle;
			}

			set
			{
				if (this.cellStyle != value)
				{
					CellStyle oldStyle = this.CellStyle;
					
					this.cellStyle = value;

					this.OnPropertyChanged(new CellEventArgs(this, CellEventType.StyleChanged, oldStyle));
				}
			}
		}


		/// <summary>
		/// Gets or sets whether the Cell is selected
		/// </summary>
		[Browsable(false)]
		public bool Selected
		{
			get
			{
				return this.GetState(STATE_SELECTED);
			}
		}


		/// <summary>
		/// Sets whether the Cell is selected
		/// </summary>
		/// <param name="selected">A boolean value that specifies whether the 
		/// cell is selected</param>
		internal void SetSelected(bool selected)
		{
			this.SetState(STATE_SELECTED, selected);
		}

		/// <summary>
		/// Gets of sets whether text can wrap in this cell (and force the cell's height to increase)
		/// </summary>
        [Category("Appearance"),
        Description("Whether the text can wrap (and force the cell's height to increase)")]
        public bool WordWrap
        {
            get
            {
                if (this.CellStyle == null || !this.CellStyle.IsWordWrapSet)
                    return false;
                else
                    return this.CellStyle.WordWrap;
            }

            set
            {
                if (this.CellStyle == null)
                {
                    this.CellStyle = new CellStyle();
                }

                if (this.CellStyle.WordWrap != value)
                {
                    bool oldValue = this.CellStyle.WordWrap;
                    this.CellStyle.WordWrap = value;
                    this.OnPropertyChanged(new CellEventArgs(this, CellEventType.WordWrapChanged, oldValue));
                }
            }
        }

		/// <summary>
		/// Gets or sets the background Color for the Cell
		/// </summary>
		[Category("Appearance"),
		Description("The background color used to display text and graphics in the cell")]
		public Color BackColor
		{
			get
			{
				if (this.CellStyle == null || ! this.CellStyle.IsBackColorSet)
				{
					if (this.Row != null)
						return this.Row.BackColor;
                    else
    					return Color.Transparent;
				}
				
				return this.CellStyle.BackColor;
			}

			set
			{
				if (this.CellStyle == null)
				{
					this.CellStyle = new CellStyle();
				}
				
				if (this.CellStyle.BackColor != value)
				{
					Color oldBackColor = this.BackColor;
					
					this.CellStyle.BackColor = value;

					this.OnPropertyChanged(new CellEventArgs(this, CellEventType.BackColorChanged, oldBackColor));
				}
			}
		}


		/// <summary>
		/// Specifies whether the BackColor property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the BackColor property should be serialized, 
		/// false otherwise</returns>
		private bool ShouldSerializeBackColor()
		{
			return (this.cellStyle != null && this.cellStyle.BackColor != Color.Empty);
		}


		/// <summary>
		/// Gets or sets the foreground Color for the Cell
		/// </summary>
		[Category("Appearance"),
		Description("The foreground color used to display text and graphics in the cell")]
		public Color ForeColor
		{
			get
			{
				if (this.CellStyle == null || !this.CellStyle.IsForeColorSet)
				{
					if (this.Row != null)
						return this.Row.ForeColor;
                    else
    					return Color.Transparent;
				}
				else
				{
					if (this.CellStyle.ForeColor == Color.Empty || this.CellStyle.ForeColor == Color.Transparent)
					{
						if (this.Row != null)
						{
							return this.Row.ForeColor;
						}
					}

					return this.CellStyle.ForeColor;
				}
			}

			set
			{
				if (this.CellStyle == null)
				{
					this.CellStyle = new CellStyle();
				}
				
				if (this.CellStyle.ForeColor != value)
				{
					Color oldForeColor = this.ForeColor;
					
					this.CellStyle.ForeColor = value;

					this.OnPropertyChanged(new CellEventArgs(this, CellEventType.ForeColorChanged, oldForeColor));
				}
			}
		}


		/// <summary>
		/// Specifies whether the ForeColor property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the ForeColor property should be serialized, 
		/// false otherwise</returns>
		private bool ShouldSerializeForeColor()
		{
			return (this.cellStyle != null && this.cellStyle.ForeColor != Color.Empty);
		}


		/// <summary>
		/// Gets or sets the Font used by the Cell
		/// </summary>
		[Category("Appearance"),
		Description("The font used to display text in the cell")]
		public Font Font
		{
			get
			{
				if (this.CellStyle == null || !this.CellStyle.IsFontSet)
				{
					if (this.Row != null)
						return this.Row.Font;
                    else
    					return null;
				}
				else
				{
					if (this.CellStyle.Font == null)
					{
						if (this.Row != null)
						{
							return this.Row.Font;
						}
					}

					return this.CellStyle.Font;
				}
			}

			set
			{
				if (this.CellStyle == null)
				{
					this.CellStyle = new CellStyle();
				}
				
				if (this.CellStyle.Font != value)
				{
					Font oldFont = this.Font;
					
					this.CellStyle.Font = value;

					this.OnPropertyChanged(new CellEventArgs(this, CellEventType.FontChanged, oldFont));
				}
			}
		}


		/// <summary>
		/// Specifies whether the Font property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the Font property should be serialized, 
		/// false otherwise</returns>
		private bool ShouldSerializeFont()
		{
			return (this.cellStyle != null && this.cellStyle.Font != null);
		}


		/// <summary>
		/// Gets or sets the amount of space between the Cells Border and its contents
		/// </summary>
		[Category("Appearance"),
		Description("The amount of space between the cells border and its contents")]
		public CellPadding Padding
		{
			get
			{
				if (this.CellStyle == null || !this.CellStyle.IsPaddingSet)
					return CellPadding.Empty;
				else
    				return this.CellStyle.Padding;
			}

			set
			{
				if (this.CellStyle == null)
				{
					this.CellStyle = new CellStyle();
				}
				
				if (this.CellStyle.Padding != value)
				{
					CellPadding oldPadding = this.Padding;
					
					this.CellStyle.Padding = value;

					this.OnPropertyChanged(new CellEventArgs(this, CellEventType.PaddingChanged, oldPadding));
				}
			}
		}


		/// <summary>
		/// Specifies whether the Padding property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the Padding property should be serialized, 
		/// false otherwise</returns>
		private bool ShouldSerializePadding()
		{
			return this.Padding != CellPadding.Empty;
		}


		/// <summary>
		/// Gets or sets whether the Cell is in the checked state
		/// </summary>
		[Category("Appearance"),
		DefaultValue(false),
		Description("Indicates whether the cell is checked or unchecked"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
		RefreshProperties(RefreshProperties.Repaint)]
		public bool Checked
		{
			get
			{
				if (this.checkStyle == null)
				{
					return false;
				}
				
				return this.checkStyle.Checked;
			}

			set
			{
				if (this.checkStyle == null)
				{
					this.checkStyle = new CellCheckStyle();
				}
				
				if (this.checkStyle.Checked != value)
				{
					bool oldCheck = this.Checked;
					
					this.checkStyle.Checked = value;

					this.OnPropertyChanged(new CellEventArgs(this, CellEventType.CheckStateChanged, oldCheck));
				}
			}
		}


		/// <summary>
		/// Gets or sets the state of the Cells check box
		/// </summary>
		[Category("Appearance"),
		DefaultValue(CheckState.Unchecked),
		Description("Indicates the state of the cells check box"),
		RefreshProperties(RefreshProperties.Repaint)]
		public CheckState CheckState
		{
			get
			{
				if (this.checkStyle == null)
				{
					return CheckState.Unchecked;
				}
				
				return this.checkStyle.CheckState;
			}

			set
			{
				if (!Enum.IsDefined(typeof(CheckState), value))
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(CheckState));
				}
				
				if (this.checkStyle == null)
				{
					this.checkStyle = new CellCheckStyle();
				}
				
				if (this.checkStyle.CheckState != value)
				{
					CheckState oldCheckState = this.CheckState;
					
					this.checkStyle.CheckState = value;

					this.OnPropertyChanged(new CellEventArgs(this, CellEventType.CheckStateChanged, oldCheckState));
				}
			}
		}


		/// <summary>
		/// Gets or sets a value indicating whether the Cells check box 
		/// will allow three check states rather than two
		/// </summary>
		[Category("Appearance"),
		DefaultValue(false),
		Description("Controls whether or not the user can select the indeterminate state of the cells check box"),
		RefreshProperties(RefreshProperties.Repaint)]
		public bool ThreeState
		{
			get
			{
				if (this.checkStyle == null)
				{
					return false;
				}
				
				return this.checkStyle.ThreeState;
			}

			set
			{
				if (this.checkStyle == null)
				{
					this.checkStyle = new CellCheckStyle();
				}
				
				if (this.checkStyle.ThreeState != value)
				{
					bool oldThreeState = this.ThreeState;
					
					this.checkStyle.ThreeState = value;

					this.OnPropertyChanged(new CellEventArgs(this, CellEventType.ThreeStateChanged, oldThreeState));
				}
			}
		}


		/// <summary>
		/// Gets or sets the image that is displayed in the Cell
		/// </summary>
		[Category("Appearance"),
		DefaultValue(null),
		Description("The image that will be displayed in the cell")]
		public Image Image
		{
			get
			{
				if (this.imageStyle == null)
				{
					return null;
				}
				
				return this.imageStyle.Image;
			}

			set
			{
				if (this.imageStyle == null)
				{
					this.imageStyle = new CellImageStyle();
				}
				
				if (this.imageStyle.Image != value)
				{
					Image oldImage = this.Image;
					
					this.imageStyle.Image = value;

					this.OnPropertyChanged(new CellEventArgs(this, CellEventType.ImageChanged, oldImage));
				}
			}
		}


		/// <summary>
		/// Gets or sets how the Cells image is sized within the Cell
		/// </summary>
		[Category("Appearance"),
		DefaultValue(ImageSizeMode.Normal),
		Description("Controls how the image is sized within the cell")]
		public ImageSizeMode ImageSizeMode
		{
			get
			{
				if (this.imageStyle == null)
				{
					return ImageSizeMode.Normal;
				}
				
				return this.imageStyle.ImageSizeMode;
			}

			set
			{
				if (this.imageStyle == null)
				{
					this.imageStyle = new CellImageStyle();
				}
				
				if (this.imageStyle.ImageSizeMode != value)
				{
					ImageSizeMode oldSizeMode = this.ImageSizeMode;
					
					this.imageStyle.ImageSizeMode = value;

					this.OnPropertyChanged(new CellEventArgs(this, CellEventType.ImageSizeModeChanged, oldSizeMode));
				}
			}
		}


		/// <summary>
		/// Gets or sets a value indicating whether the Cells contents are able 
		/// to be edited
		/// </summary>
		[Category("Appearance"),
		Description("Controls whether the cells contents are able to be changed by the user")]
		public bool Editable
		{
			get
			{
				if (!this.GetState(STATE_EDITABLE))
				{
					return false;
				}

				if (this.Row == null)
				{
					return this.Enabled;
				}

				return this.Enabled && this.Row.Editable;
			}

			set
			{
				bool editable = this.Editable;
				
				this.SetState(STATE_EDITABLE, value);
				
				if (editable != value)
				{
					this.OnPropertyChanged(new CellEventArgs(this, CellEventType.EditableChanged, editable));
				}
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
		/// Gets or sets a value indicating whether the Cell 
		/// can respond to user interaction
		/// </summary>
		[Category("Appearance"),
		Description("Indicates whether the cell is enabled")]
		public bool Enabled
		{
			get
			{
				if (!this.GetState(STATE_ENABLED))
				{
					return false;
				}

				if (this.Row == null)
				{
					return true;
				}

				return this.Row.Enabled;
			}

			set
			{
				bool enabled = this.Enabled;
				
				this.SetState(STATE_ENABLED, value);
				
				if (enabled != value)
				{
					this.OnPropertyChanged(new CellEventArgs(this, CellEventType.EnabledChanged, enabled));
				}
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
		/// Gets or sets the text displayed in the Cells tooltip
		/// </summary>
		[Category("Appearance"),
		DefaultValue(null),
		Description("The text displayed in the cells tooltip")]
		public string ToolTipText
		{
			get
			{
				return this.tooltipText;
			}

			set
			{
				if (this.tooltipText != value)
				{
					string oldToolTip = this.tooltipText;
					
					this.tooltipText = value;

					this.OnPropertyChanged(new CellEventArgs(this, CellEventType.ToolTipTextChanged, oldToolTip));
				}
			}
		}

        /// <summary>
        /// Indicates whether the text has all been shown when rendered.
        /// </summary>
        private bool _isTextTrimmed
        {
            get
            {
                return (_state & STATE_IS_TEXT_TRIMMED) != 0;
            }
            set
            {
                if (value)
                    _state |= (byte)STATE_IS_TEXT_TRIMMED;
                else
                    _state &= (byte)~STATE_IS_TEXT_TRIMMED;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the text has all been shown when rendered.
        /// </summary>
        internal bool InternalIsTextTrimmed
        {
            get { return _isTextTrimmed; }
            set { _isTextTrimmed = value; }
        }

        /// <summary>
        /// Gets a value that indicates whether the text has all been shown when rendered.
        /// </summary>
        [Browsable(false)]
        public bool IsTextTrimmed
        {
            get { return _isTextTrimmed; }
        }

        private short _Width;

        private int _width
        {
            get { return _Width; }
            set { _Width = (short)value; }
        }

        /// <summary>
        /// Gets or sets the minimum width required to display this cell.
        /// </summary>
        [Browsable(false)]
        public int ContentWidth
        {
            get { return _width; }
            set
            {
                _width = value;
                _widthSet = true;
            }
        }

        private bool _widthSet
        {
            get
            {
                return (_state & STATE_WIDTH_SET) != 0;
            }
            set
            {
                if (value)
                    _state |= (byte)STATE_WIDTH_SET;
                else
                    _state &= (byte)~STATE_WIDTH_SET;
            }
        }

        /// <summary>
        /// Returns true if the cells width property has been assigned.
        /// </summary>
        [Browsable(false)]
        public bool WidthNotSet
        {
            get { return !_widthSet; }
        }

        /// <summary>
        /// Gets or sets how many columns this cell occupies
        /// </summary>
        [Category("Appearance"),
        DefaultValue(1),
        Description("How many columns this cell occupies")]
        public int ColSpan
        {
            get
            {
                return this.colspan;
            }

            set
            {
                this.colspan = value;
            }
        }


		/// <summary>
		/// Gets or sets the information used by CellRenderers to record the current 
		/// state of the Cell
		/// </summary>
		protected internal object RendererData
		{
			get
			{
				return this.rendererData;
			}

			set
			{
				this.rendererData = value;
			}
		}


		/// <summary>
		/// Gets the Row that the Cell belongs to
		/// </summary>
		[Browsable(false)]
		public Row Row
		{
			get
			{
				return this.row;
			}
		}


		/// <summary>
		/// Gets or sets the Row that the Cell belongs to
		/// </summary>
		internal Row InternalRow
		{
			get
			{
				return this.row;
			}

			set
			{
				this.row = value;
			}
		}


		/// <summary>
		/// Gets the index of the Cell within its Row
		/// </summary>
		[Browsable(false)]
		public int Index
		{
			get
			{
				return this.index;
			}
		}


		/// <summary>
		/// Gets or sets the index of the Cell within its Row
		/// </summary>
		internal int InternalIndex
		{
			get
			{
				return this.index;
			}

			set
			{
				this.index = value;
			}
		}


		/// <summary>
		/// Gets whether the Cell is able to raise events
		/// </summary>
		protected internal bool CanRaiseEvents
		{
			get
			{
				// check if the Row that the Cell belongs to is able to 
				// raise events (if it can't, the Cell shouldn't raise 
				// events either)
				if (this.Row != null)
				{
					return this.Row.CanRaiseEvents;
				}

				return true;
			}
		}

		#endregion


		#region Events

		/// <summary>
		/// Raises the PropertyChanged event
		/// </summary>
		/// <param name="e">A CellEventArgs that contains the event data</param>
		protected virtual void OnPropertyChanged(CellEventArgs e)
		{
			e.SetColumn(this.Index);

			if (this.Row != null)
			{
				e.SetRow(this.Row.Index);
			}
			
			if (this.CanRaiseEvents)
			{
				if (this.Row != null)
				{
					this.Row.OnCellPropertyChanged(e);
				}
				
				if (PropertyChanged != null)
				{
					PropertyChanged(this, e);
				}
			}
		}

		#endregion
	}
}
