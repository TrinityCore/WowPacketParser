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

using XPTable;
using XPTable.Events;
using XPTable.Win32;


namespace XPTable.Models
{
	/// <summary>
	/// A specialized ContextMenu for Column Headers
	/// </summary>
	[ToolboxItem(false)]
	public class HeaderContextMenu : ContextMenu
	{
		#region Class Data
		
		/// <summary>
		/// The ColumnModel that owns the menu
		/// </summary>
		private ColumnModel model;

		/// <summary>
		/// Specifies whether the menu is enabled
		/// </summary>
		private bool enabled;

		/// <summary>
		/// More columns menuitem
		/// </summary>
		private MenuItem moreMenuItem;

		/// <summary>
		/// Seperator menuitem
		/// </summary>
		private MenuItem separator;

		#endregion

        
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the HeaderContextMenu class with 
		/// no menu items specified
		/// </summary>
		public HeaderContextMenu() : base()
		{
			this.model = null;
			this.enabled = true;

			this.moreMenuItem = new MenuItem("More...", new EventHandler(moreMenuItem_Click));
			this.separator = new MenuItem("-");
		}

		#endregion


		#region Methods
		
		/// <summary>
		/// Displays the shortcut menu at the specified position
		/// </summary>
		/// <param name="control">A Control object that specifies the control 
		/// with which this shortcut menu is associated</param>
		/// <param name="pos">A Point object that specifies the coordinates at 
		/// which to display the menu. These coordinates are specified relative 
		/// to the client coordinates of the control specified in the control 
		/// parameter</param>
		public new void Show(Control control, Point pos)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control", "control cannot be null");
			}

			if (!(control is Table))
			{
				throw new ArgumentException("control must be of type Table", "control");
			}

			if (((Table) control).ColumnModel == null)
			{
				throw new InvalidOperationException("The specified Table does not have an associated ColumnModel");
			}

			//
			this.model = ((Table) control).ColumnModel;

			//
			this.MenuItems.Clear();

			base.Show(control, pos);
		}


		/// <summary>
		/// 
		/// </summary>
		internal bool Enabled
		{
			get
			{
				return this.enabled;
			}

			set
			{
				this.enabled = value;
			}
		}

		#endregion


		#region Events

		/// <summary>
		/// Raises the Popup event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected override void OnPopup(EventArgs e)
		{
			if (this.model.Columns.Count > 0)
			{
				MenuItem item;
				
				for (int i=0; i<this.model.Columns.Count; i++)
				{
					if (i == 10)
					{
						this.MenuItems.Add(this.separator);
						this.MenuItems.Add(this.moreMenuItem);

						break;
					}

					item = new MenuItem(this.model.Columns[i].Text, new EventHandler(menuItem_Click));
					item.Checked = this.model.Columns[i].Visible;

					this.MenuItems.Add(item);
				}
			}

			base.OnPopup(e);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem_Click(object sender, EventArgs e)
		{
			MenuItem item = (MenuItem) sender;
			
			this.model.Columns[item.Index].Visible = !item.Checked;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void moreMenuItem_Click(object sender, EventArgs e)
		{
			ShowColumnsDialog scd = new ShowColumnsDialog();
			scd.AddColumns(this.model);
			scd.ShowDialog(this.SourceControl);
		}

		#endregion


		#region ShowColumnsDialog

		/// <summary>
		/// Summary description for ShowColumnsDialog.
		/// </summary>
		internal class ShowColumnsDialog : Form
		{
			#region Class Data
			
			/// <summary>
			/// Required designer variable.
			/// </summary>
			private System.ComponentModel.Container components = null;
			
			private ColumnModel model = null;
			
			private Label label1;
			private Button upButton;
			private Button downButton;
			private Button showButton;
			private Button hideButton;
			private Label label2;
			private TextBox widthTextBox;
			private CheckBox autoSizeCheckBox;
			private GroupBox groupBox1;
			private Button okButton;
			private Button cancelButton;
			private Table columnTable;
			
			#endregion


			#region Constructor

			/// <summary>
			/// 
			/// </summary>
			public ShowColumnsDialog()
			{
				this.label1 = new Label();
				this.columnTable = new Table();
				this.upButton = new Button();
				this.downButton = new Button();
				this.showButton = new Button();
				this.hideButton = new Button();
				this.label2 = new Label();
				this.widthTextBox = new TextBox();
				this.autoSizeCheckBox = new CheckBox();
				this.groupBox1 = new GroupBox();
				this.okButton = new Button();
				this.cancelButton = new Button();
				this.SuspendLayout();
				// 
				// label1
				// 
				this.label1.Location = new Point(8, 12);
				this.label1.Name = "label1";
				this.label1.Size = new Size(324, 28);
				this.label1.TabIndex = 0;
				//this.label1.Text = "Select the columns you want to appear in this view.  Click Move Up and Move Down " +
				//	"to arrange the columns.";
				this.label1.Text = "Select the columns you want to appear in this view.";
				// 
				// columnListBox
				// 
				this.columnTable.HeaderStyle = ColumnHeaderStyle.None;
				this.columnTable.Location = new Point(12, 52);
				this.columnTable.Name = "columnListBox";
				this.columnTable.Size = new Size(231, 240);
				this.columnTable.TabIndex = 1;
				this.columnTable.ColumnModel = new ColumnModel();
				this.columnTable.ColumnModel.Columns.Add(new CheckBoxColumn("Columns", 227));
				this.columnTable.TableModel = new TableModel();
				this.columnTable.TableModel.RowHeight += 3;
				// 
				// upButton
				// 
				this.upButton.FlatStyle = FlatStyle.System;
				this.upButton.Location = new Point(253, 52);
				this.upButton.Name = "upButton";
				this.upButton.TabIndex = 2;
				this.upButton.Text = "Move &Up";
				this.upButton.Visible = false;
				//this.upButton.Click += new EventHandler(this.upButton_Click);
				// 
				// downButton
				// 
				this.downButton.FlatStyle = FlatStyle.System;
				this.downButton.Location = new Point(253, 81);
				this.downButton.Name = "downButton";
				this.downButton.TabIndex = 3;
				this.downButton.Text = "Move &Down";
				this.downButton.Visible = false;
				//this.downButton.Click += new EventHandler(this.downButton_Click);
				// 
				// showButton
				// 
				this.showButton.FlatStyle = FlatStyle.System;
				//this.showButton.Location = new Point(253, 114);
				this.showButton.Location = new Point(253, 52);
				this.showButton.Name = "showButton";
				this.showButton.TabIndex = 4;
				this.showButton.Text = "&Show";
				this.showButton.Click += new EventHandler(this.showButton_Click);
				// 
				// hideButton
				// 
				this.hideButton.FlatStyle = FlatStyle.System;
				//this.hideButton.Location = new Point(253, 145);
				this.hideButton.Location = new Point(253, 81);
				this.hideButton.Name = "hideButton";
				this.hideButton.TabIndex = 5;
				this.hideButton.Text = "&Hide";
				this.hideButton.Click += new EventHandler(this.hideButton_Click);
				// 
				// label2
				// 
				this.label2.Location = new Point(12, 300);
				this.label2.Name = "label2";
				this.label2.Size = new Size(192, 21);
				this.label2.TabIndex = 6;
				this.label2.Text = "&Width of selected column (in pixels):";
				this.label2.TextAlign = ContentAlignment.MiddleLeft;
				// 
				// textBox1
				// 
				this.widthTextBox.Location = new Point(207, 300);
				this.widthTextBox.MaxLength = 4;
				this.widthTextBox.Name = "textBox1";
				this.widthTextBox.Size = new Size(36, 21);
				this.widthTextBox.TabIndex = 7;
				this.widthTextBox.Text = "0";
				this.widthTextBox.TextAlign = HorizontalAlignment.Right;
				this.widthTextBox.KeyPress += new KeyPressEventHandler(widthTextBox_KeyPress);
				// 
				// autoSizeCheckBox
				// 
				this.autoSizeCheckBox.Location = new Point(12, 330);
				this.autoSizeCheckBox.Name = "autoSizeCheckBox";
				this.autoSizeCheckBox.Size = new Size(228, 16);
				this.autoSizeCheckBox.TabIndex = 8;
				this.autoSizeCheckBox.Text = "&Automatically size all columns";
				this.autoSizeCheckBox.Visible = false;
				// 
				// groupBox1
				// 
				this.groupBox1.Location = new Point(8, 352);
				this.groupBox1.Name = "groupBox1";
				this.groupBox1.Size = new Size(322, 8);
				this.groupBox1.TabIndex = 9;
				this.groupBox1.TabStop = false;
				// 
				// okButton
				// 
				this.okButton.FlatStyle = FlatStyle.System;
				this.okButton.Location = new Point(168, 372);
				this.okButton.Name = "okButton";
				this.okButton.TabIndex = 10;
				this.okButton.Text = "OK";
				this.okButton.Click += new EventHandler(okButton_Click);
				// 
				// cancelButton
				// 
				this.cancelButton.DialogResult = DialogResult.Cancel;
				this.cancelButton.FlatStyle = FlatStyle.System;
				this.cancelButton.Location = new Point(253, 372);
				this.cancelButton.Name = "cancelButton";
				this.cancelButton.TabIndex = 11;
				this.cancelButton.Text = "Cancel";
				// 
				// ShowColumnsDialog
				// 
				this.AcceptButton = this.okButton;
				this.AutoScaleBaseSize = new Size(5, 14);
				this.CancelButton = this.cancelButton;
				this.ClientSize = new Size(339, 408);
				this.Controls.Add(this.cancelButton);
				this.Controls.Add(this.okButton);
				this.Controls.Add(this.groupBox1);
				this.Controls.Add(this.autoSizeCheckBox);
				this.Controls.Add(this.widthTextBox);
				this.Controls.Add(this.label2);
				this.Controls.Add(this.hideButton);
				this.Controls.Add(this.showButton);
				this.Controls.Add(this.downButton);
				this.Controls.Add(this.upButton);
				this.Controls.Add(this.columnTable);
				this.Controls.Add(this.label1);
				this.Font = new Font("Tahoma", 8.25F);
				this.FormBorderStyle = FormBorderStyle.FixedDialog;
				this.MaximizeBox = false;
				this.MinimizeBox = false;
				this.Name = "ShowColumnsDialog";
				this.ShowInTaskbar = false;
				this.StartPosition = FormStartPosition.CenterParent;
				this.Text = "Choose Columns";
				this.ResumeLayout(false);
			}

			#endregion


			#region Methods

			/// <summary>
			/// Clean up any resources being used.
			/// </summary>
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					if (components != null)
					{
						components.Dispose();
					}
				}

				base.Dispose(disposing);
			}


			/// <summary>
			/// 
			/// </summary>
			/// <param name="model"></param>
			/// <returns></returns>
			public void AddColumns(ColumnModel model)
			{
				this.model = model;

				CellStyle cellStyle = new CellStyle();
				cellStyle.Padding = new CellPadding(6, 0, 0, 0);

				this.columnTable.BeginUpdate();
				
				for (int i=0; i<model.Columns.Count; i++)
				{
					Row row = new Row();
				
					Cell cell = new Cell(model.Columns[i].Text, model.Columns[i].Visible);
					cell.Tag = model.Columns[i].Width;
					cell.CellStyle = cellStyle;
				
					row.Cells.Add(cell);

					this.columnTable.TableModel.Rows.Add(row);
				}

				this.columnTable.SelectionChanged += new XPTable.Events.SelectionEventHandler(columnTable_SelectionChanged);
				this.columnTable.CellCheckChanged += new XPTable.Events.CellCheckBoxEventHandler(columnTable_CellCheckChanged);

				if (this.columnTable.VScroll)
				{
					this.columnTable.ColumnModel.Columns[0].Width -= SystemInformation.VerticalScrollBarWidth;
				}

				if (this.columnTable.TableModel.Rows.Count > 0)
				{
					this.columnTable.TableModel.Selections.SelectCell(0, 0);

					this.showButton.Enabled = !this.model.Columns[0].Visible;
					this.hideButton.Enabled = this.model.Columns[0].Visible;

					this.widthTextBox.Text = this.model.Columns[0].Width.ToString();
				}

				this.columnTable.EndUpdate();
			}

			#endregion


			#region Events

			/*/// <summary>
			/// 
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			private void upButton_Click(object sender, System.EventArgs e)
			{
		
			}


			/// <summary>
			/// 
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			private void downButton_Click(object sender, System.EventArgs e)
			{
		
			}*/


			/// <summary>
			/// 
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			private void showButton_Click(object sender, System.EventArgs e)
			{
				int[] indicies = this.columnTable.SelectedIndicies;
				
				if (indicies.Length > 0)
				{
					this.columnTable.TableModel[indicies[0], 0].Checked = true;

					this.hideButton.Focus();
				}
			}


			/// <summary>
			/// 
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			private void hideButton_Click(object sender, System.EventArgs e)
			{
				int[] indicies = this.columnTable.SelectedIndicies;
				
				if (indicies.Length > 0)
				{
					this.columnTable.TableModel[indicies[0], 0].Checked = false;

					this.showButton.Focus();
				}
			}


			/// <summary>
			/// 
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			private void okButton_Click(object sender, EventArgs e)
			{
				int[] indicies = this.columnTable.SelectedIndicies;
				
				if (indicies.Length > 0)
				{
					if (this.widthTextBox.Text.Length == 0)
					{
						this.columnTable.TableModel[indicies[0], 0].Tag = Column.MinimumWidth;
					}
					else
					{
						int width = Convert.ToInt32(this.widthTextBox.Text);

						if (width < Column.MinimumWidth)
						{
							this.columnTable.TableModel[indicies[0], 0].Tag = Column.MinimumWidth;
						}
						else
						{
							this.columnTable.TableModel[indicies[0], 0].Tag = width;
						}
					}
				}
				
				for (int i=0; i<this.columnTable.TableModel.Rows.Count; i++)
				{
					this.model.Columns[i].Visible = this.columnTable.TableModel[i, 0].Checked;
					this.model.Columns[i].Width = (int) this.columnTable.TableModel[i, 0].Tag;
				}

				this.Close();
			}


			/// <summary>
			/// 
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			private void columnTable_SelectionChanged(object sender, SelectionEventArgs e)
			{
				if (e.OldSelectedIndicies.Length > 0)
				{
					if (this.widthTextBox.Text.Length == 0)
					{
						this.columnTable.TableModel[e.OldSelectedIndicies[0], 0].Tag = Column.MinimumWidth;
					}
					else
					{
						int width = Convert.ToInt32(this.widthTextBox.Text);

						if (width < Column.MinimumWidth)
						{
							this.columnTable.TableModel[e.OldSelectedIndicies[0], 0].Tag = Column.MinimumWidth;
						}
						else
						{
							this.columnTable.TableModel[e.OldSelectedIndicies[0], 0].Tag = width;
						}
					}
				}
				
				if (e.NewSelectedIndicies.Length > 0)
				{
					this.showButton.Enabled = !this.columnTable.TableModel[e.NewSelectedIndicies[0], 0].Checked;
					this.hideButton.Enabled = this.columnTable.TableModel[e.NewSelectedIndicies[0], 0].Checked;

					this.widthTextBox.Text = this.columnTable.TableModel[e.NewSelectedIndicies[0], 0].Tag.ToString();
				}
				else
				{
					this.showButton.Enabled = false;
					this.hideButton.Enabled = false;

					this.widthTextBox.Text = "0";
				}
			}

			
			/// <summary>
			/// 
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			private void columnTable_CellCheckChanged(object sender, CellCheckBoxEventArgs e)
			{
				this.showButton.Enabled = !e.Cell.Checked;
				this.hideButton.Enabled = e.Cell.Checked;
			}


			/// <summary>
			/// 
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			private void widthTextBox_KeyPress(object sender, KeyPressEventArgs e)
			{
				if (!char.IsDigit(e.KeyChar) && e.KeyChar != AsciiChars.Backspace && e.KeyChar != AsciiChars.Delete)
				{
					if ((Control.ModifierKeys & (Keys.Alt | Keys.Control)) == Keys.None)
					{
						e.Handled = true;

						NativeMethods.MessageBeep(0 /*MB_OK*/);
					}
				}
			}

			#endregion
		}

		#endregion
	}
}
