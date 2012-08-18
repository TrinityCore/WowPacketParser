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

using XPTable.Editors;
using XPTable.Events;
using XPTable.Models;


namespace XPTable.Renderers
{
	/// <summary>
	/// A CellRenderer that draws Cell contents as strings
	/// </summary>
	public class TextCellRenderer : CellRenderer
	{
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the TextCellRenderer class with 
		/// default settings
		/// </summary>
		public TextCellRenderer() : base()
		{
			
		}

		#endregion

        /// <summary>
        /// Returns the height that is required to render this cell. If zero is returned then the default row height is used.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        public override int GetCellHeight(Graphics graphics, Cell cell)
        {
            base.GetCellHeight(graphics, cell);

            if (cell != null)
            {
                this.Font = cell.Font;
                // Need to set this.Bounds before we access Client rectangle
                SizeF size = graphics.MeasureString(cell.Text, this.Font, this.ClientRectangle.Width, StringFormat);
                return (int)Math.Ceiling(size.Height);
            }
            else
                return 0;
        }

        /// <summary>
        /// Returns the width required to fully display this text.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        private int GetCellWidth(Graphics graphics, Cell cell)
        {
            SizeF size = graphics.MeasureString(cell.Text, this.Font);
            return (int)Math.Ceiling(size.Width);
        }

		#region Events

		#region Paint
        /// <summary>
		/// Raises the Paint event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		protected override void OnPaint(PaintCellEventArgs e)
		{
			base.OnPaint(e);
			
			// don't bother going any further if the Cell is null 
			if (e.Cell == null)
				return;

            Cell c = e.Cell;

            //////////////////
            if (c.WidthNotSet)
            {
                int w = GetCellWidth(e.Graphics, c);
                c.ContentWidth = w;
            }
            //////////////////

            string text = c.Text;

			if (text != null && text.Length != 0)
			{
				if (e.Enabled)
                    DrawString(e.Graphics, text, this.Font, this.ForeBrush, this.ClientRectangle, c.WordWrap);
				else
                    DrawString(e.Graphics, text, this.Font, this.GrayTextBrush, this.ClientRectangle, c.WordWrap);

                // Also, determine whether we need a tooltip, if the text was truncated.
                if (c.WordWrap)
                    c.InternalIsTextTrimmed = false;
                else if (e.Table.EnableToolTips)
                    c.InternalIsTextTrimmed = this.IsTextTrimmed(e.Graphics, c.Text);
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
