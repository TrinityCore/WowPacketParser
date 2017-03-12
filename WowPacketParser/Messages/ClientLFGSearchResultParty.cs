using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLFGSearchResultParty
    {
        public ulong Guid;
        public uint ChangeMask;
        public string Comment;
        public ulong InstanceID;
        public uint InstanceCompletedMask;
        public fixed byte Needs[3];
    }
}
