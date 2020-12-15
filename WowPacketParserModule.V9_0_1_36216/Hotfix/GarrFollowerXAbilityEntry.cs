using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.GarrFollowerXAbility, HasIndexInData = false)]
    public class GarrFollowerXAbilityEntry
    {
        public byte OrderIndex { get; set; }
        public byte FactionIndex { get; set; }
        public ushort GarrAbilityID { get; set; }
        public ushort GarrFollowerID { get; set; }
    }
}
