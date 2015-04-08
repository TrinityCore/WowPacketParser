using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("battle_pet_species")]
    public sealed class BattlePetSpecies
    {
        [DBFieldName("CreatureID")]
        public uint CreatureID;

        [DBFieldName("IconFileID")]
        public uint IconFileID;

        [DBFieldName("SummonSpellID")]
        public uint SummonSpellID;

        [DBFieldName("PetType")]
        public int PetType;

        [DBFieldName("Source")]
        public uint Source;

        [DBFieldName("Flags")]
        public uint Flags;

        [DBFieldName("SourceText")]
        public string SourceText;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
