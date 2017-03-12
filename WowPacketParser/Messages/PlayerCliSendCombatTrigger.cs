using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSendCombatTrigger
    {
        public ulong TargetGUID;
        public int EventID;
    }
}
