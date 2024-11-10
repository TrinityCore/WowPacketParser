using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum UpdateTypeFlag
    {
        None = 0x0000,
        Object = 0x0001,
        Item = 0x0002,
        Container = 0x0004,
        AzeriteEmpoweredItem = 0x0008,
        AzeriteItem = 0x0010,
        Unit = 0x0020,
        Player = 0x0040,
        ActivePlayer = 0x0080,
        GameObject = 0x0100,
        DynamicObject = 0x0200,
        Corpse = 0x0400,
        AreaTrigger = 0x0800,
        SceneObject = 0x1000,
        Conversation = 0x2000,
    }

}
