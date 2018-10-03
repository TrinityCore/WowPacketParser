using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Creature, HasIndexInData = false)]
    public class CreatureEntry
    {
        public string Name { get; set; }
        public string NameAlt { get; set; }
        public string Title { get; set; }
        public string TitleAlt { get; set; }
        public sbyte Classification { get; set; }
        public byte CreatureType { get; set; }
        public ushort CreatureFamily { get; set; }
        public byte StartAnimState { get; set; }
        [HotfixArray(4)]
        public int[] DisplayID { get; set; }
        [HotfixArray(4)]
        public float[] DisplayProbability { get; set; }
        [HotfixArray(3)]
        public int[] AlwaysItem { get; set; }
    }
}
