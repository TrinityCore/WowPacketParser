using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V9_0_1_36216.UpdateFields.V9_0_1_36216
{
    public class PassiveSpellHistory : IPassiveSpellHistory
    {
        public int SpellID { get; set; }
        public int AuraSpellID { get; set; }
    }
}

