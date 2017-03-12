using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct PetBattleLocations
    {
        public int LocationResult;
        public Vector3 BattleOrigin;
        public float BattleFacing;
        public Vector3[/*2*/] PlayerPositions;
    }
}
