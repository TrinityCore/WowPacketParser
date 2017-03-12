namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMissileCancel
    {
        public ulong OwnerGUID;
        public bool Reverse;
        public int SpellID;
    }
}
