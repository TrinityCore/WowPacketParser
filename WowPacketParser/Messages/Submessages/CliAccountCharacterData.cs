namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliAccountCharacterData
    {
        public uint WowAccountID;
        public uint VirtualRealmAddress;
        public ulong Guid;
        public string Name;
        public string RealmName;
        public byte RaceID;
        public byte ClassID;
        public byte SexID;
        public byte ExperienceLevel;
    }
}
