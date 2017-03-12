using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSetMeleeAnimKit
    {
        public ulong Unit;
        public ushort AnimKitID;
    }
}
