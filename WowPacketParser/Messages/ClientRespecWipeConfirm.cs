using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientRespecWipeConfirm
    {
        public ulong RespecMaster;
        public uint Cost;
        public sbyte RespecType;
    }
}
