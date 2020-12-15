using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ImportPriceWeapon, HasIndexInData = false)]
    public class ImportPriceWeaponEntry
    {
        public float Data { get; set; }
    }
}
