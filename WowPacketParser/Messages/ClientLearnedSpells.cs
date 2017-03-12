using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLearnedSpells
    {
        public List<int> SpellID;
        public bool SuppressMessaging;
    }
}
