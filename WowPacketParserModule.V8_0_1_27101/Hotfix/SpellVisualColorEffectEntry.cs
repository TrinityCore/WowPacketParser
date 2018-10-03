using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellVisualColorEffect, HasIndexInData = false)]
    public class SpellVisualColorEffectEntry
    {
        public float Duration { get; set; }
        public int Color { get; set; }
        public byte Flags { get; set; }
        public byte Type { get; set; }
        public ushort RedCurveID { get; set; }
        public ushort GreenCurveID { get; set; }
        public ushort BlueCurveID { get; set; }
        public ushort AlphaCurveID { get; set; }
        public ushort OpacityCurveID { get; set; }
        public float ColorMultiplier { get; set; }
        public uint PositionerID { get; set; }
    }
}
