namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct AddonInfo
    {
        public byte Status;
        public bool InfoProvided;
        public bool KeyProvided;
        public bool UrlProvided;
        public byte KeyVersion;
        public uint Revision;
        public string Url;
        public fixed sbyte KeyData[256];
    }
}
