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

using XPTable.Editors;
using XPTable.Events;
using XPTable.Models.Design;
using XPTable.Renderers;
using XPTable.Sorting;

namespace XPTable.Models
{
	/// <summary>
	/// Represents a Column whose Cells are displayed as a Button
	/// </summary>
	[DesignTimeVisible(false),
	ToolboxItem(false)]
	public class ButtonColumn : Column
	{
		#region Class Data
		/// <summary>
		/// Specifies the alignment of the Image displayed on the button
		/// </summary>
		private ContentAlignment imageAlignment;
        /// <summary>
        /// Specifies whether the button is shown in flat style or not.
        /// </summary>
        bool flatStyle = false;
		#endregion
		
		#region Constructor
		/// <summary>
		/// Creates a new ButtonColumn with default values
		/// </summary>
		public ButtonColumn() : base()
		{
			this.Init();
		}

		/// <summary>
		/// Creates a new ButtonColumn with the specified header text
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		public ButtonColumn(string text) : base(text)
		{
			this.Init();
		}

		/// <summary>
		/// Creates a new ButtonColumn with the specified header text and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		public ButtonColumn(string text, int width) : base(text, width)
		{
			this.Init();
		}

		/// <summary>
		/// Creates a new ButtonColumn with the specified header text, width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public ButtonColumn(string text, int width, bool visible) : base(text, width, visible)
		{
			this.Init();
		}

		/// <summary>
		/// Creates a new ButtonColumn with the specified header text and image
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		public ButtonColumn(string text, Image image) : base(text, image)
		{
			this.Init();
		}

		/// <summary>
		/// Creates a new ButtonColumn with the specified header text, image and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		public ButtonColumn(string text, Image image, int width) : base(text, image, width)
		{
			this.Init();
		}

		/// <summary>
		/// Creates a new ButtonColumn with the specified header text, image, width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public ButtonColumn(string text, Image image, int width, bool visible) : base(text, image, width, visible)
		{
			this.Init();
		}

		/// <summary>
		/// Initializes the ButtonColumn with default values
		/// </summary>
		private void Init()
		{
			this.Alignment = ColumnAlignment.Center;
			this.imageAlignment = ContentAlignment.MiddleCenter;
			this.Editable = false;
			this.Selectable = false;
		}
		#endregion

		#region Methods
		/// <summary>
		/// Gets a string that specifies the name of the Column's default CellRenderer
		/// </summary>
		/// <returns>A string that specifies the name of the Column's default 
		/// CellRenderer</returns>
		public override string GetDefaultRendererName()
		{
			return "BUTTON";
		}

		/// <summary>
		/// Gets the Column's default CellRenderer
		/// </summary>
		/// <returns>The Column's default CellRenderer</returns>
		public override ICellRenderer CreateDefaultRenderer()
		{
			return new ButtonCellRenderer();
		}

		/// <summary>
		/// Gets a string that specifies the name of the Column's default CellEditor
		/// </summary>
		/// <returns>A string that specifies the name of the Column's default 
		/// CellEditor</returns>
		public override string GetDefaultEditorName()
		{
			return null;
		}

		/// <summary>
		/// Gets the Column's default CellEditor
		/// </summary>
		/// <returns>The Column's default CellEditor</returns>
		public override ICellEditor CreateDefaultEditor()
		{
			return null;
		}
		#endregion

		#region Properties
        /// <summary>
        /// Gets or sets the horizontal alignment of the Column's Cell contents
        /// </summary>
        [Category("Appearance"),
        DefaultValue(ColumnAlignment.Center),
        Description("The horizontal alignment of the column's cell contents.")]
        public override ColumnAlignment Alignment
        {
            get { return base.Alignment; }
            set { base.Alignment = value; }
        }

		/// <summary>
        /// Gets or sets the flag that determines whether buttons are shown flat or normal.
		/// </summary>
		[Category("Appearance"),
        DefaultValue(false),
		Description("The display style for the buttons.")]
		public bool FlatStyle
		{
			get { return this.flatStyle; }
            set { this.flatStyle = value; }
		}

		/// <summary>
		/// Gets or sets the alignment of the Image displayed on the buttons
		/// </summary>
		[Category("Appearance"),
		DefaultValue(ContentAlignment.MiddleCenter),
		Description("The alignment of the Image displayed on the buttons")]
		public ContentAlignment ImageAlignment
		{
			get { return this.imageAlignment; }
			set
			{
				if (!Enum.IsDefined(typeof(ContentAlignment), value)) 
					throw new InvalidEnumArgumentException("value", (int) value, typeof(ContentAlignment));
					
				if (this.imageAlignment != value)
				{
					this.imageAlignment = value;
					this.OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.RendererChanged, null));
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the Column's Cells contents 
		/// are able to be edited
		/// </summary>
		[Category("Appearance"),
		DefaultValue(false),
		Description("Controls whether the column's cell contents are able to be changed by the user")]
		public override bool Editable
		{
			get { return base.Editable; }
			set { base.Editable = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether the Column's Cells can be selected
		/// </summary>
		[Category("Appearance"),
		DefaultValue(false),
		Description("Indicates whether the column's cells can be selected")]
		public override bool Selectable
		{
			get { return base.Selectable; }
			set { base.Selectable = value; }
		}

		/// <summary>
		/// Gets the Type of the Comparer used to compare the Column's Cells when 
		/// the Column is sorting
		/// </summary>
		public override Type DefaultComparerType
		{
			get { return typeof(TextComparer); }
		}
		#endregion
	}
}
