namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct PetBattleFinalPet
    {
        public bool Captured;
        public bool Caged;
        public bool AwardedXP;
        public bool SeenAction;
        public ushort Level;
        public ushort Xp;
        public int Health;
        public int MaxHealth;
        public ushort InitialLevel;
        public byte Pboid;
    }
}
