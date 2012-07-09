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
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using XPTable.Events;
using XPTable.Models;
using XPTable.Themes;


namespace XPTable.Renderers
{
	/// <summary>
	/// A CellRenderer that draws Cell contents as a ProgressBar
	/// </summary>
	public class ProgressBarCellRenderer : CellRenderer
	{
		#region Class Data

		/// <summary>
		/// Specifies whether the ProgressBar's value as a string 
		/// should be displayed
		/// </summary>
		private bool drawPercentageText;

		#endregion
		
		
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the ProgressBarCellRenderer class with 
		/// default settings
		/// </summary>
		public ProgressBarCellRenderer() : base()
		{
			this.drawPercentageText = true;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets the rectangle that represents the client area of the Renderer
		/// </summary>
		public new Rectangle ClientRectangle
		{
			get
			{
				Rectangle client = base.ClientRectangle;

				client.Inflate(-1, -1);

				return client;
			}
		}

		/// <summary>
		/// Gets or sets whether the ProgressBar's value as a string 
		/// should be displayed
		/// </summary>
		public bool DrawPercentageText
		{
			get
			{
				return this.drawPercentageText;
			}

			set
			{
				this.drawPercentageText = value;
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
			if (e.Table.ColumnModel.Columns[e.Column] is ProgressBarColumn)
			{
				this.drawPercentageText = ((ProgressBarColumn) e.Table.ColumnModel.Columns[e.Column]).DrawPercentageText;
			}
			else
			{
				this.drawPercentageText = false;
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

			// don't bother if the Cell is null
			if (e.Cell == null)
			{
				return;
			}

			// fill the client area with the window color (this 
			// will be the background color of the progress bar)
			e.Graphics.FillRectangle(SystemBrushes.Window, this.ClientRectangle);

			Rectangle progressRect = this.ClientRectangle;

			// draw the border
			if (e.Enabled)
			{
				// if xp themes are enabled, shrink the size of the 
				// progress bar as otherwise the focus rect appears 
				// to go awol if the cell has focus
				if (ThemeManager.VisualStylesEnabled)
				{
					progressRect.Inflate(-1, -1);
				}

				ThemeManager.DrawProgressBar(e.Graphics, progressRect);
			}
			else
			{
				using (Bitmap b = new Bitmap(progressRect.Width, progressRect.Height))
				{
					using (Graphics g = Graphics.FromImage(b))
					{
						ThemeManager.DrawProgressBar(g, new Rectangle(0, 0, progressRect.Width, progressRect.Height));
					}

					ControlPaint.DrawImageDisabled(e.Graphics, b, progressRect.X, progressRect.Y, this.BackBrush.Color);
				}
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
			int intVal = 0;

			if (e.Cell.Data != null && e.Cell.Data is int)
			{
				intVal = (int) e.Cell.Data;
			}

			if (intVal < 0)
			{
				intVal = 0;
			}
			else if (intVal > 100)
			{
				intVal = 100;
			}

			// adjust the chunk rect so we don't draw over the
			// progress bars borders
			Rectangle chunkRect = this.ClientRectangle;
			chunkRect.Inflate(-2, -2);

			// if xp themes are enabled, shrink the size of the 
			// progress bar as otherwise the focus rect appears 
			// to go awol if the cell has focus
			if (ThemeManager.VisualStylesEnabled)
			{
				chunkRect.Inflate(-1, -1);
			}

			chunkRect.Width = (int) ((((double) intVal) / 100d) * ((double) chunkRect.Width));

			if (e.Enabled)
			{
				ThemeManager.DrawProgressBarChunks(e.Graphics, chunkRect);
			}
			else
			{
				using (Bitmap b = new Bitmap(chunkRect.Width, chunkRect.Height))
				{
					using (Graphics g = Graphics.FromImage(b))
					{
						ThemeManager.DrawProgressBarChunks(g, new Rectangle(0, 0, chunkRect.Width, chunkRect.Height));
					}

					ControlPaint.DrawImageDisabled(e.Graphics, b, chunkRect.X, chunkRect.Y, this.BackBrush.Color);
				}
			}

			if (this.DrawPercentageText)
			{
				this.Alignment = ColumnAlignment.Center;
				this.LineAlignment = RowAlignment.Center;

				Font font = new Font(this.Font.FontFamily, this.Font.SizeInPoints, FontStyle.Bold);

				if (e.Enabled)
				{
					e.Graphics.DrawString("" + intVal + "%", font, SystemBrushes.ControlText, this.ClientRectangle, this.StringFormat);
				}
				else
				{
					e.Graphics.DrawString("" + intVal + "%", font, Brushes.White, this.ClientRectangle, this.StringFormat);
				}
				
				if (!ThemeManager.VisualStylesEnabled)
				{
					// remember the old clip area
					Region oldClip = e.Graphics.Clip;
					
					Rectangle clipRect = this.ClientRectangle;
					clipRect.Width = chunkRect.Width + 2;
					e.Graphics.SetClip(clipRect);

					if (e.Table.Enabled)
					{
						e.Graphics.DrawString("" + intVal + "%", font, SystemBrushes.HighlightText, this.ClientRectangle, this.StringFormat);
					}
					else
					{
						e.Graphics.DrawString("" + intVal + "%", font, Brushes.White, this.ClientRectangle, this.StringFormat);
					}

					// restore the old clip area
					e.Graphics.SetClip(oldClip, CombineMode.Replace);
				}
			}
			
			if( (e.Focused && e.Enabled)
				// only if we want to show selection rectangle
				&& ( e.Table.ShowSelectionRectangle ) )
			{
				ControlPaint.DrawFocusRectangle(e.Graphics, this.ClientRectangle);
			}
		}

		#endregion

		#endregion
	}
}
