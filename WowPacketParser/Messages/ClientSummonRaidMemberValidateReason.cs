using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSummonRaidMemberValidateReason
    {
        public ulong Member;
        public int ReasonCode;
    }
}
