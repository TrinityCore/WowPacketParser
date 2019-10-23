using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_2_5_31921
{
    public class SpellFlatModByLabel : ISpellFlatModByLabel
    {
        public int ModIndex { get; set; }
        public int ModifierValue { get; set; }
        public int LabelID { get; set; }
    }
}

