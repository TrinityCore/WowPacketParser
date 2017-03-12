using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildMemberRecipes
    {
        public ulong Member;
        public int SkillRank;
        public int SkillLineID;
        public int SkillStep;
        public fixed byte SkillLineBitArray[300];
    }
}
