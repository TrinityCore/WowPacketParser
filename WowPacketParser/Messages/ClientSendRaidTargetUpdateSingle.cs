using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSendRaidTargetUpdateSingle
    {
        public ulong ChangedBy;
        public ulong Target;
        public byte PartyIndex;
        public byte Symbol;
    }
}
