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
using System.Runtime.InteropServices;


namespace XPTable.Win32
{
	/// <summary>
	/// The RECT structure defines the coordinates of the upper-left 
	/// and lower-right corners of a rectangle
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct RECT
	{
		/// <summary>
		/// Specifies the x-coordinate of the upper-left corner of the RECT
		/// </summary>
		public int left;
			
		/// <summary>
		/// Specifies the y-coordinate of the upper-left corner of the RECT
		/// </summary>
		public int top;
			
		/// <summary>
		/// Specifies the x-coordinate of the lower-right corner of the RECT
		/// </summary>
		public int right;
			
		/// <summary>
		/// Specifies the y-coordinate of the lower-right corner of the RECT
		/// </summary>
		public int bottom;


		/// <summary>
		/// Creates a new RECT struct with the specified location and size
		/// </summary>
		/// <param name="left">The x-coordinate of the upper-left corner of the RECT</param>
		/// <param name="top">The y-coordinate of the upper-left corner of the RECT</param>
		/// <param name="right">The x-coordinate of the lower-right corner of the RECT</param>
		/// <param name="bottom">The y-coordinate of the lower-right corner of the RECT</param>
		public RECT(int left, int top, int right, int bottom)
		{
			this.left = left;
			this.top = top;
			this.right = right;
			this.bottom = bottom;
		}


		/// <summary>
		/// Creates a new RECT struct from the specified Rectangle
		/// </summary>
		/// <param name="rect">The Rectangle to create the RECT from</param>
		/// <returns>A RECT struct with the same location and size as 
		/// the specified Rectangle</returns>
		public static RECT FromRectangle(Rectangle rect)
		{
			return new RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
		}


		/// <summary>
		/// Creates a new RECT struct with the specified location and size
		/// </summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the RECT</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the RECT</param>
		/// <param name="width">The width of the RECT</param>
		/// <param name="height">The height of the RECT</param>
		/// <returns>A RECT struct with the specified location and size</returns>
		public static RECT FromXYWH(int x, int y, int width, int height)
		{
			return new RECT(x, y, x + width, y + height);
		}


		/// <summary>
		/// Returns a Rectangle with the same location and size as the RECT
		/// </summary>
		/// <returns>A Rectangle with the same location and size as the RECT</returns>
		public Rectangle ToRectangle()
		{
			return new Rectangle(this.left, this.top, this.right - this.left, this.bottom - this.top);
		}
	}
}
