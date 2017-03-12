namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientVoiceSessionLeave
    {
        public ulong ClientGUID;
        public ulong SessionGUID;
    }
}
