using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliPetBattleWildLocationFail
    {
        public ulong TargetGUID;
        public Vector3 PlayerPos;
    }
}
