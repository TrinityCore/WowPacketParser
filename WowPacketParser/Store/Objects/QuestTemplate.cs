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

        [DBFieldName("RequiredFactionId", Count = 2)]
        public uint[] RequiredFactionId;

        [DBFieldName("RequiredFactionValue", Count = 2)]
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

        //[DBFieldName("MinimapTargetMark")] 4.x
        public uint MinimapTargetMark;

        [DBFieldName("RewardTitleId")]
        public uint RewardTitleId;

        [DBFieldName("RequiredPlayerKills")]
        public uint RequiredPlayerKills;

        [DBFieldName("RewardTalents")]
        public uint RewardTalents;

        [DBFieldName("RewardArenaPoints")]
        public uint RewardArenaPoints;

        //[DBFieldName("RewardSkillId")] 4.x
        public uint RewardSkillId;

        //[DBFieldName("RewardSkillPoints")] 4.x
        public uint RewardSkillPoints;

        //[DBFieldName("RewardReputationMask")] 4.x
        public uint RewardReputationMask;

        //[DBFieldName("QuestGiverPortrait")] 4.x
        public uint QuestGiverPortrait;

        //[DBFieldName("QuestTurnInPortrait")] 4.x
        public uint QuestTurnInPortrait;

        [DBFieldName("RewardItemId", Count = 4)]
        public uint[] RewardItemId;

        [DBFieldName("RewardItemCount", Count = 4)]
        public uint[] RewardItemCount;

        [DBFieldName("RewardChoiceItemId", Count = 6)]
        public uint[] RewardChoiceItemId;

        [DBFieldName("RewardChoiceItemCount", Count = 6)]
        public uint[] RewardChoiceItemCount;

        [DBFieldName("RewardFactionId", Count = 5)]
        public uint[] RewardFactionId;

        [DBFieldName("RewardFactionValueId", Count = 5)]
        public int[] RewardFactionValueId;

        [DBFieldName("RewardFactionValueIdOverride", Count = 5)]
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

        [DBFieldName("RequiredNpcOrGo", Count = 4)]
        public int[] RequiredNpcOrGo;

        [DBFieldName("RequiredNpcOrGoCount", Count = 4)]
        public uint[] RequiredNpcOrGoCount;

        [DBFieldName("RequiredSourceItemId", Count = 4)]
        public uint[] RequiredSourceItemId;

        [DBFieldName("RequiredSourceItemCount", Count = 4)]
        public uint[] RequiredSourceItemCount;

        [DBFieldName("RequiredItemId", Count = 6)]
        public uint[] RequiredItemId;

        [DBFieldName("RequiredItemCount", Count = 6)]
        public uint[] RequiredItemCount;

        //[DBFieldName("RequiredSpell")] 4.x
        public uint RequiredSpell;

        [DBFieldName("ObjectiveText", Count = 4)]
        public string[] ObjectiveText;

        //[DBFieldName("RewardCurrencyId", Count = 4)] 4.x
        public uint[] RewardCurrencyId;

        //[DBFieldName("RewardCurrencyCount", Count = 4)] 4.x
        public uint[] RewardCurrencyCount;

        //[DBFieldName("RequiredCurrencyId", Count = 4)] 4.x
        public uint[] RequiredCurrencyId;

        //[DBFieldName("RequiredCurrencyCount", Count = 4)] 4.x
        public uint[] RequiredCurrencyCount;

        //[DBFieldName("QuestGiverTextWindow")] 4.x
        public string QuestGiverTextWindow;

        //[DBFieldName("QuestGiverTargetName")] 4.x
        public string QuestGiverTargetName;

        //[DBFieldName("QuestTurnTextWindow")] 4.x
        public string QuestTurnTextWindow;

        //[DBFieldName("QuestTurnTargetName")] 4.x
        public string QuestTurnTargetName;

        //[DBFieldName("SoundAccept")] 4.x
        public uint SoundAccept;

        //[DBFieldName("SoundTurnIn")] 4.x
        public uint SoundTurnIn;
    }
}
