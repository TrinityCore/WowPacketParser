namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientQuestPOIQuery
    {
        public int MissingQuestCount;
        public fixed int MissingQuestPOIs[50];
    }
}
