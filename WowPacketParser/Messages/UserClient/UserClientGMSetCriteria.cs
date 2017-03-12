namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientGMSetCriteria
    {
        public int CriteriaID;
        public string Target;
        public int Quantity;
        public ulong Guid;
    }
}
