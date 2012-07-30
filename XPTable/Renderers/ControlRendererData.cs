using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using XPTable.Themes;
using XPTable.Models;

namespace XPTable.Renderers
{
	/// <summary>
	/// Contains the control shown in a control column.
	/// </summary>
	public class ControlRendererData
    {
        #region Class Data
        private Control control;
        private bool hasVisual = false;
        #endregion

        #region Contstructor
        /// <summary>
        /// Creates a ControlRendererData with the given control.
        /// </summary>
        /// <param name="cellControl"></param>
        public ControlRendererData(Control cellControl)
		{
            this.control = cellControl;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the control for the cell.
        /// </summary>
        public Control Control
        {
            get { return this.control; }
            set { this.control = value; }
        }

        public void RemoveVisual(Cell cell)
        {
            if (Control != null && hasVisual)
            {
                hasVisual = false;
                Control.Visible = false;
                cell.Row.TableModel.Table.Controls.Remove(Control);
            }
        }

        public void AddVisual(Cell cell)
        {
            if (Control != null && !hasVisual)
            {
                hasVisual = true;
                cell.Row.TableModel.Table.Controls.Add(Control);
                Control.Visible = true;
            }
        }

        public void ChangeControl(Cell cell, Control control)
        {
            if (hasVisual)
            {
                RemoveVisual(cell);
                var oldControl = this.Control;
                this.Control = control;
                if (oldControl != null)
                    oldControl.Dispose();
                AddVisual(cell);
            }
            else
            {
                var oldControl = this.Control;
                this.Control = control;
                if (oldControl != null)
                    oldControl.Dispose();
            }
        }

        public void RemoveControl(Cell cell)
        {
            if (this.Control != null)
            {
                RemoveVisual(cell);
                var oldControl = this.Control;
                oldControl.Dispose();
                this.Control = null;
            }
        }

        #endregion
    }
}
