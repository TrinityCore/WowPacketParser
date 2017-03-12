using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBattlenetChallengeAbort
    {
        public uint Token;
        public bool Timeout;
    }
}
