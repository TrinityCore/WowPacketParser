using System.Collections.Generic;
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

        [DBFieldName("QuestPackageID")]
        public uint QuestPackageID;

        [DBFieldName("MinLevel")]
        public int MinLevel;

        [DBFieldName("QuestSortID")]
        public QuestSort QuestSortID;

        [DBFieldName("QuestInfoID")]
        public QuestType QuestInfoID;

        [DBFieldName("SuggestedGroupNum")]
        public uint SuggestedGroupNum;

        [DBFieldName("RewardNextQuest")]
        public uint RewardNextQuest;

        [DBFieldName("RewardXPDifficulty")]
        public uint RewardXPDifficulty;

        [DBFieldName("RewardXPMultiplier")]
        public float RewardXPMultiplier;

        [DBFieldName("RewardMoney")]
        public int RewardMoney;

        [DBFieldName("RewardMoneyDifficulty")]
        public uint RewardMoneyDifficulty;

        [DBFieldName("RewardMoneyMultiplier")]
        public float RewardMoneyMultiplier;

        [DBFieldName("RewardBonusMoney")]
        public uint RewardBonusMoney;

        [DBFieldName("RewardDisplaySpell")]
        public uint RewardDisplaySpell;

        [DBFieldName("RewardSpell")]
        public uint RewardSpell;

        [DBFieldName("RewardHonor")]
        public uint RewardHonor;

        [DBFieldName("RewardKillHonor")]
        public float RewardKillHonor;

        [DBFieldName("StartItem")]
        public uint StartItem;

        [DBFieldName("Flags")]
        public QuestFlags Flags;

        [DBFieldName("FlagsEx")]
        public uint FlagsEx;

        [DBFieldName("RewardItem", 4)]
        public uint[] RewardItems;

        [DBFieldName("RewardAmount", 4)]
        public uint[] RewardAmount;

        [DBFieldName("ItemDrop", 4)]
        public uint[] ItemDrop;

        [DBFieldName("ItemDropQuantity", 4)]
        public uint[] ItemDropQuantity;

        [DBFieldName("RewardChoiceItemID", 6)]
        public uint[] RewardChoiceItemID;

        [DBFieldName("RewardChoiceItemQuantity", 6)]
        public uint[] RewardChoiceItemQuantity;

        [DBFieldName("RewardChoiceItemDisplayID", 6)]
        public uint[] RewardChoiceItemDisplayID;

        [DBFieldName("POIContinent")]
        public uint POIContinent;

        [DBFieldName("POIx")]
        public float POIx;

        [DBFieldName("POIy")]
        public float POIy;

        [DBFieldName("POIPriority")]
        public int POIPriority;

        [DBFieldName("RewardTitle")]
        public uint RewardTitle;

        [DBFieldName("RewardTalents")]
        public uint RewardTalents;

        [DBFieldName("RewardArenaPoints")]
        public uint RewardArenaPoints;

        [DBFieldName("RewardSkillLineID")]
        public uint RewardSkillLineID;

        [DBFieldName("RewardNumSkillUps")]
        public uint RewardNumSkillUps;

        [DBFieldName("PortraitGiver")]
        public uint PortraitGiver;

        [DBFieldName("PortraitTurnIn")]
        public uint PortraitTurnIn;

        [DBFieldName("RewardFactionID", 5)]
        public uint[] RewardFactionID;

        [DBFieldName("RewardFactionValue", 5)]
        public int[] RewardFactionValue;

        [DBFieldName("RewardFactionOverride", 5)]
        public int[] RewardFactionOverride;

        [DBFieldName("RewardFactionFlags")]
        public uint RewardFactionFlags;

        [DBFieldName("RewardCurrencyID", 4)]
        public uint[] RewardCurrencyID;

        [DBFieldName("RewardCurrencyQty", 4)]
        public uint[] RewardCurrencyQty;

        [DBFieldName("AcceptedSoundKitID")]
        public uint AcceptedSoundKitID;

        [DBFieldName("CompleteSoundKitID")]
        public uint CompleteSoundKitID;

        [DBFieldName("AreaGroupID")]
        public uint AreaGroupID;

        [DBFieldName("TimeAllowed")]
        public uint TimeAllowed;

        [DBFieldName("AllowableRaces")]
        public int AllowableRaces;

        [DBFieldName("LogTitle", LocaleConstant.enUS)]
        public string LogTitle;

        [DBFieldName("LogDescription", LocaleConstant.enUS)]
        public string LogDescription;

        [DBFieldName("QuestDescription", LocaleConstant.enUS)]
        public string QuestDescription;

        [DBFieldName("AreaDescription", LocaleConstant.enUS)]
        public string AreaDescription;

        [DBFieldName("PortraitGiverText", LocaleConstant.enUS)]
        public string PortraitGiverText;

        [DBFieldName("PortraitGiverName", LocaleConstant.enUS)]
        public string PortraitGiverName;

        [DBFieldName("PortraitTurnInText", LocaleConstant.enUS)]
        public string PortraitTurnInText;

        [DBFieldName("PortraitTurnInName", LocaleConstant.enUS)]
        public string PortraitTurnInName;

        [DBFieldName("QuestCompletionLog", LocaleConstant.enUS)]
        public string QuestCompletionLog;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }

    [DBTableName("quest_objectives")]
    public sealed class QuestInfoObjective
    {
        [DBFieldName("QuestId")]
        public uint QuestId;

        [DBFieldName("Type")]
        public QuestRequirementType Type;

        [DBFieldName("StorageIndex")]
        public int StorageIndex;

        [DBFieldName("ObjectID")]
        public int ObjectID;

        [DBFieldName("Amount")]
        public int Amount;

        [DBFieldName("Flags")]
        public uint Flags;

        [DBFieldName("UnkFloat")]
        public float UnkFloat;

        public List<QuestVisualEffect> VisualEffectIds;

        [DBFieldName("Description", LocaleConstant.enUS)]
        public string Description;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }

    [DBTableName("quest_visual_effect")]
    public sealed class QuestVisualEffect
    {
        [DBFieldName("Index")]
        public uint Index;

        [DBFieldName("VisualEffect")]
        public int VisualEffect;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
