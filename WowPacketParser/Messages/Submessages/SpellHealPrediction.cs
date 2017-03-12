namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct SpellHealPrediction
    {
        public ulong BeaconGUID;
        public int Points;
        public byte Type;
    }
}
