namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGameObjectCustomAnim
    {
        public ulong ObjectGUID;
        public uint CustomAnim;
        public bool PlayAsDespawn;
    }
}
