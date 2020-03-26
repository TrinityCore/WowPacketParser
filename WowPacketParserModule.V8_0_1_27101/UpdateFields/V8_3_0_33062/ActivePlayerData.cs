using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_3_0_33062
{
    public class ActivePlayerData : IActivePlayerData
    {
        public WowGuid[] InvSlots { get; } = new WowGuid[199];
        public WowGuid FarsightObject { get; set; }
        public WowGuid SummonedBattlePetGUID { get; set; }
        public ulong Coinage { get; set; }
        public int XP { get; set; }
        public int NextLevelXP { get; set; }
        public int TrialXP { get; set; }
        public ISkillInfo Skill { get; set; }
        public int CharacterPoints { get; set; }
        public int MaxTalentTiers { get; set; }
        public int TrackCreatureMask { get; set; }
        public uint[] TrackResourceMask { get; } = new uint[2];
        public float MainhandExpertise { get; set; }
        public float OffhandExpertise { get; set; }
        public float RangedExpertise { get; set; }
        public float CombatRatingExpertise { get; set; }
        public float BlockPercentage { get; set; }
        public float DodgePercentage { get; set; }
        public float DodgePercentageFromAttribute { get; set; }
        public float ParryPercentage { get; set; }
        public float ParryPercentageFromAttribute { get; set; }
        public float CritPercentage { get; set; }
        public float RangedCritPercentage { get; set; }
        public float OffhandCritPercentage { get; set; }
        public float SpellCritPercentage { get; set; }
        public int ShieldBlock { get; set; }
        public float ShieldBlockCritPercentage { get; set; }
        public float Mastery { get; set; }
        public float Speed { get; set; }
        public float Avoidance { get; set; }
        public float Sturdiness { get; set; }
        public int Versatility { get; set; }
        public float VersatilityBonus { get; set; }
        public float PvpPowerDamage { get; set; }
        public float PvpPowerHealing { get; set; }
        public ulong[] ExploredZones { get; } = new ulong[192];
        public IRestInfo[] RestInfo { get; } = new IRestInfo[2];
        public int[] ModDamageDonePos { get; } = new int[7];
        public int[] ModDamageDoneNeg { get; } = new int[7];
        public float[] ModDamageDonePercent { get; } = new float[7];
        public int ModHealingDonePos { get; set; }
        public float ModHealingPercent { get; set; }
        public float ModHealingDonePercent { get; set; }
        public float ModPeriodicHealingDonePercent { get; set; }
        public float[] WeaponDmgMultipliers { get; } = new float[3];
        public float[] WeaponAtkSpeedMultipliers { get; } = new float[3];
        public float ModSpellPowerPercent { get; set; }
        public float ModResiliencePercent { get; set; }
        public float OverrideSpellPowerByAPPercent { get; set; }
        public float OverrideAPBySpellPowerPercent { get; set; }
        public int ModTargetResistance { get; set; }
        public int ModTargetPhysicalResistance { get; set; }
        public int LocalFlags { get; set; }
        public byte GrantableLevels { get; set; }
        public byte MultiActionBars { get; set; }
        public byte LifetimeMaxRank { get; set; }
        public byte NumRespecs { get; set; }
        public uint PvpMedals { get; set; }
        public uint[] BuybackPrice { get; } = new uint[12];
        public uint[] BuybackTimestamp { get; } = new uint[12];
        public ushort TodayHonorableKills { get; set; }
        public ushort YesterdayHonorableKills { get; set; }
        public uint LifetimeHonorableKills { get; set; }
        public int WatchedFactionIndex { get; set; }
        public int[] CombatRatings { get; } = new int[32];
        public int MaxLevel { get; set; }
        public int ScalingPlayerLevelDelta { get; set; }
        public int MaxCreatureScalingLevel { get; set; }
        public uint[] NoReagentCostMask { get; } = new uint[4];
        public int PetSpellPower { get; set; }
        public int[] ProfessionSkillLine { get; } = new int[2];
        public float UiHitModifier { get; set; }
        public float UiSpellHitModifier { get; set; }
        public int HomeRealmTimeOffset { get; set; }
        public float ModPetHaste { get; set; }
        public byte LocalRegenFlags { get; set; }
        public byte AuraVision { get; set; }
        public byte NumBackpackSlots { get; set; }
        public int OverrideSpellsID { get; set; }
        public int LfgBonusFactionID { get; set; }
        public ushort LootSpecID { get; set; }
        public uint OverrideZonePVPType { get; set; }
        public uint[] BagSlotFlags { get; } = new uint[4];
        public uint[] BankBagSlotFlags { get; } = new uint[7];
        public ulong[] QuestCompleted { get; } = new ulong[875];
        public int Honor { get; set; }
        public int HonorNextLevel { get; set; }
        public int PvpRewardAchieved { get; set; }
        public int PvpTierMaxFromWins { get; set; }
        public int PvpLastWeeksRewardAchieved { get; set; }
        public int PvpLastWeeksTierMaxFromWins { get; set; }
        public int PvpLastWeeksRewardClaimed { get; set; }
        public byte NumBankSlots { get; set; }
        public DynamicUpdateField<IResearch>[] Research { get; } = new DynamicUpdateField<IResearch>[1] { new DynamicUpdateField<IResearch>() };
        public DynamicUpdateField<ulong> KnownTitles { get; } = new DynamicUpdateField<ulong>();
        public DynamicUpdateField<ushort> ResearchSites { get; } = new DynamicUpdateField<ushort>();
        public DynamicUpdateField<uint> ResearchSiteProgress { get; } = new DynamicUpdateField<uint>();
        public DynamicUpdateField<int> DailyQuestsCompleted { get; } = new DynamicUpdateField<int>();
        public DynamicUpdateField<int> AvailableQuestLineXQuestIDs { get; } = new DynamicUpdateField<int>();
        public DynamicUpdateField<int> Heirlooms { get; } = new DynamicUpdateField<int>();
        public DynamicUpdateField<uint> HeirloomFlags { get; } = new DynamicUpdateField<uint>();
        public DynamicUpdateField<int> Toys { get; } = new DynamicUpdateField<int>();
        public DynamicUpdateField<uint> ToyFlags { get; } = new DynamicUpdateField<uint>();
        public DynamicUpdateField<uint> Transmog { get; } = new DynamicUpdateField<uint>();
        public DynamicUpdateField<int> ConditionalTransmog { get; } = new DynamicUpdateField<int>();
        public DynamicUpdateField<int> SelfResSpells { get; } = new DynamicUpdateField<int>();
        public DynamicUpdateField<ISpellPctModByLabel> SpellPctModByLabel { get; } = new DynamicUpdateField<ISpellPctModByLabel>();
        public DynamicUpdateField<ISpellFlatModByLabel> SpellFlatModByLabel { get; } = new DynamicUpdateField<ISpellFlatModByLabel>();
        public DynamicUpdateField<IReplayedQuest> ReplayedQuests { get; } = new DynamicUpdateField<IReplayedQuest>();
        public DynamicUpdateField<int> DisabledSpells { get; } = new DynamicUpdateField<int>();
        public IPVPInfo[] PvpInfo { get; } = new IPVPInfo[6];
        public bool BackpackAutoSortDisabled { get; set; }
        public bool BankAutoSortDisabled { get; set; }
        public bool SortBagsRightToLeft { get; set; }
        public bool InsertItemsLeftToRight { get; set; }
        public DynamicUpdateField<ICharacterRestriction> CharacterRestrictions { get; } = new DynamicUpdateField<ICharacterRestriction>();
        public IQuestSession QuestSession { get; set; }
    }
}

