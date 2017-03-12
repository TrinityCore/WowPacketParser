using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliTransmogrifyItems
    {
        public ulong Npc;
        public List<TransmogrifyItem> Items;
    }
}
