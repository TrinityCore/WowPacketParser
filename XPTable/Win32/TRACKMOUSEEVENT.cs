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
using System.Runtime.InteropServices;
using System.Security.Permissions;


namespace XPTable.Win32
{
	/// <summary>
	/// The TRACKMOUSEEVENT structure is used by the TrackMouseEvent function 
	/// to track when the mouse pointer leaves a window or hovers over a window 
	/// for a specified amount of time
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal class TRACKMOUSEEVENT
	{
		/// <summary>
		/// Specifies the size of the TRACKMOUSEEVENT structure
		/// </summary>
		public int cbSize;

		/// <summary>
		/// Specifies the services requested
		/// </summary>
		public int dwFlags;

		/// <summary>
		/// Specifies a handle to the window to track
		/// </summary>
		public IntPtr hwndTrack;

		/// <summary>
		/// Specifies the hover time-out in milliseconds
		/// </summary>
		public int dwHoverTime;


		/// <summary>
		/// Creates a new TRACKMOUSEEVENT struct with default settings
		/// </summary>
		public TRACKMOUSEEVENT()
		{
			// Marshal.SizeOf() uses SecurityAction.LinkDemand to prevent 
			// it from being called from untrusted code, so make sure we 
			// have permission to call it
			SecurityPermission permission = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
			permission.Demand();

			this.cbSize = Marshal.SizeOf(typeof(TRACKMOUSEEVENT));
			
			this.dwFlags = 0;
			this.hwndTrack = IntPtr.Zero;
			this.dwHoverTime = 100;
		}
	}
}
