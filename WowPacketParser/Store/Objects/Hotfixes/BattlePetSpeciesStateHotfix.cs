using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("battle_pet_species_state")]
    public sealed record BattlePetSpeciesStateHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("BattlePetStateID")]
        public ushort? BattlePetStateID;

        [DBFieldName("Value")]
        public int? Value;

        [DBFieldName("BattlePetSpeciesID")]
        public uint? BattlePetSpeciesID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
