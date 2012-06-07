using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("quest_template")]
    public sealed class QuestTemplate
    {
        [DBFieldName("Method")]
        public QuestMethod Method;

        [DBFieldName("MaxLevel")]
        public int Level;

        [DBFieldName("MinLevel")]
        public int MinLevel;

        [DBFieldName("ZoneOrSort")]
        public QuestSort ZoneOrSort; // TODO: Read correct enum

        [DBFieldName("Type")]
        public QuestType Type;

        [DBFieldName("SuggestedPlayers")]
        public uint SuggestedPlayers;

        [DBFieldName("RepObjectiveFaction", Count = 2)]
        public uint[] RequiredFactionId;

        [DBFieldName("RepObjectiveValue", Count = 2)]
        public int[] RequiredFactionValue;

        [DBFieldName("NextQuestInChain")]
        public uint NextQuestIdChain;

        [DBFieldName("RewXPId")]
        public uint RewardXPId;

        [DBFieldName("RewOrReqMoney")]
        public int RewardOrRequiredMoney;

        [DBFieldName("RewMoneyMaxLevel")]
        public uint RewardMoneyMaxLevel;

        [DBFieldName("RewSpell")]
        public uint RewardSpell;

        [DBFieldName("RewSpellCast")]
        public int RewardSpellCast;

        [DBFieldName("RewHonorAddition")]
        public int RewardHonor;

        [DBFieldName("RewHonorMultiplier")]
        public float RewardHonorMultiplier;

        //[DBFieldName("SourceItemId")]
        public uint SourceItemId;

        [DBFieldName("CharTitleId")]
        public QuestFlags Flags;

        //[DBFieldName("MinimapTargetMark")] 4.x
        public uint MinimapTargetMark;

        [DBFieldName("RewardTitleId")]
        public uint RewardTitleId;

        [DBFieldName("PlayersSlain")]
        public uint RequiredPlayerKills;

        [DBFieldName("BonusTalents")]
        public uint RewardTalents;

        [DBFieldName("RewardArenaPoints")]
        public uint RewardArenaPoints;

        [DBFieldName("RewSkillLineId")] // 4.x
        public uint RewardSkillId;

        [DBFieldName("RewSkillPoints")] // 4.x
        public uint RewardSkillPoints;

        //[DBFieldName("RewardReputationMask")] 4.x
        public uint RewardReputationMask;

        [DBFieldName("QuestGiverPortraitText")] // 4.x
        public uint QuestGiverPortrait;

        [DBFieldName("QuestTurnInPortraitText")] // 4.x
        public uint QuestTurnInPortrait;

        [DBFieldName("RewItemId", Count = 4)]
        public uint[] RewardItemId;

        [DBFieldName("RewItemCount", Count = 4)]
        public uint[] RewardItemCount;

        [DBFieldName("RewChoiceItemId", Count = 6)]
        public uint[] RewardChoiceItemId;

        [DBFieldName("RewChoiceItemCount", Count = 6)]
        public uint[] RewardChoiceItemCount;

        [DBFieldName("RewRepFaction", Count = 5)]
        public uint[] RewardFactionId;

        [DBFieldName("RewRepValueId", Count = 5)]
        public int[] RewardFactionValueId;

        [DBFieldName("RewRepValue", Count = 5)]
        public uint[] RewardFactionValueIdOverride;

        [DBFieldName("PointMapId")]
        public uint PointMapId;

        [DBFieldName("PointX")]
        public float PointX;

        [DBFieldName("PointY")]
        public float PointY;

        [DBFieldName("PointOpt")]
        public uint PointOption;

        [DBFieldName("Title")]
        public string Title;

        [DBFieldName("Objectives")]
        public string Objectives;

        [DBFieldName("Details")]
        public string Details;

        [DBFieldName("EndText")]
        public string EndText;

        [DBFieldName("CompletedText")] // 4.x
        public string CompletedText;

        [DBFieldName("ReqCreatureOrGOId", Count = 4)]
        public int[] RequiredNpcOrGo;

        [DBFieldName("ReqCreatureOrGOCount", Count = 4)]
        public uint[] RequiredNpcOrGoCount;

        [DBFieldName("ReqSourceId", Count = 4)]
        public uint[] RequiredSourceItemId;

        [DBFieldName("ReqSourceCount", Count = 4)]
        public uint[] RequiredSourceItemCount;

        [DBFieldName("ReqItemId", Count = 6)]
        public uint[] RequiredItemId;

        [DBFieldName("ReqItemCount", Count = 6)]
        public uint[] RequiredItemCount;

        //[DBFieldName("RequiredSpell")] 4.x
        public uint RequiredSpell;

        [DBFieldName("ObjectiveText", Count = 4)]
        public string[] ObjectiveText;

        [DBFieldName("RewCurrencyId", Count = 4)] // 4.x
        public uint[] RewardCurrencyId;

        [DBFieldName("RewCurrencyCount", Count = 4)] // 4.x
        public uint[] RewardCurrencyCount;

        [DBFieldName("ReqCurrencyId", Count = 4)] // 4.x
        public uint[] RequiredCurrencyId;

        [DBFieldName("ReqCurrencyCount", Count = 4)] // 4.x
        public uint[] RequiredCurrencyCount;

        //[DBFieldName("QuestGiverTextWindow")] 4.x
        public string QuestGiverTextWindow;

        //[DBFieldName("QuestGiverTargetName")] 4.x
        public string QuestGiverTargetName;

        //[DBFieldName("QuestTurnTextWindow")] 4.x
        public string QuestTurnTextWindow;

        //[DBFieldName("QuestTurnTargetName")] 4.x
        public string QuestTurnTargetName;

        [DBFieldName("SoundAccept")] // 4.x
        public uint SoundAccept;

        [DBFieldName("SoundTurnIn")] // 4.x
        public uint SoundTurnIn;
    }
}
