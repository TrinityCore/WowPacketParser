using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientClearCooldowns
    {
        public List<int> SpellID;
        public ulong Guid;
    }
}
