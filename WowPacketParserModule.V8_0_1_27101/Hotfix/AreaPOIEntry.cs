using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AreaPOI)]
    public class AreaPOIEntry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ID { get; set; }
        [HotfixArray(3)]
        public float[] Pos { get; set; }
        public int PortLocID { get; set; }
        public uint PlayerConditionID { get; set; }
        public uint UiTextureAtlasMemberID { get; set; }
        public uint Flags { get; set; }
        public int WMOGroupID { get; set; }
        public int PoiDataType { get; set; }
        public int PoiData { get; set; }
        public ushort ContinentID { get; set; }
        public short AreaID { get; set; }
        public ushort WorldStateID { get; set; }
        public ushort UiWidgetParentSetID { get; set; }
        public byte Importance { get; set; }
        public byte Icon { get; set; }
    }
}
