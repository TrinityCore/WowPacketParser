using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ObjectEffectPackageElem, HasIndexInData = false)]
    public class ObjectEffectPackageElemEntry
    {
        public ushort ObjectEffectPackageID { get; set; }
        public ushort ObjectEffectGroupID { get; set; }
        public ushort StateType { get; set; }
    }
}
