using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliQueryBattlePetName
    {
        public ulong BattlePetID;
        public ulong UnitGUID;
    }
}
