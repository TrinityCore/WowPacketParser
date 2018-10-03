using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellActivationOverlay, HasIndexInData = false)]
    public class SpellActivationOverlayEntry
    {
        [HotfixArray(4)]
        public int[] IconHighlightSpellClassMask { get; set; }
        public int SpellID { get; set; }
        public int OverlayFileDataID { get; set; }
        public sbyte ScreenLocationID { get; set; }
        public uint SoundEntriesID { get; set; }
        public int Color { get; set; }
        public float Scale { get; set; }
        public sbyte TriggerType { get; set; }
    }
}
