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
	/// A ShellSort implementation for sorting the Cells contained in a TableModel
	/// </summary>
	public class ShellSorter : SorterBase
	{
		/// <summary>
		/// Initializes a new instance of the ShellSorter class with the specified 
		/// TableModel, Column index, IComparer and SortOrder
		/// </summary>
		/// <param name="tableModel">The TableModel that contains the data to be sorted</param>
		/// <param name="column">The index of the Column to be sorted</param>
		/// <param name="comparer">The IComparer used to sort the Column's Cells</param>
		/// <param name="sortOrder">Specifies how the Column is to be sorted</param>
		public ShellSorter(TableModel tableModel, int column, IComparer comparer, SortOrder sortOrder) : base(tableModel, column, comparer, sortOrder)
		{
			
		}

		
		/// <summary>
		/// Starts sorting the Cells in the TableModel
		/// </summary>
		public override void Sort()
		{
			int h;
			int i;
			int j;
			Row b;
			bool loop = true;

			h = 1;

			while (h * 3 + 1 <= this.TableModel.Rows.Count) 
			{
				h = 3 * h + 1;
			}

			while (h > 0) 
			{
				for (i=h-1; i<this.TableModel.Rows.Count; i++) 
				{
					b = this.TableModel.Rows[i];
					j = i;
					loop = true;

					while (loop) 
					{
						if (j >= h) 
						{
                            if (this.Compare(this.TableModel.Rows[j - h], b) > 0)
							{
								this.Set(j, j-h);
								
								j = j - h;
							} 
							else 
							{
								loop = false;
							}
						} 
						else 
						{
							loop = false;
						}
					}

					this.Set(j, b);
				}

				h = h / 3;
			}
		}
	}
}
