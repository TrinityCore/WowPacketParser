namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBattlefieldStatus_WaitForGroups
    {
        public uint Timeout;
        public ClientBattlefieldStatus_Header Hdr;
        public uint Mapid;
        public fixed byte TotalPlayers[2];
        public fixed byte AwaitingPlayers[2];
    }
}
