using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ImportPriceWeapon, HasIndexInData = false)]
    public class ImportPriceWeaponEntry
    {
        public float Data { get; set; }
    }
}
