using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum LfgRoleFlag
    {
        None = 0x0,
        Leader = 0x1,
        Tank = 0x2,
        Healer = 0x4,
        Damage = 0x8
    }
}
