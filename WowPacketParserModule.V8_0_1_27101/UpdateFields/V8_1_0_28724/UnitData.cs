using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_1_0_28724
{
    public class UnitData : IMutableUnitData
    {
        public int? DisplayID { get; set; }
        public uint?[] NpcFlags { get; } = new uint?[2];
        public System.Nullable<uint> StateSpellVisualID { get; set; }
        public System.Nullable<uint> StateAnimID { get; set; }
        public System.Nullable<uint> StateAnimKitID { get; set; }
        public System.Nullable<uint> StateWorldEffectsQuestObjectiveID { get; set; }
        public System.Nullable<uint>[] StateWorldEffectIDs { get; set; }
        public WowGuid Charm { get; set; }
        public WowGuid Summon { get; set; }
        public WowGuid Critter { get; set; }
        public WowGuid CharmedBy { get; set; }
        public WowGuid SummonedBy { get; set; }
        public WowGuid CreatedBy { get; set; }
        public WowGuid DemonCreator { get; set; }
        public WowGuid LookAtControllerTarget { get; set; }
        public WowGuid Target { get; set; }
        public WowGuid BattlePetCompanionGUID { get; set; }
        public ulong BattlePetDBID { get; set; }
        public IUnitChannel ChannelData { get; set; }
        public uint SummonedByHomeRealm { get; set; }
        public byte? Race { get; set; }
        public byte? ClassId { get; set; }
        public byte PlayerClassId { get; set; }
        public byte? Sex { get; set; }
        public byte? DisplayPower { get; set; }
        public uint OverrideDisplayPowerID { get; set; }
        public long? Health { get; set; }
        public int?[] Power { get; } = new int?[6];
        public int?[] MaxPower { get; } = new int?[6];
        public float[] PowerRegenFlatModifier { get; } = new float[6];
        public float[] PowerRegenInterruptedFlatModifier { get; } = new float[6];
        public long? MaxHealth { get; set; }
        public int? Level { get; set; }
        public int? EffectiveLevel { get; set; }
        public int? ContentTuningID { get; set; }
        public int? ScalingLevelMin { get; set; }
        public int? ScalingLevelMax { get; set; }
        public int? ScalingLevelDelta { get; set; }
        public int ScalingFactionGroup { get; set; }
        public int ScalingHealthItemLevelCurveID { get; set; }
        public int ScalingDamageItemLevelCurveID { get; set; }
        public int? FactionTemplate { get; set; }
        public IVisibleItem[] VirtualItems { get; } = new IVisibleItem[3];
        public uint? Flags { get; set; }
        public uint? Flags2 { get; set; }
        public uint? Flags3 { get; set; }
        public uint? AuraState { get; set; }
        public uint?[] AttackRoundBaseTime { get; } = new uint?[2];
        public uint? RangedAttackRoundBaseTime { get; set; }
        public float? BoundingRadius { get; set; }
        public float? CombatReach { get; set; }
        public float? DisplayScale { get; set; }
        public int? NativeDisplayID { get; set; }
        public float? NativeXDisplayScale { get; set; }
        public int? MountDisplayID { get; set; }
        public int CosmeticMountDisplayID { get; set; }
        public float MinDamage { get; set; }
        public float MaxDamage { get; set; }
        public float MinOffHandDamage { get; set; }
        public float MaxOffHandDamage { get; set; }
        public byte? StandState { get; set; }
        public byte? PetTalentPoints { get; set; }
        public byte? VisFlags { get; set; }
        public byte? AnimTier { get; set; }
        public uint PetNumber { get; set; }
        public uint PetNameTimestamp { get; set; }
        public uint PetExperience { get; set; }
        public uint PetNextLevelExperience { get; set; }
        public float ModCastingSpeed { get; set; }
        public float ModSpellHaste { get; set; }
        public float ModHaste { get; set; }
        public float ModRangedHaste { get; set; }
        public float ModHasteRegen { get; set; }
        public float ModTimeRate { get; set; }
        public int? CreatedBySpell { get; set; }
        public int? EmoteState { get; set; }
        public int[] Stats { get; } = new int[4];
        public int[] StatPosBuff { get; } = new int[4];
        public int[] StatNegBuff { get; } = new int[4];
        public int?[] Resistances { get; } = new int?[7];
        public int[] BonusResistanceMods { get; } = new int[7];
        public int[] PowerCostModifier { get; } = new int[7];
        public float[] PowerCostMultiplier { get; } = new float[7];
        public int? BaseMana { get; set; }
        public int? BaseHealth { get; set; }
        public byte? SheatheState { get; set; }
        public byte? PvpFlags { get; set; }
        public byte? PetFlags { get; set; }
        public byte? ShapeshiftForm { get; set; }
        public int AttackPower { get; set; }
        public int AttackPowerModPos { get; set; }
        public int AttackPowerModNeg { get; set; }
        public float AttackPowerMultiplier { get; set; }
        public int RangedAttackPower { get; set; }
        public int RangedAttackPowerModPos { get; set; }
        public int RangedAttackPowerModNeg { get; set; }
        public float RangedAttackPowerMultiplier { get; set; }
        public int MainHandWeaponAttackPower { get; set; }
        public int OffHandWeaponAttackPower { get; set; }
        public int RangedWeaponAttackPower { get; set; }
        public int SetAttackSpeedAura { get; set; }
        public float Lifesteal { get; set; }
        public float MinRangedDamage { get; set; }
        public float MaxRangedDamage { get; set; }
        public float ManaCostModifierModifier { get; set; }
        public float MaxHealthModifier { get; set; }
        public float? HoverHeight { get; set; }
        public int MinItemLevelCutoff { get; set; }
        public int MinItemLevel { get; set; }
        public int MaxItemLevel { get; set; }
        public int WildBattlePetLevel { get; set; }
        public uint BattlePetCompanionNameTimestamp { get; set; }
        public int? InteractSpellID { get; set; }
        public int ScaleDuration { get; set; }
        public int SpellOverrideNameID { get; set; }
        public int LooksLikeMountID { get; set; }
        public int LooksLikeCreatureID { get; set; }
        public int LookAtControllerID { get; set; }
        public WowGuid GuildGUID { get; set; }
        public DynamicUpdateField<IPassiveSpellHistory> PassiveSpells { get; } = new DynamicUpdateField<IPassiveSpellHistory>();
        public DynamicUpdateField<int> WorldEffects { get; } = new DynamicUpdateField<int>();
        public DynamicUpdateField<WowGuid> ChannelObjects { get; } = new DynamicUpdateField<WowGuid>();
        public int? CreatureFamily { get; set; }
        public int? CreatureType { get; set; }
    }
}

