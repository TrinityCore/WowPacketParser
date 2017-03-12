using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientShowTradeSkill
    {
        public ulong PlayerGUID;
        public int SkillLineID;
        public int SpellID;
    }
}
