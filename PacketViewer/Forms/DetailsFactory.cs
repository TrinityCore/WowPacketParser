using System.Windows.Forms;

using XPTable.Models;

namespace PacketViewer.Forms
{
    public class DetailsFactory : ControlFactory
    {
        public DetailsFactory()
        {
        }

        public override System.Windows.Forms.Control GetControl(Cell cell)
        {
            if (cell.Data == null)
                return null;
            TextBox box = new TextBox();
            box.Text = (string)cell.Data;
            return box;
        }
    }
}
