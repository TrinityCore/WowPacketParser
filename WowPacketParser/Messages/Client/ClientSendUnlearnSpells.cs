using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSendUnlearnSpells
    {
        public List<uint> Spells;
    }
}
