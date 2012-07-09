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
using XPTable.Models.Design;
using XPTable.Renderers;
using XPTable.Sorting;


namespace XPTable.Models
{
	/// <summary>
	/// Represents a Column whose Cells are displayed as a ComboBox
	/// </summary>
	[DesignTimeVisible(false),
	ToolboxItem(false)]
	public class ComboBoxColumn : DropDownColumn
	{
		#region Constructor
		
		/// <summary>
		/// Creates a new ComboBoxColumn with default values
		/// </summary>
		public ComboBoxColumn() : base()
		{

		}


		/// <summary>
		/// Creates a new ComboBoxColumn with the specified header text
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		public ComboBoxColumn(string text) : base(text)
		{

		}


		/// <summary>
		/// Creates a new ComboBoxColumn with the specified header text and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		public ComboBoxColumn(string text, int width) : base(text, width)
		{

		}


		/// <summary>
		/// Creates a new ComboBoxColumn with the specified header text, width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public ComboBoxColumn(string text, int width, bool visible) : base(text, width, visible)
		{
		
		}


		/// <summary>
		/// Creates a new ComboBoxColumn with the specified header text and image
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		public ComboBoxColumn(string text, Image image) : base(text, image)
		{

		}


		/// <summary>
		/// Creates a new ComboBoxColumn with the specified header text, image and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		public ComboBoxColumn(string text, Image image, int width) : base(text, image, width)
		{

		}


		/// <summary>
		/// Creates a new ComboBoxColumn with the specified header text, image, width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public ComboBoxColumn(string text, Image image, int width, bool visible) : base(text, image, width, visible)
		{

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
			return "COMBOBOX";
		}


		/// <summary>
		/// Gets the Column's default CellRenderer
		/// </summary>
		/// <returns>The Column's default CellRenderer</returns>
		public override ICellRenderer CreateDefaultRenderer()
		{
			return new ComboBoxCellRenderer();
		}


		/// <summary>
		/// Gets a string that specifies the name of the Column's default CellEditor
		/// </summary>
		/// <returns>A string that specifies the name of the Column's default 
		/// CellEditor</returns>
		public override string GetDefaultEditorName()
		{
			return "COMBOBOX";
		}


		/// <summary>
		/// Gets the Column's default CellEditor
		/// </summary>
		/// <returns>The Column's default CellEditor</returns>
		public override ICellEditor CreateDefaultEditor()
		{
			return new ComboBoxCellEditor();
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets the Type of the Comparer used to compare the Column's Cells when 
		/// the Column is sorting
		/// </summary>
		public override Type DefaultComparerType
		{
			get
			{
				return typeof(TextComparer);
			}
		}

		#endregion
	}
}
