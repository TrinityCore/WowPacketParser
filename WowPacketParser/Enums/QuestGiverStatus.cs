using System;

namespace WowPacketParser.Enums
{
    public enum QuestGiverStatus
    {
        None                 = 0,
        Unavailable          = 1,
        LowLevelAvailable    = 2,
        LowLevelRewardRep    = 3,
        LowLevelAvailableRep = 4,
        Incomplete           = 5,
        RewardRep            = 6,
        AvailableRep         = 7,
        Available            = 8,
        Reward2              = 9,
        Reward               = 10
    }

    [Flags]
    public enum QuestGiverStatus4x
    {
        None                 = 0x000,
        Unk                  = 0x001,
        Unavailable          = 0x002,
        LowLevelAvailable    = 0x004,
        LowLevelRewardRep    = 0x008,
        LowLevelAvailableRep = 0x010,
        Incomplete           = 0x020,
        RewardRep            = 0x040,
        AvailableRep         = 0x080,
        Available            = 0x100,
        Reward2              = 0x200,
        Reward               = 0x400
    }
}
