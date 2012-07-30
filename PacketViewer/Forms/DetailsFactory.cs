using System.Windows.Forms;

using XPTable.Models;

namespace PacketViewer.Forms
{
    public class DetailsFactory : ControlFactory
    {
        public PacketFileTab Tab;
        public DetailsFactory(PacketFileTab tab)
        {
            Tab = tab;
        }

        public override System.Windows.Forms.Control CreateControl(Cell cell)
        {
            if (cell.Row.Index % 2 == 0)
                return null;
            DetailsView cont = new DetailsView(cell.Row);
            cont.Height = cell.Row.Height;
            return cont;
        }

        public override bool RemoveControlWhenInvisible { get { return true; } }
    }
}
