namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCastFailed
    {
        public int FailedArg2;
        public int Reason;
        public int FailedArg1;
        public int SpellID;
        public byte CastID;
    }
}
