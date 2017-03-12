using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientResyncRunes
    {
        public List<ResyncRune> Runes;
    }
}
