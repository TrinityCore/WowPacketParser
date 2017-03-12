namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliSetPetSlot
    {
        public ulong StableMaster;
        public uint PetNumber;
        public byte DestSlot;
    }
}
