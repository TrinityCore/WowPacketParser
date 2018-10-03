using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.BattlePetSpeciesXAbility, HasIndexInData = false)]
    public class BattlePetSpeciesXAbilityEntry
    {
        public ushort BattlePetAbilityID { get; set; }
        public byte RequiredLevel { get; set; }
        public sbyte SlotEnum { get; set; }
        public ushort BattlePetSpeciesID { get; set; }
    }
}
