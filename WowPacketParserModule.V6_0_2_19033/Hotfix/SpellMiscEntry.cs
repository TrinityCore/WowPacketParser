using System;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.SpellMisc)]
    public class SpellMiscEntry // New structure - 6.0.2
    {
        public uint ID { get; set; }
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
        public uint CastingTimeIndex { get; set; }
        public uint DurationIndex { get; set; }
        public uint RangeIndex { get; set; }
        public Single Speed { get; set; }
        [HotfixVersion(ClientVersionBuild.V6_2_0_20173, true)]
        public uint SpellXSpellVisualID { get; set; }
        public uint SpellIconID { get; set; }
        public uint ActiveIconID { get; set; }
        public uint SchoolMask { get; set; }
        public Single MultistrikeSpeedMod { get; set; }
    }
}