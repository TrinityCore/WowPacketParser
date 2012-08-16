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
            int packetUID = cell.Row.Parent == null ? (int)(cell.Row.Tag) : (int)cell.Row.Parent.Tag;
            DetailsView cont = new DetailsView(cell.Row);
            cont.Height = cell.Row.Height;
            Tab.PacketProcessor.GetDetailsViewControlHandler(packetUID, cont, cell);
            return cont;
        }

        public override bool RemoveControlWhenInvisible { get { return true; } }
    }
}
