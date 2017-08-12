using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.ItemEffect)]
    public class ItemEffectEntry
    {
        public uint ID { get; set; }
        public int ItemID { get; set; }
        public int OrderIndex { get; set; }
        public int SpellID { get; set; }
        public int Trigger { get; set; }
        public int Charges { get; set; }
        public int Cooldown { get; set; }
        public int CategoryCooldown { get; set; }
        [HotfixVersion(ClientVersionBuild.V6_2_0_20182, false)]
        public int ChrSpecializationID { get; set; }
    }
}
