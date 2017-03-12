namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliQueryServerBuckData
    {
        public bool AllClusters;
        public byte ClusterID;
        public uint RequestID;
        public byte Mpid;
    }
}
