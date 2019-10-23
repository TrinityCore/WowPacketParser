using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_2_5_31921
{
    public class PassiveSpellHistory : IPassiveSpellHistory
    {
        public int SpellID { get; set; }
        public int AuraSpellID { get; set; }
    }
}

