using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientRatedBattlefieldInfo
    {
        public ClientBracketInfo[/*6*/] Bracket;
    }
}
