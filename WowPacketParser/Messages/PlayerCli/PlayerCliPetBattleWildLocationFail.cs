using WowPacketParser.Misc;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliPetBattleWildLocationFail
    {
        public ulong TargetGUID;
        public Vector3 PlayerPos;
    }
}
