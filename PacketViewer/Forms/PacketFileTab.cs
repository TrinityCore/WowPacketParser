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

            var col0 = new ControlColumn("", 60);
            col0.Editable = false;
            col0.ControlFactory = new DetailsFactory();
            col0.ControlSize = new Size(50, 6);
            var col1 = new NumberColumn("Num", 200);
            col1.Editable = false;
            var col2 = new TextColumn("Opcode", 200);
            col2.Editable = false;
            var col3 = new DateTimeColumn("Time", 200);
            col3.Editable = false;
            col3.DateTimeFormat = DateTimePickerFormat.Custom;
            col3.CustomDateTimeFormat = "d/m/yyyy hh:mm";
            col3.ShowDropDownButton = false;
            var col4 = new NumberColumn("Sec", 200);
            col4.Editable = false;
            var col5 = new NumberColumn("Length", 200);
            col5.Editable = false;
            table.ColumnModel = new ColumnModel(new Column[] {col0, col1, col2, col3, col4, col5 });
            TableModel model = new TableModel();
            table.CellDoubleClick += new CellMouseEventHandler(ClickedCell);
            table.FamilyRowSelect = false;
            table.FullRowSelect = true;
            this.tablePackets.TableModel = model;

            this.tablePackets.EndUpdate();
        }

        private void ClickedCell(object sender, CellMouseEventArgs e)
        {
            e.Cell.Row.ExpandSubRows = !e.Cell.Row.ExpandSubRows;
        }

        public void AddPackets(List<PacketEntry> packets)
        {
            this.tablePackets.BeginUpdate();

            foreach (var entry in packets)
            {
                RowWithSubrows row = new RowWithSubrows();
                row.Height = 50;
                row.Cells.Add(new CellWithDataSpan(entry.Number.ToString()));
                
                row.Cells.Add(new CellWithData(entry.Number));
                row.Cells.Add(new CellWithText(entry.OpcodeString));
                row.Cells.Add(new CellWithData(entry.Time));
                row.Cells.Add(new CellWithData(entry.Sec));
                row.Cells.Add(new CellWithData(entry.Length));

                this.tablePackets.TableModel.Rows.Add(row);

                RowWithParent detailsRow = new RowWithParent();
                var cell = new CellWithDataSpan(null);
                cell.ColSpan = row.Cells.Count;
                detailsRow.Cells.Add(cell);
                detailsRow.Height = 20;
                row.SubRows.Add(detailsRow);

                row.ExpandSubRows = false;
            }

            this.tablePackets.EndUpdate();
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
