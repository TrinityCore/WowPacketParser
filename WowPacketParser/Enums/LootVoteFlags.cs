using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum LootVoteFlags
    {
        Pass         = 0x01,
        Need         = 0x02,
        Greed        = 0x04,
        Disenchant   = 0x08,
    }
}
