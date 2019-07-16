using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_1_5_29495
{
    public class PassiveSpellHistory : IPassiveSpellHistory
    {
        public int SpellID { get; set; }
        public int AuraSpellID { get; set; }
    }
}

