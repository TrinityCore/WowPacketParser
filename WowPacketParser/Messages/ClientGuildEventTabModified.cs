using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildEventTabModified
    {
        public string Icon;
        public int Tab;
        public string Name;
    }
}
