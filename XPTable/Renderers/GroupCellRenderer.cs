using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

using XPTable.Events;
using XPTable.Models;
using XPTable.Themes;

namespace XPTable.Renderers
{
	/// <summary>
	/// A CellRenderer that draws Cell contents as a collapse/expand icon.
	/// </summary>
	public class GroupCellRenderer : CellRenderer
	{

		#region Class Data
		
		/// <summary>
		/// The size of the checkbox
		/// </summary>
		private Size checkSize;

		/// <summary>
		/// Specifies whether any text contained in the Cell should be drawn
		/// </summary>
		private bool drawText;

		/// <summary>
		/// Specifies the colour of the box and connecting lines
		/// </summary>
		private Color lineColor;

		/// <summary>
		/// Used to draw the box and connecting lines
		/// </summary>
		private Pen lineColorPen;

		/// <summary>
		/// Determies whether the collapse/expand is performed on the Click event. If false then Double Click toggles the state.
		/// </summary>
		private bool toggleOnSingleClick = false;

		#endregion


		#region Constructor
		/// <summary>
		/// Initializes a new instance of the GroupCellRenderer class with 
		/// default settings
		/// </summary>
		public GroupCellRenderer()
		{
			this.checkSize = new Size(13, 13);
			this.lineColor = Color.LightBlue;
		}
		#endregion


		#region Methods
		/// <summary>
		/// Gets the Rectangle that specifies the Size and Location of 
		/// the check box contained in the current Cell
		/// </summary>
		/// <returns>A Rectangle that specifies the Size and Location of 
		/// the check box contained in the current Cell</returns>
		protected Rectangle CalcCheckRect(RowAlignment rowAlignment, ColumnAlignment columnAlignment)
		{
			Rectangle checkRect = new Rectangle(this.ClientRectangle.Location, this.checkSize);
			
			if (checkRect.Height > this.ClientRectangle.Height)
			{
				checkRect.Height = this.ClientRectangle.Height;
				checkRect.Width = checkRect.Height;
			}

			switch (rowAlignment)
			{
				case RowAlignment.Center:
				{
					checkRect.Y += (this.ClientRectangle.Height - checkRect.Height) / 2;

					break;
				}

				case RowAlignment.Bottom:
				{
					checkRect.Y = this.ClientRectangle.Bottom - checkRect.Height;

					break;
				}
			}

			if (!this.drawText)
			{
				if (columnAlignment == ColumnAlignment.Center)
				{
					checkRect.X += (this.ClientRectangle.Width - checkRect.Width) / 2;
				}
				else if (columnAlignment == ColumnAlignment.Right)
				{
					checkRect.X = this.ClientRectangle.Right - checkRect.Width;
				}
			}

			return checkRect;
		}

		/// <summary>
		/// Gets the GroupRendererData specific data used by the Renderer from 
		/// the specified Cell
		/// </summary>
		/// <param name="cell">The Cell to get the GroupRendererData data for</param>
		/// <returns>The GroupRendererData data for the specified Cell</returns>
		protected GroupRendererData GetGroupRendererData(Cell cell)
		{
			object rendererData = this.GetRendererData(cell);

			if (rendererData == null || !(rendererData is GroupRendererData))
			{
				rendererData = new GroupRendererData();

				this.SetRendererData(cell, rendererData);
			}

			return (GroupRendererData) rendererData;
		}

		/// <summary>
		/// Returns true if this cell is in a sub row.
		/// </summary>
		/// <param name="cell"></param>
		/// <returns></returns>
		private bool IsSubRow(Cell cell)
		{
			return cell.Row.Parent != null;
		}

        /// <summary>
        /// Returns true if this cell is in the last subrow.
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private bool IsLastRow(Cell cell)
        {
            if (cell.Row.Parent != null)
            {
                Row parent = cell.Row.Parent;
                if (parent.SubRows.IndexOf(cell.Row) == parent.SubRows.Count-1)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        #endregion


		#region Properties

		/// <summary>
		/// Gets or sets whether the collapse/expand is performed on the Click event. If false then Double Click toggles the state.
		/// </summary>
		public bool ToggleOnSingleClick
		{
			get { return toggleOnSingleClick; }
			set { toggleOnSingleClick = value; }
		}

		/// <summary>
		/// Specifies the colour of the box and connecting lines.
		/// </summary>
		public Color LineColor
		{
			get { return lineColor; }
			set { lineColor = value; }
		}

		/// <summary>
		/// The Pen to use to draw the box and connecting lines.
		/// </summary>
		private Pen LineColorPen
		{
			get 
			{
				if (this.lineColorPen == null)
					this.lineColorPen = new Pen(this.lineColor);
				return this.lineColorPen;
			}
		}

		#endregion


		#region Events

		/// <summary>
		/// Fires the DoubleClick event.
		/// </summary>
		/// <param name="e"></param>
		public override void OnDoubleClick(CellMouseEventArgs e)
		{
			base.OnDoubleClick (e);

			if (!this.toggleOnSingleClick)
				ToggleState(e);
		}

		/// <summary>
		/// Fires the Click event.
		/// </summary>
		/// <param name="e"></param>
		public override void OnClick(CellMouseEventArgs e)
		{
			base.OnClick (e);

			if (this.toggleOnSingleClick)
				ToggleState(e);
		}

		private void ToggleState(CellMouseEventArgs e)
		{
			GroupRendererData data = this.GetGroupRendererData(e.Cell);

			// Toggle the group state
			data.Grouped = !data.Grouped;

			Row r = e.Table.TableModel.Rows[e.Row];
			r.ExpandSubRows = !r.ExpandSubRows;
		}

		#endregion


		#region Paint

		/// <summary>
		/// Raises the OnPaintCell event
		/// </summary>
		/// <param name="e"></param>
		public override void OnPaintCell(PaintCellEventArgs e)
		{
			if (e.Table.ColumnModel.Columns[e.Column] is GroupColumn)
			{
				GroupColumn column = (GroupColumn) e.Table.ColumnModel.Columns[e.Column];

				this.drawText = column.DrawText;
				this.lineColor = column.LineColor;
				this.toggleOnSingleClick = column.ToggleOnSingleClick;
			}
			else
			{
				this.drawText = false;
			}

			base.OnPaintCell (e);
		}


		private void DrawBox(Graphics g, Pen p, Rectangle rect)
		{
			int x = (int)Math.Floor(rect.X + (double)rect.Width/2);
			int y = (int)Math.Floor(rect.Y + (double)rect.Height/2);
			g.DrawRectangle(p, x - 4, y - 4, 8, 8);
		}

		private void DrawMinus(Graphics g, Pen p, Rectangle rect)
		{
			int y = (int)Math.Floor(rect.Y + (double)rect.Height/2);
			int center = (int)Math.Floor(rect.X + (double)rect.Width/2);

			g.DrawLine(p, center + 2, y, center - 2, y);
		}

		private void DrawCross(Graphics g, Pen p, Rectangle rect)
		{
			DrawMinus(g, p, rect);

			int x = (int)Math.Floor(rect.X + (double)rect.Width/2);
			int middle = (int)Math.Floor(rect.Y + (double)rect.Height/2);

			g.DrawLine(p, x, middle + 2, x, middle - 2);
        }

        #region Style 1
        /// <summary>
        /// Draws a line on the RHS
        /// </summary>
        /// <param name="g"></param>
        /// <param name="p"></param>
        /// <param name="rect"></param>
		private void DrawLine1(Graphics g, Pen p, Rectangle rect)
		{
			int halfwidth = (int)Math.Floor(this.Bounds.Width * 0.75);
			int x = this.Bounds.X + halfwidth;

			g.DrawLine(p, x, this.Bounds.Top, x, this.Bounds.Bottom);
		}

        /// <summary>
        /// Draws a line on the RHS and joins it up to the RHS of the box
        /// </summary>
        /// <param name="g"></param>
        /// <param name="p"></param>
        /// <param name="rect"></param>
		private void DrawHalfLine1(Graphics g, Pen p, Rectangle rect)
		{
			int halfwidth = (int)Math.Floor(this.Bounds.Width * 0.75);
			int x = this.Bounds.X + halfwidth;
			int top = (int)Math.Floor(this.Bounds.Top + (double)this.Bounds.Height / 2);

			g.DrawLine(p, x, top, x, this.Bounds.Bottom);

			// and connect it to the box
			int x2 = (int)Math.Floor(rect.X + (double)rect.Width/2);
			g.DrawLine(p, x, top, x2 + 4, top);
        }
        #endregion

        #region Style 2
        /// <summary>
        /// Draws a line down the middle
        /// </summary>
        /// <param name="g"></param>
        /// <param name="p"></param>
        /// <param name="rect"></param>
        private void DrawLine2(Graphics g, Pen p, Rectangle rect)
        {
            int halfwidth = (int)Math.Floor(this.Bounds.Width * 0.75);
            int x = (int)Math.Floor(rect.X + (double)rect.Width / 2);

            g.DrawLine(p, x, this.Bounds.Top, x, this.Bounds.Bottom);
        }

        private void DrawEndLine2(Graphics g, Pen p, Rectangle rect)
        {
            int halfwidth = (int)Math.Floor(this.Bounds.Width * 0.75);
            int x1 = (int)Math.Floor(rect.X + (double)rect.Width / 2);

            int bottom = (int)Math.Floor(this.Bounds.Y + (double)this.Bounds.Height / 2);
            g.DrawLine(p, x1, this.Bounds.Top, x1, bottom);

            int x2 = 4 + (int)Math.Floor(rect.X + (double)rect.Width / 2);

            g.DrawLine(p, x1, bottom, x2, bottom);
        }


        /// <summary>
        /// Draw a line down the middle, up to the bottom of the box.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="p"></param>
        /// <param name="rect"></param>
        private void DrawHalfLine2(Graphics g, Pen p, Rectangle rect)
        {
            int halfwidth = (int)Math.Floor(this.Bounds.Width * 0.75);
            int x = (int)Math.Floor(rect.X + (double)rect.Width / 2);
            int top = 4 + (int)Math.Floor(rect.Y + (double)rect.Height / 2);

            g.DrawLine(p, x, top, x, this.Bounds.Bottom);

            // and connect it to the box
            int x2 = (int)Math.Floor(rect.X + (double)rect.Width / 2);
            g.DrawLine(p, x, top, x2 + 4, top);
        }
        #endregion

		/// <summary>
		/// Raises the Paint event
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintCellEventArgs e)
		{
			base.OnPaint (e);

			// don't bother if the Cell is null
			if (e.Cell == null)
			{
				return;
			}

			Rectangle checkRect = this.CalcCheckRect(this.LineAlignment, this.Alignment);

            if (!this.IsSubRow(e.Cell))
            {
                // Draw nothing if this row has no child rows
                if (e.Cell.Row.SubRows.Count > 0)
                {
                    // This is a parent row - draw a + or - in a box
                    GroupRendererData data = this.GetGroupRendererData(e.Cell);

                    DrawBox(e.Graphics, this.LineColorPen, checkRect);

                    if (data.Grouped)
                    {
                        DrawCross(e.Graphics, Pens.Gray, checkRect);
                    }
                    else
                    {
                        DrawMinus(e.Graphics, Pens.Gray, checkRect);
                        DrawHalfLine2(e.Graphics, this.LineColorPen, checkRect);
                    }
                }
            }
            else
            {
                // This is a subrow so either draw the end-line or the normal line
                if (this.IsLastRow(e.Cell))
                    DrawEndLine2(e.Graphics, this.LineColorPen, checkRect);
                else
                    DrawLine2(e.Graphics, this.LineColorPen, checkRect);
            }

			#region Draw text
			if (this.drawText)
			{
				string text = e.Cell.Text;

				if (text != null && text.Length != 0)
				{
					Rectangle textRect = this.ClientRectangle;
					textRect.X += checkRect.Width + 1;
					textRect.Width -= checkRect.Width + 1;

					if (e.Enabled)
					{
						e.Graphics.DrawString(e.Cell.Text, this.Font, this.ForeBrush, textRect, this.StringFormat);
					}
					else
					{
						e.Graphics.DrawString(e.Cell.Text, this.Font, this.GrayTextBrush, textRect, this.StringFormat);
					}
				}
			}
			#endregion

		}

		#endregion
	}
}
