using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.BattlePetAbility, HasIndexInData = false)]
    public class BattlePetAbilityEntry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int IconFileDataID { get; set; }
        public sbyte PetTypeEnum { get; set; }
        public uint Cooldown { get; set; }
        public ushort BattlePetVisualID { get; set; }
        public byte Flags { get; set; }
    }
}
