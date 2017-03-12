namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientPetCastFailed
    {
        public int Reason;
        public int FailedArg2;
        public int FailedArg1;
        public int SpellID;
        public byte CastID;
    }
}
