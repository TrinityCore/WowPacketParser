using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.BattlePetSpecies)]
    public class BattlePetSpeciesEntry
    {
        public string Description { get; set; }
        public string SourceText { get; set; }
        public int ID { get; set; }
        public int CreatureID { get; set; }
        public int SummonSpellID { get; set; }
        public int IconFileDataID { get; set; }
        public byte PetTypeEnum { get; set; }
        public ushort Flags { get; set; }
        public sbyte SourceTypeEnum { get; set; }
        public int CardUIModelSceneID { get; set; }
        public int LoadoutUIModelSceneID { get; set; }
    }
}
