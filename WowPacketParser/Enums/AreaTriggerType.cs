namespace WowPacketParser.Enums
{
    public enum AreaTriggerType : byte
    {
        Sphere          = 0,
        Box             = 1,
        Quad2D          = 2,
        Polygon         = 3,
        Cylinder        = 4,
        Script          = 5,
        FromUnit        = 6,
        Disk            = 7,
        BoundedPlane    = 8,
    }
}
