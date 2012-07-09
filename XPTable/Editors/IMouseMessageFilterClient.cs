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

using XPTable.Events;
using XPTable.Win32;


namespace XPTable.Editors
{
	/// <summary>
	/// Indicates that an object is interested in receiving mouse messages 
	/// before they are sent to their destination
	/// </summary>
	public interface IMouseMessageFilterClient
	{
		/// <summary>
		/// Filters out a mouse message before it is dispatched
		/// </summary>
		/// <param name="target">The Control that will receive the message</param>
		/// <param name="msg">A WindowMessage that represents the message to process</param>
		/// <param name="wParam">Specifies the WParam field of the message</param>
		/// <param name="lParam">Specifies the LParam field of the message</param>
		/// <returns>true to filter the message and prevent it from being dispatched; 
		/// false to allow the message to continue to the next filter or control</returns>
		bool ProcessMouseMessage(Control target, WindowMessage msg, int wParam, int lParam);
	}
}
