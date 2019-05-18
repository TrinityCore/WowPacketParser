using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.TaxiNodes)]
    public class TaxiNodesEntry
    {
        public string Name { get; set; }
        [HotfixArray(3)]
        public float[] Pos { get; set; }
        [HotfixArray(2)]
        public float[] MapOffset { get; set; }
        [HotfixArray(2)]
        public float[] FlightMapOffset { get; set; }
        public int ID { get; set; }
        public ushort ContinentID { get; set; }
        public ushort ConditionID { get; set; }
        public ushort CharacterBitNumber { get; set; }
        public byte Flags { get; set; }
        public int UiTextureKitID { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_1_0_28724, false)]
        public int MinimapAtlasMemberID { get; set; }
        public float Facing { get; set; }
        public uint SpecialIconConditionID { get; set; }
        public uint VisibilityConditionID { get; set; }
        [HotfixArray(2)]
        public int[] MountCreatureID { get; set; }
    }
}
