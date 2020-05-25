using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AzeriteItemMilestonePower, ClientVersionBuild.V8_0_1_27101, ClientVersionBuild.V8_2_0_30898, HasIndexInData = false)]
    public class AzeriteItemMilestonePowerEntry
    {
        public byte RequiredLevel { get; set; }
        public short AzeritePowerID { get; set; }
    }
}
