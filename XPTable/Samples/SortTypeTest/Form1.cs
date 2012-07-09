using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using XPTable.Models;

namespace TESTAPP
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("01", "AAAAA"), new Cell("AAAAA", "AAAAA"), new Cell("AAAAA", "AAAAA") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("02", "BBBBB"), new Cell("BBBBB", "BBBBB"), new Cell("BBBBB", "BBBBB") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("03", "CCCCC"), new Cell("CCCCC", "CCCCC"), new Cell("CCCCC", "CCCCC") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("04", ""), new Cell("", ""), new Cell("", "") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("01", "AAAAA"), new Cell("AAAAA", "AAAAA"), new Cell("AAAAA", "AAAAA") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("02", "BBBBB"), new Cell("BBBBB", "BBBBB"), new Cell("BBBBB", "BBBBB") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("03", "CCCCC"), new Cell("CCCCC", "CCCCC"), new Cell("CCCCC", "CCCCC") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("04", ""), new Cell("", ""), new Cell("", "") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("01", "AAAAA"), new Cell("AAAAA", "AAAAA"), new Cell("AAAAA", "AAAAA") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("02", "BBBBB"), new Cell("BBBBB", "BBBBB"), new Cell("BBBBB", "BBBBB") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("03", "CCCCC"), new Cell("CCCCC", "CCCCC"), new Cell("CCCCC", "CCCCC") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("04", ""), new Cell("", ""), new Cell("", "") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("01", "AAAAA"), new Cell("AAAAA", "AAAAA"), new Cell("AAAAA", "AAAAA") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("02", "BBBBB"), new Cell("BBBBB", "BBBBB"), new Cell("BBBBB", "BBBBB") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("03", "CCCCC"), new Cell("CCCCC", "CCCCC"), new Cell("CCCCC", "CCCCC") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("04", ""), new Cell("", ""), new Cell("", "") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("01", "AAAAA"), new Cell("AAAAA", "AAAAA"), new Cell("AAAAA", "AAAAA") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("02", "BBBBB"), new Cell("BBBBB", "BBBBB"), new Cell("BBBBB", "BBBBB") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("03", "CCCCC"), new Cell("CCCCC", "CCCCC"), new Cell("CCCCC", "CCCCC") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("04", ""), new Cell("", ""), new Cell("", "") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("01", "AAAAA"), new Cell("AAAAA", "AAAAA"), new Cell("AAAAA", "AAAAA") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("02", "BBBBB"), new Cell("BBBBB", "BBBBB"), new Cell("BBBBB", "BBBBB") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("03", "CCCCC"), new Cell("CCCCC", "CCCCC"), new Cell("CCCCC", "CCCCC") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("04", ""), new Cell("", ""), new Cell("", "") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("01", "AAAAA"), new Cell("AAAAA", "AAAAA"), new Cell("AAAAA", "AAAAA") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("02", "BBBBB"), new Cell("BBBBB", "BBBBB"), new Cell("BBBBB", "BBBBB") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("03", "CCCCC"), new Cell("CCCCC", "CCCCC"), new Cell("CCCCC", "CCCCC") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("04", ""), new Cell("", ""), new Cell("", "") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("01", "AAAAA"), new Cell("AAAAA", "AAAAA"), new Cell("AAAAA", "AAAAA") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("02", "BBBBB"), new Cell("BBBBB", "BBBBB"), new Cell("BBBBB", "BBBBB") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("03", "CCCCC"), new Cell("CCCCC", "CCCCC"), new Cell("CCCCC", "CCCCC") }));
			this.zRows.Rows.Add(new Row(new Cell[] { new Cell("04", ""), new Cell("", ""), new Cell("", "") }));
		}
	}
}