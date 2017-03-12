using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliActivateTaxi
    {
        public ulong Vendor;
        public uint StartNode;
        public uint DestNode;
    }
}
