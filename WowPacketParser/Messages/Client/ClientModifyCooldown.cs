namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientModifyCooldown
    {
        public ulong UnitGUID;
        public int DeltaTime;
        public int SpellID;
    }
}
