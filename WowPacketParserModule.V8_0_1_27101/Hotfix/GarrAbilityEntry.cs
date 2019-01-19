using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrAbility)]
    public class GarrAbilityEntry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ID { get; set; }
        public byte GarrAbilityCategoryID { get; set; }
        public byte GarrFollowerTypeID { get; set; }
        public int IconFileDataID { get; set; }
        public ushort FactionChangeGarrAbilityID { get; set; }
        public ushort Flags { get; set; }
    }
}
