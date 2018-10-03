using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UiMapFogOfWar)]
    public class UiMapFogOfWarEntry
    {
        public int ID { get; set; }
        public int UiMapID { get; set; }
        public int PlayerConditionID { get; set; }
        public int UiMapFogOfWarVisID { get; set; }
    }
}
