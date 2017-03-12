using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct SkillLineData
    {
        public int SpellID;
        public List<int> SkillLineIDs;
        public List<int> SkillRanks;
        public List<int> SkillMaxRanks;
        public List<int> KnownAbilitySpellIDs;
    }
}
