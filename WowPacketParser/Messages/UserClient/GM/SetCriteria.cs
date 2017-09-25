namespace WowPacketParser.Messages.UserClient.GM
{
    public unsafe struct SetCriteria
    {
        public int CriteriaID;
        public string Target;
        public int Quantity;
        public ulong Guid;
    }
}
