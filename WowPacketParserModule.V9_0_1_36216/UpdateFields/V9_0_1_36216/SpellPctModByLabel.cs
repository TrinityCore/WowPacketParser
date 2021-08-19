using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V9_0_1_36216.UpdateFields.V9_0_1_36216
{
    public class SpellPctModByLabel : ISpellPctModByLabel
    {
        public int ModIndex { get; set; }
        public float ModifierValue { get; set; }
        public int LabelID { get; set; }
    }
}

