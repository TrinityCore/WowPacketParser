using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSendKnownSpells
    {
        public bool InitialLogin;
        public List<uint> KnownSpells;
    }
}
