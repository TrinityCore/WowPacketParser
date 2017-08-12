using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.PlayerCondition, HasIndexInData = false)]
    public class PlayerConditionEntry
    {
        public uint RaceMask { get; set; }
        public uint SkillLogic { get; set; }
        public uint ReputationLogic { get; set; }
        public uint PrevQuestLogic { get; set; }
        public uint CurrQuestLogic { get; set; }
        public uint CurrentCompletedQuestLogic { get; set; }
        public uint SpellLogic { get; set; }
        public uint ItemLogic { get; set; }
        [HotfixArray(2)]
        public uint[] Time { get; set; }
        public uint AuraSpellLogic { get; set; }
        [HotfixArray(4)]
        public uint[] AuraSpellID { get; set; }
        public uint AchievementLogic { get; set; }
        public uint AreaLogic { get; set; }
        public uint QuestKillLogic { get; set; }
        public string FailureDescription { get; set; }
        public ushort MinLevel { get; set; }
        public ushort MaxLevel { get; set; }
        [HotfixArray(4)]
        public ushort[] SkillID { get; set; }
        [HotfixArray(4)]
        public short[] MinSkill { get; set; }
        [HotfixArray(4)]
        public short[] MaxSkill { get; set; }
        public ushort MaxFactionID { get; set; }
        [HotfixArray(4)]
        public ushort[] PrevQuestID { get; set; }
        [HotfixArray(4)]
        public ushort[] CurrQuestID { get; set; }
        [HotfixArray(4)]
        public ushort[] CurrentCompletedQuestID { get; set; }
        [HotfixArray(2)]
        public ushort[] Explored { get; set; }
        public ushort WorldStateExpressionID { get; set; }
        [HotfixArray(4)]
        public ushort[] Achievement { get; set; }
        [HotfixArray(4)]
        public ushort[] AreaID { get; set; }
        public ushort QuestKillID { get; set; }
        public ushort PhaseID { get; set; }
        public ushort MinAvgEquippedItemLevel { get; set; }
        public ushort MaxAvgEquippedItemLevel { get; set; }
        public ushort ModifierTreeID { get; set; }
        public byte Flags { get; set; }
        public sbyte Gender { get; set; }
        public sbyte NativeGender { get; set; }
        public byte MinLanguage { get; set; }
        public byte MaxLanguage { get; set; }
        [HotfixArray(3)]
        public byte[] MinReputation { get; set; }
        public byte MaxReputation { get; set; }
        public byte Unknown1 { get; set; }
        public byte MinPVPRank { get; set; }
        public byte MaxPVPRank { get; set; }
        public byte PvpMedal { get; set; }
        public byte ItemFlags { get; set; }
        [HotfixArray(4)]
        public byte[] AuraCount { get; set; }
        public byte WeatherID { get; set; }
        public byte PartyStatus { get; set; }
        public byte LifetimeMaxPVPRank { get; set; }
        [HotfixArray(4)]
        public byte[] LfgStatus { get; set; }
        [HotfixArray(4)]
        public byte[] LfgCompare { get; set; }
        [HotfixArray(4)]
        public byte[] CurrencyCount { get; set; }
        public sbyte MinExpansionLevel { get; set; }
        public sbyte MaxExpansionLevel { get; set; }
        public sbyte MinExpansionTier { get; set; }
        public sbyte MaxExpansionTier { get; set; }
        public byte MinGuildLevel { get; set; }
        public byte MaxGuildLevel { get; set; }
        public byte PhaseUseFlags { get; set; }
        public sbyte ChrSpecializationIndex { get; set; }
        public sbyte ChrSpecializationRole { get; set; }
        public sbyte PowerType { get; set; }
        public sbyte PowerTypeComp { get; set; }
        public sbyte PowerTypeValue { get; set; }
        public uint ClassMask { get; set; }
        public uint LanguageID { get; set; }
        [HotfixArray(3)]
        public uint[] MinFactionID { get; set; }
        [HotfixArray(4)]
        public uint[] SpellID { get; set; }
        [HotfixArray(4)]
        public uint[] ItemID { get; set; }
        [HotfixArray(4)]
        public uint[] ItemCount { get; set; }
        public uint LfgLogic { get; set; }
        [HotfixArray(4)]
        public uint[] LfgValue { get; set; }
        public uint CurrencyLogic { get; set; }
        [HotfixArray(4)]
        public uint[] CurrencyID { get; set; }
        [HotfixArray(6)]
        public uint[] QuestKillMonster { get; set; }
        public uint PhaseGroupID { get; set; }
        public uint MinAvgItemLevel { get; set; }
        public uint MaxAvgItemLevel { get; set; }
        [HotfixArray(2)]
        public uint[] MovementFlags { get; set; }
        [HotfixVersion(ClientVersionBuild.V7_2_0_23826, false)]
        public uint MainHandItemSubclassMask { get; set; }
    }
}