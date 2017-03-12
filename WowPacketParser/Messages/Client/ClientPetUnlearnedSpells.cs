using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientPetUnlearnedSpells
    {
        public List<int> SpellID;
    }
}
