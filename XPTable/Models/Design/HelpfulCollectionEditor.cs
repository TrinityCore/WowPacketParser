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
using System.ComponentModel.Design;
using System.Windows.Forms;


namespace XPTable.Models.Design
{
	/// <summary>
	/// A CollectionEditor that displays the help and command areas of its PropertyGrid
	/// </summary>
	public class HelpfulCollectionEditor : CollectionEditor
	{
        private PropertyGrid propertyGrid;

		/// <summary>
		/// Initializes a new instance of the HelpfulCollectionEditor class using 
		/// the specified collection type
		/// </summary>
		/// <param name="type">The type of the collection for this editor to edit</param>
        public HelpfulCollectionEditor(Type type)
            : base(type)
        {
        }

		/// <summary>
		/// If it can be found, the PropertyGrid is made available here, it's availability not guaranteed.
		/// Inheritors should check that it is available before attempting to access its members.  It is 
		/// discovered in the <see cref="HelpfulCollectionEditor.CreateCollectionForm"/> method, inheritors
		/// who want to change the PropertyGrids properties would typically do so in an override of 
		/// that method, after invoking the base method.
		/// </summary>
        public PropertyGrid PropertyGrid
        {
            get { return this.propertyGrid; }
//            private set { this.propertyGrid = value; }
        }

		/// <summary>
		/// Discovers the CollectionForm's property grid
		/// </summary>
		/// <returns>The CollectionEditor.CollectionForm returned from base method</returns>
		protected override CollectionEditor.CollectionForm CreateCollectionForm()
		{
			CollectionEditor.CollectionForm editor = base.CreateCollectionForm();

			this.findPropertyGrid((Control) editor);

			return editor;
		}

		private bool findPropertyGrid(Control control)
		{
			if (control is PropertyGrid)
			{
				propertyGrid = (PropertyGrid) control;
				return true;
			}

			foreach (Control c in control.Controls)
			{
				if (this.findPropertyGrid(c))
				{
					return true;
				}
			}
			return false;
		}

	}
}
