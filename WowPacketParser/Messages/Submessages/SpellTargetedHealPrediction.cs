namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct SpellTargetedHealPrediction
    {
        public ulong TargetGUID;
        public SpellHealPrediction Predict;
    }
}
