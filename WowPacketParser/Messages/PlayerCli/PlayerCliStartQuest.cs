namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliStartQuest
    {
        public bool AbandonExisting;
        public int QuestID;
        public bool AutoAccept;
    }
}
