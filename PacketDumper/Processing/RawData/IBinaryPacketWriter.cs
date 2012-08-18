using System;
using System.IO;
using PacketParser.DataStructures;
using PacketDumper.Misc;

namespace PacketDumper.Processing.RawData
{
    public interface IBinaryPacketWriter
    {
        void WritePacket(Packet packet, BinaryWriter writer);
        void WriteHeader(BinaryWriter writer);
    }

    public static class BinaryPacketWriter
    {
        public static IBinaryPacketWriter Get()
        {
            switch (Settings.RawOutputType)
            {
                case "kszor":
                    return new KSZorBinaryPacketWriter();
                default:
                    throw new Exception("Unsuported binary packet writer type");
            }
        }
    }
}
