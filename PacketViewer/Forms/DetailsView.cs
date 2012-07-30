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
        Row _row;
        public DetailsView(Row row)
        {
            InitializeComponent();
            _row = row;
            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.ResizeRedraw |
            ControlStyles.OptimizedDoubleBuffer, true);
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
                _row.Height = this.Height;
            }
            else if (IsOnGrip(e.Location)) this.Cursor = Cursors.SizeNS;
            else this.Cursor = Cursors.Default;
            base.OnMouseMove(e);
        }
    }
}
