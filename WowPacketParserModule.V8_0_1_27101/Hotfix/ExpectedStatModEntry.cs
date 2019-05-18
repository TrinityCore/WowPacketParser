using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ExpectedStatMod, HasIndexInData = false)]
    public class ExpectedStatModEntry
    {
        public float CreatureHealthMod { get; set; }
        public float PlayerHealthMod { get; set; }
        public float CreatureAutoAttackDPSMod { get; set; }
        public float CreatureArmorMod { get; set; }
        public float PlayerManaMod { get; set; }
        public float PlayerPrimaryStatMod { get; set; }
        public float PlayerSecondaryStatMod { get; set; }
        public float ArmorConstantMod { get; set; }
        public float CreatureSpellDamageMod { get; set; }
    }
}
