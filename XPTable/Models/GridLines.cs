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

namespace XPTable.Models
{
	/// <summary>
	/// Specifies how a Table draws grid lines between its rows and columns
	/// </summary>
	public enum GridLines
	{
		/// <summary>
		/// No grid lines are drawn
		/// </summary>
		None = 0,

		/// <summary>
		/// Grid lines are only drawn between columns
		/// </summary>
		Columns = 1,

		/// <summary>
		/// Grid lines are only drawn between rows
		/// </summary>
		Rows = 2,

		/// <summary>
		/// Grid lines are drawn between rows and columns
		/// </summary>
		Both = 3,

        /// <summary>
        /// Grid lines are only drawn between families of rows (i.e. main row plus sub row)
        /// </summary>
        RowsOnlyParent = 4,

        /// <summary>
        /// Grid lines are only drawn between families of rows (i.e. main row plus sub row) and parent columns
        /// </summary>
        RowsColumnsOnlyParent = 5
	}
}
