using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.AreaTrigger)]
    public class AreaTriggerEntry
    {
        [HotfixArray(3)]
        public float[] Pos { get; set; }
        public float Radius { get; set; }
        public float BoxLength { get; set; }
        public float BoxWidth { get; set; }
        public float BoxHeight { get; set; }
        public float BoxYaw { get; set; }
        public ushort MapID { get; set; }
        public ushort PhaseID { get; set; }
        public ushort PhaseGroupID { get; set; }
        public ushort ShapeID { get; set; }
        public ushort AreaTriggerActionSetID { get; set; }
        public byte PhaseUseFlags { get; set; }
        public byte ShapeType { get; set; }
        public byte Flag { get; set; }
        public uint ID { get; set; }
    }
}