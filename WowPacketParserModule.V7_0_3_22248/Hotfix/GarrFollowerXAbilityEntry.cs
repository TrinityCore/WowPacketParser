using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.GarrFollowerXAbility, HasIndexInData = false)]
    public class GarrFollowerXAbilityEntry
    {
        public ushort GarrFollowerID { get; set; }
        public ushort GarrAbilityID { get; set; }
        public byte FactionIndex { get; set; }
    }
}