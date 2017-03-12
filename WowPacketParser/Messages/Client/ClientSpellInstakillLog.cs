namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSpellInstakillLog
    {
        public ulong Target;
        public ulong Caster;
        public int SpellID;
    }
}
