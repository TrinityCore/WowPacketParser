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


namespace XPTable.Win32
{
	/// <summary>
	/// The WindowMessage enemeration contains Windows messages that the 
	/// XPTable may be interested in listening for
	/// </summary>
	public enum WindowMessage
	{
		/// <summary>
		/// The WM_ACTIVATE message is sent to both the window being activated and the 
		/// window being deactivated. If the windows use the same input queue, the message 
		/// is sent synchronously, first to the window procedure of the top-level window 
		/// being deactivated, then to the window procedure of the top-level window being 
		/// activated. If the windows use different input queues, the message is sent 
		/// asynchronously, so the window is activated immediately
		/// </summary>
		WM_ACTIVATE = 0x0006,

		/// <summary>
		/// The WM_PAINT message is sent when the system or another application makes a request 
		/// to paint a portion of an application's window. The message is sent when the 
		/// UpdateWindow or RedrawWindow function is called, or by the DispatchMessage 
		/// function when the application obtains a WM_PAINT message by using the GetMessage 
		/// or PeekMessage function. A window receives this message through its WindowProc 
		/// function
		/// </summary>
		WM_PAINT = 0x000F,
		
		/// <summary>
		/// The WM_CLOSE message is sent as a signal that a window or an application 
		/// should terminate
		/// </summary>
		WM_CLOSE = 0x0010,

		/// <summary>
		/// The WM_ACTIVATEAPP message is sent when a window belonging to a different 
		/// application than the active window is about to be activated. The message is 
		/// sent to the application whose window is being activated and to the application 
		/// whose window is being deactivated
		/// </summary>
		WM_ACTIVATEAPP = 0x001C,
		
		/// <summary>
		/// The WM_MOUSEACTIVATE message is sent when the cursor is in an inactive window 
		/// and the user presses a mouse button. The parent window receives this message 
		/// only if the child window passes it to the DefWindowProc function
		/// </summary>
		WM_MOUSEACTIVATE = 0x0021,

		/// <summary>
		/// The WM_NCACTIVATE message is sent to a window when its nonclient area needs to 
		/// be changed to indicate an active or inactive state
		/// </summary>
		WM_NCACTIVATE = 0x0086,
		

		/// <summary>
		/// The WM_NCMOUSEMOVE message is posted to a window when the cursor is moved 
		/// within the nonclient area of the window. This message is posted to the window 
		/// that contains the cursor. If a window has captured the mouse, this message 
		/// is not posted
		/// </summary>
		WM_NCMOUSEMOVE = 0x00A0,
		
		/// <summary>
		/// The WM_NCLBUTTONDOWN message is posted when the user presses the left mouse 
		/// button while the cursor is within the nonclient area of a window. This message 
		/// is posted to the window that contains the cursor. If a window has captured 
		/// the mouse, this message is not posted
		/// </summary>
		WM_NCLBUTTONDOWN = 0x00A1,
		
		/// <summary>
		/// The WM_NCLBUTTONUP message is posted when the user releases the left mouse 
		/// button while the cursor is within the nonclient area of a window. This message 
		/// is posted to the window that contains the cursor. If a window has captured 
		/// the mouse, this message is not posted
		/// </summary>
		WM_NCLBUTTONUP = 0x00A2,
		
		/// <summary>
		/// The WM_NCLBUTTONDBLCLK message is posted when the user double-clicks the 
		/// left mouse button while the cursor is within the nonclient area of a window. 
		/// This message is posted to the window that contains the cursor. If a window 
		/// has captured the mouse, this message is not posted
		/// </summary>
		WM_NCLBUTTONDBLCLK = 0x00A3,
		
		/// <summary>
		/// The WM_NCRBUTTONDOWN message is posted when the user presses the right mouse 
		/// button while the cursor is within the nonclient area of a window. This message 
		/// is posted to the window that contains the cursor. If a window has captured 
		/// the mouse, this message is not posted
		/// </summary>
		WM_NCRBUTTONDOWN = 0x00A4,
		
		/// <summary>
		/// The WM_NCRBUTTONUP message is posted when the user releases the right mouse 
		/// button while the cursor is within the nonclient area of a window. This message 
		/// is posted to the window that contains the cursor. If a window has captured 
		/// the mouse, this message is not posted
		/// </summary>
		WM_NCRBUTTONUP = 0x00A5,
		
		/// <summary>
		/// The WM_NCRBUTTONDBLCLK message is posted when the user double-clicks the right 
		/// mouse button while the cursor is within the nonclient area of a window. This 
		/// message is posted to the window that contains the cursor. If a window has 
		/// captured the mouse, this message is not posted
		/// </summary>
		WM_NCRBUTTONDBLCLK = 0x00A6,
		
		/// <summary>
		/// The WM_NCMBUTTONDOWN message is posted when the user presses the middle mouse 
		/// button while the cursor is within the nonclient area of a window. This message 
		/// is posted to the window that contains the cursor. If a window has captured the 
		/// mouse, this message is not posted
		/// </summary>
		WM_NCMBUTTONDOWN = 0x00A7,
		
		/// <summary>
		/// The WM_NCMBUTTONUP message is posted when the user releases the middle mouse 
		/// button while the cursor is within the nonclient area of a window. This message 
		/// is posted to the window that contains the cursor. If a window has captured the 
		/// mouse, this message is not posted
		/// </summary>
		WM_NCMBUTTONUP = 0x00A8,
		
		/// <summary>
		/// The WM_NCMBUTTONDBLCLK message is posted when the user double-clicks the middle 
		/// mouse button while the cursor is within the nonclient area of a window. This 
		/// message is posted to the window that contains the cursor. If a window has 
		/// captured the mouse, this message is not posted
		/// </summary>
		WM_NCMBUTTONDBLCLK = 0x00A9, 
		
		/// <summary>
		/// The WM_NCXBUTTONDOWN message is posted when the user presses the first or second X 
		/// button while the cursor is in the nonclient area of a window. This message is posted 
		/// to the window that contains the cursor. If a window has captured the mouse, this 
		/// message is not posted
		/// </summary>
		WM_NCXBUTTONDOWN = 0x00AB,
		
		/// <summary>
		/// The WM_NCXBUTTONUP message is posted when the user releases the first or second 
		/// X button while the cursor is in the nonclient area of a window. This message is 
		/// posted to the window that contains the cursor. If a window has captured the mouse, 
		/// this message is not posted
		/// </summary>
		WM_NCXBUTTONUP = 0x00AC,
		
		/// <summary>
		/// The WM_NCXBUTTONDBLCLK message is posted when the user double-clicks the first or 
		/// second X button while the cursor is in the nonclient area of a window. This message 
		/// is posted to the window that contains the cursor. If a window has captured the mouse, 
		/// this message is not posted
		/// </summary>
		WM_NCXBUTTONDBLCLK = 0x00AD, 

		/// <summary>
		/// The WM_KEYDOWN message is posted to the window with the keyboard focus when a 
		/// nonsystem key is pressed. A nonsystem key is a key that is pressed when the ALT 
		/// key is not pressed
		/// </summary>
		WM_KEYDOWN = 0x0100,
		
		/// <summary>
		/// The WM_KEYUP message is posted to the window with the keyboard focus when a 
		/// nonsystem key is released. A nonsystem key is a key that is pressed when the ALT 
		/// key is not pressed, or a keyboard key that is pressed when a window has the 
		/// keyboard focus
		/// </summary>
		WM_KEYUP = 0x0101,
		
		/// <summary>
		/// The WM_CHAR message is posted to the window with the keyboard focus when a 
		/// WM_KEYDOWN message is translated by the TranslateMessage function. The WM_CHAR 
		/// message contains the character code of the key that was pressed
		/// </summary>
		WM_CHAR = 0x0102,
		
		/// <summary>
		/// The WM_DEADCHAR message is posted to the window with the keyboard focus when a 
		/// WM_KEYUP message is translated by the TranslateMessage function. WM_DEADCHAR 
		/// specifies a character code generated by a dead key. A dead key is a key that 
		/// generates a character, such as the umlaut (double-dot), that is combined with 
		/// another character to form a composite character. For example, the umlaut-O 
		/// character (Ö) is generated by typing the dead key for the umlaut character, 
		/// and then typing the O key
		/// </summary>
		WM_DEADCHAR = 0x0103,
		
		/// <summary>
		/// The WM_SYSKEYDOWN message is posted to the window with the keyboard focus when 
		/// the user presses the F10 key (which activates the menu bar) or holds down the 
		/// ALT key and then presses another key. It also occurs when no window currently 
		/// has the keyboard focus; in this case, the WM_SYSKEYDOWN message is sent to the 
		/// active window. The window that receives the message can distinguish between 
		/// these two contexts by checking the context code in the lParam parameter
		/// </summary>
		WM_SYSKEYDOWN = 0x0104,
		
		/// <summary>
		/// The WM_SYSKEYUP message is posted to the window with the keyboard focus when 
		/// the user releases a key that was pressed while the ALT key was held down. It 
		/// also occurs when no window currently has the keyboard focus; in this case, the 
		/// WM_SYSKEYUP message is sent to the active window. The window that receives the 
		/// message can distinguish between these two contexts by checking the context code 
		/// in the lParam parameter
		/// </summary>
		WM_SYSKEYUP = 0x0105,
		
		/// <summary>
		/// The WM_MOUSEMOVE message is posted to a window when the cursor moves. If the mouse 
		/// is not captured, the message is posted to the window that contains the cursor. 
		/// Otherwise, the message is posted to the window that has captured the mouse
		/// </summary>
		WM_MOUSEMOVE = 0x0200,
		
		/// <summary>
		/// The WM_LBUTTONDOWN message is posted when the user presses the left mouse button 
		/// while the cursor is in the client area of a window. If the mouse is not captured, 
		/// the message is posted to the window beneath the cursor. Otherwise, the message is 
		/// posted to the window that has captured the mouse
		/// </summary>
		WM_LBUTTONDOWN = 0x0201,
		
		/// <summary>
		/// The WM_LBUTTONUP message is posted when the user releases the left mouse button 
		/// while the cursor is in the client area of a window. If the mouse is not captured, 
		/// the message is posted to the window beneath the cursor. Otherwise, the message is 
		/// posted to the window that has captured the mouse
		/// </summary>
		WM_LBUTTONUP = 0x0202,
		
		/// <summary>
		/// The WM_LBUTTONDBLCLK message is posted when the user double-clicks the left mouse 
		/// button while the cursor is in the client area of a window. If the mouse is not 
		/// captured, the message is posted to the window beneath the cursor. Otherwise, the 
		/// message is posted to the window that has captured the mouse
		/// </summary>
		WM_LBUTTONDBLCLK = 0x0203,
		
		/// <summary>
		/// The WM_RBUTTONDOWN message is posted when the user presses the right mouse button 
		/// while the cursor is in the client area of a window. If the mouse is not captured, 
		/// the message is posted to the window beneath the cursor. Otherwise, the message is 
		/// posted to the window that has captured the mouse
		/// </summary>
		WM_RBUTTONDOWN = 0x0204,
		
		/// <summary>
		/// The WM_RBUTTONUP message is posted when the user releases the right mouse button 
		/// while the cursor is in the client area of a window. If the mouse is not captured, 
		/// the message is posted to the window beneath the cursor. Otherwise, the message is 
		/// posted to the window that has captured the mouse
		/// </summary>
		WM_RBUTTONUP = 0x0205,
		
		/// <summary>
		/// The WM_RBUTTONDBLCLK message is posted when the user double-clicks the right mouse 
		/// button while the cursor is in the client area of a window. If the mouse is not 
		/// captured, the message is posted to the window beneath the cursor. Otherwise, the 
		/// message is posted to the window that has captured the mouse
		/// </summary>
		WM_RBUTTONDBLCLK = 0x0206,
		
		/// <summary>
		/// The WM_MBUTTONDOWN message is posted when the user presses the middle mouse button 
		/// while the cursor is in the client area of a window. If the mouse is not captured, 
		/// the message is posted to the window beneath the cursor. Otherwise, the message is 
		/// posted to the window that has captured the mouse
		/// </summary>
		WM_MBUTTONDOWN = 0x0207,
		
		/// <summary>
		/// The WM_MBUTTONUP message is posted when the user releases the middle mouse button 
		/// while the cursor is in the client area of a window. If the mouse is not captured, 
		/// the message is posted to the window beneath the cursor. Otherwise, the message is 
		/// posted to the window that has captured the mouse
		/// </summary>
		WM_MBUTTONUP = 0x0208,
		
		/// <summary>
		/// The WM_MBUTTONDBLCLK message is posted when the user double-clicks the middle mouse 
		/// button while the cursor is in the client area of a window. If the mouse is not 
		/// captured, the message is posted to the window beneath the cursor. Otherwise, the 
		/// message is posted to the window that has captured the mouse
		/// </summary>
		WM_MBUTTONDBLCLK = 0x0209,
		
		/// <summary>
		/// The WM_XBUTTONDOWN message is posted when the user presses the first or second X 
		/// button while the cursor is in the client area of a window. If the mouse is not captured, 
		/// the message is posted to the window beneath the cursor. Otherwise, the message is 
		/// posted to the window that has captured the mouse
		/// </summary>
		WM_XBUTTONDOWN = 0x020B,
		
		/// <summary>
		/// The WM_XBUTTONUP message is posted when the user releases the first or second X 
		/// button while the cursor is in the client area of a window. If the mouse is not 
		/// captured, the message is posted to the window beneath the cursor. Otherwise, the 
		/// message is posted to the window that has captured the mouse
		/// </summary>
		WM_XBUTTONUP = 0x020C,
		
		/// <summary>
		/// The WM_XBUTTONDBLCLK message is posted when the user double-clicks the first or 
		/// second X button while the cursor is in the client area of a window. If the mouse 
		/// is not captured, the message is posted to the window beneath the cursor. Otherwise, 
		/// the message is posted to the window that has captured the mouse
		/// </summary>
		WM_XBUTTONDBLCLK = 0x020D,
		
		/// <summary>
		/// The WM_MOUSEWHEEL message is sent to the focus window when the mouse wheel is 
		/// rotated. The DefWindowProc function propagates the message to the window's parent. 
		/// There should be no internal forwarding of the message, since DefWindowProc propagates 
		/// it up the parent chain until it finds a window that processes it
		/// </summary>
		WM_MOUSEWHEEL = 0x020A
	}
}
