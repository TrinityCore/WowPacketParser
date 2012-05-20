using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowPacketParser.Misc;
using System.IO;

namespace WowPacketParser.Processing
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
