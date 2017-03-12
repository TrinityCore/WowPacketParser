using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSendUnlearnSpells
    {
        public List<uint> Spells;
    }
}
