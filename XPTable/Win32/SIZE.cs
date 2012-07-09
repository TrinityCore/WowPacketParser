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
	/// The SIZE structure specifies the width and height of a rectangle
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct SIZE
	{
		/// <summary>
		/// Specifies the x-coordinate of the point
		/// </summary>
		public int cx;
			
		/// <summary>
		/// Specifies the y-coordinate of the point
		/// </summary>
		public int cy;


		/// <summary>
		/// Creates a new SIZE struct with the specified width and height
		/// </summary>
		/// <param name="cx">The width component of the new SIZE</param>
		/// <param name="cy">The height component of the new SIZE</param>
		public SIZE(int cx, int cy)
		{
			this.cx = cx;
			this.cy = cy;
		}


		/// <summary>
		/// Creates a new SIZE struct from the specified Size
		/// </summary>
		/// <param name="s">The Size to create the SIZE from</param>
		/// <returns>A SIZE struct with the same width and height values as 
		/// the specified Point</returns>
		public static SIZE FromSize(Size s)
		{
			return new SIZE(s.Width, s.Height);
		}


		/// <summary>
		/// Returns a Point with the same width and height values as the SIZE
		/// </summary>
		/// <returns>A Point with the same width and height values as the SIZE</returns>
		public Size ToSize()
		{
			return new Size(this.cx, this.cy);
		}
	}
}
