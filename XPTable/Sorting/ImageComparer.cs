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
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

using XPTable.Models;


namespace XPTable.Sorting
{
	/// <summary>
	/// An IComparer for sorting Cells that contain Images
	/// </summary>
	public class ImageComparer : ComparerBase
	{
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the ImageComparer class with the specified 
		/// TableModel, Column index and SortOrder
		/// </summary>
		/// <param name="tableModel">The TableModel that contains the data to be sorted</param>
		/// <param name="column">The index of the Column to be sorted</param>
		/// <param name="sortOrder">Specifies how the Column is to be sorted</param>
		public ImageComparer(TableModel tableModel, int column, SortOrder sortOrder) : base(tableModel, column, sortOrder)
		{
			
		}

		#endregion


		#region Methods
		
        /// <summary>
        /// Compares two cells and returns a value indicating whether one is less 
        /// than, equal to or greater than the other.
        /// </summary>
        /// <param name="cell1"></param>
        /// <param name="cell2"></param>
        /// <returns></returns>
        protected override int CompareCells(Cell cell1, Cell cell2)
        {
            int retVal = 0;

			// check for null data
			if (cell1.Data == null && cell2.Data == null)
			{
				retVal = 0;
			}
			else if (cell1.Data == null)
			{
				retVal = -1;
			}
			else if (cell2.Data == null)
			{
				retVal = 1;
			}

			// since images aren't comparable, if retVal = 0 and the ImageColumn 
			// they belong to allows text drawing, compare the text properties 
			// to determine order
			if (retVal == 0 && ((ImageColumn) this.TableModel.Table.ColumnModel.Columns[this.SortColumn]).DrawText)
			{
				// check for null data
				if (cell1.Text == null && cell2.Text == null)
				{
					return 0;
				}
				else if (cell1.Text == null)
				{
					return -1;
				}
			
				retVal = cell1.Text.CompareTo(cell2.Text);
			}

			return retVal;
		}

		#endregion
	}
}
