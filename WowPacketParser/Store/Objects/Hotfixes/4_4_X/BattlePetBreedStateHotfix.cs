using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("battle_pet_breed_state")]
    public sealed record BattlePetBreedStateHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("BattlePetStateID")]
        public byte? BattlePetStateID;

        [DBFieldName("Value")]
        public ushort? Value;

        [DBFieldName("BattlePetBreedID")]
        public int? BattlePetBreedID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
