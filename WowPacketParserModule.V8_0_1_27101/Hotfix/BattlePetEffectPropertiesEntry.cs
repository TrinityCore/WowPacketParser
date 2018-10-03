using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.BattlePetEffectProperties, HasIndexInData = false)]
    public class BattlePetEffectPropertiesEntry
    {
        [HotfixArray(6)]
        public string[] ParamLabel { get; set; }
        public ushort BattlePetVisualID { get; set; }
        [HotfixArray(6)]
        public byte[] ParamTypeEnum { get; set; }
    }
}
