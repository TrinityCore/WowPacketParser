namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGameObjectPlaySpellVisual
    {
        public ulong ObjectGUID;
        public ulong ActivatorGUID;
        public int SpellVisualID;
    }
}
