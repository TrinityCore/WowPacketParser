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

            backgroundWorkerProcessPackets.WorkerSupportsCancellation = true;
            backgroundWorkerTableVirtualData.RunWorkerAsync();
        }

        public string filename;

        public PacketFileViewer PacketProcessor = null;

        public bool Selected = false;

        public void BecameSelected()
        {
            Selected = true;
            UpdateStatus();
        }

        public void BecameUnSelected()
        {
            Selected = false;
        }

        public VirtualDataManager dataManager = new VirtualDataManager();

        private void InitTable()
        {
            Table table = this.tablePackets;
            table.BeginUpdate();
            table.SelectionStyle = SelectionStyle.ListView; // The Table control on a form - already initialised
            table.EnableWordWrap = false;    // If false, then Cell.WordWrap is ignored
            table.GridLines = GridLines.None;
            table.AllowDrop = false;
            table.AllowRMBSelection = false;

            var col0 = new ControlColumn("", 1);
            col0.ControlFactory = new DetailsFactory(this);
            col0.ControlOffset = new Point(0, 0);
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
            table.MultiSelect = true;
            this.tablePackets.TableModel = model;

            this.tablePackets.EndUpdate();

            dataManager.Init(table);
        }

        private void ClickedCell(object sender, CellMouseEventArgs e)
        {
            this.tablePackets.TableModel.Rows[e.Row].ExpandSubRows = !this.tablePackets.TableModel.Rows[e.Row].ExpandSubRows;
        }

        public void AddPackets(List<PacketEntry> packets)
        {
            this.tablePackets.BeginUpdate();
            var rows = this.tablePackets.TableModel.Rows;
            foreach (var entry in packets)
            {
                dataManager.Add(entry, (int)entry.Number*2);
                RowWithSubrows row = new RowWithSubrows();
                row.ExpandSubRows = false;

                this.tablePackets.TableModel.Rows.Add(row);
                RowWithParent detailsRow = new RowWithParent();
                detailsRow.Height = 150;
                row.SubRows.Add(detailsRow);
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

        private string _status = "";
        private void SetStatus(string status)
        {
            _status = status;
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            if (Selected && (FormMain)this.ParentForm != null)
                ((FormMain)this.ParentForm).toolStripStatusLabel.Text = _status;
        }

        private void backgroundWorkerProcessPackets_DoWork(object sender, DoWorkEventArgs e)
        {
            PacketProcessor.Process(sender, e);
        }

        private void backgroundWorkerProcessPackets_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            SetStatus("Loading file..." + e.ProgressPercentage.ToString() + "%");
        }

        private void backgroundWorkerTableVirtualData_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            while (true)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    return;
                }
                if (this.tablePackets != null && this.tablePackets.TableModel != null)
                    dataManager.MakeUnusedIndexesVirtual(this.tablePackets.TableModel.Rows);
            }
        }

        private void backgroundWorkerProcessPackets_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetStatus("Done.");
        }
    }

    public class VirtualDataPackage
    {
        public PacketEntry[] data = new PacketEntry[VirtualDataManager.virtualBlockDataSize];
    }

    public class VirtualDataManager
    {
        public List<VirtualDataPackage> virtualData = new List<VirtualDataPackage>();
        public List<byte> cachedBlockUpdatesLeft = new List<byte>();
        public const int virtualBlockSize = 100;
        public const int virtualBlockDataSize = virtualBlockSize / 2;
        public const int virtualBlocksNearIndex = 3;

        public const int cachedBlockUpdateInterval = 1000;
        public const byte cachedBlockUpdatesBeforeRemove = 3;

        private Table table;

        public void Init(Table _table)
        {
            table = _table;
            table.TabIndexChanged += new EventHandler(TabIndexChanged);
            table.RowAddedAnytime += new TableModelEventHandler(RowAdded);
            table.RowDataRequestAnytime += new RowEventHandler(RowDataRequested);
            TabIndexChanged(null, null);
        }

        private void TabIndexChanged(object sender, EventArgs args)
        {
            CacheRowBlock(table.TabIndex, table.TableModel.Rows, virtualBlocksNearIndex);
        }

        private void RowAdded(object sender, TableModelEventArgs args)
        {
            var index = args.Row.Index;
            var rows = args.Row.TableModel.Rows;
            int blockId = index / virtualBlockSize;
            if (cachedBlockUpdatesLeft[blockId] != 0)
                CacheRow(index, rows, (index - blockId * virtualBlockSize)/2, virtualData[blockId]);
        }

        private void RowDataRequested(object caller, RowEventArgs args)
        {
            int index = args.Row.Index;
            int blockId = index / virtualBlockSize;
            if (cachedBlockUpdatesLeft[blockId] != 0)
            {
                cachedBlockUpdatesLeft[blockId] = cachedBlockUpdatesBeforeRemove;
                return;
            }
            CacheRowBlock(index, args.Row.TableModel.Rows);
        }

        public void Add(PacketEntry entry, int index)
        {
            int blockId = index / virtualBlockSize;
            while (virtualData.Count <= blockId)
                virtualData.Add(new VirtualDataPackage());
            while (cachedBlockUpdatesLeft.Count <= blockId)
                cachedBlockUpdatesLeft.Add(0);
            virtualData[blockId].data[(index - blockId * virtualBlockSize)/2] = entry;
        }

        public PacketEntry Get(int index)
        {
            int blockId = index / virtualBlockSize;
            return virtualData[blockId].data[(index - blockId * virtualBlockSize) / 2];
        }

        public void MakeUnusedIndexesVirtual(RowCollection rows)
        {
            if (table == null)
                return;
            var topIndex = table.TopIndex;
            var topIndexBlock = topIndex / virtualBlockSize;
            var count = virtualData.Count;
            for (int i = 0; i < count; ++i)
            {
                if (Math.Abs(i - topIndexBlock) <= virtualBlocksNearIndex)
                    continue;
                if (cachedBlockUpdatesLeft[i] == 0)
                    continue;

                if (cachedBlockUpdatesLeft[i] == 1)
                {
                    cachedBlockUpdatesLeft[i] = 0;
                    var firstRowIndex = i * virtualBlockSize;
                    for (int j = 0; j < virtualBlockSize; ++j)
                    {
                        var rowIndex = firstRowIndex + j;
                        if (rows[rowIndex] != null)
                            rows.RemoveCacheAt(rowIndex);
                    }
                }
                else
                {
                    cachedBlockUpdatesLeft[i]--;
                }
            }
            Thread.Sleep(cachedBlockUpdateInterval);
        }

        public void CacheRowBlock(int rownum, RowCollection rows, int nearbyBlocks = 0)
        {
            var rowBlock = rownum / virtualBlockSize;
            var startBlock = Math.Max(rowBlock - nearbyBlocks, 0);
            var endBlock = rowBlock + nearbyBlocks+1;
            while (cachedBlockUpdatesLeft.Count < endBlock)
                cachedBlockUpdatesLeft.Add(0);
            var dataBlocks = virtualData.Count;
            for (int blockIndex = startBlock; blockIndex < endBlock; ++blockIndex)
            {
                if (cachedBlockUpdatesLeft[blockIndex] != 0)
                {
                    cachedBlockUpdatesLeft[blockIndex] = cachedBlockUpdatesBeforeRemove;
                    continue;
                }
                cachedBlockUpdatesLeft[blockIndex] = cachedBlockUpdatesBeforeRemove;
                if (blockIndex >= dataBlocks)
                    continue;

                int firstRowIndex = blockIndex * virtualBlockSize;
                var dataBlock = virtualData[blockIndex];
                for (int i = 0; i < virtualBlockSize; ++i)
                {
                    var rowIndex = firstRowIndex + i;
                    if (rowIndex >= rows.Count)
                        break;
                    CacheRow(rowIndex, rows, i/2, dataBlock);
                }
            }
        }

        private void CacheRow(int rownum, RowCollection rows, int dataBlockIndex, VirtualDataPackage dataBlock)
        {
            if (dataBlock.data[dataBlockIndex] == null)
                return;
            var row = rows[rownum];
            //if (row.cells != null)
                //throw new Exception("cells should be unloaded at this point");
            var entry = dataBlock.data[dataBlockIndex];
            row.cells = new CellCollection(row);
            if ((rownum % 2) == 0)
            {
                row.cells.AddRange(new Cell[] {new CellWithData(null), new CellWithData(entry.Number+1), new CellWithText(entry.OpcodeString),
                    new CellWithData(entry.Time), new CellWithData(entry.Sec), new CellWithData(entry.Length)});
            }
            else
            {
                var cell = new CellWithSpan();
                cell.ColSpan = 6;
                row.cells.Add(cell);
            }
        }
    }
}
