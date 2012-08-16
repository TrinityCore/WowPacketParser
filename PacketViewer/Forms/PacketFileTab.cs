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
using PacketParser.Enums;

namespace PacketViewer.Forms
{
    public partial class PacketFileTab : UserControl
    {
        public PacketFileTab(string file, FormFileOpenDetails fileOpenDetails)
        {
            InitializeComponent();
            filename = file;
            backgroundWorkerProcessPackets.WorkerReportsProgress = true;
            backgroundWorkerProcessPackets.WorkerSupportsCancellation = true;

            PacketProcessor = new PacketFileViewer(filename, this, fileOpenDetails, new Tuple<int, int>(0, 0));
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

        public VirtualDataManager dataManager = null;

        private void InitTable()
        {
            dataManager = new VirtualDataManager();
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
            var col1 = new NumberColumn("Id", 50);
            col1.Editable = false;
            var col12 = new NumberColumn("SubId", 50);
            col12.Editable = false;
            var col13 = new NumberColumn("OpcId", 50);
            col13.Editable = false;
            var col2 = new TextColumn("OpcName", 300);
            col2.Editable = false;
            var col21 = new TextColumn("Dir", 40);
            col21.Editable = false;
            var col3 = new DateTimeColumn("Time", 120);
            col3.Editable = false;
            col3.DateTimeFormat = DateTimePickerFormat.Custom;
            col3.CustomDateTimeFormat = "d/m/yyyy hh:mm:ss";
            col3.ShowDropDownButton = false;
            var col4 = new NumberColumn("Sec", 50);
            col4.Editable = false;
            var col41 = new TextColumn("Status", 50);
            col41.Editable = false;
            var col5 = new NumberColumn("Length", 50);
            col5.Editable = false;
            table.ColumnModel = new ColumnModel(new Column[] { col0, col1, col12, col13, col2, col21, col3, col4, col41, col5 });
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
            if (this.tablePackets == null)
                return;
            this.tablePackets.BeginUpdate();
            dataManager.BeginUpdate((int)(packets[0].UID));
            var rows = this.tablePackets.TableModel.Rows;
            foreach (var entry in packets)
            {
                dataManager.AddDataForRow(entry, (int)entry.UID);
                RowWithSubrows row = new RowWithSubrows();
                row.ExpandSubRows = false;
                row.Tag = entry.UID;

                this.tablePackets.TableModel.Rows.Add(row);
                if (row.Index != (entry.UID*2))
                    throw new Exception("Incorrect add row operation");

                RowWithParent detailsRow = new RowWithParent();
                detailsRow.Height = 2;
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

    public class VirtualDataManager : IDisposable
    {
        private CacheFileManager<PacketEntry[]> _data = null;
        public List<byte> cachedBlockUpdatesLeft = new List<byte>();
        public const int virtualBlocksNearIndex = 2;

        public const int cachedBlockUpdateInterval = 1000;
        public const byte cachedBlockUpdatesBeforeRemove = 3;

        public const int virtualBlockSize = 10;

        private Table _table;
        private List<Row> _rows = new List<Row>();

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
            if (_table.TableModel.Rows.Count == 0)
                return;
            CacheTableRowBlock(_table.TableModel.Rows[_table.TabIndex], virtualBlocksNearIndex);
        }

        private void RowAdded(object sender, TableModelEventArgs args)
        {
            var rowIndex = args.Row.Index;
            if (rowIndex >= _rows.Count)
                _rows.AddRange(new Row[rowIndex - _rows.Count + 1]);
            _rows[rowIndex] = args.Row;
            int blockId = GetBlockIndexForTableRow(args.Row);
            if (cachedBlockUpdatesLeft[blockId] != 0)
            {
                CacheTableRow(args.Row, GetBlockDataIndexForTableRow(args.Row), _data.GetBlock(blockId));
                cachedBlockUpdatesLeft[blockId] = 4;
            }
        }

        private void RowDataRequested(object caller, RowEventArgs args)
        {
            int blockId = GetBlockIndexForTableRow(args.Row);
            if (cachedBlockUpdatesLeft[blockId] != 0)
            {
                cachedBlockUpdatesLeft[blockId] = cachedBlockUpdatesBeforeRemove;
                return;
            }
            CacheTableRowBlock(args.Row);
        }

        // table row
        public int GetBlockIndexForTableRow(Row row)
        {
            int rIndex = row.Parent == null ? (int)row.Tag : (int)row.Parent.Tag;
            return rIndex / (virtualBlockSize );
        }

        public int GetFirstTableRowIndexForBlock(int block)
        {
            return block * virtualBlockSize * 2;
        }

        public int GetLastTableRowIndexForBlock(int block)
        {
            return GetFirstTableRowIndexForBlock(block+1)-1;
        }

        public int GetBlockDataIndexForTableRow(Row row)
        {
            int rIndex = row.Parent == null ? (int)row.Tag : (int)row.Parent.Tag;
            return (rIndex * 2 - GetFirstTableRowIndexForBlock(GetBlockIndexForTableRow(row))) / 2;
        }

        // data row indexes
        public int GetBlockIndexForRow(int rowIndex)
        {
            return rowIndex / (virtualBlockSize);
        }

        public int GetBlockDataIndexForRow(int rowIndex)
        {
            return (rowIndex - GetFirstRowIndexForBlock(GetBlockIndexForRow(rowIndex)));
        }

        public int GetFirstRowIndexForBlock(int block)
        {
            return block * virtualBlockSize;
        }

        public void BeginUpdate(int rowIndex)
        {
            _data.BeginBlocksUpdate(GetBlockIndexForRow(rowIndex));
        }

        public void EndUpdate()
        {
            _data.EndBlocksUpdate();
            _data.UnCacheAllBlocks();
        }

        public void AddDataForRow(PacketEntry entry, int rowIndex)
        {
            int blockId = GetBlockIndexForRow(rowIndex);
            while (cachedBlockUpdatesLeft.Count <= blockId)
                cachedBlockUpdatesLeft.Add(0);

            var block = _data.GetBlock(blockId);
            if (block == null)
                block = new PacketEntry[virtualBlockSize];
            var index = GetBlockDataIndexForRow(rowIndex);
            block[index] = entry;
            _data.ChangeBlock(blockId, block);
        }

        public void MakeUnusedIndexesVirtual(RowCollectionForTable rows)
        {
            if (_table == null || _table.TopIndex == -1)
                return;
            /*var topIndexBlock = GetBlockIndexForTableRow(rows[_table.TopIndex]);
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
                        _table.Invoke(d, new object[] { i });
                    }
                    else
                    {
                        UnCacheRowsInBlock(i);
                    }
                }
                else
                {
                    cachedBlockUpdatesLeft[i]--;
                }
            }*/
            Thread.Sleep(cachedBlockUpdateInterval);
        }

        public delegate void UnCacheRowCallback(int blockId);

        public void CacheRowsInBlock(int blockIndex)
        {
            // already cached
            if (cachedBlockUpdatesLeft[blockIndex] != 0)
            {
                cachedBlockUpdatesLeft[blockIndex] = cachedBlockUpdatesBeforeRemove;
                return;
            }
            cachedBlockUpdatesLeft[blockIndex] = cachedBlockUpdatesBeforeRemove;
            var dataBlock = _data.GetBlock(blockIndex);
            var lastRow = Math.Min(GetLastTableRowIndexForBlock(blockIndex), _rows.Count-1);
            for (int tableRowIndex = GetFirstTableRowIndexForBlock(blockIndex); tableRowIndex <= lastRow; ++tableRowIndex)
                CacheTableRow(_rows[tableRowIndex], GetBlockDataIndexForTableRow(_rows[tableRowIndex]), dataBlock);
        }

        public void UnCacheRowsInBlock(int blockIndex)
        {
            if (cachedBlockUpdatesLeft[blockIndex] == 0)
                return;
            cachedBlockUpdatesLeft[blockIndex] = 0;
            var lastRow = Math.Min(GetLastTableRowIndexForBlock(blockIndex), _rows.Count - 1);
            for (int tableRowIndex = GetFirstTableRowIndexForBlock(blockIndex); tableRowIndex <= lastRow; ++tableRowIndex)
                UnCacheTableRow(_rows[tableRowIndex]);
            _data.UnCacheBlock(blockIndex);
        }

        public void CacheTableRowBlock(Row row, int nearbyBlocks = 0)
        {
            int rowBlock = GetBlockIndexForTableRow(row);
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
                CacheRowsInBlock(blockIndex);
        }

        private void CacheTableRow(Row row, int dataBlockIndex, PacketEntry[] dataBlock)
        {
            if (dataBlock[dataBlockIndex] == null)
                return;
            int tableRowIndex = row.Index;
            if (row.cells != null)
                return;
            var entry = dataBlock[dataBlockIndex];
            row.cells = new CellCollection(row);
            _table.EventsDisabled = true;
            if ((tableRowIndex % 2) == 0)
            {
                row.cells.AddRange(new Cell[] {new Cell(), new CellWithData(entry.Number+1), new CellWithData((int)entry.SubPacketNumber), new CellWithData(entry.Opcode), new CellWithText(entry.OpcodeString),
                    new CellWithText(entry.Dir), new CellWithData(entry.Time), new CellWithData(entry.Sec), new CellWithText(entry.Status), new CellWithData(entry.Length)});
            }
            else
            {
                var cell = new CellWithSpan();
                cell.ColSpan = 10;
                row.cells.Add(cell);
            }
            _table.EventsDisabled = false;
        }

        private void UnCacheTableRow(Row row)
        {
            if (row != null)
                row.TableModel.Rows.RemoveCacheAt(row.Index);
        }

        public void Dispose()
        {
            _data.Dispose();
        }
    }
}
