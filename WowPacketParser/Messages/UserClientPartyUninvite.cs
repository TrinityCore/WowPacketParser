using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientPartyUninvite
    {
        public string Reason;
        public byte PartyIndex;
        public ulong TargetGuid;
    }
}
