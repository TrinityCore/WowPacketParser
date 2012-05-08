namespace WowPacketParser.Enums
{
    public enum HighGuidType
    {
        None            = -1,
        Player          = 0x000, // Seen 0x280 for players too
        BattleGround1   = 0x101,
        InstanceSave    = 0x104,
        Group           = 0x105,
        BattleGround2   = 0x109,
        MOTransport     = 0x10C,
        Guild           = 0x10F,
        Item            = 0x400, // Container
        DynObject       = 0xF00, // Corpses
        GameObject      = 0xF01,
        Transport       = 0xF02,
        Unit            = 0xF03,
        Pet             = 0xF04,
        Vehicle         = 0xF05,
    }
}
