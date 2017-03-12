namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliQuestPushResult
    {
        public ulong TargetGUID;
        public int QuestID;
        public byte Result;
    }
}
