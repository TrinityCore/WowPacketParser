namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliTargetCast
    {
        public int SpellID;
        public ulong CastTarget;
        public ulong TargetGUID;
        public bool CreatureAICast;
    }
}
