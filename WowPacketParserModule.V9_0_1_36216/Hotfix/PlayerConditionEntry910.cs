using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_1_0_39185.Hotfix
{
    [HotfixStructure(DB2Hash.PlayerCondition, ClientVersionBuild.V9_1_0_39185, HasIndexInData = false)]
    public class PlayerConditionEntry
    {
        public long RaceMask { get; set; }
        public string FailureDescription { get; set; }
        public int ClassMask { get; set; }
        public uint SkillLogic { get; set; }
        public int LanguageID { get; set; }
        public byte MinLanguage { get; set; }
        public int MaxLanguage { get; set; }
        public ushort MaxFactionID { get; set; }
        public byte MaxReputation { get; set; }
        public uint ReputationLogic { get; set; }
        public sbyte CurrentPvpFaction { get; set; }
        public byte PvpMedal { get; set; }
        public uint PrevQuestLogic { get; set; }
        public uint CurrQuestLogic { get; set; }
        public uint CurrentCompletedQuestLogic { get; set; }
        public uint SpellLogic { get; set; }
        public uint ItemLogic { get; set; }
        public byte ItemFlags { get; set; }
        public uint AuraSpellLogic { get; set; }
        public ushort WorldStateExpressionID { get; set; }
        public int WeatherID { get; set; }
        public byte PartyStatus { get; set; }
        public byte LifetimeMaxPVPRank { get; set; }
        public uint AchievementLogic { get; set; }
        public sbyte Gender { get; set; }
        public sbyte NativeGender { get; set; }
        public uint AreaLogic { get; set; }
        public uint LfgLogic { get; set; }
        public uint CurrencyLogic { get; set; }
        public int QuestKillID { get; set; }
        public uint QuestKillLogic { get; set; }
        public sbyte MinExpansionLevel { get; set; }
        public sbyte MaxExpansionLevel { get; set; }
        public int MinAvgItemLevel { get; set; }
        public int MaxAvgItemLevel { get; set; }
        public ushort MinAvgEquippedItemLevel { get; set; }
        public ushort MaxAvgEquippedItemLevel { get; set; }
        public byte PhaseUseFlags { get; set; }
        public ushort PhaseID { get; set; }
        public uint PhaseGroupID { get; set; }
        public int Flags { get; set; }
        public sbyte ChrSpecializationIndex { get; set; }
        public sbyte ChrSpecializationRole { get; set; }
        public uint ModifierTreeID { get; set; }
        public sbyte PowerType { get; set; }
        public byte PowerTypeComp { get; set; }
        public byte PowerTypeValue { get; set; }
        public int WeaponSubclassMask { get; set; }
        public byte MaxGuildLevel { get; set; }
        public byte MinGuildLevel { get; set; }
        public sbyte MaxExpansionTier { get; set; }
        public sbyte MinExpansionTier { get; set; }
        public byte MinPVPRank { get; set; }
        public byte MaxPVPRank { get; set; }
        public int ContentTuningID { get; set; }
        public int CovenantID { get; set; }
        [HotfixArray(4)]
        public ushort[] SkillID { get; set; }
        [HotfixArray(4)]
        public ushort[] MinSkill { get; set; }
        [HotfixArray(4)]
        public ushort[] MaxSkill { get; set; }
        [HotfixArray(3)]
        public uint[] MinFactionID { get; set; }
        [HotfixArray(3)]
        public byte[] MinReputation { get; set; }
        [HotfixArray(4)]
        public int[] PrevQuestID { get; set; }
        [HotfixArray(4)]
        public int[] CurrQuestID { get; set; }
        [HotfixArray(4)]
        public int[] CurrentCompletedQuestID { get; set; }
        [HotfixArray(4)]
        public int[] SpellID { get; set; }
        [HotfixArray(4)]
        public int[] ItemID { get; set; }
        [HotfixArray(4)]
        public uint[] ItemCount { get; set; }
        [HotfixArray(2)]
        public ushort[] Explored { get; set; }
        [HotfixArray(2)]
        public uint[] Time { get; set; }
        [HotfixArray(4)]
        public int[] AuraSpellID { get; set; }
        [HotfixArray(4)]
        public byte[] AuraStacks { get; set; }
        [HotfixArray(4)]
        public ushort[] Achievement { get; set; }
        [HotfixArray(4)]
        public ushort[] AreaID { get; set; }
        [HotfixArray(4)]
        public byte[] LfgStatus { get; set; }
        [HotfixArray(4)]
        public byte[] LfgCompare { get; set; }
        [HotfixArray(4)]
        public uint[] LfgValue { get; set; }
        [HotfixArray(4)]
        public uint[] CurrencyID { get; set; }
        [HotfixArray(4)]
        public uint[] CurrencyCount { get; set; }
        [HotfixArray(6)]
        public uint[] QuestKillMonster { get; set; }
        [HotfixArray(2)]
        public int[] MovementFlags { get; set; }
    }
}
