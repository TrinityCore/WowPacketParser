using WowPacketParser.Misc;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliPetAction
    {
        public ulong PetGUID;
        public ulong TargetGUID;
        public Vector3 ActionPosition;
        public uint Action;
    }
}
