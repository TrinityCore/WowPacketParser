using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_1_5_29683.Hotfix
{
    [HotfixStructure(DB2Hash.TaxiNodes, ClientVersionBuild.V8_1_5_29683)]
    public class TaxiNodesEntry
    {
        public string Name { get; set; }
        [HotfixArray(3, true)]
        public float[] Pos { get; set; }
        [HotfixArray(2, true)]
        public float[] MapOffset { get; set; }
        [HotfixArray(2, true)]
        public float[] FlightMapOffset { get; set; }
        public uint ID { get; set; }
        public ushort ContinentID { get; set; }
        public int ConditionID { get; set; }
        public ushort CharacterBitNumber { get; set; }
        public byte Flags { get; set; }
        public int UiTextureKitID { get; set; }
        public int MinimapAtlasMemberID { get; set; }
        public float Facing { get; set; }
        public uint SpecialIconConditionID { get; set; }
        public uint VisibilityConditionID { get; set; }
        [HotfixArray(2)]
        public int[] MountCreatureID { get; set; }
    }
}
