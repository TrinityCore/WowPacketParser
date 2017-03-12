using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientPartyInviteResponse
    {
        public byte PartyIndex;
        public bool Accept;
        public uint RolesDesired; // Optional
    }
}
