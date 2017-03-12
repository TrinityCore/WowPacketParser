namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientPetAdded
    {
        public string Name;
        public int CreatureID;
        public int Level;
        public uint PetNumber;
        public int DisplayID;
        public byte Flags;
        public int PetSlot;
    }
}
