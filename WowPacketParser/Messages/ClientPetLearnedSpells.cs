using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPetLearnedSpells
    {
        public List<int> SpellID;
    }
}
