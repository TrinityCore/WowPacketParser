using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.BattlePetSpecies)]
    public class BattlePetSpeciesEntry
    {
        public uint CreatureID { get; set; }
        public uint IconFileID { get; set; }
        public uint SummonSpellID { get; set; }
        public string SourceText { get; set; }
        public string Description { get; set; }
        public ushort Flags { get; set; }
        public byte PetType { get; set; }
        public sbyte Source { get; set; }
        public uint ID { get; set; }
        [HotfixVersion(ClientVersionBuild.V7_2_0_23826, false)]
        public uint CardModelSceneID { get; set; }
        [HotfixVersion(ClientVersionBuild.V7_2_0_23826, false)]
        public uint LoadoutModelSceneID { get; set; }
    }
}