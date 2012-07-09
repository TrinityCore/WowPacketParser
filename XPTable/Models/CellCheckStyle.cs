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
using System.Windows.Forms;


namespace XPTable.Models
{
	/// <summary>
	/// Stores CheckBox related properties for a Cell
	/// </summary>
	internal class CellCheckStyle
	{
		#region Class Data

		/// <summary>
		/// The CheckState of the Cells check box
		/// </summary>
		private CheckState checkState;

		/// <summary>
		/// Specifies whether the Cells check box supports an indeterminate state
		/// </summary>
		private bool threeState;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the CellCheckStyle class with default settings
		/// </summary>
		public CellCheckStyle()
		{
			this.checkState = CheckState.Unchecked;
			this.threeState = false;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets whether the Cell is in the checked state
		/// </summary>
		public bool Checked
		{
			get
			{
				return (this.checkState != CheckState.Unchecked);
			}

			set
			{
				if (value != this.Checked)
				{
					this.CheckState = value ? CheckState.Checked : CheckState.Unchecked;
				}
			}
		}


		/// <summary>
		/// Gets or sets the state of the Cells check box
		/// </summary>
		public CheckState CheckState
		{
			get
			{
				return this.checkState;
			}

			set
			{
				if (!Enum.IsDefined(typeof(CheckState), value))
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(CheckState));
				}
				
				if (this.checkState != value)
				{
					this.checkState = value;
				}
			}
		}
		

		/// <summary>
		/// Gets or sets a value indicating whether the Cells check box 
		/// will allow three check states rather than two
		/// </summary>
		public bool ThreeState
		{
			get
			{
				return this.threeState;
			}

			set
			{
				this.threeState = value;
			}
		}

		#endregion
	}
}
