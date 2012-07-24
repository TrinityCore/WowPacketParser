using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using System.Threading;

using XPTable.Models;
using XPTable.Events;

using PacketViewer.Processing;

using PacketViewer.DataStructures;
using PacketParser.Misc;

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
            _fileinfo = PacketProcessor.GetFileInfoString();
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
            UpdateStatusBar();
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
            if (packets.Count == 0)
                return;
            this.tablePackets.BeginUpdate();
            dataManager.BeginUpdate((int)(packets[0].Number * 2));
            var rows = this.tablePackets.TableModel.Rows;
            foreach (var entry in packets)
            {
                dataManager.AddDataForTableRow(entry, (int)entry.Number*2);
                RowWithSubrows row = new RowWithSubrows();
                row.ExpandSubRows = false;

                this.tablePackets.TableModel.Rows.Add(row);
                RowWithParent detailsRow = new RowWithParent();
                detailsRow.Height = 150;
                row.SubRows.Add(detailsRow);
            }
            dataManager.EndUpdate();
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
            UpdateStatusBar();
        }

        private string _fileinfo = "";

        private void UpdateStatusBar()
        {
            if (Selected)
            {
                if ((FormMain)this.ParentForm != null)
                    ((FormMain)this.ParentForm).toolStripStatusLabel.Text = _status;
                if ((FormMain)this.ParentForm != null)
                    ((FormMain)this.ParentForm).toolStripStatusLabelFileInfo.Text = _fileinfo;
            }
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
                else
                    Thread.Sleep(500);
            }
        }

        private void backgroundWorkerProcessPackets_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetStatus("Done.");
        }
    }

    public class VirtualDataManager
    {
        private CacheFileManager<PacketEntry[]> _data = null;
        public List<byte> cachedBlockUpdatesLeft = new List<byte>();
        public const int virtualBlocksNearIndex = 7;

        public const int cachedBlockUpdateInterval = 1000;
        public const byte cachedBlockUpdatesBeforeRemove = 3;

        public const int virtualBlockSize = 20;

        private Table _table;

        public void Init(Table table)
        {
            _data = new CacheFileManager<PacketEntry[]>();
            _table = table;
            table.TabIndexChanged += new EventHandler(TabIndexChanged);
            table.RowAddedAnytime += new TableModelEventHandler(RowAdded);
            table.RowDataRequestAnytime += new RowEventHandler(RowDataRequested);
            TabIndexChanged(null, null);
        }

        // data requests handlers
        private void TabIndexChanged(object sender, EventArgs args)
        {
            CacheTableRowBlock(_table.TabIndex, _table.TableModel.Rows, virtualBlocksNearIndex);
        }

        private void RowAdded(object sender, TableModelEventArgs args)
        {
            var tableRowIndex = args.Row.Index;
            var rows = args.Row.TableModel.Rows;
            int blockId = GetBlockIndexForTableRow(tableRowIndex);
            if (cachedBlockUpdatesLeft[blockId] != 0)
                CacheTableRow(tableRowIndex, rows, GetBlockDataIndexForTableRow(tableRowIndex), _data.GetBlock(blockId));
        }

        private void RowDataRequested(object caller, RowEventArgs args)
        {
            int tableRowIndex = args.Row.Index;
            int blockId = GetBlockIndexForTableRow(tableRowIndex);
            if (cachedBlockUpdatesLeft[blockId] != 0)
            {
                cachedBlockUpdatesLeft[blockId] = cachedBlockUpdatesBeforeRemove;
                return;
            }
            CacheTableRowBlock(tableRowIndex, args.Row.TableModel.Rows);
        }

        public int GetBlockIndexForTableRow(int tableRowIndex)
        {
            return tableRowIndex / (virtualBlockSize * 2);
        }

        public int GetFirstTableRowIndexForBlock(int block)
        {
            return block * virtualBlockSize * 2;
        }

        public int GetLastTableRowIndexForBlock(int block)
        {
            return GetFirstTableRowIndexForBlock(block+1)-1;
        }

        public int GetBlockDataIndexForTableRow(int tableRowIndex)
        {
            return (tableRowIndex - GetFirstTableRowIndexForBlock(GetBlockIndexForTableRow(tableRowIndex))) / 2;
        }

        public void BeginUpdate(int tableRowIndex)
        {
            _data.BeginBlocksUpdate(GetBlockIndexForTableRow(tableRowIndex));
        }

        public void EndUpdate()
        {
            _data.EndBlocksUpdate();
            _data.UnCacheAllBlocks();
        }

        public void AddDataForTableRow(PacketEntry entry, int tableRowIndex)
        {
            int blockId = GetBlockIndexForTableRow(tableRowIndex);
            while (cachedBlockUpdatesLeft.Count <= blockId)
                cachedBlockUpdatesLeft.Add(0);

            var block = _data.GetBlock(blockId);
            if (block == null)
                block = new PacketEntry[virtualBlockSize];
            var index  = GetBlockDataIndexForTableRow(tableRowIndex);
            block[index] = entry;
            _data.ChangeBlock(blockId, block);
        }

        public PacketEntry GetDataForTableRow(int tableRowIndex)
        {
            int blockId = GetBlockIndexForTableRow(tableRowIndex);
            var block = _data.GetBlock(blockId);
            if (block != null)
                return block[GetBlockDataIndexForTableRow(tableRowIndex)];
            return null;
        }

        public void MakeUnusedIndexesVirtual(RowCollection rows)
        {
            if (_table == null)
                return;
            var topIndexBlock = GetBlockIndexForTableRow(_table.TopIndex);
            var count = _data.GetBlocksCount();
            for (int i = 0; i < count; ++i)
            {
                if (Math.Abs(i - topIndexBlock) <= virtualBlocksNearIndex)
                    continue;
                if (cachedBlockUpdatesLeft[i] == 0)
                    continue;

                if (cachedBlockUpdatesLeft[i] == 1)
                {
                    if (_table.InvokeRequired)
                    {
                        UnCacheRowCallback d = new UnCacheRowCallback(UnCacheRowsInBlock);
                        _table.Invoke(d, new object[] { i, rows });
                    }
                    else
                    {
                        UnCacheRowsInBlock(i, rows);
                    }
                }
                else
                {
                    cachedBlockUpdatesLeft[i]--;
                }
            }
            Thread.Sleep(cachedBlockUpdateInterval);
        }

        public delegate void UnCacheRowCallback(int blockId, RowCollection rows);

        public void CacheRowsInBlock(int blockIndex, RowCollection rows)
        {
            // already cached
            if (cachedBlockUpdatesLeft[blockIndex] != 0)
            {
                cachedBlockUpdatesLeft[blockIndex] = cachedBlockUpdatesBeforeRemove;
                return;
            }
            cachedBlockUpdatesLeft[blockIndex] = cachedBlockUpdatesBeforeRemove;
            var dataBlock = _data.GetBlock(blockIndex);
            var lastRow = Math.Min(GetLastTableRowIndexForBlock(blockIndex), rows.Count-1);
            for (int tableRowIndex = GetFirstTableRowIndexForBlock(blockIndex); tableRowIndex <= lastRow; ++tableRowIndex)
                CacheTableRow(tableRowIndex, rows, GetBlockDataIndexForTableRow(tableRowIndex), dataBlock);
        }

        public void UnCacheRowsInBlock(int blockIndex, RowCollection rows)
        {
            if (cachedBlockUpdatesLeft[blockIndex] == 0)
                return;
            cachedBlockUpdatesLeft[blockIndex] = 0;
            var lastRow = Math.Min(GetLastTableRowIndexForBlock(blockIndex), rows.Count - 1);
            for (int tableRowIndex = GetFirstTableRowIndexForBlock(blockIndex); tableRowIndex <= lastRow; ++tableRowIndex)
                UnCacheTableRow(tableRowIndex, rows);
            _data.UnCacheBlock(blockIndex);
        }

        public void CacheTableRowBlock(int rownum, RowCollection rows, int nearbyBlocks = 0)
        {
            var rowBlock = GetBlockIndexForTableRow(rownum);
            var startBlock = Math.Max(rowBlock - nearbyBlocks, 0);
            var endBlock = Math.Min(rowBlock + nearbyBlocks+1, _data.GetBlocksCount());
            while (cachedBlockUpdatesLeft.Count < rowBlock + nearbyBlocks+1)
            {
                if (cachedBlockUpdatesLeft.Count < _data.GetBlocksCount())
                    cachedBlockUpdatesLeft.Add(0);
                else
                    cachedBlockUpdatesLeft.Add(cachedBlockUpdatesBeforeRemove);
            }
            for (int blockIndex = startBlock; blockIndex < endBlock; ++blockIndex)
                CacheRowsInBlock(blockIndex, rows);
        }

        private void CacheTableRow(int tableRowIndex, RowCollection rows, int dataBlockIndex, PacketEntry[] dataBlock)
        {
            if (dataBlock[dataBlockIndex] == null)
                return;
            var row = rows[tableRowIndex];
            if (row.cells != null)
                return;
            var entry = dataBlock[dataBlockIndex];
            row.cells = new CellCollection(row);
            _table.EventsDisabled = true;
            if ((tableRowIndex % 2) == 0)
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
            _table.EventsDisabled = false;
        }

        private void UnCacheTableRow(int tableRowIndex, RowCollection rows)
        {
            if (rows[tableRowIndex] != null)
                rows.RemoveCacheAt(tableRowIndex);
        }
    }
}
