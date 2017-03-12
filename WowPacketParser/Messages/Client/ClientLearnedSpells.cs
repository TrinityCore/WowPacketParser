using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLearnedSpells
    {
        public List<int> SpellID;
        public bool SuppressMessaging;
    }
}
