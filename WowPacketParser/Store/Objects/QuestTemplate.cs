using WowPacketParser.Enums;

namespace WowPacketParser.Store.Objects
{
    public sealed class QuestTemplate
    {
        public QuestMethod Method;

        public int Level;

        public int MinLevel;

        public QuestSort ZoneOrSort; // TODO: Read correct enum

        public QuestType Type;

        public uint SuggestedPlayers;

        public uint[] RequiredFactionId;

        public int[] RequiredFactionValue;

        public uint NextQuestIdChain;

        public uint RewardXPId;

        public int RewardOrRequiredMoney;

        public uint RewardMoneyMaxLevel;

        public uint RewardSpell;

        public uint RewardSpellCast;

        public uint RewardHonor;

        public float RewardHonorMultiplier;

        public int SourceItemId;

        public QuestFlags Flags;

        public uint MinimapTargetMark;

        public uint RewardTitleId;

        public uint RequiredPlayerKills;

        public uint RewardTalents;

        public uint RewardArenaPoints;

        public uint RewardSkillId;

        public uint RewardSkillPoints;

        public uint RewardReputationMask;

        public uint QuestGiverPortrait;

        public uint QuestTurnInPortrait;

        public uint[] RewardItemId;

        public uint[] RewardItemCount;

        public uint[] RewardChoiceItemId;

        public uint[] RewardChoiceItemCount;

        public uint[] RewardFactionId;

        public int[] RewardFactionValueId;

        public uint[] RewardFactionValueIdOverride; // ?

        public uint PointMapId;

        public float PointX;

        public float PointY;

        public uint PointOption;

        public string Title;

        public string Objectives;

        public string Details;

        public string EndText;

        public string CompletedText;

        public int[] RequiredNpcOrGo;

        public uint[] RequiredNpcOrGoCount;

        public uint[] RequiredSourceItemId;

        public uint[] RequiredSourceItemCount;

        public uint[] RequiredItemId;

        public uint[] RequiredItemCount;

        public uint RequiredSpell;

        public string[] ObjectiveText;

        public uint[] RewardCurrencyId;

        public uint[] RewardCurrencyCount;

        public uint[] RequiredCurrencyId;

        public uint[] RequiredCurrencyCount;

        public string QuestGiverTextWindow;

        public string QuestGiverTargetName;

        public string QuestTurnTextWindow;

        public string QuestTurnTargetName;

        public uint SoundAccept;

        public uint SoundTurnIn;
    }
}
