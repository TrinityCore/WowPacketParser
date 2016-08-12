using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.WorldSafeLocs, HasIndexInData = false)]
    public class WorldSafeLocsEntry
    {
        [HotfixArray(3)]
        public float[] Loc { get; set; }
        public float Facing { get; set; }
        public string AreaName { get; set; }
        public ushort MapID { get; set; }
    }
}