namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSpellProcScriptLog
    {
        public ulong Caster;
        public int SpellID;
        public sbyte Result;
    }
}
