namespace WowPacketParser.Messages.Global
{
    public unsafe struct GlobalGuildNewsUpdateSticky
    {
        public int NewsID;
        public ulong GuildGUID;
        public bool Sticky;
    }
}
