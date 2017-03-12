namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientPlayObjectSound
    {
        public ulong TargetObjectGUID;
        public ulong SourceObjectGUID;
        public int SoundKitID;
    }
}
