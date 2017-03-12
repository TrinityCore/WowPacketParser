using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientOpponentSpecData
    {
        public ulong Guid;
        public int SpecializationID;
    }
}
