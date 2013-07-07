using WowPacketParser.Enums;
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

        [DBFieldName("WDBVerified")]
        public int WDBVerified;
    }
}
