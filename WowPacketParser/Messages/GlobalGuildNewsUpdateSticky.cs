using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GlobalGuildNewsUpdateSticky
    {
        public int NewsID;
        public ulong GuildGUID;
        public bool Sticky;
    }
}
