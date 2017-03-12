namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliPetSetAction
    {
        public ulong PetGUID;
        public uint Action;
        public uint Index;
    }
}
