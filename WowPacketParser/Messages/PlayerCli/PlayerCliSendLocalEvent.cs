namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliSendLocalEvent
    {
        public ulong TargetGUID;
        public int EventID;
    }
}
