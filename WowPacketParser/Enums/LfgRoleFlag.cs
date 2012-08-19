using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum LfgRoleFlag
    {
        None   = 0x00,
        Leader = 0x01,
        Tank   = 0x02,
        Healer = 0x04,
        Damage = 0x08,
        Unk10  = 0x10,
        Unk20  = 0x20,
        Unk40  = 0x40
    }
}
