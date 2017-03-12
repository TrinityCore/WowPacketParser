using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientPetLearnedSpells
    {
        public List<int> SpellID;
    }
}
