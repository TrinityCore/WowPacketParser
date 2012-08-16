using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PacketParser.Processing;
using PacketViewer.Forms;
using XPTable.Models;

namespace PacketViewer.Processing
{
    interface IDetailsViewPacketProcessor : IPacketProcessor
    {
        void GetDetailsViewControl(int packetUID, DetailsView detailsView, Cell cell);
    }

    public delegate void GetDetailsViewControlHandler(int packetUID, DetailsView detailsView, Cell cell);
}
