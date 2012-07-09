using System;
using System.Text;
using System.Windows.Forms;

namespace XPTable.Models
{
    /// <summary>
    /// Abstract base class. Implement this and override GetControl to provide the control instance
    /// for each cell.
    /// </summary>
    public abstract class ControlFactory
    {
        #region Construcor
        /// <summary>
        /// Creates a ControlFactory with default values.
        /// </summary>
        public ControlFactory()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates the control to show for the given cell.
        /// Use Cell.Data to store any state object for the cell.
        /// </summary>
        /// <param name="cell">The cell that the control will be added to.</param>
        /// <returns></returns>
        public abstract Control GetControl(Cell cell);

        /// <summary>
        /// Allows an existing control to be modified or swapped for another control.
        /// Return null if the same control is left in place, or return a control if that is to replace the current one.
        /// </summary>
        /// <param name="cell">The cell that contains the control.</param>
        /// <param name="control">The current control in that cell.</param>
        /// <returns></returns>
        public virtual Control UpdateControl(Cell cell, Control control)
        {
            return null;
        }
        
        #endregion
    }
}
