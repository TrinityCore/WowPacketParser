namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGameEventDebugLog
    {
        public ulong TriggeredBy;
        public int EventID;
        public int EventType;
        public string Reason;
        public string TriggeredByName;
        public string EventName;
    }
}
