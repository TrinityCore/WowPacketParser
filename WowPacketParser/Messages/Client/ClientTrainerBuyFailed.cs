namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientTrainerBuyFailed
    {
        public ulong TrainerGUID;
        public int TrainerFailedReason;
        public int SpellID;
    }
}
