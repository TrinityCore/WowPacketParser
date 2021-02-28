using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("playerchoice")]
    public sealed class PlayerChoiceTemplate : IDataModel
    {
        [DBFieldName("ChoiceId", true)]
        public int? ChoiceId;

        [DBFieldName("UiTextureKitId", TargetedDatabase.Legion)]
        public int? UiTextureKitId;

        [DBFieldName("SoundKitId", TargetedDatabase.BattleForAzeroth)]
        public uint? SoundKitId;

        [DBFieldName("Question")]
        public string Question;

        [DBFieldName("HideWarboardHeader", TargetedDatabase.Legion)]
        public int HideWarboardHeader;

        [DBFieldName("KeepOpenAfterChoice", TargetedDatabase.BattleForAzeroth)]
        public int KeepOpenAfterChoice;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [DBTableName("playerchoice_response")]
    public sealed class PlayerChoiceResponseTemplate : IDataModel
    {
        [DBFieldName("ChoiceId", true)]
        public int? ChoiceId;

        [DBFieldName("ResponseId", true)]
        public int? ResponseId;

        [DBFieldName("ResponseIdentifier", TargetedDatabase.Shadowlands)]
        public short? ResponseIdentifier;

        [DBFieldName("Index", true)]
        public uint? Index;

        [DBFieldName("ChoiceArtFileId")]
        public int? ChoiceArtFileId;

        [DBFieldName("Flags", TargetedDatabase.BattleForAzeroth)]
        public int? Flags;

        [DBFieldName("WidgetSetId", TargetedDatabase.BattleForAzeroth)]
        public uint? WidgetSetId;

        [DBFieldName("UiTextureAtlasElementID", TargetedDatabase.BattleForAzeroth)]
        public uint? UiTextureAtlasElementID;

        [DBFieldName("SoundKitId", TargetedDatabase.BattleForAzeroth)]
        public uint? SoundKitId;

        [DBFieldName("GroupId", TargetedDatabase.BattleForAzeroth)]
        public int? GroupId;

        [DBFieldName("Header", TargetedDatabase.Legion)]
        public string Header;

        [DBFieldName("Subheader", TargetedDatabase.BattleForAzeroth)]
        public string Subheader;

        [DBFieldName("ButtonTooltip", TargetedDatabase.BattleForAzeroth)]
        public string ButtonTooltip;

        [DBFieldName("Answer")]
        public string Answer;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("Confirmation", TargetedDatabase.Legion)]
        public string Confirmation;

        [DBFieldName("RewardQuestID", TargetedDatabase.BattleForAzeroth)]
        public uint? RewardQuestID;

        [DBFieldName("UiTextureKitID", TargetedDatabase.Shadowlands)]
        public uint? UiTextureKitID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [DBTableName("playerchoice_response_reward")]
    public sealed class PlayerChoiceResponseRewardTemplate : IDataModel
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
    public sealed class PlayerChoiceResponseRewardCurrencyTemplate : IDataModel
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
    public sealed class PlayerChoiceResponseRewardFactionTemplate : IDataModel
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
    public sealed class PlayerChoiceResponseRewardItemTemplate : IDataModel
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
