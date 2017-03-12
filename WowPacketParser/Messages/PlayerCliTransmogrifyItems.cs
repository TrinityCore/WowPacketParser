using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliTransmogrifyItems
    {
        public ulong Npc;
        public List<TransmogrifyItem> Items;
    }
}
