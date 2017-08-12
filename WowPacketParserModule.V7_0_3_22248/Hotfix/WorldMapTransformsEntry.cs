using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.WorldMapTransforms, HasIndexInData = false)]
    public class WorldMapTransformsEntry
    {
        [HotfixArray(3)]
        public float[] RegionMin { get; set; }
        [HotfixArray(3)]
        public float[] RegionMax { get; set; }
        [HotfixArray(2)]
        public float[] RegionOffset { get; set; }
        public float RegionScale { get; set; }
        public ushort MapID { get; set; }
        public ushort AreaID { get; set; }
        public ushort NewMapID { get; set; }
        public ushort NewDungeonMapID { get; set; }
        public ushort NewAreaID { get; set; }
        public byte Flags { get; set; }
    }
}