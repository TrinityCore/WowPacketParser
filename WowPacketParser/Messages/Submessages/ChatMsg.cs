namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct ChatMsg
    {
        public byte SlashCmd;
        public sbyte Language;
        public ulong SenderGUID;
        public ulong SenderGuildGUID;
        public ulong TargetGUID;
        public ulong PartyGUID;
        public uint SenderVirtualAddress;
        public uint TargetVirtualAddress;
        public string SenderName;
        public string TargetName;
        public string Prefix;
        public string Channel;
        public string ChatText;
        public int AchievementID;
        public ushort ChatFlags;
        public float DisplayTime;
        public bool HideChatLog;
        public bool FakeSenderName;
    }
}
