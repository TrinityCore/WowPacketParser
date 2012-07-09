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


namespace XPTable.Events
{
	/// <summary>
	/// Specifies the type of event generated when the value of a 
	/// Row's property changes
	/// </summary>
	public enum RowEventType
	{
		/// <summary>
		/// Occurs when the Row's property change type is unknown
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// Occurs when the value of a Row's BackColor property changes
		/// </summary>
		BackColorChanged = 1,

		/// <summary>
		/// Occurs when the value of a Row's ForeColor property changes
		/// </summary>
		ForeColorChanged = 2,

		/// <summary>
		/// Occurs when the value of a Row's Font property changes
		/// </summary>
		FontChanged = 3,

		/// <summary>
		/// Occurs when the value of a Row's RowStyle property changes
		/// </summary>
		StyleChanged = 4,

		/// <summary>
		/// Occurs when the value of a Row's Alignment property changes
		/// </summary>
		AlignmentChanged = 5,

		/// <summary>
		/// Occurs when the value of a Row's Enabled property changes
		/// </summary>
		EnabledChanged = 6,

		/// <summary>
		/// Occurs when the value of a Row's Editable property changes
		/// </summary>
		EditableChanged = 7,

        /// <summary>
        /// Occurs when the value of a Row's ExpandSubRows property changes
        /// </summary>
        ExpandSubRowsChanged = 8,

        /// <summary>
        /// Occurs when a sub row is added
        /// </summary>
        SubRowAdded = 9,

        /// <summary>
        /// Occurs when a sub row is removed
        /// </summary>
        SubRowRemoved = 10
	}
}
