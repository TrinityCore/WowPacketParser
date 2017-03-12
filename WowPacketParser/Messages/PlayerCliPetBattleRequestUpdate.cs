using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliPetBattleRequestUpdate
    {
        public bool Canceled;
        public ulong TargetGUID;
    }
}
