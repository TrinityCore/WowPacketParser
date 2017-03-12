namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBFMgrEntering
    {
        public bool Relocated;
        public bool ClearedAFK;
        public bool OnOffense;
        public ulong QueueID;
    }
}
