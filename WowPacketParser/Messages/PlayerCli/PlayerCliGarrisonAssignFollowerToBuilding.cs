namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliGarrisonAssignFollowerToBuilding
    {
        public ulong NpcGUID;
        public ulong FollowerDBID;
        public int PlotInstanceID;
    }
}
