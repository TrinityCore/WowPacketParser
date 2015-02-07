using System;

namespace WowPacketParser.Enums
{
    [Flags]
    enum UpdateFlag
    {
        None              = 0x000,
        Self              = 0x001,
        Transport         = 0x002,
        AttackingTarget   = 0x004,
        Unknown1          = 0x008,
        LowGuid           = 0x010,
        Living            = 0x020,
        StationaryObject  = 0x040,
        Vehicle           = 0x080,
        GOPosition        = 0x100,
        GORotation        = 0x200,
        Unknown2          = 0x400,
        AnimKits          = 0x800, // 4.x
        TransportUnkArray = 0x1000, // 4.x
        EnablePortals     = 0x2000, // 4.x
        Unknown           = 0x4000  // 4.x
    }
}
