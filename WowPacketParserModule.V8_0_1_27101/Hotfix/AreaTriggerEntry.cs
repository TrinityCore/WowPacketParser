using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AreaTrigger)]
    public class AreaTriggerEntry
    {
        [HotfixArray(3, true)]
        public float[] Pos { get; set; }
        public uint ID { get; set; }
        public short ContinentID { get; set; }
        public sbyte PhaseUseFlags { get; set; }
        public short PhaseID { get; set; }
        public short PhaseGroupID { get; set; }
        public float Radius { get; set; }
        public float BoxLength { get; set; }
        public float BoxWidth { get; set; }
        public float BoxHeight { get; set; }
        public float BoxYaw { get; set; }
        public sbyte ShapeType { get; set; }
        public short ShapeID { get; set; }
        public short AreaTriggerActionSetID { get; set; }
        public sbyte Flags { get; set; }
    }
}
