namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSetSpellCharges
    {
        public bool IsPet;
        public float Count;
        public int Category;
    }
}
