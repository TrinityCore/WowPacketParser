using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("battle_pet_breed_quality")]
    public sealed record BattlePetBreedQualityHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MaxQualityRoll")]
        public int? MaxQualityRoll;

        [DBFieldName("StateMultiplier")]
        public float? StateMultiplier;

        [DBFieldName("QualityEnum")]
        public sbyte? QualityEnum;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
