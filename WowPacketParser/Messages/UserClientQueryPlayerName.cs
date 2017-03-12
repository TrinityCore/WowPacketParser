using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientQueryPlayerName
    {
        public ulong Player;
        public PlayerGuidLookupHint Hint;
    }
}
