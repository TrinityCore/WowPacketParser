using System;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.BattlePetSpecies)]
    public class BattlePetSpeciesEntry
    {
        public uint ID { get; set; }
        public uint CreatureID { get; set; }
        public uint IconFileID { get; set; }
        public uint SummonSpellID { get; set; }
        public Int32 PetType { get; set; }
        public uint Source { get; set; }
        public uint Flags { get; set; }
        public string SourceText { get; set; }
        public string Description { get; set; }
    }
}