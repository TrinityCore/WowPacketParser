using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_3_0_33062
{
    public class SpellPctModByLabel : ISpellPctModByLabel
    {
        public int ModIndex { get; set; }
        public float ModifierValue { get; set; }
        public int LabelID { get; set; }
    }
}

