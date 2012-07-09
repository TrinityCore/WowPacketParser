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
	/// The POINT structure defines the x- and y- coordinates of a point
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct POINT
	{
		/// <summary>
		/// Specifies the x-coordinate of the point
		/// </summary>
		public int x;
			
		/// <summary>
		/// Specifies the y-coordinate of the point
		/// </summary>
		public int y;


		/// <summary>
		/// Creates a new RECT struct with the specified x and y coordinates
		/// </summary>
		/// <param name="x">The x-coordinate of the point</param>
		/// <param name="y">The y-coordinate of the point</param>
		public POINT(int x, int y)
		{
			this.x = x;
			this.y = y;
		}


		/// <summary>
		/// Creates a new POINT struct from the specified Point
		/// </summary>
		/// <param name="p">The Point to create the POINT from</param>
		/// <returns>A POINT struct with the same x and y coordinates as 
		/// the specified Point</returns>
		public static POINT FromPoint(Point p)
		{
			return new POINT(p.X, p.Y);
		}


		/// <summary>
		/// Returns a Point with the same x and y coordinates as the POINT
		/// </summary>
		/// <returns>A Point with the same x and y coordinates as the POINT</returns>
		public Point ToPoint()
		{
			return new Point(this.x, this.y);
		}
	}
}
