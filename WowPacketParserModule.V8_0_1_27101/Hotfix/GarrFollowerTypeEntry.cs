using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrFollowerType, HasIndexInData = false)]
    public class GarrFollowerTypeEntry
    {
        public byte GarrTypeID { get; set; }
        public byte MaxFollowers { get; set; }
        public byte MaxFollowerBuildingType { get; set; }
        public ushort MaxItemLevel { get; set; }
        public byte LevelRangeBias { get; set; }
        public byte ItemLevelRangeBias { get; set; }
        public byte Flags { get; set; }
    }
}
