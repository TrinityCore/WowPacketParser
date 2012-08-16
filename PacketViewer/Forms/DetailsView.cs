using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XPTable.Models;

namespace PacketViewer.Forms
{
    public partial class DetailsView : UserControl
    {
        Row _row = null;
        bool first = true;
        public DetailsView(Row row)
        {
            _row = row;
            InitializeComponent();
            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.ResizeRedraw |
            ControlStyles.OptimizedDoubleBuffer, true);
            this.MinimumSize = new Size(0, cGripSize);
        }

        private const int cGripSize = 2;
        private bool mDragging;
        private Point mDragPos;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        private bool IsOnGrip(Point pos)
        {
            return pos.Y >= this.ClientSize.Height - cGripSize;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            mDragging = IsOnGrip(e.Location);
            mDragPos = e.Location;
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            mDragging = false;
            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (mDragging)
            {
                var nsize = new Size(this.Width,
                  this.Height + e.Y - mDragPos.Y);
                if (nsize.Height < cGripSize)
                {
                    nsize.Height = cGripSize;
                }
                this.Size = nsize;
                mDragPos = e.Location;
                
            }
            else if (IsOnGrip(e.Location)) this.Cursor = Cursors.SizeNS;
            else this.Cursor = Cursors.Default;
            base.OnMouseMove(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (_row != null)
                _row.Height = this.Height;
            panelMain.Height = this.Height - cGripSize;
            base.OnSizeChanged(e);
        }

        public void AddView(Control control)
        {
            if (!first)
            {
                var s = new Splitter();
                s.Dock = DockStyle.Left;
                this.panelMain.Controls.Add(s);
                control.Dock = DockStyle.Left;
            }
            else
            {
                control.Dock = DockStyle.Fill;
            }
            first = false;
            
            this.panelMain.Controls.Add(control);
        }
    }
}
