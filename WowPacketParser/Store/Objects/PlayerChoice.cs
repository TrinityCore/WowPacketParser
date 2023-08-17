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

        [DBFieldName("UiTextureKitId", TargetedDatabaseFlag.SinceLegion)]
        public int? UiTextureKitId;

        [DBFieldName("SoundKitId", TargetedDatabaseFlag.SinceBattleForAzeroth)]
        public uint? SoundKitId;

        [DBFieldName("CloseSoundKitId", TargetedDatabaseFlag.SinceShadowlands)]
        public uint? CloseSoundKitId;

        [DBFieldName("Duration", TargetedDatabaseFlag.SinceShadowlands)]
        public long? Duration;

        [DBFieldName("Question", LocaleConstant.enUS)]
        public string Question;

        [DBFieldName("PendingChoiceText", TargetedDatabaseFlag.SinceShadowlands, LocaleConstant.enUS)]
        public string PendingChoiceText;

        [DBFieldName("HideWarboardHeader", TargetedDatabaseFlag.SinceLegion)]
        public int HideWarboardHeader;

        [DBFieldName("KeepOpenAfterChoice", TargetedDatabaseFlag.SinceBattleForAzeroth)]
        public int KeepOpenAfterChoice;

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

        [DBFieldName("ResponseIdentifier", TargetedDatabaseFlag.SinceShadowlands)]
        public short? ResponseIdentifier;

        [DBFieldName("Index", true)]
        public uint? Index;

        [DBFieldName("ChoiceArtFileId")]
        public int? ChoiceArtFileId;

        [DBFieldName("Flags", TargetedDatabaseFlag.SinceBattleForAzeroth)]
        public int? Flags;

        [DBFieldName("WidgetSetId", TargetedDatabaseFlag.SinceBattleForAzeroth)]
        public uint? WidgetSetId;

        [DBFieldName("UiTextureAtlasElementID", TargetedDatabaseFlag.SinceBattleForAzeroth)]
        public uint? UiTextureAtlasElementID;

        [DBFieldName("SoundKitId", TargetedDatabaseFlag.SinceBattleForAzeroth)]
        public uint? SoundKitId;

        [DBFieldName("GroupId", TargetedDatabaseFlag.SinceBattleForAzeroth)]
        public int? GroupId;

        [DBFieldName("Header", TargetedDatabaseFlag.SinceLegion, LocaleConstant.enUS)]
        public string Header;

        [DBFieldName("Subheader", TargetedDatabaseFlag.SinceBattleForAzeroth)]
        public string Subheader;

        [DBFieldName("ButtonTooltip", TargetedDatabaseFlag.SinceBattleForAzeroth, LocaleConstant.enUS)]
        public string ButtonTooltip;

        [DBFieldName("Answer", LocaleConstant.enUS)]
        public string Answer;

        [DBFieldName("Description", LocaleConstant.enUS)]
        public string Description;

        [DBFieldName("Confirmation", TargetedDatabaseFlag.SinceLegion, LocaleConstant.enUS)]
        public string Confirmation;

        [DBFieldName("RewardQuestID", TargetedDatabaseFlag.SinceBattleForAzeroth)]
        public uint? RewardQuestID;

        [DBFieldName("UiTextureKitID", TargetedDatabaseFlag.SinceShadowlands)]
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
