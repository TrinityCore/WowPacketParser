using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;

using XPTable.Models;
using XPTable.Events;

using PacketViewer.Processing;

using PacketViewer.DataStructures;

namespace PacketViewer.Forms
{
    public partial class PacketFileTab : UserControl
    {
        public PacketFileTab(string file)
        {
            InitializeComponent();
            filename = file;
            backgroundWorkerProcessPackets.WorkerReportsProgress = true;
            backgroundWorkerProcessPackets.WorkerSupportsCancellation = true;

            PacketProcessor = new PacketFileViewer(filename, new Tuple<int, int>(0, 0), this);
            BecameSelected();

            backgroundWorkerProcessPackets.RunWorkerAsync();
        }

        public string filename;

        public PacketFileViewer PacketProcessor = null;

        public bool Selected = false;

        public void BecameSelected()
        {
            Selected = true;
        }

        public void BecameUnSelected()
        {
            Selected = false;
        }

        private void InitTable()
        {
            Table table = this.tablePackets;
            table.BeginUpdate();
            table.SelectionStyle = SelectionStyle.ListView; // The Table control on a form - already initialised
            table.EnableWordWrap = false;    // If false, then Cell.WordWrap is ignored
            table.GridLines = GridLines.None;
            table.AllowDrop = false;
            table.AllowRMBSelection = false;

            var col1 = new NumberColumn("Num", 200);
            col1.Editable = false;
            var col2 = new TextColumn("Opcode", 200);
            col2.Editable = false;
            var col3 = new DateTimeColumn("Time", 200);
            col3.Editable = false;
            col3.DateTimeFormat = DateTimePickerFormat.Custom;
            col3.CustomDateTimeFormat = "d/m/yyyy hh:mm";
            var col4 = new NumberColumn("Sec", 200);
            col4.Editable = false;
            var col5 = new NumberColumn("Length", 200);
            col5.Editable = false;
            table.ColumnModel = new ColumnModel(new Column[] { col1, col2, col3, col4, col5 });
            TableModel model = new TableModel();

            this.tablePackets.TableModel = model;

            model.Table.FullRowSelect = true;

            this.tablePackets.EndUpdate();
        }

        public void AddPackets(List<PacketEntry> packets)
        {
            this.tablePackets.BeginUpdate();

            foreach (var entry in packets)
            {
                Row row = new Row();
                row.Cells.Add(new Cell(entry.Number));
                row.Cells.Add(new Cell(entry.OpcodeString));
                row.Cells.Add(new Cell(entry.Time));
                row.Cells.Add(new Cell(entry.Sec));
                row.Cells.Add(new Cell(entry.Length));
                this.tablePackets.TableModel.Rows.Add(row);
            }

            this.tablePackets.EndUpdate();

            // Add a sub-row that shows just the email subject in grey (single line only)
            //Row subrow = new Row();
            //subrow.Cells.Add(new Cell());
            //Cell cell = new Cell(subject);
            //cell.ForeColor = Color.Gray;
            //cell.ColSpan = 2;
            //subrow.Cells.Add(cell);
            //row.SubRows.Add(subrow);

            //// Add a sub-row that shows just a preview of the email body in blue, and wraps too
            //subrow = new Row();
            //subrow.Cells.Add(new Cell(new Button()));
            //subrow.Cells.Add(new Cell(new Button()));
            //subrow.Cells.Add(new Cell(new Button()));
            //subrow.Cells.Add(new Cell(new Button()));
            //cell = new Cell(preview);
            //cell.ForeColor = Color.Blue;
            //cell.ColSpan = 2;
            // cell.WordWrap = true;
            //subrow.Cells.Add(cell);
            //row.SubRows.Add(subrow);
        }

        private void PacketFileTab2_Load(object sender, EventArgs e)
        {
            InitTable();
        }

        private void PacketFileTab_ControlRemoved(object sender, ControlEventArgs e)
        {

        }

        private void backgroundWorkerProcessPackets_DoWork(object sender, DoWorkEventArgs e)
        {
            PacketProcessor.Process(sender, e);
        }

        private void backgroundWorkerProcessPackets_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ((FormMain)this.ParentForm).toolStripStatusLabel.Text = "Loading file..." + e.ProgressPercentage.ToString() + "%";
        }
    }
}
