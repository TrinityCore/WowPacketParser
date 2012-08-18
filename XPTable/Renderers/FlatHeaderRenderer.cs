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
using XPTable.Models;
using XPTable.Themes;


namespace XPTable.Renderers
{
	/// <summary>
	/// A HeaderRenderer that draws flat Column headers
	/// </summary>
	public class FlatHeaderRenderer : HeaderRenderer
	{
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the XPHeaderRenderer class 
		/// with default settings
		/// </summary>
		public FlatHeaderRenderer() : base()
		{
			this.SetBackBrushColor(SystemColors.Control);
		}

		#endregion


		#region Events

		#region Paint

		/// <summary>
		/// Raises the PaintBackground event
		/// </summary>
		/// <param name="e">A PaintHeaderEventArgs that contains the event data</param>
		protected override void OnPaintBackground(PaintHeaderEventArgs e)
		{
			base.OnPaintBackground(e);

			e.Graphics.FillRectangle(this.BackBrush, this.Bounds);
		}


		/// <summary>
		/// Raises the Paint event
		/// </summary>
		/// <param name="e">A PaintHeaderEventArgs that contains the event data</param>
		protected override void OnPaint(PaintHeaderEventArgs e)
		{
			base.OnPaint(e);

			if (e.Column == null)
			{
				return;
			}

			Rectangle textRect = this.ClientRectangle;
			Rectangle imageRect = Rectangle.Empty;

            int imageWidth = 0;
            int arrowWidth = 0;
            int textWidth = 0;

			if (e.Column.Image != null)
			{
				imageRect = this.CalcImageRect();

				textRect.Width -= imageRect.Width;
				textRect.X += imageRect.Width;

				if (e.Column.ImageOnRight)
				{
					imageRect.X = this.ClientRectangle.Right - imageRect.Width;
					textRect.X = this.ClientRectangle.X;
				}

				this.DrawColumnHeaderImage(e.Graphics, e.Column.Image, imageRect, e.Column.Enabled);
                imageWidth = imageRect.Width;
			}

			if (e.Column.SortOrder != SortOrder.None)
			{
				Rectangle arrowRect = this.CalcSortArrowRect();
				
                if (this.Alignment == ColumnAlignment.Right)
                {
                    arrowRect.X = textRect.Left;
                    textRect.Width -= arrowRect.Width;
                    textRect.X += arrowRect.Width;
                }
                else
                {
				arrowRect.X = textRect.Right - arrowRect.Width;
				textRect.Width -= arrowRect.Width;
                }

				this.DrawSortArrow(e.Graphics, arrowRect, e.Column.SortOrder, e.Column.Enabled);
                arrowWidth = arrowRect.Width;
			}

            if (e.Column.Text != null && e.Column.Text.Length > 0 && textRect.Width > 0)
            {
                if (e.Column.Enabled)
                {
                    e.Graphics.DrawString(e.Column.Text, this.Font, this.ForeBrush, textRect, this.StringFormat);
                }
                else
                {
                    using (SolidBrush brush = new SolidBrush(SystemPens.GrayText.Color))
                    {
                        e.Graphics.DrawString(e.Column.Text, this.Font, brush, textRect, this.StringFormat);
                    }
                }

                if (e.Column.WidthNotSet)
                {
                    SizeF size = e.Graphics.MeasureString(e.Column.Text, this.Font);
                    textWidth = (int)Math.Ceiling(size.Width);
                }

                // Also, determine whether we need a tooltip, if the text was truncated.
                if (e.Table.EnableToolTips)
                {
                    e.Column.IsTextTrimmed = this.IsTextTrimmed(e.Graphics, e.Column.Text);
                }
            }
            if (e.Column.WidthNotSet)
            {
                e.Column.ContentWidth = imageWidth + arrowWidth + textWidth;
            }
		}

		#endregion

		#endregion
	}
}
