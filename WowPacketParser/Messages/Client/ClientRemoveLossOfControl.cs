namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientRemoveLossOfControl
    {
        public int SpellID;
        public ulong Caster;
        public int Type;
    }
}
