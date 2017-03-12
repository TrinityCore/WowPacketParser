using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientUnlearnedSpells
    {
        public List<int> SpellID;
    }
}
