using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_1_5_29495
{
    public class SpellFlatModByLabel : ISpellFlatModByLabel
    {
        public int ModIndex { get; set; }
        public int ModifierValue { get; set; }
        public int LabelID { get; set; }
    }
}

