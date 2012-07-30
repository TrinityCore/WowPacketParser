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
        public abstract Control CreateControl(Cell cell);

        /// <summary>
        /// Changes the behaviour when row becomes invisible - remove control when true, hide control when false
        /// </summary>
        public virtual bool RemoveControlWhenInvisible { get { return false; } }
        
        #endregion
    }
}
