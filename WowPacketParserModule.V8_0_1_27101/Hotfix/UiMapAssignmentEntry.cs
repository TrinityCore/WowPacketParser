using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UiMapAssignment)]
    public class UiMapAssignmentEntry
    {
        [HotfixArray(2)]
        public float[] UiMin { get; set; }
        [HotfixArray(2)]
        public float[] UiMax { get; set; }
        [HotfixArray(6)]
        public float[] Region { get; set; }
        public int ID { get; set; }
        public int UiMapID { get; set; }
        public int OrderIndex { get; set; }
        public int MapID { get; set; }
        public int AreaID { get; set; }
        public int WmoDoodadPlacementID { get; set; }
        public int WmoGroupID { get; set; }
    }
}
