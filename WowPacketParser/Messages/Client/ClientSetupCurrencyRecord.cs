namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSetupCurrencyRecord
    {
        public int Type;
        public int Quantity;
        public int? WeeklyQuantity; // Optional
        public int? MaxWeeklyQuantity; // Optional
        public int? TrackedQuantity; // Optional
        public byte Flags;
    }
}
