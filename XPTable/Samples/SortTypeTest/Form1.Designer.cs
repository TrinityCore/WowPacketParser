namespace TESTAPP
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			XPTable.Models.DataSourceColumnBinder dataSourceColumnBinder1 = new XPTable.Models.DataSourceColumnBinder();
			this.zTable = new XPTable.Models.Table();
			this.zColumns = new XPTable.Models.ColumnModel();
			this.zRows = new XPTable.Models.TableModel();
			this.textColumn1 = new XPTable.Models.TextColumn();
			this.textColumn2 = new XPTable.Models.TextColumn();
			this.textColumn3 = new XPTable.Models.TextColumn();
			((System.ComponentModel.ISupportInitialize) (this.zTable)).BeginInit();
			this.SuspendLayout();
			// 
			// zTable
			// 
			this.zTable.BorderColor = System.Drawing.Color.Black;
			this.zTable.ColumnModel = this.zColumns;
			this.zTable.DataMember = null;
			this.zTable.DataSourceColumnBinder = dataSourceColumnBinder1;
			this.zTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.zTable.Location = new System.Drawing.Point(0, 0);
			this.zTable.Name = "zTable";
			this.zTable.Size = new System.Drawing.Size(292, 271);
			this.zTable.TabIndex = 0;
			this.zTable.TableModel = this.zRows;
			this.zTable.Text = "table1";
			this.zTable.UnfocusedBorderColor = System.Drawing.Color.Black;
			// 
			// zColumns
			// 
			this.zColumns.Columns.AddRange(new XPTable.Models.Column[] {
            this.textColumn1,
            this.textColumn2,
            this.textColumn3});
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 271);
			this.Controls.Add(this.zTable);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize) (this.zTable)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private XPTable.Models.Table zTable;
		private XPTable.Models.ColumnModel zColumns;
		private XPTable.Models.TableModel zRows;
		private XPTable.Models.TextColumn textColumn1;
		private XPTable.Models.TextColumn textColumn2;
		private XPTable.Models.TextColumn textColumn3;
	}
}

