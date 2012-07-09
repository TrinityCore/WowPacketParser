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
using System.Windows.Forms.VisualStyles;

using XPTable.Themes;


namespace XPTable.Renderers
{
	/// <summary>
	/// Contains information about the current state of a Cell's check box
	/// </summary>
	public class CheckBoxRendererData
	{
		#region Class Data

		/// <summary>
		/// The current state of the Cells check box
		/// </summary>
		private CheckBoxState checkState;

		#endregion


		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the ButtonRendererData class with the 
		/// specified CheckBox state
		/// </summary>
		/// <param name="checkState">The current state of the Cells CheckBox</param>
		public CheckBoxRendererData(CheckBoxState checkState)
		{
			this.checkState = checkState;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the current state of the Cells checkbox
		/// </summary>
		public CheckBoxState CheckState
		{
			get
			{
				return this.checkState;
			}

			set
			{
				if (!Enum.IsDefined(typeof(CheckBoxState), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(CheckBoxState));
				}
					
				this.checkState = value;
			}
		}

		#endregion
	}
}
