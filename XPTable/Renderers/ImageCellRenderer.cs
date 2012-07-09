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

using XPTable.Events;
using XPTable.Models;


namespace XPTable.Renderers
{
	/// <summary>
	/// A CellRenderer that draws Cell contents as Images
	/// </summary>
	public class ImageCellRenderer : CellRenderer
	{
		#region Class Data

		/// <summary>
		/// Specifies whether any text contained in the Cell should be drawn
		/// </summary>
		private bool drawText;

		#endregion
		
		
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the ImageCellRenderer class with 
		/// default settings
		/// </summary>
		public ImageCellRenderer() : base()
		{
			this.drawText = true;
		}

		#endregion


		#region Methods

		/// <summary>
		/// Gets the Rectangle that specifies the Size and Location of 
		/// the Image contained in the current Cell
		/// </summary>
		/// <param name="image">The Image to be drawn</param>
		/// <param name="sizeMode">An ImageSizeMode that specifies how the 
		/// specified Image is scaled</param>
		/// <param name="rowAlignment">The alignment of the current Cell's row</param>
		/// <param name="columnAlignment">The alignment of the current Cell's Column</param>
		/// <returns>A Rectangle that specifies the Size and Location of 
		/// the Image contained in the current Cell</returns>
		protected Rectangle CalcImageRect(Image image, ImageSizeMode sizeMode, RowAlignment rowAlignment, ColumnAlignment columnAlignment)
		{
			if (this.DrawText)
			{
				sizeMode = ImageSizeMode.ScaledToFit;
			}

			Rectangle imageRect = this.ClientRectangle;

			if (sizeMode == ImageSizeMode.Normal)
			{
				if (image.Width < imageRect.Width)
				{
					imageRect.Width = image.Width;
				}

				if (image.Height < imageRect.Height)
				{
					imageRect.Height = image.Height;
				}
			}
			else if (sizeMode == ImageSizeMode.ScaledToFit)
			{
				if (image.Width >= imageRect.Width || image.Height >= imageRect.Height)
				{
					double hScale = ((double) imageRect.Width) / ((double) image.Width);
					double vScale = ((double) imageRect.Height) / ((double) image.Height);

					double scale = Math.Min(hScale, vScale);

					imageRect.Width = (int) (((double) image.Width) * scale);
					imageRect.Height = (int) (((double) image.Height) * scale);
				}
				else
				{
					imageRect.Width = image.Width;
					imageRect.Height = image.Height;
				}
			}
            else if (sizeMode == ImageSizeMode.NoClip)
            {
                imageRect.Width = image.Width;
                imageRect.Height = image.Height;
            }

			if (rowAlignment == RowAlignment.Center)
			{
				imageRect.Y += (this.ClientRectangle.Height - imageRect.Height) / 2;
			}
			else if (rowAlignment == RowAlignment.Bottom)
			{
				imageRect.Y = this.ClientRectangle.Bottom - imageRect.Height;
			}

			if (!this.DrawText)
			{
				if (columnAlignment == ColumnAlignment.Center)
				{
					imageRect.X += (this.ClientRectangle.Width - imageRect.Width) / 2;
				}
				else if (columnAlignment == ColumnAlignment.Right)
				{
					imageRect.X = this.ClientRectangle.Width - imageRect.Width;
				}
			}

			return imageRect;
		}
		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets whether any text contained in the Cell should be drawn
		/// </summary>
		public bool DrawText
		{
			get
			{
				return this.drawText;
			}
		}

		#endregion

		#region Events

		#region Paint
		/// <summary>
		/// Raises the PaintCell event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		public override void OnPaintCell(PaintCellEventArgs e)
		{
			if (e.Table.ColumnModel.Columns[e.Column] is ImageColumn)
			{
				this.drawText = ((ImageColumn) e.Table.ColumnModel.Columns[e.Column]).DrawText;
			}
			else
			{
				this.drawText = true;
			}
			
			base.OnPaintCell(e);
		}

		/// <summary>
		/// Raises the Paint event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		protected override void OnPaint(PaintCellEventArgs e)
		{
			base.OnPaint(e);
			
			// don't bother if the Cell is null or doesn't have an image
			if (e.Cell == null || e.Cell.Image == null)
			{
				return;
			}

			// work out the size and location of the image
			Rectangle imageRect = this.CalcImageRect(e.Cell.Image, e.Cell.ImageSizeMode, this.LineAlignment, this.Alignment);
			
			// draw the image
			bool scaled = (this.DrawText || e.Cell.ImageSizeMode != ImageSizeMode.Normal);
			this.DrawImage(e.Graphics, e.Cell.Image, imageRect, scaled, e.Table.Enabled);

            int textWidth = 0;

			// check if we need to draw any text
			if (this.DrawText)
			{
				if (e.Cell.Text != null && e.Cell.Text.Length != 0)
				{
                    if (e.Cell.WidthNotSet)
                    {
                        SizeF size = e.Graphics.MeasureString(e.Cell.Text, this.Font);
                        textWidth = (int)Math.Ceiling(size.Width);
                    }

					// rectangle the text will be drawn in
					Rectangle textRect = this.ClientRectangle;
				
					// take the imageRect into account so we don't 
					// draw over it
					textRect.X += imageRect.Width;
					textRect.Width -= imageRect.Width;

					// check that we will be able to see the text
					if (textRect.Width > 0)
					{
						// draw the text
						if (e.Enabled)
						{
							e.Graphics.DrawString(e.Cell.Text, this.Font, this.ForeBrush, textRect, this.StringFormat);
						}
						else
						{
							e.Graphics.DrawString(e.Cell.Text, this.Font, this.GrayTextBrush, textRect, this.StringFormat);
						}
					}
				}
			}

            if (e.Cell.WidthNotSet)
            {
                SizeF size = e.Graphics.MeasureString(e.Cell.Text, this.Font);
                e.Cell.ContentWidth = textWidth + e.Cell.Image.Width;
            }

			if( (e.Focused && e.Enabled)
				// only if we want to show selection rectangle
				&& ( e.Table.ShowSelectionRectangle ) )
			{
				ControlPaint.DrawFocusRectangle(e.Graphics, this.ClientRectangle);
			}
		}

		/// <summary>
		/// Draws the Image contained in the Cell
		/// </summary>
		/// <param name="g">The Graphics used to paint the Image</param>
		/// <param name="image">The Image to be drawn</param>
		/// <param name="imageRect">A rectangle that specifies the Size and 
		/// Location of the Image</param>
		/// <param name="scaled">Specifies whether the image is to be scaled</param>
		/// <param name="enabled">Specifies whether the Image should be drawn 
		/// in an enabled state</param>
		protected void DrawImage(Graphics g, Image image, Rectangle imageRect, bool scaled, bool enabled)
		{
			if (scaled)
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
			else
			{
				if (enabled)
				{
					g.DrawImageUnscaled(image, imageRect);
				}
				else
				{
					ControlPaint.DrawImageDisabled(g, image, imageRect.X, imageRect.Y, this.BackBrush.Color);
				}
			}
		}

		#endregion

		#endregion
	}
}
