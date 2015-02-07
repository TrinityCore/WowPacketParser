namespace WowPacketParser.Enums
{
    public enum UpdateType
    {
        Values        = 0,
        Movement      = 1,
        CreateObject1 = 2,
        CreateObject2 = 3,
        FarObjects    = 4,
        NearObjects   = 5
    }

    public enum UpdateTypeCataclysm
    {
        Values         = 0,
        CreateObject1  = 1,
        CreateObject2  = 2,
        DestroyObjects = 3
    }
}
