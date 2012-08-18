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

        private Point controlOffset;

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

            controlRect.Offset(this.controlOffset);

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

            if (rendererData == null)
            {
                rendererData = new ControlRendererData(null);
                this.SetRendererData(cell, rendererData);
            }
            if (rendererData == null)
                throw new Exception("asd");
            return rendererData as ControlRendererData;
        }

        protected void InitControl(Cell cell)
        {
            var renderData = GetControlRendererData(cell);
            if (renderData.Control != null)
                return;
            if (this.ControlFactory != null)
            {
                // Never shown a control, so ask what we should do
                Control control = this.ControlFactory.CreateControl(cell);
                renderData.ChangeControl(cell, control);
            }
        }

        internal static void RemoveControlRenderData(Cell cell)
        {
            if (cell.RendererData == null)
                return;
            var controlData = ((ControlRendererData)cell.RendererData);
            controlData.RemoveControl(cell);
            cell.RendererData = null;
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

				
                this.controlFactory = column.ControlFactory;
                this.controlOffset = column.ControlOffset;
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
            ControlRendererData controlData = GetControlRendererData(e.Cell);

			if (controlData.Control != null)
			{
                this.controlSize = controlData.Control.Size;
                Rectangle controlRect = this.CalcControlRect(this.LineAlignment, this.Alignment);

				controlData.Control.Location = controlRect.Location;
                
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

        public override void OnRowBecameVisible(Cell cell)
        {
            ControlRendererData controlData = GetControlRendererData(cell);
            InitControl(cell);
            if (controlData.Control != null)
            {
                Rectangle controlRect = this.CalcControlRect(this.LineAlignment, this.Alignment);
                controlData.Control.Location = controlRect.Location;
                controlData.AddVisual(cell);
            }
        }

        public override void OnRowBecameInvisible(Cell cell)
        {
            ControlRendererData controlData = GetControlRendererData(cell);
            if (controlData.Control != null)
            {
                if (ControlFactory != null && ControlFactory.RemoveControlWhenInvisible == true)
                    controlData.RemoveControl(cell);
                else
                    controlData.RemoveVisual(cell);
            }
        }
		#endregion
	}
}
