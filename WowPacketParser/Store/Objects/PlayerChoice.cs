using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("playerchoice")]
    public sealed record PlayerChoiceTemplate : IDataModel
    {
        [DBFieldName("ChoiceId", true)]
        public int? ChoiceId;

        [DBFieldName("UiTextureKitId", TargetedDatabaseFlag.SinceLegion | TargetedDatabaseFlag.CataClassic)]
        public int? UiTextureKitId;

        [DBFieldName("SoundKitId", TargetedDatabaseFlag.SinceBattleForAzeroth | TargetedDatabaseFlag.CataClassic)]
        public uint? SoundKitId;

        [DBFieldName("CloseSoundKitId", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.CataClassic)]
        public uint? CloseSoundKitId;

        [DBFieldName("Duration", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.CataClassic, nullable: true)]
        public long? Duration;

        [DBFieldName("Question", LocaleConstant.enUS)]
        public string Question;

        [DBFieldName("PendingChoiceText", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.CataClassic, LocaleConstant.enUS)]
        public string PendingChoiceText;

        [DBFieldName("InfiniteRange", TargetedDatabaseFlag.SinceTheWarWithin)]
        public int InfiniteRange;

        [DBFieldName("HideWarboardHeader", TargetedDatabaseFlag.SinceLegion | TargetedDatabaseFlag.CataClassic)]
        public int HideWarboardHeader;

        [DBFieldName("KeepOpenAfterChoice", TargetedDatabaseFlag.SinceBattleForAzeroth | TargetedDatabaseFlag.CataClassic)]
        public int KeepOpenAfterChoice;

        [DBFieldName("ShowChoicesAsList", TargetedDatabaseFlag.SinceTheWarWithin)]
        public int ShowChoicesAsList;

        [DBFieldName("ForceDontShowChoicesAsList", TargetedDatabaseFlag.SinceTheWarWithin)]
        public int ForceDontShowChoicesAsList;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [DBTableName("playerchoice_response")]
    public sealed record PlayerChoiceResponseTemplate : IDataModel
    {
        [DBFieldName("ChoiceId", true)]
        public int? ChoiceId;

        [DBFieldName("ResponseId", true)]
        public int? ResponseId;

        [DBFieldName("ResponseIdentifier", TargetedDatabaseFlag.Shadowlands | TargetedDatabaseFlag.Dragonflight | TargetedDatabaseFlag.CataClassic)]
        public short? ResponseIdentifier;

        [DBFieldName("Index")]
        public uint? Index;

        [DBFieldName("ChoiceArtFileId")]
        public int? ChoiceArtFileId;

        [DBFieldName("Flags", TargetedDatabaseFlag.SinceBattleForAzeroth | TargetedDatabaseFlag.CataClassic)]
        public int? Flags;

        [DBFieldName("WidgetSetId", TargetedDatabaseFlag.SinceBattleForAzeroth | TargetedDatabaseFlag.CataClassic)]
        public uint? WidgetSetId;

        [DBFieldName("UiTextureAtlasElementID", TargetedDatabaseFlag.SinceBattleForAzeroth | TargetedDatabaseFlag.CataClassic)]
        public uint? UiTextureAtlasElementID;

        [DBFieldName("SoundKitId", TargetedDatabaseFlag.SinceBattleForAzeroth | TargetedDatabaseFlag.CataClassic)]
        public uint? SoundKitId;

        [DBFieldName("GroupId", TargetedDatabaseFlag.SinceBattleForAzeroth | TargetedDatabaseFlag.CataClassic)]
        public int? GroupId;

        [DBFieldName("Header", TargetedDatabaseFlag.SinceLegion | TargetedDatabaseFlag.CataClassic, LocaleConstant.enUS)]
        public string Header;

        [DBFieldName("Subheader", TargetedDatabaseFlag.SinceBattleForAzeroth | TargetedDatabaseFlag.CataClassic)]
        public string Subheader;

        [DBFieldName("ButtonTooltip", TargetedDatabaseFlag.SinceBattleForAzeroth | TargetedDatabaseFlag.CataClassic, LocaleConstant.enUS)]
        public string ButtonTooltip;

        [DBFieldName("Answer", LocaleConstant.enUS)]
        public string Answer;

        [DBFieldName("Description", LocaleConstant.enUS)]
        public string Description;

        [DBFieldName("Confirmation", TargetedDatabaseFlag.SinceLegion | TargetedDatabaseFlag.CataClassic, LocaleConstant.enUS)]
        public string Confirmation;

        [DBFieldName("RewardQuestID", TargetedDatabaseFlag.SinceBattleForAzeroth | TargetedDatabaseFlag.CataClassic)]
        public uint? RewardQuestID;

        [DBFieldName("UiTextureKitID", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.CataClassic)]
        public uint? UiTextureKitID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [DBTableName("playerchoice_response_reward")]
    public sealed record PlayerChoiceResponseRewardTemplate : IDataModel
    {
        [DBFieldName("ChoiceId", true)]
        public int? ChoiceId;

        [DBFieldName("ResponseId", true)]
        public int? ResponseId;

        [DBFieldName("TitleId")]
        public int? TitleId;

        [DBFieldName("PackageId")]
        public int? PackageId;

        [DBFieldName("SkillLineId")]
        public int? SkillLineId;

        [DBFieldName("SkillPointCount")]
        public uint? SkillPointCount;

        [DBFieldName("ArenaPointCount")]
        public uint? ArenaPointCount;

        [DBFieldName("HonorPointCount")]
        public uint? HonorPointCount;

        [DBFieldName("Money")]
        public ulong? Money;

        [DBFieldName("Xp")]
        public uint? Xp;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [DBTableName("playerchoice_response_reward_currency")]
    public sealed record PlayerChoiceResponseRewardCurrencyTemplate : IDataModel
    {
        [DBFieldName("ChoiceId", true)]
        public int? ChoiceId;

        [DBFieldName("ResponseId", true)]
        public int? ResponseId;

        [DBFieldName("Index", true)]
        public uint? Index;

        [DBFieldName("CurrencyId")]
        public int? CurrencyId;

        [DBFieldName("Quantity")]
        public int? Quantity;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [DBTableName("playerchoice_response_reward_faction")]
    public sealed record PlayerChoiceResponseRewardFactionTemplate : IDataModel
    {
        [DBFieldName("ChoiceId", true)]
        public int? ChoiceId;

        [DBFieldName("ResponseId", true)]
        public int? ResponseId;

        [DBFieldName("Index", true)]
        public uint? Index;

        [DBFieldName("FactionId")]
        public int? FactionId;

        [DBFieldName("Quantity")]
        public int? Quantity;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [DBTableName("playerchoice_response_reward_item")]
    public sealed record PlayerChoiceResponseRewardItemTemplate : IDataModel
    {
        [DBFieldName("ChoiceId", true)]
        public int? ChoiceId;

        [DBFieldName("ResponseId", true)]
        public int? ResponseId;

        [DBFieldName("Index", true)]
        public uint? Index;

        [DBFieldName("ItemId")]
        public int? ItemId;

        [DBFieldName("BonusListIDs")]
        public string BonusListIDs;

        [DBFieldName("Quantity")]
        public int? Quantity;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
