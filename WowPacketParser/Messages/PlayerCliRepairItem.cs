using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliRepairItem
    {
        public ulong NpcGUID;
        public bool UseGuildBank;
        public ulong ItemGUID;
    }
}
