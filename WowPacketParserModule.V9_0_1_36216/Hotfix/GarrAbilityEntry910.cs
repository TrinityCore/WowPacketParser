using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_1_0_39185.Hotfix
{
    [HotfixStructure(DB2Hash.GarrAbility, ClientVersionBuild.V9_1_0_39185, HasIndexInData = false)]
    public class GarrAbilityEntry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte GarrAbilityCategoryID { get; set; }
        public byte GarrFollowerTypeID { get; set; }
        public int IconFileDataID { get; set; }
        public ushort FactionChangeGarrAbilityID { get; set; }
        public ushort Flags { get; set; }
    }
}
