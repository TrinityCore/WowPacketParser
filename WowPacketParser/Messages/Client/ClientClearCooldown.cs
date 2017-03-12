namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientClearCooldown
    {
        public bool ClearOnHold;
        public ulong CasterGUID;
        public int SpellID;
    }
}
