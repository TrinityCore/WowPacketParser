namespace WowPacketParser.Enums
{
    public enum ReportPvPAFKResult : byte
    {
        Success         = 0,
        GenericFailure  = 1, // there are more error codes but they are impossible to receive without modifying the client
        SystemEnabled   = 5,
        SystemDisabled  = 6
    };
}
