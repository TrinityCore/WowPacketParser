using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum GameObjectDynamicFlag // 4.x
    {
        ConditionalUseOk      = 0x01,
        LoAnimate             = 0x02,
        DisableUse            = 0x04,
        PassiveHighlight      = 0x08,
        WaitAtCurrentLocation = 0x10,
        BlockUse              = 0x20,
        MoveBackwards         = 0x40
    }
}
