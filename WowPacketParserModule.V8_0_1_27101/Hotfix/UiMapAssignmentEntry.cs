using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UiMapAssignment)]
    public class UiMapAssignmentEntry
    {
        [HotfixArray(2, true)]
        public float[] UiMin { get; set; }
        [HotfixArray(2, true)]
        public float[] UiMax { get; set; }
        [HotfixArray(3, true)]
        public float[] Region1 { get; set; }
        [HotfixArray(3, true)]
        public float[] Region2 { get; set; }
        public uint ID { get; set; }
        public int UiMapID { get; set; }
        public int OrderIndex { get; set; }
        public int MapID { get; set; }
        public int AreaID { get; set; }
        public int WmoDoodadPlacementID { get; set; }
        public int WmoGroupID { get; set; }
    }
}
