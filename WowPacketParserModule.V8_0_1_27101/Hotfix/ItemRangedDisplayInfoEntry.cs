using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemRangedDisplayInfo, HasIndexInData = false)]
    public class ItemRangedDisplayInfoEntry
    {
        public uint CastSpellVisualID { get; set; }
        public uint AutoAttackSpellVisualID { get; set; }
        public uint QuiverFileDataID { get; set; }
        public uint MissileSpellVisualEffectNameID { get; set; }
    }
}
