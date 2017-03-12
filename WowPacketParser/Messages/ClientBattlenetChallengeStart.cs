using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBattlenetChallengeStart
    {
        public uint Token;
        public string ChallengeURL;
    }
}
