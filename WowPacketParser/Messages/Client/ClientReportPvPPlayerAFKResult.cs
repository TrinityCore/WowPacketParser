namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientReportPvPPlayerAFKResult
    {
        public ulong Offender;
        public byte NumPlayersIHaveReported;
        public byte NumBlackMarksOnOffender;
        public byte Result;
    }
}
