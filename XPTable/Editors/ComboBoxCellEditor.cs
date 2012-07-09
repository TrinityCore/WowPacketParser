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
using System.Drawing;
using System.Windows.Forms;

using XPTable.Models;
using XPTable.Renderers;
using XPTable.Win32;


namespace XPTable.Editors
{
	/// <summary>
	/// A class for editing Cells that look like a ComboBox
	/// </summary>
	public class ComboBoxCellEditor : DropDownCellEditor
	{
		#region EventHandlers

		/// <summary>
		/// Occurs when the SelectedIndex property has changed
		/// </summary>
		public event EventHandler SelectedIndexChanged;

		/// <summary>
		/// Occurs when a visual aspect of an owner-drawn ComboBoxCellEditor changes
		/// </summary>
		public event DrawItemEventHandler DrawItem;

		/// <summary>
		/// Occurs each time an owner-drawn ComboBoxCellEditor item needs to be 
		/// drawn and when the sizes of the list items are determined
		/// </summary>
		public event MeasureItemEventHandler MeasureItem;

		#endregion
		
		
		#region Class Data

		/// <summary>
		/// The ListBox that contains the items to be shown in the 
		/// drop-down portion of the ComboBoxCellEditor
		/// </summary>
		private ListBox listbox;

		/// <summary>
		/// The maximum number of items to be shown in the drop-down 
		/// portion of the ComboBoxCellEditor
		/// </summary>
		private int maxDropDownItems;

		/// <summary>
		/// The width of the Cell being edited
		/// </summary>
		private int cellWidth;

		#endregion
		
		
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the ComboBoxCellEditor class with default settings
		/// </summary>
		public ComboBoxCellEditor() : base()
		{
			this.listbox = new ListBox();
			this.listbox.BorderStyle = BorderStyle.None;
			this.listbox.Location = new Point(0, 0);
			this.listbox.Size = new Size(100, 100);
			this.listbox.Dock = DockStyle.Fill;
			this.listbox.DrawItem += new DrawItemEventHandler(this.listbox_DrawItem);
			this.listbox.MeasureItem += new MeasureItemEventHandler(this.listbox_MeasureItem);
			this.listbox.MouseEnter += new EventHandler(this.listbox_MouseEnter);
			this.listbox.KeyDown += new KeyEventHandler(this.OnKeyDown);
			this.listbox.KeyPress += new KeyPressEventHandler(base.OnKeyPress);
			this.listbox.Click += new EventHandler(listbox_Click);
			
			this.TextBox.KeyDown += new KeyEventHandler(OnKeyDown);
			this.TextBox.MouseWheel += new MouseEventHandler(OnMouseWheel);

			this.maxDropDownItems = 8;

			this.cellWidth = 0;

			this.DropDown.Control = this.listbox;
		}	

		#endregion


		#region Methods

		/// <summary>
		/// Sets the location and size of the CellEditor
		/// </summary>
		/// <param name="cellRect">A Rectangle that represents the size and location 
		/// of the Cell being edited</param>
		protected override void SetEditLocation(Rectangle cellRect)
		{
			// calc the size of the textbox
			ICellRenderer renderer = this.EditingTable.ColumnModel.GetCellRenderer(this.EditingCellPos.Column);
			int buttonWidth = ((ComboBoxCellRenderer) renderer).ButtonWidth;

			this.TextBox.Size = new Size(cellRect.Width - 1 - buttonWidth, cellRect.Height-1);
			this.TextBox.Location = cellRect.Location;

			this.cellWidth = cellRect.Width;
		}


		/// <summary>
		/// Sets the initial value of the editor based on the contents of 
		/// the Cell being edited
		/// </summary>
		protected override void SetEditValue()
		{
			this.TextBox.Text = this.EditingCell.Text;
			this.listbox.SelectedItem = this.EditingCell.Text;
		}


		/// <summary>
		/// Sets the contents of the Cell being edited based on the value 
		/// in the editor
		/// </summary>
		protected override void SetCellValue()
		{
			this.EditingCell.Text = this.TextBox.Text;
		}


		/// <summary>
		/// Starts editing the Cell
		/// </summary>
		public override void StartEditing()
		{
			this.listbox.SelectedIndexChanged += new EventHandler(listbox_SelectedIndexChanged);
			
			base.StartEditing();
		}


		/// <summary>
		/// Stops editing the Cell and commits any changes
		/// </summary>
		public override void StopEditing()
		{
			this.listbox.SelectedIndexChanged -= new EventHandler(listbox_SelectedIndexChanged);
			
			base.StopEditing();
		}


		/// <summary>
		/// Stops editing the Cell and ignores any changes
		/// </summary>
		public override void CancelEditing()
		{
			this.listbox.SelectedIndexChanged -= new EventHandler(listbox_SelectedIndexChanged);
			
			base.CancelEditing();
		}



		/// <summary>
		/// Displays the drop down portion to the user
		/// </summary>
		protected override void ShowDropDown()
		{
			if (this.InternalDropDownWidth == -1)
			{
				this.DropDown.Width = this.cellWidth;
				this.listbox.Width = this.cellWidth;
			}
			
			if (this.IntegralHeight)
			{
				int visItems = this.listbox.Height / this.ItemHeight;

				if (visItems > this.MaxDropDownItems)
				{
					visItems = this.MaxDropDownItems;
				}

				if (this.listbox.Items.Count < this.MaxDropDownItems)
				{
					visItems = this.listbox.Items.Count;
				}

				if (visItems == 0)
				{
					visItems = 1;
				}

				this.DropDown.Height = (visItems * this.ItemHeight) + 2;
				this.listbox.Height = visItems * this.ItemHeight;
			}
			
			base.ShowDropDown();
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the maximum number of items to be shown in the drop-down 
		/// portion of the ComboBoxCellEditor
		/// </summary>
		public int MaxDropDownItems
		{
			get
			{
				return this.maxDropDownItems;
			}

			set
			{
				if ((value < 1) || (value > 100))
				{
					throw new ArgumentOutOfRangeException("MaxDropDownItems must be between 1 and 100");
				}

				this.maxDropDownItems = value;
			}
		}


		/// <summary>
		/// Gets or sets a value indicating whether your code or the operating 
		/// system will handle drawing of elements in the list
		/// </summary>
		public DrawMode DrawMode
		{
			get
			{
				return this.listbox.DrawMode;
			}

			set
			{
				if (!Enum.IsDefined(typeof(DrawMode), value))
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(DrawMode));
				}
				
				this.listbox.DrawMode = value;
			}
		}


		/// <summary>
		/// Gets or sets a value indicating whether the drop-down portion of the 
		/// editor should resize to avoid showing partial items
		/// </summary>
		public bool IntegralHeight
		{
			get
			{
				return this.listbox.IntegralHeight;
			}

			set
			{
				this.listbox.IntegralHeight = value;
			}
		}


		/// <summary>
		/// Gets or sets the height of an item in the editor
		/// </summary>
		public int ItemHeight
		{
			get
			{
				return this.listbox.ItemHeight;
			}

			set
			{
				this.listbox.ItemHeight = value;
			}
		}


		/// <summary>
		/// Gets an object representing the collection of the items contained 
		/// in this ComboBoxCellEditor
		/// </summary>
		public ListBox.ObjectCollection Items
		{
			get
			{
				return this.listbox.Items;
			}
		}


		/// <summary>
		/// Gets or sets the maximum number of characters allowed in the editable 
		/// portion of a ComboBoxCellEditor
		/// </summary>
		public int MaxLength
		{
			get
			{
				return this.TextBox.MaxLength;
			}

			set
			{
				this.TextBox.MaxLength = value;
			}
		}


		/// <summary>
		/// Gets or sets the index specifying the currently selected item
		/// </summary>
		public int SelectedIndex
		{
			get
			{
				return this.listbox.SelectedIndex;
			}

			set
			{
				this.listbox.SelectedIndex = value;
			}
		}


		/// <summary>
		/// Gets or sets currently selected item in the ComboBoxCellEditor
		/// </summary>
		public object SelectedItem
		{
			get
			{
				return this.listbox.SelectedItem;
			}

			set
			{
				this.listbox.SelectedItem = value;
			}
		}

		#endregion


		#region Events

		/// <summary>
		/// Handler for the editors TextBox.KeyDown and ListBox.KeyDown events
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">A KeyEventArgs that contains the event data</param>
		protected virtual void OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Up)
			{
				int index = this.SelectedIndex;

				if (index == -1)
				{
					this.SelectedIndex = 0;
				}
				else if (index > 0)
				{
					this.SelectedIndex--;
				}

				e.Handled = true;
			}
			else if (e.KeyData == Keys.Down)
			{
				int index = this.SelectedIndex;

				if (index == -1)
				{
					this.SelectedIndex = 0;
				}
				else if (index < this.Items.Count - 1)
				{
					this.SelectedIndex++;
				}

				e.Handled = true;
			}
		}


		/// <summary>
		/// Handler for the editors TextBox.MouseWheel event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">A MouseEventArgs that contains the event data</param>
		protected virtual void OnMouseWheel(object sender, MouseEventArgs e)
		{
			int index = this.SelectedIndex;

			if (index == -1)
			{
				this.SelectedIndex = 0;
			}
			else
			{
				if (e.Delta > 0)
				{
					if (index > 0)
					{
						this.SelectedIndex--;
					}
				}
				else
				{
					if (index < this.Items.Count - 1)
					{
						this.SelectedIndex++;
					}
				}
			}
		}


		/// <summary>
		/// Raises the DrawItem event
		/// </summary>
		/// <param name="e">A DrawItemEventArgs that contains the event data</param>
		protected virtual void OnDrawItem(DrawItemEventArgs e)
		{
			if (DrawItem != null)
			{
				DrawItem(this, e);
			}
		}


		/// <summary>
		/// Raises the MeasureItem event
		/// </summary>
		/// <param name="e">A MeasureItemEventArgs that contains the event data</param>
		protected virtual void OnMeasureItem(MeasureItemEventArgs e)
		{
			if (MeasureItem != null)
			{
				MeasureItem(this, e);
			}
		}

		
		/// <summary>
		/// Raises the SelectedIndexChanged event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			if (SelectedIndexChanged != null)
			{
				SelectedIndexChanged(this, e);
			}

			this.TextBox.Text = this.SelectedItem.ToString();
		}


		/// <summary>
		/// Handler for the editors ListBox.Click event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">An EventArgs that contains the event data</param>
		private void listbox_Click(object sender, EventArgs e)
		{
			this.DroppedDown = false;

			this.EditingTable.StopEditing();
		}


		/// <summary>
		/// Handler for the editors ListBox.SelectedIndexChanged event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">An EventArgs that contains the event data</param>
		private void listbox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.OnSelectedIndexChanged(e);
		}


		/// <summary>
		/// Handler for the editors ListBox.MouseEnter event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">An EventArgs that contains the event data</param>
		private void listbox_MouseEnter(object sender, EventArgs e)
		{
			this.EditingTable.RaiseCellMouseLeave(this.EditingCellPos);
		}


		/// <summary>
		/// Handler for the editors ListBox.DrawItem event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">A DrawItemEventArgs that contains the event data</param>
		private void listbox_DrawItem(object sender, DrawItemEventArgs e)
		{
			this.OnDrawItem(e);
		}

		
		/// <summary>
		/// Handler for the editors ListBox.MeasureItem event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">A MeasureItemEventArgs that contains the event data</param>
		private void listbox_MeasureItem(object sender, MeasureItemEventArgs e)
		{
			this.OnMeasureItem(e);
		}

		#endregion
	}
}
