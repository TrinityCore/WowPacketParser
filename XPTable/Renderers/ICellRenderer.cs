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
using System.Windows.Forms;

using XPTable.Models;
using XPTable.Events;


namespace XPTable.Renderers
{
	/// <summary>
	/// Exposes common methods provided by Cell renderers
	/// </summary>
	public interface ICellRenderer : IRenderer
	{
		/// <summary>
		/// Raises the PaintCell event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		void OnPaintCell(PaintCellEventArgs e);


		/// <summary>
		/// Raises the GotFocus event
		/// </summary>
		/// <param name="e">A CellFocusEventArgs that contains the event data</param>
		void OnGotFocus(CellFocusEventArgs e);


		/// <summary>
		/// Raises the LostFocus event
		/// </summary>
		/// <param name="e">A CellFocusEventArgs that contains the event data</param>
		void OnLostFocus(CellFocusEventArgs e);


		/// <summary>
		/// Raises the KeyDown event
		/// </summary>
		/// <param name="e">A CellKeyEventArgs that contains the event data</param>
		void OnKeyDown(CellKeyEventArgs e);


		/// <summary>
		/// Raises the KeyUp event
		/// </summary>
		/// <param name="e">A CellKeyEventArgs that contains the event data</param>
		void OnKeyUp(CellKeyEventArgs e);


		/// <summary>
		/// Raises the MouseEnter event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		void OnMouseEnter(CellMouseEventArgs e);


		/// <summary>
		/// Raises the MouseLeave event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		void OnMouseLeave(CellMouseEventArgs e);


		/// <summary>
		/// Raises the MouseUp event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		void OnMouseUp(CellMouseEventArgs e);


		/// <summary>
		/// Raises the MouseDown event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		void OnMouseDown(CellMouseEventArgs e);


		/// <summary>
		/// Raises the MouseMove event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		void OnMouseMove(CellMouseEventArgs e);


		/// <summary>
		/// Raises the Click event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		void OnClick(CellMouseEventArgs e);


		/// <summary>
		/// Raises the DoubleClick event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		void OnDoubleClick(CellMouseEventArgs e);


        /// <summary>
        /// Returns the height that is required to render this cell. If zero is returned then the default row height is used.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="graphics">The GDI+ drawing surface provided by the Paint event.</param>
        /// <returns></returns>
        int GetCellHeight(System.Drawing.Graphics graphics, Cell cell);
	}
}
