using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PacketParser.Misc;
using PacketParser.DataStructures;
using System.IO;

namespace PacketDumper.Processing.RawData
{
    public class KSZorBinaryPacketWriter : IBinaryPacketWriter
    {
        public void WritePacket(Packet packet, BinaryWriter writer)
        {
            writer.Write(packet.Opcode);
            writer.Write((int) packet.Length);
            writer.Write((int) Utilities.GetUnixTimeFromDateTime(packet.Time));
            writer.Write((byte) packet.Direction);
            writer.Write(packet.GetStream(0));
        }
        public void WriteHeader(BinaryWriter writer)
        { }
    }
}
