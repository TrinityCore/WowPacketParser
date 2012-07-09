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
	/// Represents a Column whose Cells are displayed as an Image
	/// </summary>
	[DesignTimeVisible(false),
	ToolboxItem(false)]
	public class ImageColumn : Column
	{
		#region Class Data

		/// <summary>
		/// Specifies whether any text contained in the Cell should be drawn
		/// </summary>
		private bool drawText;

		#endregion
		
		
		#region Constructor
		
		/// <summary>
		/// Creates a new ImageColumn with default values
		/// </summary>
		public ImageColumn() : base()
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new ImageColumn with the specified header text
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		public ImageColumn(string text) : base(text)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new ImageColumn with the specified header text and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		public ImageColumn(string text, int width) : base(text, width)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new ImageColumn with the specified header text, width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public ImageColumn(string text, int width, bool visible) : base(text, width, visible)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new ImageColumn with the specified header text and image
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		public ImageColumn(string text, Image image) : base(text, image)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new ImageColumn with the specified header text, image and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		public ImageColumn(string text, Image image, int width) : base(text, image, width)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new ImageColumn with the specified header text, image, width 
		/// and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public ImageColumn(string text, Image image, int width, bool visible) : base(text, image, width, visible)
		{
			this.Init();
		}


		/// <summary>
		/// Initializes the ImageColumn with default values
		/// </summary>
		private void Init()
		{
			this.drawText = true;
			this.Editable = false;
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
			return "IMAGE";
		}


		/// <summary>
		/// Gets the Column's default CellRenderer
		/// </summary>
		/// <returns>The Column's default CellRenderer</returns>
		public override ICellRenderer CreateDefaultRenderer()
		{
			return new ImageCellRenderer();
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
		/// Gets or sets whether any text contained in the Column's Cells should be drawn
		/// </summary>
		[Category("Appearance"),
		DefaultValue(true),
		Description("Determines whether any text contained in the Column's Cells should be drawn")]
		public bool DrawText
		{
			get
			{
				return this.drawText;
			}

			set
			{
				if (this.drawText != value)
				{
					this.drawText = value;

					this.OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.RendererChanged, null));
				}
			}
		}


		/// <summary>
		/// Gets the Type of the Comparer used to compare the Column's Cells when 
		/// the Column is sorting
		/// </summary>
		public override Type DefaultComparerType
		{
			get
			{
				return typeof(ImageComparer);
			}
		}


		/// <summary>
		/// Gets or sets a value indicating whether the Column's Cells contents 
		/// are able to be edited
		/// </summary>
		[Category("Appearance"),
		DefaultValue(false),
		Description("Controls whether the column's cell contents are able to be changed by the user")]
		public new bool Editable
		{
			get
			{
				return base.Editable;
			}

			set
			{
				base.Editable = value;
			}
		}

		#endregion
	}
}
