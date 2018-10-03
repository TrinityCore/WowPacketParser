using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.WeaponTrailModelDef, HasIndexInData = false)]
    public class WeaponTrailModelDefEntry
    {
        public int LowDefFileDataID { get; set; }
        public ushort WeaponTrailID { get; set; }
        public ushort AnimEnumID { get; set; }
    }
}
