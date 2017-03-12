using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliPartyLFGInfo
    {
        public byte MyLfgFlags;
        public uint LfgSlot;
        public uint MyLfgRandomSlot;
        public bool LfgAborted;
        public byte MyLfgPartialClear;
        public float MyLfgGearDiff;
        public byte MyLfgStrangerCount;
        public byte MyLfgKickVoteCount;
        public byte LfgBootCount;
        public bool MyLfgFirstReward;
    }
}
