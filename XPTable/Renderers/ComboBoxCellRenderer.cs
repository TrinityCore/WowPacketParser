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
using XPTable.Themes;


namespace XPTable.Renderers
{
	/// <summary>
	/// A CellRenderer that draws Cell contents as a ComboBox
	/// </summary>
	public class ComboBoxCellRenderer : DropDownCellRenderer
	{
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the ComboBoxCellRenderer class with 
		/// default settings
		/// </summary>
		public ComboBoxCellRenderer() : base()
		{
			
		}

		#endregion


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

			Rectangle buttonRect = this.CalcDropDownButtonBounds();
            Rectangle textRect = this.ClientRectangle;

			if (this.ShowDropDownButton)
				textRect.Width -= buttonRect.Width - 1;

			// draw the text
            if (e.Cell.Text != null && e.Cell.Text.Length != 0)
            {
                if (e.Enabled)
                    DrawString(e.Graphics, e.Cell.Text, this.Font, this.ForeBrush, textRect, e.Cell.WordWrap);
                else
                    DrawString(e.Graphics, e.Cell.Text, this.Font, this.GrayTextBrush, textRect, e.Cell.WordWrap);

                if (e.Cell.WidthNotSet)
                {
                    SizeF size = e.Graphics.MeasureString(e.Cell.Text, this.Font);
                    e.Cell.ContentWidth = (int)Math.Ceiling(size.Width) + (this.ShowDropDownButton ? buttonRect.Width : 0);
                }
            }
            else
            {
                if (e.Cell.WidthNotSet)
                    e.Cell.ContentWidth = this.ShowDropDownButton ? buttonRect.Width : 0;
            }
			
			if( (e.Focused && e.Enabled)
				// only if we want to show selection rectangle
				&& ( e.Table.ShowSelectionRectangle ) )
			{
				Rectangle focusRect = this.ClientRectangle;

				if (this.ShowDropDownButton)
					focusRect.Width -= buttonRect.Width;
				
				ControlPaint.DrawFocusRectangle(e.Graphics, focusRect);
			}
		}
		#endregion

		#endregion
	}
}
