using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Cfg_Configs, HasIndexInData = false)]
    public class Cfg_ConfigsEntry
    {
        public byte PlayerKillingAllowed { get; set; }
        public byte Roleplaying { get; set; }
        public ushort PlayerAttackSpeedBase { get; set; }
        public float MaxDamageReductionPctPhysical { get; set; }
    }
}
