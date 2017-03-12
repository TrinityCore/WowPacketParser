namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientSetCurrencyCheat
    {
        public ulong TargetGUID;
        public int Type;
        public int Quantity;
        public sbyte IsDelta;
        public sbyte Column;
    }
}
