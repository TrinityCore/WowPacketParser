namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSpellMultistrikeEffect
    {
        public ulong Target;
        public ulong Caster;
        public int SpellID;
        public short ProcCount;
        public short ProcNum;
    }
}
