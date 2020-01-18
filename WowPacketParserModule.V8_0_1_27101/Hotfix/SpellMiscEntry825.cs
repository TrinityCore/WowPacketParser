using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_2_5_31921.Hotfix
{
    [HotfixStructure(DB2Hash.SpellMisc, ClientVersionBuild.V8_2_5_31921, HasIndexInData = false)]
    public class SpellMiscEntry
    {
        [HotfixArray(14)]
        public int[] Attributes { get; set; }
        public byte DifficultyID { get; set; }
        public ushort CastingTimeIndex { get; set; }
        public ushort DurationIndex { get; set; }
        public ushort RangeIndex { get; set; }
        public byte SchoolMask { get; set; }
        public float Speed { get; set; }
        public float LaunchDelay { get; set; }
        public float MinDuration { get; set; }
        public int SpellIconFileDataID { get; set; }
        public int ActiveIconFileDataID { get; set; }
        public int ContentTuningID { get; set; }
        public int SpellID { get; set; }
    }
}
