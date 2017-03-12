using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSupercededSpells
    {
        public List<int> SpellID;
        public List<int> Superceded;
    }
}
