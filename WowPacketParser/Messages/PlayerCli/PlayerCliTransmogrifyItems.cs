using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliTransmogrifyItems
    {
        public ulong Npc;
        public List<TransmogrifyItem> Items;
    }
}
