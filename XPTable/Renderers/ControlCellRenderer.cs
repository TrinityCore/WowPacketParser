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
	/// A CellRenderer that draws Cell contents as controls
	/// </summary>
	public class ControlCellRenderer : CellRenderer
	{
        #region Class Data

        /// <summary>
        /// Class that provides control instances for each cell.
        /// </summary>
        private ControlFactory controlFactory = null;

		/// <summary>
		/// The size of the checkbox
		/// </summary>
		private Size controlSize;

		#endregion
		
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the ControlCellRenderer class with 
		/// default settings
		/// </summary>
		public ControlCellRenderer() : base()
		{
			this.controlSize = new Size(13, 13);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets the Rectangle that specifies the Size and Location of 
		/// the control contained in the current Cell
		/// </summary>
		/// <returns>A Rectangle that specifies the Size and Location of 
		/// the control contained in the current Cell</returns>
		protected Rectangle CalcControlRect(RowAlignment rowAlignment, ColumnAlignment columnAlignment)
		{
			Rectangle controlRect = new Rectangle(this.ClientRectangle.Location, this.ControlSize);
			
			if (controlRect.Height > this.ClientRectangle.Height)
			{
				controlRect.Height = this.ClientRectangle.Height;
				controlRect.Width = controlRect.Height;
			}

			switch (rowAlignment)
			{
				case RowAlignment.Center:
				{
					controlRect.Y += (this.ClientRectangle.Height - controlRect.Height) / 2;

					break;
				}

				case RowAlignment.Bottom:
				{
					controlRect.Y = this.ClientRectangle.Bottom - controlRect.Height;

					break;
				}
			}

			if (columnAlignment == ColumnAlignment.Center)
			{
				controlRect.X += (this.ClientRectangle.Width - controlRect.Width) / 2;
			}
            else if (columnAlignment == ColumnAlignment.Right)
            {
                controlRect.X = this.ClientRectangle.Right - controlRect.Width;
            }

			return controlRect;
		}

		/// <summary>
		/// Gets the ControlRendererData specific data used by the Renderer from 
		/// the specified Cell
		/// </summary>
		/// <param name="cell"></param>
		/// <returns></returns>
		protected ControlRendererData GetControlRendererData(Cell cell)
		{
			object rendererData = this.GetRendererData(cell);

            if (this.ControlFactory != null)
            {
                if (rendererData == null || !(rendererData is ControlRendererData))
                {
                    // Never shown a control, so ask what we should do
                    Control control = this.ControlFactory.GetControl(cell);
                    if (control != null)
                    {
                        cell.Row.TableModel.Table.Controls.Add(control);
                        control.BringToFront();

                        ControlRendererData data = new ControlRendererData(control);
                        this.SetRendererData(cell, data);
                        rendererData = data;
                    }
                }
                else
                {
                    // Already got a control, but should we swap it for another
                    ControlRendererData data = (ControlRendererData)rendererData;
                    Control oldControl = data.Control;
                    // This next call allows the properties of the control to be updated, or to provide
                    // an entirely new control
                    Control newControl = this.ControlFactory.UpdateControl(cell, data.Control);
                    if (newControl != null)
                    {
                        // We need to take off the old control and wire up the new one
                        cell.Row.TableModel.Table.Controls.Remove(oldControl);
                        cell.Row.TableModel.Table.Controls.Add(newControl);
                        data.Control = newControl;
                        newControl.BringToFront();
                    }
                }
            }

            return (ControlRendererData)rendererData;
        }

		#endregion

		#region Properties

		/// <summary>
		/// Gets the size of the control
		/// </summary>
		protected Size ControlSize
		{
			get
			{
				return this.controlSize;
			}
		}

        /// <summary>
        /// Gets or sets the object that provides control instances for each cell.
        /// </summary>
        public ControlFactory ControlFactory
        {
            get { return this.controlFactory; }
            set { this.controlFactory = value; }
        }

		#endregion

		#region Paint

		/// <summary>
		/// Raises the PaintCell event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		public override void OnPaintCell(PaintCellEventArgs e)
		{
			if (e.Table.ColumnModel.Columns[e.Column] is ControlColumn)
			{
				ControlColumn column = (ControlColumn) e.Table.ColumnModel.Columns[e.Column];

				this.controlSize = column.ControlSize;
                this.controlFactory = column.ControlFactory;
			}
			else
			{
				this.controlSize = new Size(13, 13);
			}
			
			base.OnPaintCell(e);
		}


		/// <summary>
		/// Raises the Paint event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		protected override void OnPaint(PaintCellEventArgs e)
		{
			base.OnPaint(e);

			// don't bother if the Cell is null
			if (e.Cell == null)
			{
				return;
			}

			Rectangle controlRect = this.CalcControlRect(this.LineAlignment, this.Alignment);

			ControlRendererData controlData = this.GetControlRendererData(e.Cell);

			if (controlData != null && controlData.Control != null)
			{
				controlData.Control.Size = controlRect.Size;
				controlData.Control.Location = controlRect.Location;
				controlData.Control.BringToFront();

                if (e.Cell.WidthNotSet)
                    e.Cell.ContentWidth = controlRect.Size.Width;
			}
		}

        /// <summary>
        /// We don't want any background.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintCellEventArgs e)
        {
        }
		#endregion
	}
}
