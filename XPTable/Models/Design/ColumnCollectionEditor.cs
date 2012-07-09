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
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

using XPTable.Events;
using XPTable.Models;


namespace XPTable.Models.Design
{
	/// <summary>
	/// Provides a user interface that can edit collections of Columns 
	/// at design time
	/// </summary>
	public class ColumnCollectionEditor : HelpfulCollectionEditor
	{
        /// <summary>
        /// The ColumnCollection being edited
        /// </summary>
        private ColumnCollection columnCollection;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="type">The type of the collection to be edited</param>
        public ColumnCollectionEditor(Type type)
            : base(type)
        {
        }

		/// <summary>
		/// If the property grid is available it's HelpVisible property is set to true, the help pane backcolor is changed and
		/// the CommandsVisibleIfAvailable property is set to true ((hot) commands are elsewhere known as designer verbs).
		/// </summary>
		/// <returns>The CollectionEditor.CollectionForm returned from base method</returns>
		protected override CollectionEditor.CollectionForm CreateCollectionForm()
		{
			CollectionEditor.CollectionForm collectionForm = base.CreateCollectionForm();

			if (this.PropertyGrid != null)
			{
				this.PropertyGrid.HelpVisible = true;
				this.PropertyGrid.HelpBackColor = System.Drawing.SystemColors.InactiveCaption;
				this.PropertyGrid.CommandsVisibleIfAvailable = true;
			}

			return collectionForm;
		}

		/// <summary>
		/// Gets the data types that this collection editor can contain
		/// </summary>
		/// <returns>An array of data types that this collection can contain</returns>
		protected override Type[] CreateNewItemTypes()
		{
			return new Type[] {typeof(TextColumn),
							   typeof(ButtonColumn),
							   typeof(CheckBoxColumn),
							   typeof(ColorColumn),
							   typeof(ComboBoxColumn),
							   typeof(DateTimeColumn),
							   typeof(ImageColumn),
							   typeof(NumberColumn),
							   typeof(ProgressBarColumn)};
		}
		
		/// <summary>
		/// Creates a new instance of the specified collection item type
		/// </summary>
		/// <param name="itemType">The type of item to create</param>
		/// <returns>A new instance of the specified object</returns>
		protected override object CreateInstance(Type itemType)
		{
			Column column = (Column) base.CreateInstance(itemType);

			this.columnCollection.Add(column);

			column.PropertyChanged += new ColumnEventHandler(this.column_PropertyChanged);

			return column;
		}

		/// <summary>
		/// Destroys the specified instance of the object
		/// </summary>
		/// <param name="instance">The object to destroy</param>
		protected override void DestroyInstance(object instance)
		{
            if (instance != null && instance is Column)
			{
                Column column = (Column)instance;

				this.columnCollection.Remove(column);
				column.PropertyChanged -= new ColumnEventHandler(this.column_PropertyChanged);
				column.Dispose();
			}
			base.DestroyInstance(instance);
		}

		/// <summary>
		/// Edits the value of the specified object using the specified 
		/// service provider and context
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that can be used to gain additional context information</param>
		/// <param name="isp">A service provider object through which editing services can be obtained</param>
		/// <param name="value">the value of the object under edit</param>
		/// <returns>The new value of the object. If the value is not changed, this should return the original value</returns>
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider isp, object value)
		{
			this.columnCollection = (ColumnCollection) value;

            foreach (Column column in this.columnCollection)
			{
				column.PropertyChanged += new ColumnEventHandler(this.column_PropertyChanged);
			}

			object newCollection = base.EditValue(context, isp, value);

            ColumnModel columns = (ColumnModel)context.Instance;

			if (columns.Table != null)
			{
				columns.Table.PerformLayout();
				columns.Table.Refresh();
			}

			return newCollection;
		}

		/// <summary>
		/// Handler for a Column's PropertyChanged event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">A ColumnEventArgs that contains the event data</param>
		private void column_PropertyChanged(object sender, ColumnEventArgs e)
		{
            this.columnCollection.ColumnModel.OnColumnPropertyChanged(e);
		}
	}
}
