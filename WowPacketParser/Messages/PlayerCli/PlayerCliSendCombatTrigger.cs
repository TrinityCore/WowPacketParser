namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliSendCombatTrigger
    {
        public ulong TargetGUID;
        public int EventID;
    }
}
