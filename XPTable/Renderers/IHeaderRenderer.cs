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

using XPTable.Events;


namespace XPTable.Renderers
{
	/// <summary>
	/// Exposes common methods provided by Column header renderers
	/// </summary>
	public interface IHeaderRenderer : IRenderer
	{
		/// <summary>
		/// Raises the PaintHeader event
		/// </summary>
		/// <param name="e">A PaintHeaderEventArgs that contains the event data</param>
		void OnPaintHeader(PaintHeaderEventArgs e);
		
		
		/// <summary>
		/// Raises the MouseEnter event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		void OnMouseEnter(HeaderMouseEventArgs e);


		/// <summary>
		/// Raises the MouseLeave event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		void OnMouseLeave(HeaderMouseEventArgs e);


		/// <summary>
		/// Raises the MouseUp event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		void OnMouseUp(HeaderMouseEventArgs e);


		/// <summary>
		/// Raises the MouseDown event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		void OnMouseDown(HeaderMouseEventArgs e);


		/// <summary>
		/// Raises the MouseMove event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		void OnMouseMove(HeaderMouseEventArgs e);


		/// <summary>
		/// Raises the Click event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		void OnClick(HeaderMouseEventArgs e);


		/// <summary>
		/// Raises the DoubleClick event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		void OnDoubleClick(HeaderMouseEventArgs e);
	}
}
