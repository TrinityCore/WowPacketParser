namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientChatAutoResponded
    {
        public bool IsDND;
        public uint RealmAddress;
        public string AfkMessage;
    }
}
