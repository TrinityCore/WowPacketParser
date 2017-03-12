using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientUnlearnedSpells
    {
        public List<int> SpellID;
    }
}
