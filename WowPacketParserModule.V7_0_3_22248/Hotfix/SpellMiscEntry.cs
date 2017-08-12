using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SpellMisc, ClientVersionBuild.V7_0_3_22248, ClientVersionBuild.V7_2_0_23826, HasIndexInData = false)]
    public class SpellMiscEntry
    {
        public uint Attributes { get; set; }
        public uint AttributesEx { get; set; }
        public uint AttributesExB { get; set; }
        public uint AttributesExC { get; set; }
        public uint AttributesExD { get; set; }
        public uint AttributesExE { get; set; }
        public uint AttributesExF { get; set; }
        public uint AttributesExG { get; set; }
        public uint AttributesExH { get; set; }
        public uint AttributesExI { get; set; }
        public uint AttributesExJ { get; set; }
        public uint AttributesExK { get; set; }
        public uint AttributesExL { get; set; }
        public uint AttributesExM { get; set; }
        public float Speed { get; set; }
        public float MultistrikeSpeedMod { get; set; }
        public ushort CastingTimeIndex { get; set; }
        public ushort DurationIndex { get; set; }
        public ushort RangeIndex { get; set; }
        public ushort SpellIconID { get; set; }
        public ushort ActiveIconID { get; set; }
        public byte SchoolMask { get; set; }
    }
}