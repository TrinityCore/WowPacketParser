using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientChallengeModeMember
    {
        public ulong Guid;
        public uint VirtualRealmAddress;
        public uint NativeRealmAddress;
        public int SpecializationID;
    }
}
