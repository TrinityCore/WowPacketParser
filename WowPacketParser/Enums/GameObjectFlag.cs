using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum GameObjectFlag
    {
        None = 0x00000000,
        InUse = 0x00000001,
        Locked = 0x00000002,
        InteractionCondition = 0x00000004,
        Transport = 0x00000008,
        Unknown1 = 0x00000010,
        NeverDespawn = 0x00000020,
        Triggered = 0x00000040,
        Unknown2 = 0x00000080,
        Unknown3 = 0x00000100,
        Damaged = 0x00000200,
        Destroyed = 0x00000400
    }
}
