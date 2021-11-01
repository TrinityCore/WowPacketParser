using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.MawPower)]
    public class MawPowerEntry
    {
        public uint ID { get; set; }
        public int SpellID { get; set; }
        public int MawPowerRarityID { get; set; }
    }
}
