namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSuppressNPCGreetings
    {
        public bool SuppressNPCGreetings;
        public ulong UnitGUID;
    }
}
