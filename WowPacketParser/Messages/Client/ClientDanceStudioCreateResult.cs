namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientDanceStudioCreateResult
    {
        public bool Enable;
        public fixed int Secrets[4];
    }
}
