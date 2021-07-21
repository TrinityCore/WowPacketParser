using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.GarrAbility, ClientVersionBuild.V9_0_1_36216, ClientVersionBuild.V9_1_0_39185)]
    public class GarrAbilityEntry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public uint ID { get; set; }
        public byte GarrAbilityCategoryID { get; set; }
        public byte GarrFollowerTypeID { get; set; }
        public int IconFileDataID { get; set; }
        public ushort FactionChangeGarrAbilityID { get; set; }
        public ushort Flags { get; set; }
    }
}
