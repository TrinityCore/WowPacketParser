namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientDestructibleBuildingDamage
    {
        public ulong Target;
        public ulong Caster;
        public ulong Owner;
        public int Damage;
        public int SpellID;
    }
}
