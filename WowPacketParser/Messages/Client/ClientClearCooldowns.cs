using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientClearCooldowns
    {
        public List<int> SpellID;
        public ulong Guid;
    }
}
