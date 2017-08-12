using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V5_3_0_16981.Hotfix
{
    [HotfixStructure(DB2Hash.Creature)]
    public class CreatureEntry
    {
        public int ID { get; set; }
        [HotfixArray(3)]
        public int[] Item { get; set; }
        [HotfixArray(2)]
        public int[] Projectile { get; set; }
        public uint Mount { get; set; }
        [HotfixArray(4)]
        public int[] DisplayID { get; set; }
        [HotfixArray(4)]
        public float[] UnkType { get; set; }
        public string Name { get; set; }
        public int InhabitType { get; set; }
    }
}
