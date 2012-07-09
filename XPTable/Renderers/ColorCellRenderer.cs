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

using XPTable.Events;
using XPTable.Models;


namespace XPTable.Renderers
{
	/// <summary>
	/// A CellRenderer that draws Cell contents as Buttons
	/// </summary>
	public class ColorCellRenderer : DropDownCellRenderer
	{
		#region Class Data
		
		/// <summary>
		/// Specifies whether the Cells Color should be drawn
		/// </summary>
		private bool showColor;

		/// <summary>
		/// Specifies whether the Cells Color name should be drawn
		/// </summary>
		private bool showColorName;

		#endregion


		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the ColorCellRenderer class with 
		/// default settings
		/// </summary>
		public ColorCellRenderer() : base()
		{
			this.showColor = true;
			this.showColorName = true;
		}

		#endregion


		#region Methods

		/// <summary>
		/// Returns a Rectangle that specifies the size and location of the Color 
		/// rectangle
		/// </summary>
		/// <param name="rowAlignment">The alignment of the Cells Row</param>
		/// <param name="columnAlignment">The alignment of the Cells Column</param>
		/// <returns>A Rectangle that specifies the size and location of the Color 
		/// rectangle</returns>
		protected Rectangle CalcColorRect(RowAlignment rowAlignment, ColumnAlignment columnAlignment)
		{
			Rectangle rect = this.ClientRectangle;

			rect.X += 2;
			rect.Y += 2;
			rect.Height -= 6;
			rect.Width = 16;

			return rect;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets whether the Cells Color should be drawn
		/// </summary>
		public bool ShowColor
		{
			get
			{
				return this.showColor;
			}

			set
			{
				this.showColor = value;
			}
		}


		/// <summary>
		/// Gets or sets whether the Cells Color name should be drawn
		/// </summary>
		public bool ShowColorName
		{
			get
			{
				return this.showColorName;
			}

			set
			{
				this.showColorName = value;
			}
		}

		#endregion


		#region Events

		/// <summary>
		/// Raises the PaintCell event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		public override void OnPaintCell(PaintCellEventArgs e)
		{
			if (e.Table.ColumnModel.Columns[e.Column] is ColorColumn)
			{
				ColorColumn column = (ColorColumn) e.Table.ColumnModel.Columns[e.Column];

				this.ShowColor = column.ShowColor;
				this.ShowColorName = column.ShowColorName;
			}
			else
			{
				this.ShowColor = false;
				this.ShowColorName = true;
			}
			
			base.OnPaintCell(e);
		}


		/// <summary>
		/// Raises the Paint event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		protected override void OnPaint(PaintCellEventArgs e)
		{
			base.OnPaint (e);

			// don't bother if the Cell is null
			if (e.Cell == null)
			{
				return;
			}

			// get the Cells value
			Color color = Color.Empty;

			if (e.Cell.Data != null && e.Cell.Data is Color)
			{
				color = (Color) e.Cell.Data;
			}

			Rectangle buttonRect = this.CalcDropDownButtonBounds();

			Rectangle textRect = this.ClientRectangle;

			if (this.ShowDropDownButton)
			{
				textRect.Width -= buttonRect.Width - 1;
			}

			e.Graphics.SetClip(textRect);

            int colourWidth = 0;
			if (this.ShowColor)
			{
				Rectangle colorRect = this.CalcColorRect(e.Table.TableModel.Rows[e.Row].Alignment, e.Table.ColumnModel.Columns[e.Column].Alignment);

				if (color != Color.Empty)
				{
					using (SolidBrush brush = new SolidBrush(color))
					{
						if (e.Enabled)
						{
							e.Graphics.FillRectangle(brush, colorRect);
							e.Graphics.DrawRectangle(SystemPens.ControlText, colorRect);
						}
						else
						{
							using (Bitmap b = new Bitmap(colorRect.Width, colorRect.Height))
							{
								using (Graphics g = Graphics.FromImage(b))
								{
									g.FillRectangle(brush, 0, 0, colorRect.Width, colorRect.Height);
									g.DrawRectangle(SystemPens.ControlText, 0, 0, colorRect.Width-1, colorRect.Height-1);
								}

								ControlPaint.DrawImageDisabled(e.Graphics, b, colorRect.X, colorRect.Y, this.BackColor);
							}
						}
                        colourWidth = colorRect.Width;
					}

					textRect.X = colorRect.Right + 2;
					textRect.Width -= colorRect.Width + 4;
				}
			}

            int textWidth = 0;
			if (this.ShowColorName)
			{
				string text = "";

				if (color.IsEmpty)
				{
					text = "Empty";
				}
				else if (color.IsNamedColor || color.IsSystemColor)
				{
					text = color.Name;
				}
				else
				{
					if (color.A != 255)
					{
						text += color.A + ", ";
					}

					text += color.R +", " + color.G + ", " + color.B;
				}

				if (e.Enabled)
				{
					e.Graphics.DrawString(text, this.Font, this.ForeBrush, textRect, this.StringFormat);
				}
				else
				{
					e.Graphics.DrawString(text, this.Font, this.GrayTextBrush, textRect, this.StringFormat);
				}

                if (e.Cell.WidthNotSet)
                {
                    SizeF size = e.Graphics.MeasureString(text, this.Font);
                    textWidth = (int)Math.Ceiling(size.Width);
                }
			}

            if (e.Cell.WidthNotSet)
            {
                e.Cell.ContentWidth = colourWidth + textWidth + (this.ShowDropDownButton ? buttonRect.Width : 0);
            }

			if( (e.Focused && e.Enabled)
				// only if we want to show selection rectangle
				&& ( e.Table.ShowSelectionRectangle ) )
			{
				Rectangle focusRect = this.ClientRectangle;

				if (this.ShowDropDownButton)
				{
					focusRect.Width -= buttonRect.Width;
				}
				
				ControlPaint.DrawFocusRectangle(e.Graphics, focusRect);
			}
		}

		#endregion
	}
}
