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
using System.Windows.Forms.VisualStyles;

using XPTable.Themes;


namespace XPTable.Renderers
{
	/// <summary>
	/// Contains information about the current state of a number Cell's 
	/// up and down buttons
	/// </summary>
	public class NumberRendererData
	{
		#region Class Data

		/// <summary>
		/// The current state of the up button
		/// </summary>
		private UpDownState upState;

		/// <summary>
		/// The current state of the down button
		/// </summary>
		private UpDownState downState;
		
		/// <summary>
		/// The x coordinate of the last mouse click point
		/// </summary>
		private int clickX;

		/// <summary>
		/// The y coordinate of the last mouse click point
		/// </summary>
		private int clickY;

		#endregion


		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the NumberRendererData class
		/// </summary>
		public NumberRendererData()
		{
			this.upState = UpDownState.Normal;
			this.downState = UpDownState.Normal;
			this.clickX = -1;
			this.clickY = -1;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the current state of the up button
		/// </summary>
		public UpDownState UpButtonState
		{
			get
			{
				return this.upState;
			}

			set
			{
				if (!Enum.IsDefined(typeof(UpDownState), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(UpDownState));
				}
					
				this.upState = value;
			}
		}


		/// <summary>
		/// Gets or sets the current state of the down button
		/// </summary>
		public UpDownState DownButtonState
		{
			get
			{
				return this.downState;
			}

			set
			{
				if (!Enum.IsDefined(typeof(UpDownState), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(UpDownState));
				}
					
				this.downState = value;
			}
		}
		

		/// <summary>
		/// Gets or sets the Point that the mouse was last clicked in a button
		/// </summary>
		public Point ClickPoint
		{
			get
			{
				return new Point(this.clickX, this.clickY);
			}

			set
			{
				this.clickX = value.X;
				this.clickY = value.Y;
			}
		}

		#endregion
	}
}
