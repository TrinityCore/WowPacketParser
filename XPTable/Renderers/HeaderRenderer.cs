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


namespace XPTable.Renderers
{
	/// <summary>
	/// Base class for Renderers that draw Column headers
	/// </summary>
	public abstract class HeaderRenderer : Renderer, IHeaderRenderer
	{
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the HeaderRenderer class with default settings
		/// </summary>
		protected HeaderRenderer() : base()
		{
			this.StringFormat.Alignment = StringAlignment.Near;
		}

		#endregion


		#region Methods

		/// <summary>
		/// Returns a Rectangle that represents the size and location of the Image 
		/// displayed on the ColumnHeader
		/// </summary>
		/// <returns>A Rectangle that represents the size and location of the Image 
		/// displayed on the ColumnHeader</returns>
		protected Rectangle CalcImageRect()
		{
			Rectangle imageRect = this.ClientRectangle;

			if (imageRect.Width > 16)
			{
				imageRect.Width = 16;
			}

			if (imageRect.Height > 16)
			{
				imageRect.Height = 16;

				imageRect.Y += (this.ClientRectangle.Height - imageRect.Height) / 2;
			}

			return imageRect;
		}


		/// <summary>
		/// Returns a Rectangle that represents the size and location of the sort arrow
		/// </summary>
		/// <returns>A Rectangle that represents the size and location of the sort arrow</returns>
		protected Rectangle CalcSortArrowRect()
		{
			Rectangle arrowRect = this.ClientRectangle;

			arrowRect.Width = 12;
			arrowRect.X = this.ClientRectangle.Right - arrowRect.Width;

			return arrowRect;
		}

		#endregion
		

		#region Properties
		
		/// <summary>
		/// Overrides Renderer.ClientRectangle
		/// </summary>
		[Browsable(false)]
		public override Rectangle ClientRectangle
		{
			get
			{
				Rectangle client = new Rectangle(this.Bounds.Location, this.Bounds.Size);
				
				//
				client.Inflate(-2, -2);

				return client;
			}
		}

		#endregion


		#region Events

		#region Mouse

		#region MouseEnter

		/// <summary>
		/// Raises the MouseEnter event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		public virtual void OnMouseEnter(HeaderMouseEventArgs e)
		{
			this.Bounds = e.HeaderRect;

			bool tooltipActive = e.Table.ToolTip.Active;

			if (tooltipActive)
				e.Table.ToolTip.Active = false;

			e.Table.ResetMouseEventArgs();

			if (tooltipActive)
			{
                if (e.Column != null)
                {
                    HeaderToolTipEventArgs args = new HeaderToolTipEventArgs(e.Column, new Point(e.X, e.Y));

                    // The default tooltip is to show the full text for any cell that has been truncated
                    if (e.Column.IsTextTrimmed)
                        args.ToolTipText = e.Column.Text;

                    // Allow the outside world to modify the text or cancel this tooltip
                    e.Table.OnHeaderToolTipPopup(args);

                    if (args.Cancel)
                        e.Table.ToolTip.SetToolTip(e.Table, string.Empty);
                    else
                        e.Table.ToolTip.SetToolTip(e.Table, args.ToolTipText);
                }
                else
                {
                    e.Table.ToolTip.SetToolTip(e.Table, string.Empty);
                }
				e.Table.ToolTip.Active = true;
			}
		}
		#endregion

		#region MouseLeave

		/// <summary>
		/// Raises the MouseLeave event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		public virtual void OnMouseLeave(HeaderMouseEventArgs e)
		{
			this.Bounds = e.HeaderRect;
		}

		#endregion

		#region MouseUp

		/// <summary>
		/// Raises the MouseUp event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		public virtual void OnMouseUp(HeaderMouseEventArgs e)
		{
			this.Bounds = e.HeaderRect;
		}

		#endregion

		#region MouseDown

		/// <summary>
		/// Raises the MouseDown event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		public virtual void OnMouseDown(HeaderMouseEventArgs e)
		{
			if (!e.Table.Focused)
			{
				e.Table.Focus();
			}
			
			this.Bounds = e.HeaderRect;
		}

		#endregion

		#region MouseMove

		/// <summary>
		/// Raises the MouseMove event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		public virtual void OnMouseMove(HeaderMouseEventArgs e)
		{
			this.Bounds = e.HeaderRect;
		}

		#endregion

		#region Click

		/// <summary>
		/// Raises the Click event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		public virtual void OnClick(HeaderMouseEventArgs e)
		{
			this.Bounds = e.HeaderRect;
		}


		/// <summary>
		/// Raises the DoubleClick event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		public virtual void OnDoubleClick(HeaderMouseEventArgs e)
		{
			this.Bounds = e.HeaderRect;
		}

		#endregion

		#endregion
		
		#region Paint

		/// <summary>
		/// Raises the PaintHeader event
		/// </summary>
		/// <param name="e">A PaintHeaderEventArgs that contains the event data</param>
		public virtual void OnPaintHeader(PaintHeaderEventArgs e)
		{
            // Apply the column alignment to the header
            if ((e.Column != null) && (e.Table != null) && (e.Table.HeaderAlignWithColumn))
                this.Alignment = e.Column.Alignment;

			// paint the Column header's background
			this.OnPaintBackground(e);

			// paint the Column headers foreground
			this.OnPaint(e);
		}


		/// <summary>
		/// Raises the PaintBackground event
		/// </summary>
		/// <param name="e">A PaintHeaderEventArgs that contains the event data</param>
		protected virtual void OnPaintBackground(PaintHeaderEventArgs e)
		{
			
		}


		/// <summary>
		/// Raises the Paint event
		/// </summary>
		/// <param name="e">A PaintHeaderEventArgs that contains the event data</param>
		protected virtual void OnPaint(PaintHeaderEventArgs e)
		{
			
		}
		
		
		/// <summary>
		/// Draws the Image contained in the ColumnHeader
		/// </summary>
		/// <param name="g">The Graphics used to paint the Image</param>
		/// <param name="image">The Image to be drawn</param>
		/// <param name="imageRect">A rectangle that specifies the Size and 
		/// Location of the Image</param>
		/// <param name="enabled">Specifies whether the Image should be drawn 
		/// in an enabled state</param>
		protected void DrawColumnHeaderImage(Graphics g, Image image, Rectangle imageRect, bool enabled)
		{
			if (enabled)
			{
				g.DrawImage(image, imageRect);
			}
			else
			{
				using (Image im = new Bitmap(image, imageRect.Width, imageRect.Height))
				{
					ControlPaint.DrawImageDisabled(g, im, imageRect.X, imageRect.Y, this.BackBrush.Color);
				}
			}
		}


		/// <summary>
		/// Draws the ColumnHeader's sort arrow
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="drawRect">A Rectangle that specifies the location 
		/// of the sort arrow</param>
		/// <param name="direction">The direction of the sort arrow</param>
		/// <param name="enabled">Specifies whether the sort arrow should be 
		/// drawn in an enabled state</param>
		protected virtual void DrawSortArrow(Graphics g, Rectangle drawRect, SortOrder direction, bool enabled)
		{
			if (direction != SortOrder.None)
			{
				using (Font font = new Font("Marlett", 9f))
				{
					using (StringFormat format = new StringFormat())
					{
                        if (this.Alignment == XPTable.Models.ColumnAlignment.Right)
                            format.Alignment = StringAlignment.Near;
                        else
						format.Alignment = StringAlignment.Far;
						format.LineAlignment = StringAlignment.Center;

						if (direction == SortOrder.Ascending)
						{
							if (enabled)
							{
								g.DrawString("t", font, SystemBrushes.ControlDarkDark, drawRect, format);
							}
							else
							{
								using (SolidBrush brush = new SolidBrush(SystemPens.GrayText.Color))
								{
									g.DrawString("t", font, brush, drawRect, format);
								}
							}
						}
						else
						{
							if (enabled)
							{
								g.DrawString("u", font, SystemBrushes.ControlDarkDark, drawRect, format);
							}
							else
							{
								using (SolidBrush brush = new SolidBrush(SystemPens.GrayText.Color))
								{
									g.DrawString("u", font, brush, drawRect, format);
								}
							}
						}
					}
				}
			}
		}

		#endregion

		#endregion
	}
}
