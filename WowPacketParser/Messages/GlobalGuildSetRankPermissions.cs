using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GlobalGuildSetRankPermissions
    {
        public uint OldFlags;
        public int RankID;
        public int RankOrder;
        public string RankName;
        public uint Flags;
        public uint WithdrawGoldLimit;
        public fixed uint TabFlags[8];
        public fixed uint TabWithdrawItemLimit[8];
    }
}
