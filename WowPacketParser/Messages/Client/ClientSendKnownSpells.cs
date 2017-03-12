using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSendKnownSpells
    {
        public bool InitialLogin;
        public List<uint> KnownSpells;
    }
}
