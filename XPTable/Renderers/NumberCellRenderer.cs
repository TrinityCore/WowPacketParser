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
using System.Windows.Forms.VisualStyles;

using XPTable.Editors;
using XPTable.Events;
using XPTable.Models;
using XPTable.Themes;


namespace XPTable.Renderers
{
	/// <summary>
	/// A base class for drawing Cells contents as numbers
	/// </summary>
	public class NumberCellRenderer : CellRenderer
	{
		#region Class Data

		/// <summary>
		/// The width of the ComboBox's dropdown button
		/// </summary>
		private int buttonWidth;

		/// <summary>
		/// Specifies whether the up and down buttons should be drawn
		/// </summary>
		private bool showUpDownButtons;

		/// <summary>
		/// The alignment of the up and down buttons in the Cell
		/// </summary>
		private LeftRightAlignment upDownAlignment;

		/// <summary>
		/// The maximum value for the Cell
		/// </summary>
		private decimal maximum;

		/// <summary>
		/// The minimum value for the Cell
		/// </summary>
		private decimal minimum;

		#endregion


		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the NumberCellRenderer class with 
		/// default settings
		/// </summary>
		public NumberCellRenderer() : base()
		{
			this.StringFormat.Trimming = StringTrimming.None;
			this.Format = "G";
			this.buttonWidth = 15;
			this.showUpDownButtons = false;
			this.upDownAlignment = LeftRightAlignment.Right;
			this.maximum = (decimal) 100;
			this.minimum = (decimal) 0;
		}

		#endregion


		#region Methods

		/// <summary>
		/// Returns a Rectangle that specifies the size and location of the 
		/// up and down buttons
		/// </summary>
		/// <returns>A Rectangle that specifies the size and location of the 
		/// up and down buttons</returns>
		protected Rectangle CalcButtonBounds()
		{
			Rectangle buttonRect = this.ClientRectangle;

			buttonRect.Width = this.ButtonWidth;

			if (this.UpDownAlign == LeftRightAlignment.Right)
			{
				buttonRect.X = this.ClientRectangle.Right - buttonRect.Width;
			}

			if (buttonRect.Width > this.ClientRectangle.Width)
			{
				buttonRect = this.ClientRectangle;
			}

			return buttonRect;
		}


		/// <summary>
		/// Returns a Rectangle that specifies the size and location of the up button
		/// </summary>
		/// <returns>A Rectangle that specifies the size and location of the up button</returns>
		protected Rectangle GetUpButtonBounds()
		{
			Rectangle buttonRect = this.CalcButtonBounds();

			buttonRect.Height /= 2;

			return buttonRect;
		}


		/// <summary>
		/// Returns a Rectangle that specifies the size and location of the down button
		/// </summary>
		/// <returns>A Rectangle that specifies the size and location of the down button</returns>
		protected Rectangle GetDownButtonBounds()
		{
			Rectangle buttonRect = this.CalcButtonBounds();

			int height = buttonRect.Height / 2;

			buttonRect.Height -= height;
			buttonRect.Y += height;

			return buttonRect;
		}


		/// <summary>
		/// Gets the NumberRendererData specific data used by the Renderer from 
		/// the specified Cell
		/// </summary>
		/// <param name="cell">The Cell to get the NumberRendererData data for</param>
		/// <returns>The NumberRendererData data for the specified Cell</returns>
		protected NumberRendererData GetNumberRendererData(Cell cell)
		{
			object rendererData = this.GetRendererData(cell);

			if (rendererData == null || !(rendererData is NumberRendererData))
			{
				rendererData = new NumberRendererData();

				this.SetRendererData(cell, rendererData);
			}

			return (NumberRendererData) rendererData;
		}


		/// <summary>
		/// Gets whether the specified Table is using a NumericCellEditor to edit the 
		/// Cell at the specified CellPos
		/// </summary>
		/// <param name="table">The Table to check</param>
		/// <param name="cellPos">A CellPos that represents the Cell to check</param>
		/// <returns>true if the specified Table is using a NumericCellEditor to edit the 
		/// Cell at the specified CellPos, false otherwise</returns>
		internal bool TableUsingNumericCellEditor(Table table, CellPos cellPos)
		{
			return (table.IsEditing && cellPos == table.EditingCell && table.EditingCellEditor is NumberCellEditor);
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
		/// Gets or sets whether the up and down buttons should be drawn
		/// </summary>
		protected bool ShowUpDownButtons
		{
			get
			{
				return this.showUpDownButtons;
			}

			set
			{
				this.showUpDownButtons = value;
			}
		}


		/// <summary>
		/// Gets or sets the alignment of the up and down buttons in the Cell
		/// </summary>
		protected LeftRightAlignment UpDownAlign
		{
			get
			{
				return this.upDownAlignment;
			}

			set
			{
				if (!Enum.IsDefined(typeof(LeftRightAlignment), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(LeftRightAlignment));
				}
					
				this.upDownAlignment = value;
			}
		}


		/// <summary>
		/// Gets or sets the maximum value for the Cell
		/// </summary>
		protected decimal Maximum
		{
			get
			{
				return this.maximum;
			}

			set
			{
				this.maximum = value;
				
				if (this.minimum > this.maximum)
				{
					this.minimum = this.maximum;
				}
			}
		}


		/// <summary>
		/// Gets or sets the minimum value for the Cell
		/// </summary>
		protected decimal Minimum
		{
			get
			{
				return this.minimum;
			}

			set
			{
				this.minimum = value;
				
				if (this.minimum > this.maximum)
				{
					this.maximum = value;
				}
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

			if (this.ShowUpDownButtons || this.TableUsingNumericCellEditor(e.Table, e.CellPos))
			{
				if (e.Table.IsCellEditable(e.CellPos))
				{
					// get the button renderer data
					NumberRendererData rendererData = this.GetNumberRendererData(e.Cell);

					if (rendererData.UpButtonState != UpDownState.Normal)
					{
						rendererData.UpButtonState = UpDownState.Normal;

						e.Table.Invalidate(e.CellRect);
					}
					else if (rendererData.DownButtonState != UpDownState.Normal)
					{
						rendererData.DownButtonState = UpDownState.Normal;

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

			//
			if (this.ShowUpDownButtons || this.TableUsingNumericCellEditor(e.Table, e.CellPos))
			{
				if (e.Table.IsCellEditable(e.CellPos))
				{
					// get the renderer data
					NumberRendererData rendererData = this.GetNumberRendererData(e.Cell);

					rendererData.ClickPoint = new Point(-1, -1);

					if (this.GetUpButtonBounds().Contains(e.X, e.Y))
					{
						rendererData.UpButtonState = UpDownState.Hot;

						if (!e.Table.IsEditing)
						{
                            e.Table.EditCell(e.CellPos);        // Editing may be cancelled by an event handler
						}

                        if (e.Table.IsEditing)
                        {
                            ((IEditorUsesRendererButtons)e.Table.EditingCellEditor).OnEditorButtonMouseUp(this, e);

                            e.Table.Invalidate(e.CellRect);
                        }
					}
					else if (this.GetDownButtonBounds().Contains(e.X, e.Y))
					{
						rendererData.DownButtonState = UpDownState.Hot;

						if (!e.Table.IsEditing)
						{
							e.Table.EditCell(e.CellPos);
						}

                        if (e.Table.IsEditing)
                        {
                            ((IEditorUsesRendererButtons)e.Table.EditingCellEditor).OnEditorButtonMouseUp(this, e);

                            e.Table.Invalidate(e.CellRect);
                        }
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

			//
			if (this.ShowUpDownButtons || this.TableUsingNumericCellEditor(e.Table, e.CellPos))
			{
				if (e.Table.IsCellEditable(e.CellPos))
				{
					// get the button renderer data
					NumberRendererData rendererData = this.GetNumberRendererData(e.Cell);

					rendererData.ClickPoint = new Point(e.X, e.Y);
					
					if (this.CalcButtonBounds().Contains(e.X, e.Y))
					{
						if (!(e.Table.ColumnModel.GetCellEditor(e.CellPos.Column) is NumberCellEditor))
						{
							throw new InvalidOperationException("Cannot edit Cell as NumberCellRenderer requires a NumberColumn that uses a NumberCellEditor");
						}
						
						if (!e.Table.IsEditing)
						{
							e.Table.EditCell(e.CellPos);    // Editing may be cancelled by an event handler
						}

                        if (e.Table.IsEditing)
                        {
                            if (this.GetUpButtonBounds().Contains(e.X, e.Y))
                            {
                                rendererData.UpButtonState = UpDownState.Pressed;

                                ((IEditorUsesRendererButtons)e.Table.EditingCellEditor).OnEditorButtonMouseDown(this, e);

                                e.Table.Invalidate(e.CellRect);
                            }
                            else if (this.GetDownButtonBounds().Contains(e.X, e.Y))
                            {
                                rendererData.DownButtonState = UpDownState.Pressed;

                                ((IEditorUsesRendererButtons)e.Table.EditingCellEditor).OnEditorButtonMouseDown(this, e);

                                e.Table.Invalidate(e.CellRect);
                            }
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

			if (this.ShowUpDownButtons || this.TableUsingNumericCellEditor(e.Table, e.CellPos))
			{
				if (e.Table.IsCellEditable(e.CellPos))
				{
					// get the button renderer data
					NumberRendererData rendererData = this.GetNumberRendererData(e.Cell);

					if (this.GetUpButtonBounds().Contains(e.X, e.Y))
					{
						if (rendererData.UpButtonState == UpDownState.Normal)
						{
							if (e.Button == MouseButtons.Left && e.Row == e.Table.LastMouseDownCell.Row && e.Column == e.Table.LastMouseDownCell.Column)
							{
								if (this.GetUpButtonBounds().Contains(rendererData.ClickPoint))
								{
									rendererData.UpButtonState = UpDownState.Pressed;

									if (this.TableUsingNumericCellEditor(e.Table, e.CellPos))
									{
										((IEditorUsesRendererButtons) e.Table.EditingCellEditor).OnEditorButtonMouseDown(this, e);
									}
								}
								else if (this.GetDownButtonBounds().Contains(rendererData.ClickPoint))
								{
									rendererData.DownButtonState = UpDownState.Normal;

									if (this.TableUsingNumericCellEditor(e.Table, e.CellPos))
									{
										((IEditorUsesRendererButtons) e.Table.EditingCellEditor).OnEditorButtonMouseUp(this, e);
									}
								}
							}
							else
							{
								rendererData.UpButtonState = UpDownState.Hot;

								if (rendererData.DownButtonState == UpDownState.Hot)
								{
									rendererData.DownButtonState = UpDownState.Normal;
								}
							}

							e.Table.Invalidate(e.CellRect);
						}
					}
					else if (this.GetDownButtonBounds().Contains(e.X, e.Y))
					{
						if (rendererData.DownButtonState == UpDownState.Normal)
						{
							if (e.Button == MouseButtons.Left && e.Row == e.Table.LastMouseDownCell.Row && e.Column == e.Table.LastMouseDownCell.Column)
							{
								if (this.GetDownButtonBounds().Contains(rendererData.ClickPoint))
								{
									rendererData.DownButtonState = UpDownState.Pressed;

									if (this.TableUsingNumericCellEditor(e.Table, e.CellPos))
									{
										((IEditorUsesRendererButtons) e.Table.EditingCellEditor).OnEditorButtonMouseDown(this, e);
									}
								}
								else if (this.GetUpButtonBounds().Contains(rendererData.ClickPoint))
								{
									rendererData.UpButtonState = UpDownState.Normal;

									if (this.TableUsingNumericCellEditor(e.Table, e.CellPos))
									{
										((IEditorUsesRendererButtons) e.Table.EditingCellEditor).OnEditorButtonMouseUp(this, e);
									}
								}
							}
							else
							{
								rendererData.DownButtonState = UpDownState.Hot;

								if (rendererData.UpButtonState == UpDownState.Hot)
								{
									rendererData.UpButtonState = UpDownState.Normal;
								}
							}

							e.Table.Invalidate(e.CellRect);
						}
					}
					else
					{
						if (rendererData.UpButtonState != UpDownState.Normal || rendererData.DownButtonState != UpDownState.Normal)
						{
							rendererData.UpButtonState = UpDownState.Normal;
							rendererData.DownButtonState = UpDownState.Normal;

							if (this.TableUsingNumericCellEditor(e.Table, e.CellPos))
							{
								((IEditorUsesRendererButtons) e.Table.EditingCellEditor).OnEditorButtonMouseUp(this, e);
							}

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
			if (e.Table.ColumnModel.Columns[e.Column] is NumberColumn)
			{
				NumberColumn column = (NumberColumn) e.Table.ColumnModel.Columns[e.Column];
			
				this.ShowUpDownButtons = column.ShowUpDownButtons;
				this.UpDownAlign = column.UpDownAlign;
				this.Maximum = column.Maximum;
				this.Minimum = column.Minimum;

				// if the table is editing this cell and the editor is a 
				// NumberCellEditor then we should display the updown buttons
				if (e.Table.IsEditing && e.Table.EditingCell == e.CellPos && e.Table.EditingCellEditor is NumberCellEditor)
				{
					this.ShowUpDownButtons = true;
				}
			}
			else
			{
				this.ShowUpDownButtons = false;
				this.UpDownAlign = LeftRightAlignment.Right;
				this.Maximum = 100;
				this.Minimum = 0;
			}
			
			base.OnPaintCell(e);
		}


		/// <summary>
		/// Raises the PaintBackground event
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

			if (this.ShowUpDownButtons)
			{
				UpDownState upButtonState = this.GetNumberRendererData(e.Cell).UpButtonState;
				UpDownState downButtonState = this.GetNumberRendererData(e.Cell).DownButtonState;
				
				if (!e.Enabled)
				{
					upButtonState = UpDownState.Disabled;
					downButtonState = UpDownState.Disabled;
				}

				ThemeManager.DrawUpDownButtons(e.Graphics, this.GetUpButtonBounds(), upButtonState, this.GetDownButtonBounds(), downButtonState);
			}
		}


		/// <summary>
		/// Raises the Paint event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		protected override void OnPaint(PaintCellEventArgs e)
		{
			base.OnPaint(e);

			// don't bother if the Cell is null
			if (e.Cell == null)
			{
				return;
			}
			
			// get the Cells value
			decimal decimalVal = decimal.MinValue;

            if (e.Cell.Data != null && 
				(e.Cell.Data is uint || e.Cell.Data is UInt16 || e.Cell.Data is UInt32 || e.Cell.Data is UInt64 || 
				e.Cell.Data is int || e.Cell.Data is Int16 || e.Cell.Data is Int32 || e.Cell.Data is Int64 || 
				e.Cell.Data is double || e.Cell.Data is float || e.Cell.Data is decimal))
			{
				decimalVal = Convert.ToDecimal(e.Cell.Data);
			}

			// draw the value
			if (decimalVal != decimal.MinValue)
			{
				Rectangle textRect = this.ClientRectangle;

				if (this.ShowUpDownButtons)
				{
					textRect.Width -= this.CalcButtonBounds().Width - 1;

					if (this.UpDownAlign == LeftRightAlignment.Left)
					{
						textRect.X = this.ClientRectangle.Right - textRect.Width;
					}
				}

                string text = decimalVal.ToString(this.Format, this.FormatProvider);

                if (e.Cell.WidthNotSet)
                {
                    SizeF size = e.Graphics.MeasureString(text, this.Font);
                    e.Cell.ContentWidth = (int)Math.Ceiling(size.Width);
                }

				if (e.Enabled)
                    DrawString(e.Graphics, text, this.Font, this.ForeBrush, textRect, e.Cell.WordWrap);
				else
                    DrawString(e.Graphics, text, this.Font, this.GrayTextBrush, textRect, e.Cell.WordWrap);
			}
			
			if( (e.Focused && e.Enabled)
				// only if we want to show selection rectangle
				&& ( e.Table.ShowSelectionRectangle ) )
			{
				Rectangle focusRect = this.ClientRectangle;

				if (this.ShowUpDownButtons)
				{
					focusRect.Width -= this.CalcButtonBounds().Width;

					if (this.UpDownAlign == LeftRightAlignment.Left)
					{
						focusRect.X = this.ClientRectangle.Right - focusRect.Width;
					}
				}
				
				ControlPaint.DrawFocusRectangle(e.Graphics, focusRect);
			}
		}


		#endregion

		#endregion
	}
}
