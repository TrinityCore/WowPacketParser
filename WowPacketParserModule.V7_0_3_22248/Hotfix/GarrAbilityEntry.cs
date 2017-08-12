using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.GarrAbility)]
    public class GarrAbilityEntry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public uint IconFileDataID { get; set; }
        public ushort Flags { get; set; }
        public ushort OtherFactionGarrAbilityID { get; set; }
        public byte GarrAbilityCategoryID { get; set; }
        public byte FollowerTypeID { get; set; }
        public uint ID { get; set; }
    }
}