using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GuildKnownRecipes
    {
        public int SkillLineID;
        public fixed byte SkillLineBitArray[300];
    }
}
