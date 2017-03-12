using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildPartyState
    {
        public float GuildXPEarnedMult;
        public int NumMembers;
        public bool InGuildParty;
        public int NumRequired;
    }
}
