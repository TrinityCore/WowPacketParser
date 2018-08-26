namespace WowPacketParser.Enums
{
    public enum ReportPvPAFKResult : byte
    {
        PVP_REPORT_AFK_SUCCESS          = 0,
        PVP_REPORT_AFK_GENERIC_FAILURE  = 1, // there are more error codes but they are impossible to receive without modifying the client
        PVP_REPORT_AFK_SYSTEM_ENABLED   = 5,
        PVP_REPORT_AFK_SYSTEM_DISABLED  = 6
    };
}
