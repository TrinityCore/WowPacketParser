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
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

using XPTable.Editors;
using XPTable.Events;
using XPTable.Models;
using XPTable.Themes;


namespace XPTable.Renderers
{
	/// <summary>
	/// Base class for CellRenderers that Cell contents like ComboBoxes
	/// </summary>
	public abstract class DropDownCellRenderer : CellRenderer
	{
		#region Class Data

		/// <summary>
		/// The width of the DropDownCellRenderer's dropdown button
		/// </summary>
		private int buttonWidth;

		/// <summary>
		/// Specifies whether the DropDownCellRenderer dropdown button should be drawn
		/// </summary>
		private bool showButton;

		#endregion


		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the DropDownCellRenderer class with 
		/// default settings
		/// </summary>
		protected DropDownCellRenderer() : base()
		{
			this.buttonWidth = 15;
			this.showButton = true;
		}

		#endregion


		#region Methods

		/// <summary>
		/// Gets the Rectangle that specifies the Size and Location of 
		/// the current Cell's dropdown button
		/// </summary>
		/// <returns>A Rectangle that specifies the Size and Location of 
		/// the current Cell's dropdown button</returns>
		protected internal Rectangle CalcDropDownButtonBounds()
		{
			Rectangle buttonRect = this.ClientRectangle;

			buttonRect.Width = this.ButtonWidth;
			buttonRect.X = this.ClientRectangle.Right - buttonRect.Width;

			if (buttonRect.Width > this.ClientRectangle.Width)
			{
				buttonRect = this.ClientRectangle;
			}

			return buttonRect;
		}


		/// <summary>
		/// Gets the DropDownRendererData specific data used by the Renderer from 
		/// the specified Cell
		/// </summary>
		/// <param name="cell">The Cell to get the DropDownRendererData data for</param>
		/// <returns>The DropDownRendererData data for the specified Cell</returns>
		protected DropDownRendererData GetDropDownRendererData(Cell cell)
		{
			object rendererData = this.GetRendererData(cell);

			if (rendererData == null || !(rendererData is DropDownRendererData))
			{
				rendererData = new DropDownRendererData();

				this.SetRendererData(cell, rendererData);
			}

			return (DropDownRendererData) rendererData;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the width of the dropdown button
		/// </summary>
		protected internal int ButtonWidth
		{
			get
			{
				return this.buttonWidth;
			}

			set
			{
				this.buttonWidth = value;
			}
		}


		/// <summary>
		/// Gets or sets whether the DropDownCellRenderer dropdown button should be drawn
		/// </summary>
		protected bool ShowDropDownButton
		{
			get
			{
				return this.showButton;
			}

			set
			{
				this.showButton = value;
			}
		}

		#endregion


		#region Events

		#region Mouse

		#region MouseLeave

		/// <summary>
		/// Raises the MouseLeave event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		public override void OnMouseLeave(CellMouseEventArgs e)
		{
			base.OnMouseLeave(e);

			if (this.ShowDropDownButton || (e.Table.IsEditing && e.CellPos == e.Table.EditingCell))
			{
				if (e.Table.IsCellEditable(e.CellPos))
				{
					// get the button renderer data
					DropDownRendererData rendererData = this.GetDropDownRendererData(e.Cell);

					if (rendererData.ButtonState != ComboBoxState.Normal)
					{
						rendererData.ButtonState = ComboBoxState.Normal;

						e.Table.Invalidate(e.CellRect);
					}
				}
			}
		}

		#endregion

		#region MouseUp

		/// <summary>
		/// Raises the MouseUp event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		public override void OnMouseUp(CellMouseEventArgs e)
		{
			base.OnMouseUp(e);

			if (this.ShowDropDownButton || (e.Table.IsEditing && e.CellPos == e.Table.EditingCell))
			{
				if (e.Table.IsCellEditable(e.CellPos))
				{
					// get the renderer data
					DropDownRendererData rendererData = this.GetDropDownRendererData(e.Cell);

					if (this.CalcDropDownButtonBounds().Contains(e.X, e.Y))
					{
						rendererData.ButtonState = ComboBoxState.Hot;

						e.Table.Invalidate(e.CellRect);
					}
				}
			}
		}

		#endregion

		#region MouseDown

		/// <summary>
		/// Raises the MouseDown event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		public override void OnMouseDown(CellMouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (this.ShowDropDownButton || (e.Table.IsEditing && e.CellPos == e.Table.EditingCell))
			{
				if (e.Table.IsCellEditable(e.CellPos))
				{
					// get the button renderer data
					DropDownRendererData rendererData = this.GetDropDownRendererData(e.Cell);

					if (this.CalcDropDownButtonBounds().Contains(e.X, e.Y))
					{
						if (!(e.Table.ColumnModel.GetCellEditor(e.CellPos.Column) is DropDownCellEditor))
						{
							throw new InvalidOperationException("Cannot edit Cell as DropDownCellRenderer requires a DropDownColumn that uses a DropDownCellEditor");
						}
						
						rendererData.ButtonState = ComboBoxState.Pressed;
						
						if (!e.Table.IsEditing)
						{
							e.Table.EditCell(e.CellPos);
						}

						//netus - fix from John Boyce on 2006-02-08
						if (e.Table.IsEditing)
						{
							((IEditorUsesRendererButtons)e.Table.EditingCellEditor).OnEditorButtonMouseDown(this, e);

							e.Table.Invalidate(e.CellRect);
						}
					}
				}
			}
		}

		#endregion

		#region MouseMove
	
		/// <summary>
		/// Raises the MouseMove event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		public override void OnMouseMove(XPTable.Events.CellMouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (this.ShowDropDownButton || (e.Table.IsEditing && e.CellPos == e.Table.EditingCell))
			{
				if (e.Table.IsCellEditable(e.CellPos))
				{
					// get the button renderer data
					DropDownRendererData rendererData = this.GetDropDownRendererData(e.Cell);

					if (this.CalcDropDownButtonBounds().Contains(e.X, e.Y))
					{
						if (rendererData.ButtonState == ComboBoxState.Normal)
						{
							if (e.Button == MouseButtons.Left && e.Row == e.Table.LastMouseDownCell.Row && e.Column == e.Table.LastMouseDownCell.Column)
							{
								rendererData.ButtonState = ComboBoxState.Pressed;
							}
							else
							{
								rendererData.ButtonState = ComboBoxState.Hot;
							}

							e.Table.Invalidate(e.CellRect);
						}
					}
					else
					{
						if (rendererData.ButtonState != ComboBoxState.Normal)
						{
							rendererData.ButtonState = ComboBoxState.Normal;

							e.Table.Invalidate(e.CellRect);
						}
					}
				}
			}
		}

		#endregion

		#endregion

		#region Paint

		/// <summary>
		/// Raises the PaintCell event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		public override void OnPaintCell(PaintCellEventArgs e)
		{
			if (e.Table.ColumnModel.Columns[e.Column] is DropDownColumn)
			{
				this.showButton = ((DropDownColumn) e.Table.ColumnModel.Columns[e.Column]).ShowDropDownButton;
			}
			else
			{
				this.showButton = true;
			}
			
			base.OnPaintCell(e);
		}


		/// <summary>
		/// Paints the Cells background
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		protected override void OnPaintBackground(PaintCellEventArgs e)
		{
			base.OnPaintBackground(e);

			// don't bother going any further if the Cell is null 
			if (e.Cell == null)
			{
				return;
			}

			if (this.ShowDropDownButton || (e.Table.IsEditing && e.CellPos == e.Table.EditingCell))
			{
				ComboBoxState state = this.GetDropDownRendererData(e.Cell).ButtonState;

				if (!e.Enabled)
				{
					state = ComboBoxState.Disabled;
				}

				ThemeManager.DrawComboBoxButton(e.Graphics, this.CalcDropDownButtonBounds(), state);
			}
		}

		#endregion

		#endregion
	}
}
