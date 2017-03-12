using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
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
