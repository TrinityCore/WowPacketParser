using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBattlePetOwnerInfo
    {
        public ulong Guid;
        public uint PlayerVirtualRealm;
        public uint PlayerNativeRealm;
    }
}
