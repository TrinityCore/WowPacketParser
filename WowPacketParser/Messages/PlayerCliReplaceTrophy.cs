using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliReplaceTrophy
    {
        public ulong TrophyGUID;
        public int NewTrophyID;
    }
}
