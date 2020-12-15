using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ItemBonusListLevelDelta)]
    public class ItemBonusListLevelDeltaEntry
    {
        public short ItemLevelDelta { get; set; }
        public uint ID { get; set; }
    }
}
