using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_cond")]
    public sealed record TraitCondHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("CondType")]
        public int? CondType;

        [DBFieldName("TraitTreeID")]
        public int? TraitTreeID;

        [DBFieldName("GrantedRanks")]
        public int? GrantedRanks;

        [DBFieldName("QuestID")]
        public int? QuestID;

        [DBFieldName("AchievementID")]
        public int? AchievementID;

        [DBFieldName("SpecSetID")]
        public int? SpecSetID;

        [DBFieldName("TraitNodeGroupID")]
        public int? TraitNodeGroupID;

        [DBFieldName("TraitNodeID")]
        public int? TraitNodeID;

        [DBFieldName("TraitCurrencyID")]
        public int? TraitCurrencyID;

        [DBFieldName("SpentAmountRequired")]
        public int? SpentAmountRequired;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("RequiredLevel")]
        public int? RequiredLevel;

        [DBFieldName("FreeSharedStringID")]
        public int? FreeSharedStringID;

        [DBFieldName("SpendMoreSharedStringID")]
        public int? SpendMoreSharedStringID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("trait_cond")]
    public sealed record TraitCondHotfix341: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("CondType")]
        public int? CondType;

        [DBFieldName("TraitTreeID")]
        public int? TraitTreeID;

        [DBFieldName("GrantedRanks")]
        public int? GrantedRanks;

        [DBFieldName("QuestID")]
        public int? QuestID;

        [DBFieldName("AchievementID")]
        public int? AchievementID;

        [DBFieldName("SpecSetID")]
        public int? SpecSetID;

        [DBFieldName("TraitNodeGroupID")]
        public int? TraitNodeGroupID;

        [DBFieldName("TraitNodeID")]
        public int? TraitNodeID;

        [DBFieldName("TraitCurrencyID")]
        public int? TraitCurrencyID;

        [DBFieldName("SpentAmountRequired")]
        public int? SpentAmountRequired;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("RequiredLevel")]
        public int? RequiredLevel;

        [DBFieldName("FreeSharedStringID")]
        public int? FreeSharedStringID;

        [DBFieldName("SpendMoreSharedStringID")]
        public int? SpendMoreSharedStringID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
