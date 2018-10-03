using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Trophy, HasIndexInData = false)]
    public class TrophyEntry
    {
        public string Name { get; set; }
        public byte TrophyTypeID { get; set; }
        public ushort GameObjectDisplayInfoID { get; set; }
        public uint PlayerConditionID { get; set; }
    }
}
