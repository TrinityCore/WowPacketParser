using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading
{
    interface IDumpWriter : IDisposable
    {
        void WriteHeader(string headers);
        void WriteItem(Packet packet);

    }
}
