namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliBattlemasterJoin
    {
        public bool JoinAsGroup;
        public byte Roles;
        public ulong QueueID;
        public fixed int BlacklistMap[2];
    }
}
