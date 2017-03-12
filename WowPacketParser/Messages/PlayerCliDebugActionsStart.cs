using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliDebugActionsStart
    {
        public ulong Target;
        public int SpawnGroupID;
        public int CreatureID;
    }
}
