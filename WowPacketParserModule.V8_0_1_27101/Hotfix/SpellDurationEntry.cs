using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellDuration, HasIndexInData = false)]
    public class SpellDurationEntry
    {
        public int Duration { get; set; }
        public uint DurationPerLevel { get; set; }
        public int MaxDuration { get; set; }
    }
}
