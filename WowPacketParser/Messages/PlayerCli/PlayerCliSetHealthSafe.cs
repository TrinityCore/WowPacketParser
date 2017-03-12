namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliSetHealthSafe
    {
        public ulong Target;
        public int ProcType;
        public int Health;
        public int ProcSubType;
    }
}
