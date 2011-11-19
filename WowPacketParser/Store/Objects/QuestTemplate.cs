using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowPacketParser.Enums;

namespace WowPacketParser.Store.Objects
{
    public sealed class QuestTemplate
    {
        public QuestMethod Method;

        public int Level;

        public uint MinLevel;

        public QuestSort Sort;

        public QuestType Type;

        public uint SuggestedPlayers;

        public uint[] RequiredFactionId;

        public int[] RequiredFactionValue;

        public uint NextQuestId;

        public uint RewardXPId;

        public int RewardOrRequiredMoney;

        public uint RewardMoneyMaxLevel;

        public uint RewardSpell;

        public uint RewardSpellCast;

        public uint RewardHonor;

        public float RewardHonorMultiplier;

        public int SourceItemId;

        public QuestFlags Flags;

        public uint RewardTitleId;

        public uint RequiredPlayerKills;

        public uint RewardTalents;

        public uint RewardArenaPoints;

        public int RewardUnknown;

        public uint RewardSkillPoints;

        public uint RewardReputationMask;

        public uint QuestGiverPortrait;

        public uint QuestTurnInPortrait;

        public uint UnknownUInt32;

        public uint[] RewardItemId;

        public uint[] RewardItemCount;

        public uint[] RewardChoiceItemId;

        public uint[] RewardChoiceItemCount;

        public uint[] RewardFactionId;

        public uint[] RewardReputationId;

        public uint[] RewardReputationIdOverride; // ?

        public uint PointMapId;

        public float PointX;

        public float PointY;

        public uint PointOption;

        public string Title;

        public string Objectives;

        public string Details;

        public string EndText;

        public string ReturnText;

        public int[] RequiredNpcOrGo;

        public uint[] RequiredNpcOrGoCount;

        public uint[] RequiredSourceItemId;

        public uint[] RequiredSourceItemCount;

        public uint[] RequiredItemId;

        public uint[] RequiredItemCount;

        public uint RequiredSpell;

        public string[] ObjectiveTexts;

        public uint[] RewardCurrencyId;

        public uint[] RewardCurrencyCount;

        public uint[] RequiredCurrencyId;

        public uint[] RequiredCurrencyCount;

        public string QuestGiverTextWindow;

        public string QuestGiverTextName;

        public string QuestTurnTextWindow;

        public string QuestTurnTargetName;

        public uint SoundAccept;

        public uint SoundTurnIn;

    }
}
