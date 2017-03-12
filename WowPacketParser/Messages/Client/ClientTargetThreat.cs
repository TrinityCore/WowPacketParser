namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientTargetThreat
    {
        public ulong TargetGUID;
        public uint Threat;
    }
}
