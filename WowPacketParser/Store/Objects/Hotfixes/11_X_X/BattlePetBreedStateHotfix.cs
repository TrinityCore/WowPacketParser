using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("battle_pet_breed_state")]
    public sealed record BattlePetBreedStateHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("BattlePetStateID")]
        public int? BattlePetStateID;

        [DBFieldName("Value")]
        public ushort? Value;

        [DBFieldName("BattlePetBreedID")]
        public uint? BattlePetBreedID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
