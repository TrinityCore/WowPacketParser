using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.ChrUpgradeBucketSpell)]
    public class ChrUpgradeBucketSpellEntry
    {
        public uint ID { get; set; }
        public uint BucketID { get; set; }
        public uint SpellID { get; set; }
    }
}