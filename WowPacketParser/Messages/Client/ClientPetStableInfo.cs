namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientPetStableInfo
    {
        public uint PetSlot;
        public uint PetNumber;
        public uint CreatureID;
        public uint DisplayID;
        public uint ExperienceLevel;
        public string PetName;
        public byte PetFlags;
    }
}
