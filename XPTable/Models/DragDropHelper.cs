using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

using XPTable.Editors;
using XPTable.Events;
using XPTable.Models;
using XPTable.Renderers;
using XPTable.Sorting;
using XPTable.Themes;
using XPTable.Win32;

namespace XPTable.Models
{
    /// <summary>
    /// Encapsulates drag drop functionality for Table.
    /// </summary>
    class DragDropHelper
    {
        #region Members
        Table _table;
        bool _isMouseDown;
        bool _isStartDrag;
        int _selectedRow;
        Row _previousRow;
        IDragDropRenderer _renderer;
        #endregion

        /// <summary>
        /// Creates a drag drop helper for the given table.
        /// </summary>
        /// <param name="table"></param>
        public DragDropHelper(Table table)
        {
            _table = table;
            _table.DragEnter += new DragEventHandler(table_DragEnter);
            _table.DragOver += new DragEventHandler(table_DragOver);
            _table.DragDrop += new DragEventHandler(table_DragDrop);
            Reset();
        }

        /// <summary>
        /// Gets or sets the renderer that draws the drag drop hover indicator.
        /// </summary>
        public IDragDropRenderer DragDropRenderer
        {
            get { return _renderer; }
            set { _renderer = value; }
        }

        #region Drag drop events
        void table_DragDrop(object sender, DragEventArgs drgevent)
        {
            if (_table.UseBuiltInDragDrop)
            {
                if (drgevent.Data.GetDataPresent(typeof(DragItemData).ToString()))
                {
                    Point point = _table.PointToClient(new Point(drgevent.X, drgevent.Y));
                    int nRow = -1;
                    if (point.Y <= _table.HeaderHeight)
                    {
                        nRow = _table.TopIndex - 1;
                        if (nRow < 0)
                            nRow = 0;
                    }
                    else
                        nRow = _table.RowIndexAt(point);

                    Row hoverItem = _table.TableModel.Rows[nRow];

                    DragItemData data = (DragItemData)drgevent.Data.GetData(typeof(DragItemData).ToString());
                    int srcIndex = -1;
                    if ((data.table == null) || (data.DragItems.Count == 0))
                    {
                        _isStartDrag = false;
                        _isMouseDown = false;
                        _selectedRow = -1;
                        return;
                    }

                    if (data.table != null)
                    {
                        if (data.table.SelectedIndicies.GetLength(0) > 0)
                        {
                            if (data.table.AllowDrop && ((data.table == _table) || (data.table.ExternalDropRemovesRows)))
                                data.table.TableModel.Rows.Remove(data.table.SelectedItems[0]);
                            _isMouseDown = false;
                            _isStartDrag = false;
                            if (data.table == _table)
                                srcIndex = _selectedRow;
                            _selectedRow = -1;
                            _previousRow = null;
                        }
                    }

                    if (hoverItem == null)
                    {
                        for (int i = 0; i < data.DragItems.Count; i++)
                        {
                            Row newItem = (Row)data.DragItems[i];
                            _table.TableModel.Rows.Add(newItem);
                            _table.DragDropRowInsertedAt(_table.TableModel.Rows.Count - 1);
                        }
                    }
                    else
                    {
                        for (int i = data.DragItems.Count - 1; i >= 0; i--)
                        {
                            Row newItem = (Row)data.DragItems[i];

                            if (nRow < 0)
                            {
                                _table.TableModel.Rows.Add(newItem);
                                if (srcIndex < 0)
                                    _table.DragDropRowInsertedAt(_table.TableModel.Rows.Count - 1);
                                else
                                    _table.DragDropRowMoved(srcIndex, _table.TableModel.Rows.Count - 1);
                            }
                            else
                            {
                                _table.TableModel.Rows.Insert(nRow, newItem);
                                if (srcIndex < 0)
                                    _table.DragDropRowInsertedAt(nRow);
                                else
                                    _table.DragDropRowMoved(srcIndex, nRow);
                            }
                        }
                    }
                }
                else
                    _table.DragDropExternalType(sender, drgevent);

                if (_previousRow != null)
                    _previousRow = null;

                _table.Invalidate();

                _isStartDrag = false;
                _isMouseDown = false;
                _selectedRow = -1;
            }
            else
                _table.DragDropExternalType(sender, drgevent);
        }

        void table_DragOver(object sender, DragEventArgs drgevent)
        {
            //if (!drgevent.Data.GetDataPresent(typeof(DragItemData).ToString()))
            //{
            //    //drgevent.Effect = DragDropEffects.None;
            //    return;
            //}

            if (_table.UseBuiltInDragDrop && _table.TableModel.Rows.Count > 0)
            {
                Point point = _table.PointToClient(new Point(drgevent.X, drgevent.Y));
                int nRow = -1;
                if (point.Y <= _table.HeaderHeight)
                {
                    nRow = _table.TopIndex - 1;
                    if (nRow < 0)
                        nRow = 0;
                }
                else
                    nRow = _table.RowIndexAt(point);

                Row hoverItem = _table.TableModel.Rows[nRow];

                Graphics g = _table.CreateGraphics();

                if (hoverItem == null)
                {
                    if (_previousRow != null)
                    {
                        _previousRow = null;
                        _table.Invalidate();
                    }
                    return;
                }

                if ((_previousRow != null && _previousRow != hoverItem) || _previousRow == null)
                {
                    _table.Invalidate();
                }

                _previousRow = hoverItem;

                if (drgevent.Data.GetDataPresent(typeof(DragItemData).ToString()) && _selectedRow == nRow && (((DragItemData)drgevent.Data.GetData(typeof(DragItemData).ToString())).table == _table))
                {
                    drgevent.Effect = DragDropEffects.None;
                }
                else
                {
                    if (drgevent.Data.GetDataPresent(typeof(DragItemData).ToString()))
                    {
                        DragItemData data = (DragItemData)drgevent.Data.GetData(typeof(DragItemData).ToString());
                        if (!data.table.ExternalDropRemovesRows && (data.table != _table))
                            drgevent.Effect = DragDropEffects.Copy;
                        else
                            drgevent.Effect = DragDropEffects.Move;
                    }
                    else
                        if (!drgevent.Data.GetDataPresent(typeof(DragItemData).ToString()))
                            drgevent.Effect = _table.DragDropExternalTypeEffectSelector(sender, drgevent);

                    if (drgevent.Effect != DragDropEffects.None)
                        _renderer.PaintDragDrop(g, hoverItem, _table.RowRect(nRow));
                }
                _table.EnsureVisible(nRow, 0);
            }
        }

        void table_DragEnter(object sender, DragEventArgs drgevent)
        {
            if (_table.UseBuiltInDragDrop)
            {
                if (!drgevent.Data.GetDataPresent(typeof(DragItemData).ToString()))
                    drgevent.Effect = _table.DragDropExternalTypeEffectSelector(sender, drgevent);
                else
                {
                    DragItemData data = (DragItemData)drgevent.Data.GetData(typeof(DragItemData).ToString());
                    if (!data.table.ExternalDropRemovesRows && (data.table != _table))
                        drgevent.Effect = DragDropEffects.Copy;
                    else
                        drgevent.Effect = DragDropEffects.Move;
                }

                _isStartDrag = true;
            }
            else
                drgevent.Effect = _table.DragDropExternalTypeEffectSelector(sender, drgevent);
        }
        #endregion

        #region Drag drop helpers
        private DragItemData GetDataForDragDrop(int nRow)
        {
            DragItemData data = new DragItemData(_table);
            Row rowData = new Row();
            rowData = _table.TableModel.Rows[nRow];
            data.DragItems.Add(rowData);

            return data;
        }

        private class DragItemData
        {
            private Table m_Table;
            private ArrayList m_DragItems;

            public Table table
            {
                get { return m_Table; }
            }

            public ArrayList DragItems
            {
                get { return m_DragItems; }
            }

            public DragItemData(Table table)
            {
                m_Table = table;
                m_DragItems = new ArrayList();
            }
        }
        #endregion

        void Reset()
        {
            _isMouseDown = false;
            _isStartDrag = false;
            _selectedRow = -1;
        }

        #region Mouse events
        /// <summary>
        /// Called by the MouseDown event, if drag drop is enabled and the left
        /// button is pressed.
        /// </summary>
        /// <param name="selectedRow"></param>
        internal void MouseDown(int selectedRow)
        {
            _selectedRow = selectedRow;
            _isMouseDown = true;
        }

        /// <summary>
        /// Called by the MouseMove event (if the left button is pressed).
        /// </summary>
        /// <param name="e"></param>
        internal void MouseMove(MouseEventArgs e)
        {
            // Drag & Drop Code Added - by tankun
            if (_table.UseBuiltInDragDrop && (this._isStartDrag == false) && (this._isMouseDown == true))
            {
                int row = _table.RowIndexAt(e.X, e.Y);
                _table.DoDragDrop(GetDataForDragDrop(row), DragDropEffects.All);
            }
        }

        /// <summary>
        /// Called by the MouseUp event for the left mouse button.
        /// </summary>
        internal void MouseUp()
        {
            Reset();
        }
        #endregion
    }

    #region Delegates

    /// <summary>
    /// Represents the method that will handle selecting the correct DragDropEffects value
    /// for an external data type.
    /// </summary>
    public delegate DragDropEffects DragDropExternalTypeEffectsHandler(object sender, DragEventArgs drgevent);

    /// <summary>
    /// Represents the method that will handle Drop functionality when the data is an external type.
    /// </summary>
    public delegate void DragDropExternalTypeEventHandler(object sender, DragEventArgs drgevent);

    /// <summary>
    /// Represents the method that will supply the index of the new row following a
    /// successful DragDrop operation.
    /// </summary>
    public delegate void DragDropRowInsertedAtEventHandler(int destIndex);

    /// <summary>
    /// Represents the method that will supply the source and destination index 
    /// when a row is moved following a successful DragDrop operation.
    /// </summary>
    public delegate void DragDropRowMovedEventHandler(int srcIndex, int destIndex);

    #endregion
}
