using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientReadyCheckCompleted
    {
        public ulong PartyGUID;
        public byte PartyIndex;
    }
}
