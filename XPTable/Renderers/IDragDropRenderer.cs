using System;
using System.Drawing;
using System.Text;

using XPTable.Models;

namespace XPTable.Renderers
{
    /// <summary>
    /// Draws a rectangle round the destination drag drop row.
    /// </summary>
    public interface IDragDropRenderer
    {
        /// <summary>
        /// Called when the given row is hovered during drag drop.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="row"></param>
        /// <param name="rowRect"></param>
        void PaintDragDrop(Graphics g, Row row, Rectangle rowRect);
    }
}
