using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum QuestFlagsEx2 : uint
    {
        None           = 0x0,
        Unknown1       = 0x1,
        NoWarModeBonus = 0x2
    }
}
