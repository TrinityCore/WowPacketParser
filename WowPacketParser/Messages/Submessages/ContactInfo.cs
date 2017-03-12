namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct ContactInfo
    {
        public ulong Guid;
        public uint VirtualRealmAddr;
        public uint NativeRealmAddr;
        public uint TypeFlags;
        public string Notes;
        public byte Status;
        public uint AreaID;
        public uint Level;
        public uint ClassID;
    }
}
