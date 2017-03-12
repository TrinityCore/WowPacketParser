using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PetBattleLocations
    {
        public int LocationResult;
        public Vector3 BattleOrigin;
        public float BattleFacing;
        public Vector3[/*2*/] PlayerPositions;
    }
}
