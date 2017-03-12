using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSupercededSpells
    {
        public List<int> SpellID;
        public List<int> Superceded;
    }
}
