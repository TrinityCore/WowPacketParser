namespace WowPacketParser.Enums
{
    public enum LfgRoleCheckStatus
    {
        None        = 0,
        Success     = 1,
        Initiating  = 2,
        MissingRole = 3,
        NoInstance  = 4,
        Aborted     = 5,
        NoRole      = 6
    }
}
