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

        public override System.Windows.Forms.Control GetControl(Cell cell)
        {
            if (cell.Row.Index % 2 == 0)
                return null;
            DetailsView cont = new DetailsView();

            var entry = Tab.dataManager.Get(cell.Row.Index - 1);
            cont.textBox1.Text = entry.ParsedPacket;
            cont.Height = cell.Row.Height;
            return cont;
        }
    }
}
