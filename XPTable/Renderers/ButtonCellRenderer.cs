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
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

using XPTable.Events;
using XPTable.Models;
using XPTable.Themes;

namespace XPTable.Renderers
{
	/// <summary>
	/// A CellRenderer that draws Cell contents as Buttons
	/// </summary>
	public class ButtonCellRenderer : CellRenderer
	{
		#region Class Data
		/// <summary>
		/// Specifies the alignment of the Image displayed on the button
		/// </summary>
		private System.Drawing.ContentAlignment imageAlignment;
        /// <summary>
        /// Specifies whether the button is shown in flat style or not.
        /// </summary>
        bool flatStyle = false;
		#endregion
		
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the ButtonCellRenderer class with 
		/// default settings
		/// </summary>
		public ButtonCellRenderer() : base()
		{
			this.imageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets the ButtonCellRenderer specific data used by the Renderer from 
		/// the specified Cell
		/// </summary>
		/// <param name="cell">The Cell to get the ButtonCellRenderer data for</param>
		/// <returns>The ButtonCellRenderer data for the specified Cell</returns>
		protected ButtonRendererData GetButtonRendererData(Cell cell)
		{
			object rendererData = this.GetRendererData(cell);

			if (rendererData == null || !(rendererData is ButtonRendererData))
			{
				rendererData = new ButtonRendererData();

				this.SetRendererData(cell, rendererData);
			}

			return (ButtonRendererData) rendererData;
		}

		/// <summary>
		/// Returns a Rectangle that specifies the size and location of the button
		/// </summary>
		/// <returns>A Rectangle that specifies the size and location of the button</returns>
		protected virtual Rectangle CalcButtonBounds()
		{
			return this.ClientRectangle;
		}

		/// <summary>
		/// Returns a Rectangle that specifies the size and location of the buttons Image
		/// </summary>
		/// <param name="image">The buttons image</param>
		/// <param name="imageAlignment">The alignment of the image</param>
		/// <returns>A Rectangle that specifies the size and location of the buttons Image</returns>
		protected Rectangle CalcImageRect(Image image, System.Drawing.ContentAlignment imageAlignment)
		{
			Rectangle imageRect = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, image.Width, image.Height);

			switch (imageAlignment)
			{
				case System.Drawing.ContentAlignment.TopCenter:
				case System.Drawing.ContentAlignment.MiddleCenter:
				case System.Drawing.ContentAlignment.BottomCenter:
				{
					imageRect.X += (this.ClientRectangle.Width - image.Width) / 2;

					break;
				}

				case System.Drawing.ContentAlignment.TopRight:
				case System.Drawing.ContentAlignment.MiddleRight:
				case System.Drawing.ContentAlignment.BottomRight:
				{
					imageRect.X = this.ClientRectangle.Right - image.Width;

					break;
				}
			}

			switch (imageAlignment)
			{
				case System.Drawing.ContentAlignment.TopLeft:
				case System.Drawing.ContentAlignment.TopCenter:
				case System.Drawing.ContentAlignment.TopRight:
				{
					imageRect.Y += 2;

					break;
				}
				
				case System.Drawing.ContentAlignment.MiddleLeft:
				case System.Drawing.ContentAlignment.MiddleCenter:
				case System.Drawing.ContentAlignment.MiddleRight:
				{
					imageRect.Y += (this.ClientRectangle.Height - image.Height) / 2;

					break;
				}

				case System.Drawing.ContentAlignment.BottomLeft:
				case System.Drawing.ContentAlignment.BottomCenter:
				case System.Drawing.ContentAlignment.BottomRight:
				{
					imageRect.Y = this.ClientRectangle.Bottom - image.Height - 2;

					break;
				}
			}

			return imageRect;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the alignment of the Image displayed on the buttons
		/// </summary>
		public System.Drawing.ContentAlignment ImageAlignment
		{
			get { return this.imageAlignment; }
			set { this.imageAlignment = value; }
		}

        /// <summary>
        /// Gets or sets the flag that determines whether buttons are shown flat or normal.
        /// </summary>
        public bool FlatStyle
        {
            get { return this.flatStyle; }
            set { this.flatStyle = value; }
        }
		#endregion

		#region Events

		#region Focus

		/// <summary>
		/// Raises the GotFocus event
		/// </summary>
		/// <param name="e">A CellFocusEventArgs that contains the event data</param>
		public override void OnGotFocus(CellFocusEventArgs e)
		{
			base.OnGotFocus(e);

			// get the table to redraw the cell
			e.Table.Invalidate(e.CellRect);
		}


		/// <summary>
		/// Raises the LostFocus event
		/// </summary>
		/// <param name="e">A CellFocusEventArgs that contains the event data</param>
		public override void OnLostFocus(CellFocusEventArgs e)
		{
			base.OnLostFocus(e);

			// get the table to redraw the cell
			e.Table.Invalidate(e.CellRect);
		}

		#endregion

		#region Keys

		/// <summary>
		/// Raises the KeyDown event
		/// </summary>
		/// <param name="e">A CellKeyEventArgs that contains the event data</param>
		public override void OnKeyDown(CellKeyEventArgs e)
		{
			base.OnKeyDown(e);

			// get the button renderer data
			ButtonRendererData rendererData = this.GetButtonRendererData(e.Cell);

			// 
			if (e.KeyData == Keys.Enter || e.KeyData == Keys.Space)
			{
				rendererData.ButtonState = PushButtonState.Pressed;

				e.Table.Invalidate(e.CellRect);
			}
		}


		/// <summary>
		/// Raises the KeyUp event
		/// </summary>
		/// <param name="e">A CellKeyEventArgs that contains the event data</param>
		public override void OnKeyUp(CellKeyEventArgs e)
		{
			base.OnKeyUp(e);

			// get the button renderer data
			ButtonRendererData rendererData = this.GetButtonRendererData(e.Cell);

			// 
			if (e.KeyData == Keys.Enter || e.KeyData == Keys.Space)
			{
				rendererData.ButtonState = PushButtonState.Normal;

				e.Table.Invalidate(e.CellRect);
				e.Table.OnCellButtonClicked(new CellButtonEventArgs(e.Cell, e.Column, e.Row));
			}
		}

		#endregion

		#region Mouse

		#region MouseEnter

		/// <summary>
		/// Raises the MouseEnter event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		public override void OnMouseEnter(CellMouseEventArgs e)
		{
			base.OnMouseEnter(e);

			// get the button renderer data
			ButtonRendererData rendererData = this.GetButtonRendererData(e.Cell);

			// if the mouse is inside the button, make sure it is "hot"
			if (this.CalcButtonBounds().Contains(e.X, e.Y))
			{
				if (rendererData.ButtonState != PushButtonState.Hot)
				{
					rendererData.ButtonState = PushButtonState.Hot;

					e.Table.Invalidate(e.CellRect);
				}
			}
				// the mouse isn't inside the button, so it is in its normal state
			else
			{
				if (rendererData.ButtonState != PushButtonState.Normal)
				{
					rendererData.ButtonState = PushButtonState.Normal;

					e.Table.Invalidate(e.CellRect);
				}
			}
		}

		#endregion

		#region MouseLeave

		/// <summary>
		/// Raises the MouseLeave event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		public override void OnMouseLeave(CellMouseEventArgs e)
		{
			base.OnMouseLeave(e);

			// get the button renderer data
			ButtonRendererData rendererData = this.GetButtonRendererData(e.Cell);

			// make sure the button is in its normal state
			if (rendererData.ButtonState != PushButtonState.Normal)
			{
				rendererData.ButtonState = PushButtonState.Normal;

				e.Table.Invalidate(e.CellRect);
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

			// get the button renderer data
			ButtonRendererData rendererData = this.GetButtonRendererData(e.Cell);

			// check for the left mouse button
			if (e.Button == MouseButtons.Left)
			{
				Rectangle buttonRect = this.CalcButtonBounds();
				
				// if the mouse pointer is over the button, make sure 
				// the button is "hot"
				if (buttonRect.Contains(e.X, e.Y))
				{
					rendererData.ButtonState = PushButtonState.Hot;

					e.Table.Invalidate(e.CellRect);

					// check if the click started inside the button.  if 
					// it did, Raise the tables CellButtonClicked event
					if (buttonRect.Contains(rendererData.ClickPoint))
					{
						e.Table.OnCellButtonClicked(new CellButtonEventArgs(e.Cell, e.Column, e.Row));
					}
				}
				else
				{
					// the mouse was released somewhere outside of the button, 
					// so make set the button back to its normal state
					if (rendererData.ButtonState != PushButtonState.Normal)
					{
						rendererData.ButtonState = PushButtonState.Normal;

						e.Table.Invalidate(e.CellRect);
					}
				}
			}

			// reset the click point
			rendererData.ClickPoint = Point.Empty;
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

			// get the button renderer data
			ButtonRendererData rendererData = this.GetButtonRendererData(e.Cell);

			// check if the left mouse button is pressed
			if (e.Button == MouseButtons.Left)
			{
				// record where the click started
				rendererData.ClickPoint = new Point(e.X, e.Y);
				
				// if the click was inside the button, set the button state to pressed
				if (this.CalcButtonBounds().Contains(rendererData.ClickPoint))
				{
					rendererData.ButtonState = PushButtonState.Pressed;

					e.Table.Invalidate(e.CellRect);
				}
			}
		}

		#endregion

		#region MouseMove

		/// <summary>
		/// Raises the MouseMove event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		public override void OnMouseMove(CellMouseEventArgs e)
		{
			base.OnMouseMove(e);

			// get the button renderer data
			ButtonRendererData rendererData = this.GetButtonRendererData(e.Cell);

			Rectangle buttonRect = this.CalcButtonBounds();
			
			// check if the left mouse button is pressed
			if (e.Button == MouseButtons.Left)
			{
				// check if the mouse press originated in the button area
				if (buttonRect.Contains(rendererData.ClickPoint))
				{
					// check if the mouse is currently in the button
					if (buttonRect.Contains(e.X, e.Y))
					{
						// make sure the button is pressed
						if (rendererData.ButtonState != PushButtonState.Pressed)
						{
							rendererData.ButtonState = PushButtonState.Pressed;

							e.Table.Invalidate(e.CellRect);
						}
					}
					else
					{
						// the mouse isn't inside the button so make sure it is "hot"
						if (rendererData.ButtonState != PushButtonState.Hot)
						{
							rendererData.ButtonState = PushButtonState.Hot;

							e.Table.Invalidate(e.CellRect);
						}
					}
				}
			}
			else
			{
				// check if the mouse is currently in the button
				if (buttonRect.Contains(e.X, e.Y))
				{
					// the mouse is inside the button so make sure it is "hot"
					if (rendererData.ButtonState != PushButtonState.Hot)
					{
						rendererData.ButtonState = PushButtonState.Hot;

						e.Table.Invalidate(e.CellRect);
					}
				}
				else
				{
					// not inside the button so make sure it is in its normal state
					if (rendererData.ButtonState != PushButtonState.Normal)
					{
						rendererData.ButtonState = PushButtonState.Normal;

						e.Table.Invalidate(e.CellRect);
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
			if (e.Table.ColumnModel.Columns[e.Column] is ButtonColumn)
			{
                ButtonColumn buttonCol = ((ButtonColumn)e.Table.ColumnModel.Columns[e.Column]);
                this.ImageAlignment = buttonCol.ImageAlignment;
                this.FlatStyle = buttonCol.FlatStyle;
			}
			else
			{
				this.ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
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

			// get the button state
			ButtonRendererData rendererData = this.GetButtonRendererData(e.Cell);
			PushButtonState state = rendererData.ButtonState;

			// if the cell has focus and is in its normal state, 
			// make the button look like a default button
			if (state == PushButtonState.Normal && e.Focused)
			{
				state = PushButtonState.Default;
			}

			// if the table is not enabled, make sure the button is disabled
			if (!e.Enabled)
			{
				state = PushButtonState.Disabled;
			}

			// draw the button
			ThemeManager.DrawButton(e.Graphics, this.CalcButtonBounds(), state, flatStyle);
		}


		/// <summary>
		/// Raises the Paint event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		protected override void OnPaint(PaintCellEventArgs e)
		{
			base.OnPaint(e);

			// don't bother going any further if the Cell is null 
			if (e.Cell == null)
			{
				return;
			}

			Rectangle textRect = this.CalcButtonBounds();
			textRect.Inflate(-4, -2);

            int imageWidth = 0;
            int textWidth = 0;

			if (e.Cell.Image != null)
			{
				Rectangle imageRect = this.CalcImageRect(e.Cell.Image, this.ImageAlignment);

				if (this.GetButtonRendererData(e.Cell).ButtonState == PushButtonState.Pressed && !ThemeManager.VisualStylesEnabled)
				{
					imageRect.X += 1;
					imageRect.Y += 1;
				}
				
				this.DrawImage(e.Graphics, e.Cell.Image, imageRect, e.Enabled);
                imageWidth = imageRect.Width;
			}

			// draw the text
			if (e.Cell.Text != null && e.Cell.Text.Length != 0)
			{
				if (e.Enabled)
				{
					if (!ThemeManager.VisualStylesEnabled && this.GetButtonRendererData(e.Cell).ButtonState == PushButtonState.Pressed)
					{
						textRect.X += 1;
						textRect.Y += 1;
					}

					// if the cell or the row it is in is selected 
					// our forecolor will be the selection forecolor.
					// we'll ignore this and reset our forecolor to 
					// that of the cell being rendered
					if (e.Selected)
					{
						this.ForeColor = e.Cell.ForeColor;
					}
					
					e.Graphics.DrawString(e.Cell.Text, this.Font, this.ForeBrush, textRect, this.StringFormat);
				}
				else
				{
					e.Graphics.DrawString(e.Cell.Text, this.Font, this.GrayTextBrush, textRect, this.StringFormat);
				}

                if (e.Cell.WidthNotSet)
                {
                    SizeF size = e.Graphics.MeasureString(e.Cell.Text, this.Font);
                    textWidth = (int)Math.Ceiling(size.Width);
                }
            }

            if (e.Cell.WidthNotSet)
            {
                e.Cell.ContentWidth = imageWidth + textWidth;
            }

			// draw focus
			if( (e.Focused && e.Enabled)
				// only if we want to show selection rectangle
				&& ( e.Table.ShowSelectionRectangle ) )
			{
				Rectangle focusRect = this.CalcButtonBounds();
				
				if (ThemeManager.VisualStylesEnabled)
				{
					focusRect.Inflate(-3, -3);

					if (this.GetButtonRendererData(e.Cell).ButtonState != PushButtonState.Pressed)
					{
						ControlPaint.DrawFocusRectangle(e.Graphics, focusRect);
					}
				}
				else
				{
					focusRect.Inflate(-4, -4);

					ControlPaint.DrawFocusRectangle(e.Graphics, focusRect);
				}
			}
		}
		
		
		/// <summary>
		/// Draws the Image displayed on the button
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="image">The Image to draw</param>
		/// <param name="imageRect">A Rectangle that specifies the location 
		/// of the Image</param>
		/// <param name="enabled">Specifies whether the Image should be drawn 
		/// in an enabled state</param>
		protected void DrawImage(Graphics g, Image image, Rectangle imageRect, bool enabled)
		{
			if (enabled)
			{
				g.DrawImageUnscaled(image, imageRect);
			}
			else
			{
				ControlPaint.DrawImageDisabled(g, image, imageRect.X, imageRect.Y, this.BackColor);
			}
		}

		#endregion

		#endregion
	}
}
