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

using XPTable.Events;


namespace XPTable.Models
{
	/// <summary>
	/// Represents a collection of Row objects
	/// </summary>
	public interface RowCollection
	{
		#region Methods
		
		/// <summary>
		/// Adds the specified Row to the end of the collection
		/// </summary>
		/// <param name="row">The Row to add</param>
        int Add(Row row);

        /// <summary>
        /// Collapses all sub rows.
        /// </summary>
        void CollapseAllSubRows();

        /// <summary>
        /// Expands all sub rows.
        /// </summary>
        void ExpandAllSubRows();

		/// <summary>
		/// Adds an array of Row objects to the collection
		/// </summary>
		/// <param name="rows">An array of Row objects to add 
		/// to the collection</param>
		void AddRange(Row[] rows);


		/// <summary>
		/// Removes the specified Row from the model
		/// </summary>
		/// <param name="row">The Row to remove</param>
		void Remove(Row row);


		/// <summary>
		/// Removes an array of Row objects from the collection
		/// </summary>
		/// <param name="rows">An array of Row objects to remove 
		/// from the collection</param>
		void RemoveRange(Row[] rows);


		/// <summary>
		/// Removes the Row at the specified index from the collection
		/// </summary>
		/// <param name="index">The index of the Row to remove</param>
		void RemoveAt(int index);

        void RemoveCacheAt(int index);


		/// <summary>
		/// Removes all Rows from the collection
		/// </summary>
		void Clear();

		/// <summary>
		/// Inserts a Row into the collection at the specified index
		/// </summary>
		/// <param name="index">The zero-based index at which the Row 
		/// should be inserted</param>
		/// <param name="row">The Row to insert</param>
		void Insert(int index, Row row);


		/// <summary>
		/// Inserts an array of Rows into the collection at the specified 
		/// index
		/// </summary>
		/// <param name="index">The zero-based index at which the rows 
		/// should be inserted</param>
		/// <param name="rows">The array of Rows to be inserted into 
		/// the collection</param>
		void InsertRange(int index, Row[] rows);


		/// <summary>
		///	Returns the index of the specified Row in the model
		/// </summary>
		/// <param name="row">The Row to look for</param>
		/// <returns>The index of the specified Row in the model</returns>
		int IndexOf(Row row);

		#endregion


		#region Properties


        int Count
        {
            get;
        }

		/// <summary>
		/// Gets the Row at the specified index
		/// </summary>
		//Row this[int index];

		#endregion
	}
}
