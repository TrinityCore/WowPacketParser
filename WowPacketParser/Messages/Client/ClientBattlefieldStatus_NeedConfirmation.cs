namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBattlefieldStatus_NeedConfirmation
    {
        public uint Timeout;
        public uint Mapid;
        public ClientBattlefieldStatus_Header Hdr;
        public byte Role;
    }
}
