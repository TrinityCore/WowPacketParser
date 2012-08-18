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
using System.Security.Permissions;
using System.Windows.Forms;

using XPTable.Events;
using XPTable.Win32;


namespace XPTable.Editors
{
	/// <summary>
	/// A message filter that filters mouse messages
	/// </summary>
	internal class MouseMessageFilter : IMessageFilter
	{
		/// <summary>
		/// An IMouseMessageFilterClient that wishes to receive mouse events
		/// </summary>
		private IMouseMessageFilterClient client;
		
		
		/// <summary>
		/// Initializes a new instance of the CellEditor class with the 
		/// specified IMouseMessageFilterClient client
		/// </summary>
		public MouseMessageFilter(IMouseMessageFilterClient client)
		{
			this.client = client;
		}


		/// <summary>
		/// Gets or sets the IMouseMessageFilterClient that wishes to 
		/// receive mouse events
		/// </summary>
		public IMouseMessageFilterClient Client
		{
			get
			{
				return this.client;
			}

			set
			{
				this.client = value;
			}
		}


		/// <summary>
		/// Filters out a message before it is dispatched
		/// </summary>
		/// <param name="m">The message to be dispatched. You cannot modify 
		/// this message</param>
		/// <returns>true to filter the message and prevent it from being 
		/// dispatched; false to allow the message to continue to the next 
		/// filter or control</returns>
		public bool PreFilterMessage(ref Message m)
		{
			// make sure we have a client
			if (this.Client == null)
			{
				return false;
			}

			// make sure the message is a mouse message
			if ((m.Msg >= (int) WindowMessage.WM_MOUSEMOVE && m.Msg <= (int) WindowMessage.WM_XBUTTONDBLCLK) || 
				(m.Msg >= (int) WindowMessage.WM_NCMOUSEMOVE && m.Msg <= (int) WindowMessage.WM_NCXBUTTONUP))
			{
				// try to get the target control
				UIPermission uiPermission = new UIPermission(UIPermissionWindow.AllWindows);
				uiPermission.Demand();
				Control target = Control.FromChildHandle(m.HWnd);

				return this.Client.ProcessMouseMessage(target, (WindowMessage) m.Msg, m.WParam.ToInt32(), m.LParam.ToInt32());
			}
				
			return false;
		}
	}
}
