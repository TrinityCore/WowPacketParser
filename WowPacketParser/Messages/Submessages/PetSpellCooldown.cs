namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct PetSpellCooldown
    {
        public int SpellID;
        public int Duration;
        public int CategoryDuration;
        public ushort Category;
    }
}
