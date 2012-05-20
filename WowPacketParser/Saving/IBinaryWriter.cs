using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowPacketParser.Misc;
using System.IO;

namespace WowPacketParser.Saving
{
    public interface IBinaryWriter
    {
        void WritePacket(Packet packet, BinaryWriter writer);
        void WriteHeader(BinaryWriter writer);
    }
}
