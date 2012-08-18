/*
 * Copyright © 2005, Mathew Hall
 * All rights reserved.
 * 
 * Modified from the C# implementation written by Jonathan de Halleux, Marc Clifton, 
 * and Robert Rohde (see http://www.codeproject.com/csharp/cssorters.asp) based on 
 * Java implementations by James Gosling, Jason Harrison, Jack Snoeyink, Jim Boritz, 
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
	/// Base class for the sorters used to sort the Cells contained in a TableModel
	/// </summary>
	public abstract class SorterBase
	{
		#region Class Data

		/// <summary>
		/// The TableModel that contains the Cells to be sorted
		/// </summary>
		private TableModel tableModel;

		/// <summary>
		/// The index of the Column to be sorted
		/// </summary>
		private int column;

		/// <summary>
		/// The IComparer used to sort the Column's Cells
		/// </summary>
		private IComparer comparer;

		/// <summary>
		/// Specifies how the Column is to be sorted
		/// </summary>
		private SortOrder sortOrder;

        /// <summary>
        /// Specifies a collection of underlying sort order(s)
        /// </summary>
        private SortColumnCollection secondarySortOrder;

        /// <summary>
        /// Specifies a collection of comparers for the underlying sort order(s)
        /// </summary>
        private IComparerCollection secondaryComparers;

		#endregion
		

		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the SorterBase class with the specified 
		/// TableModel, Column index, IComparer and SortOrder
		/// </summary>
		/// <param name="tableModel">The TableModel that contains the data to be sorted</param>
		/// <param name="column">The index of the Column to be sorted</param>
		/// <param name="comparer">The IComparer used to sort the Column's Cells</param>
		/// <param name="sortOrder">Specifies how the Column is to be sorted</param>
		public SorterBase(TableModel tableModel, int column, IComparer comparer, SortOrder sortOrder)
		{
			this.tableModel = tableModel;
			this.column = column;
			this.comparer = comparer;
			this.sortOrder = sortOrder;
            this.secondarySortOrder = new SortColumnCollection();
            this.secondaryComparers = new IComparerCollection();
		}

		#endregion


		#region Methods

		/// <summary>
		/// Compares two rows and returns a value indicating whether one is less 
		/// than, equal to or greater than the other. Takes into account the sort order.
		/// </summary>
		/// <param name="row1">First row to compare</param>
        /// <param name="row2">Second row to compare</param>
		/// <returns>-1 if a is less than b, 1 if a is greater than b, or 0 if a equals b</returns>
        protected int Compare(Row row1, Row row2)
        {
            int result = 0;

            if (this.SortOrder == SortOrder.None)
            {
                result = 0;
            }

            // check for null rows
            else if (row1 == null && row2 == null)
            {
                result = 0;
            }
            else if (row1 == null)
            {
                result = -1;
            }
            else if (row2 == null)
            {
                result = 1;
            }
            else
            {
                /*
                 * 1. If both are subrows and from the same parent, then compare the rows
                 * 2. If both are subrows, but from different parents, then compare parent rows
                 * 3. If only one is a subrow, then compare the row against the parent row
                 * 4. If both are not subrows, then just compare the rows
                 * */

                if (row1.Parent == null)
                {
                    if (row2.Parent == null)
                    {
                        // 4. If both are not subrows, then just compare the rows
                        result = CompareRows(row1, row2);
                    }
                    else
                    {
                        // 3. If only one is a subrow, then compare the row against the parent row
                        result = CompareRows(row1, row2.Parent);
                    }
                }
                else
                {
                    if (row2.Parent == null)
                    {
                        // 3. If only one is a subrow, then compare the row against the parent row
                        result = CompareRows(row1.Parent, row2);
                    }
                    else
                    {
                        // Both input rows are sub rows

                        if (row1.Parent.Index == row2.Parent.Index)
                        {
                            // 1. From the same parent - compare subrows...
                            //                    result = CompareRows(row1, row2);

                            // ...or keep them in 'insert' order
                            result = row1.ChildIndex.CompareTo(row2.ChildIndex);

                            // If we are sorting in reverse order, then this would put the subrows in reverse
                            // order too, bt we want to preserve the top-down order, so need to reverse the comparison

                            //if (this.SortOrder == SortOrder.Descending)
                            //    result = -result;
                        }
                        else
                        {
                            // 2. From different parents - compare parents
                            result = CompareRows(row1.Parent, row2.Parent);
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Compares two rows and returns a value indicating whether one is less 
        /// than, equal to or greater than the other.
        /// Compares the given rows without considering parent/sub-rows.
        /// </summary>
        /// <param name="row1"></param>
        /// <param name="row2"></param>
        /// <returns></returns>
        protected virtual int CompareRows(Row row1, Row row2)
        {

            int result = CompareRows(row1, row2, this.SortColumn, this.Comparer);

            if (result == 0)
            {
                int i = 0;  // used to get the right comparer out of the collection

                foreach (SortColumn col in this.SecondarySortOrders)
                {
                    IComparer comparer = this.SecondaryComparers[i];
                    result = CompareRows(row1, row2, col.SortColumnIndex, comparer);

                    if (result != 0)
                    {
                        if (col.SortOrder == SortOrder.Descending)
                            result = -result;

                        // Need to invert the result if a DESC sort order was used

                        break;
                    }

                    i++;
                }
            }

            // If we do this then the direction of the secondary sorting is NOT reversed when
            // the main sort order is DESC
            else if (this.SortOrder == SortOrder.Descending)
                result = -result;

            // If we do this then the direction of the secondary sorting IS reversed when
            // the main sort order is DESC
            //if (this.SortOrder == SortOrder.Descending)
            //    result = -result;

            return result;
        }

        /// <summary>
        /// Compares two rows and returns a value indicating whether one is less 
        /// than, equal to or greater than the other.
        /// Compares the given rows without considering parent/sub-rows.
        /// </summary>
        /// <param name="row1"></param>
        /// <param name="row2"></param>
        /// <param name="column"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        protected virtual int CompareRows(Row row1, Row row2, int column, IComparer comparer)
        {
            Cell cell1 = row1.Cells[column];
            Cell cell2 = row2.Cells[column];

            // check for null cells
            if (cell1 == null && cell2 == null)
            {
                return 0;
            }
            else if (cell1 == null)
            {
                return -1;
            }
            else if (cell2 == null)
            {
                return 1;
            }

            int result = comparer.Compare(cell1, cell2);

            return result;
        }

		/// <summary>
		/// Starts sorting the Cells in the TableModel
		/// </summary>
		public abstract void Sort();


		/// <summary>
		/// Swaps the Rows in the TableModel at the specified indexes
		/// </summary>
		/// <param name="a">The index of the first Row to be swapped</param>
		/// <param name="b">The index of the second Row to be swapped</param>
		protected void Swap(int a, int b)
		{
			Row swap = this.TableModel.Rows[a];
			
			this.TableModel.Rows.SetRow(a, this.TableModel.Rows[b]);
			this.TableModel.Rows.SetRow(b, swap);
		}
		

		/// <summary>
		/// Replaces the Row in the TableModel located at index a with the Row 
		/// located at index b
		/// </summary>
		/// <param name="a">The index of the Row that will be replaced</param>
		/// <param name="b">The index of the Row that will be moved to index a</param>
		protected void Set(int a, int b)
		{
			this.TableModel.Rows.SetRow(a, this.TableModel.Rows[b]);
		}


		/// <summary>
		/// Replaces the Row in the TableModel located at index a with the specified Row
		/// </summary>
		/// <param name="a">The index of the Row that will be replaced</param>
		/// <param name="row">The Row that will be moved to index a</param>
		protected void Set(int a, Row row)
		{
			this.TableModel.Rows.SetRow(a, row);
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets the TableModel that contains the Cells to be sorted
		/// </summary>
		public TableModel TableModel
		{
			get
			{
				return this.tableModel;
			}
		}


		/// <summary>
		/// Gets the index of the Column to be sorted
		/// </summary>
		public int SortColumn
		{
			get
			{
				return this.column;
			}
		}


		/// <summary>
		/// Gets the IComparer used to sort the Column's Cells
		/// </summary>
		public IComparer Comparer
		{
			get
			{
				return this.comparer;
			}
		}


		/// <summary>
		/// Gets how the Column is to be sorted
		/// </summary>
		public SortOrder SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}

        /// <summary>
        /// Gets or sets a collection of underlying sort order(s)
        /// </summary>
        public SortColumnCollection SecondarySortOrders
        {
            get
            {
                return this.secondarySortOrder;
            }
            set
            {
                this.secondarySortOrder = value;
            }
        }

        /// <summary>
        /// Gets or sets a collection of comparers for the underlying sort order(s)
        /// </summary>
        public IComparerCollection SecondaryComparers
        {
            get
            {
                return this.secondaryComparers;
            }
            set
            {
                this.secondaryComparers = value;
            }
        }

		#endregion
	}
}
