using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPetUnlearnedSpells
    {
        public List<int> SpellID;
    }
}
