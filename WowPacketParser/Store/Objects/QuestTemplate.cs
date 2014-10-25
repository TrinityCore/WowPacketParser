using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("quest_template")]
    public sealed class QuestTemplate
    {
        [DBFieldName("Method")]
        public QuestMethod Method;

        [DBFieldName("Level")]
        public int Level;

        [DBFieldName("MinLevel")]
        public int MinLevel;

        [DBFieldName("ZoneOrSort")]
        public QuestSort ZoneOrSort; // TODO: Read correct enum

        [DBFieldName("Type")]
        public QuestType Type;

        [DBFieldName("SuggestedPlayers")]
        public uint SuggestedPlayers;

        [DBFieldName("RequiredFactionId", 2)]
        public uint[] RequiredFactionId;

        [DBFieldName("RequiredFactionValue", 2)]
        public int[] RequiredFactionValue;

        [DBFieldName("NextQuestIdChain")]
        public uint NextQuestIdChain;

        [DBFieldName("RewardXPId")]
        public uint RewardXPId;

        [DBFieldName("RewardOrRequiredMoney")]
        public int RewardOrRequiredMoney;

        [DBFieldName("RewardMoneyMaxLevel")]
        public uint RewardMoneyMaxLevel;

        [DBFieldName("RewardSpell")]
        public uint RewardSpell;

        [DBFieldName("RewardSpellCast")]
        public int RewardSpellCast;

        [DBFieldName("RewardHonor")]
        public int RewardHonor;

        [DBFieldName("RewardHonorMultiplier")]
        public float RewardHonorMultiplier;

        [DBFieldName("SourceItemId")]
        public uint SourceItemId;

        [DBFieldName("Flags")]
        public QuestFlags Flags;

        [DBFieldName("MinimapTargetMark", ClientVersionBuild.V4_0_1_13164)]
        public uint MinimapTargetMark;

        [DBFieldName("RewardTitleId")]
        public uint RewardTitleId;

        [DBFieldName("RequiredPlayerKills")]
        public uint RequiredPlayerKills;

        [DBFieldName("RewardTalents")]
        public uint RewardTalents;

        [DBFieldName("RewardArenaPoints")]
        public uint RewardArenaPoints;

        [DBFieldName("RewardSkillId", ClientVersionBuild.V4_0_1_13164)]
        public uint RewardSkillId;

        [DBFieldName("RewardSkillPoints", ClientVersionBuild.V4_0_1_13164)]
        public uint RewardSkillPoints;

        [DBFieldName("RewardReputationMask", ClientVersionBuild.V4_0_1_13164)]
        public uint RewardReputationMask;

        [DBFieldName("QuestGiverPortrait", ClientVersionBuild.V4_0_1_13164)]
        public uint QuestGiverPortrait;

        [DBFieldName("QuestTurnInPortrait", ClientVersionBuild.V4_0_1_13164)]
        public uint QuestTurnInPortrait;

        [DBFieldName("RewardItemId", 4)]
        public uint[] RewardItemId;

        [DBFieldName("RewardItemCount", 4)]
        public uint[] RewardItemCount;

        [DBFieldName("RewardChoiceItemId", 6)]
        public uint[] RewardChoiceItemId;

        [DBFieldName("RewardChoiceItemCount", 6)]
        public uint[] RewardChoiceItemCount;

        [DBFieldName("RewardFactionId", 5)]
        public uint[] RewardFactionId;

        [DBFieldName("RewardFactionValueId", 5)]
        public int[] RewardFactionValueId;

        [DBFieldName("RewardFactionValueIdOverride", 5)]
        public uint[] RewardFactionValueIdOverride;

        [DBFieldName("PointMapId")]
        public uint PointMapId;

        [DBFieldName("PointX")]
        public float PointX;

        [DBFieldName("PointY")]
        public float PointY;

        [DBFieldName("PointOption")]
        public uint PointOption;

        [DBFieldName("Title")]
        public string Title;

        [DBFieldName("Objectives")]
        public string Objectives;

        [DBFieldName("Details")]
        public string Details;

        [DBFieldName("EndText")]
        public string EndText;

        [DBFieldName("CompletedText")]
        public string CompletedText;

        [DBFieldName("RequiredNpcOrGo", 4)]
        public int[] RequiredNpcOrGo;

        [DBFieldName("RequiredNpcOrGoCount", 4)]
        public uint[] RequiredNpcOrGoCount;

        [DBFieldName("RequiredSourceItemId", 4)]
        public uint[] RequiredSourceItemId;

        [DBFieldName("RequiredSourceItemCount", 4)]
        public uint[] RequiredSourceItemCount;

        [DBFieldName("RequiredItemId", 6)]
        public uint[] RequiredItemId;

        [DBFieldName("RequiredItemCount", 6)]
        public uint[] RequiredItemCount;

        [DBFieldName("RequiredSpell", ClientVersionBuild.V4_0_1_13164)]
        public uint RequiredSpell;

        [DBFieldName("ObjectiveText", 4)]
        public string[] ObjectiveText;

        [DBFieldName("RewardCurrencyId", ClientVersionBuild.V4_0_1_13164, 4)]
        public uint[] RewardCurrencyId;

        [DBFieldName("RewardCurrencyCount", ClientVersionBuild.V4_0_1_13164, 4)]
        public uint[] RewardCurrencyCount;

        [DBFieldName("RequiredCurrencyId", ClientVersionBuild.V4_0_1_13164, 4)]
        public uint[] RequiredCurrencyId;

        [DBFieldName("RequiredCurrencyCount", ClientVersionBuild.V4_0_1_13164, 4)]
        public uint[] RequiredCurrencyCount;

        [DBFieldName("QuestGiverTextWindow", ClientVersionBuild.V4_0_1_13164)]
        public string QuestGiverTextWindow;

        [DBFieldName("QuestGiverTargetName", ClientVersionBuild.V4_0_1_13164)]
        public string QuestGiverTargetName;

        [DBFieldName("QuestTurnTextWindow", ClientVersionBuild.V4_0_1_13164)]
        public string QuestTurnTextWindow;

        [DBFieldName("QuestTurnTargetName", ClientVersionBuild.V4_0_1_13164)]
        public string QuestTurnTargetName;

        [DBFieldName("SoundAccept", ClientVersionBuild.V4_0_1_13164)]
        public uint SoundAccept;

        [DBFieldName("SoundTurnIn", ClientVersionBuild.V4_0_1_13164)]
        public uint SoundTurnIn;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }


    [DBTableName("quest_template")]
    public sealed class QuestTemplateWod
    {
        [DBFieldName("QuestType")]
        public QuestMethod QuestType;

        [DBFieldName("QuestLevel")]
        public int QuestLevel;

        [DBFieldName("QuestLevel")]
        public int Level;

        [DBFieldName("QuestPackageID")]
        public int QuestPackageID;

        [DBFieldName("MinLevel")]
        public int MinLevel;

        [DBFieldName("QuestSortID")]
        public QuestSort QuestSortID;

        [DBFieldName("QuestInfoID")]
        public QuestType QuestInfoID;

        [DBFieldName("SuggestedGroupNum")]
        public int SuggestedGroupNum;

        [DBFieldName("RewardNextQuest")]
        public int RewardNextQuest;

        [DBFieldName("RewardXPDifficulty")]
        public int RewardXPDifficulty;

        [DBFieldName("Float10")]
        public float Float10;

        [DBFieldName("RewardOrRequiredMoney")]
        public int RewardOrRequiredMoney;

        [DBFieldName("RewardMoneyMaxLevel")]
        public int RewardMoneyMaxLevel;

        [DBFieldName("Float13")]
        public float Float13;

        [DBFieldName("RewardBonusMoney")]
        public int RewardBonusMoney;

        [DBFieldName("RewardDisplaySpell")]
        public int RewardDisplaySpell;

        [DBFieldName("RewardSpell")]
        public int RewardSpell;

        [DBFieldName("RewardHonor")]
        public int RewardHonor;

        [DBFieldName("RewardKillHonor")]
        public float RewardKillHonor;

        [DBFieldName("StartItem")]
        public int StartItem;

        [DBFieldName("Flags")]
        public QuestFlags Flags;

        [DBFieldName("FlagsEx")]
        public int FlagsEx;

        [DBFieldName("RewardItems", 4)]
        public int[] RewardItems;

        [DBFieldName("RewardAmount", 4)]
        public int[] RewardAmount;

        [DBFieldName("ItemDrop", 4)]
        public int[] ItemDrop;

        [DBFieldName("ItemDropQuantity", 4)]
        public int[] ItemDropQuantity;

        [DBFieldName("ItemID", 6)]
        public int[] ItemID;

        [DBFieldName("Quantity", 6)]
        public int[] Quantity;

        [DBFieldName("DisplayID", 6)]
        public int[] DisplayID;

        [DBFieldName("POIContinent")]
        public int POIContinent;

        [DBFieldName("POIx")]
        public float POIx;

        [DBFieldName("POIy")]
        public float POIy;

        [DBFieldName("POIPriority")]
        public int POIPriority;

        [DBFieldName("RewardTitle")]
        public int RewardTitle;

        [DBFieldName("RewardTalents")]
        public int RewardTalents;

        [DBFieldName("RewardArenaPoints")]
        public int RewardArenaPoints;

        [DBFieldName("RewardSkillLineID")]
        public int RewardSkillLineID;

        [DBFieldName("RewardNumSkillUps")]
        public int RewardNumSkillUps;

        [DBFieldName("PortraitGiver")]
        public int PortraitGiver;

        [DBFieldName("PortraitTurnIn")]
        public int PortraitTurnIn;

        [DBFieldName("RewardFactionID", 5)]
        public int[] RewardFactionID;

        [DBFieldName("RewardFactionValue", 5)]
        public int[] RewardFactionValue;

        [DBFieldName("RewardFactionOverride", 5)]
        public int[] RewardFactionOverride;

        [DBFieldName("RewardFactionFlags")]
        public int RewardFactionFlags;

        [DBFieldName("RewardCurrencyID", 4)]
        public int[] RewardCurrencyID;

        [DBFieldName("RewardCurrencyQty", 4)]
        public int[] RewardCurrencyQty;

        [DBFieldName("AcceptedSoundKitID")]
        public int AcceptedSoundKitID;

        [DBFieldName("CompleteSoundKitID")]
        public int CompleteSoundKitID;

        [DBFieldName("AreaGroupID")]
        public int AreaGroupID;

        [DBFieldName("TimeAllowed")]
        public int TimeAllowed;

        [DBFieldName("Int2950")]
        public int Int2950;

        [DBFieldName("LogTitle")]
        public string LogTitle;

        [DBFieldName("LogDescription")]
        public string LogDescription;

        [DBFieldName("QuestDescription")]
        public string QuestDescription;

        [DBFieldName("AreaDescription")]
        public string AreaDescription;

        [DBFieldName("PortraitGiverText")]
        public string PortraitGiverText;

        [DBFieldName("PortraitGiverName")]
        public string PortraitGiverName;

        [DBFieldName("PortraitTurnInText")]
        public string PortraitTurnInText;

        [DBFieldName("PortraitTurnInName")]
        public string PortraitTurnInName;

        [DBFieldName("QuestCompletionLog")]
        public string QuestCompletionLog;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
