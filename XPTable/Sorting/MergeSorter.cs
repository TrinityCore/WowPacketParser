/*
 * Copyright © 2005, Mathew Hall
 * All rights reserved.
 * 
 * Sort algorithm modified from the C# implementation written by Jonathan de Halleux, 
 * Marc Clifton, and Robert Rohde (see http://www.codeproject.com/csharp/cssorters.asp) 
 * based on Java implementations by James Gosling, Jason Harrison, Jack Snoeyink, Jim Boritz, 
 * Denis Ahrens, Alvin Raj (see http://www.cs.ubc.ca/spider/harrison/Java/sorting-demo.html).
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
using System.Windows.Forms;

using XPTable.Models;


namespace XPTable.Sorting
{
	/// <summary>
	/// A MergeSort implementation for sorting the Cells contained in a TableModel
	/// </summary>
	public class MergeSorter : SorterBase
	{
		/// <summary>
		/// Initializes a new instance of the MergeSorter class with the specified 
		/// TableModel, Column index, IComparer and SortOrder
		/// </summary>
		/// <param name="tableModel">The TableModel that contains the data to be sorted</param>
		/// <param name="column">The index of the Column to be sorted</param>
		/// <param name="comparer">The IComparer used to sort the Column's Cells</param>
		/// <param name="sortOrder">Specifies how the Column is to be sorted</param>
		public MergeSorter(TableModel tableModel, int column, IComparer comparer, SortOrder sortOrder) : base(tableModel, column, comparer, sortOrder)
		{
			
		}

		
		/// <summary>
		/// Starts sorting the Cells in the TableModel
		/// </summary>
		public override void Sort()
		{
			this.Sort(0, this.TableModel.Rows.Count - 1);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="fromPos"></param>
		/// <param name="toPos"></param>
		private void Sort(int fromPos, int toPos) 
		{
			int end_low;
			int start_high;
			int i;
			Row tmp;
			int mid;
			
			if (fromPos < toPos) 
			{
				mid = (fromPos + toPos) / 2;
			
				this.Sort(fromPos, mid);
				this.Sort(mid + 1, toPos);

				end_low = mid;
				start_high = mid + 1;

				while (fromPos <= end_low & start_high <= toPos) 
				{
					if (this.Compare(this.TableModel.Rows[fromPos], this.TableModel.Rows[start_high]) < 0) 
					{
						fromPos++;
					} 
					else 
					{
						tmp = this.TableModel.Rows[start_high];
						
						for (i = start_high - 1; i >= fromPos; i--) 
						{
							this.Set(i+1, this.TableModel.Rows[i]);
						}

						this.Set(fromPos, tmp);
						
						fromPos++;
						end_low++;
						start_high++;
					}
				}
			}
		}
	}
}
