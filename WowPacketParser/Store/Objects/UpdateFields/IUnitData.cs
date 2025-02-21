using System;
using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects.UpdateFields
{
    // when adding new properties, remember to include them in
    // IMutableUnitData and in Extensions.UpdateData
    public interface IUnitData
    {
        int? DisplayID { get; }
        uint?[] NpcFlags { get; }
        WowGuid SummonedBy { get; }
        WowGuid CreatedBy { get; }
        byte? ClassId { get; }
        byte? Sex { get; }
        long? Health { get; }
        int?[] Power { get; }
        int?[] MaxPower { get; }
        long? MaxHealth { get; }
        int? Level { get; }
        int? ContentTuningID { get; }
        int? ScalingLevelMin { get; }
        int? ScalingLevelMax { get; }
        int? ScalingLevelDelta { get; }
        int? FactionTemplate { get; }
        IVisibleItem[] VirtualItems { get; }
        uint? Flags { get; }
        uint? Flags2 { get; }
        uint? Flags3 { get; }
        uint?[] AttackRoundBaseTime { get; }
        uint? RangedAttackRoundBaseTime { get; }
        float? BoundingRadius { get; }
        float? CombatReach { get; }
        int? MountDisplayID { get; }
        byte? StandState { get; }
        byte? PetTalentPoints { get; }
        byte? VisFlags { get; }
        byte? AnimTier { get; }
        int? CreatedBySpell { get; }
        int? EmoteState { get; }
        byte? SheatheState { get; }
        byte? PvpFlags { get; }
        byte? PetFlags { get; }
        byte? ShapeshiftForm { get; }
        float? HoverHeight { get; }
        int? InteractSpellID { get; }
        uint? StateSpellVisualID { get; }
        uint? StateAnimID { get; }
        uint? StateAnimKitID { get; }
        uint? StateWorldEffectsQuestObjectiveID { get; }
        uint?[] StateWorldEffectIDs { get; }
        WowGuid Charm { get; }
        WowGuid Summon { get; }
        WowGuid Critter { get; }
        WowGuid CharmedBy { get; }
        WowGuid DemonCreator { get; }
        WowGuid LookAtControllerTarget { get; }
        WowGuid Target { get; }
        byte? Race { get; }
        byte? DisplayPower { get; }
        int? EffectiveLevel { get; }
        uint? AuraState { get; }
        float? DisplayScale { get; }
        int? CreatureFamily { get; }
        int? CreatureType { get; }
        int? NativeDisplayID { get; }
        float? NativeXDisplayScale { get; }
        int?[] Resistances { get; }
        int? BaseMana { get; }
        int? BaseHealth { get; }
    }

    public interface IMutableUnitData : IUnitData
    {
        new int? DisplayID { get; set; }
        new WowGuid SummonedBy { get; set; }
        new WowGuid CreatedBy { get; set; }
        new byte? ClassId { get; set; }
        new byte? Sex { get; set; }
        new long? Health { get; set; }
        new long? MaxHealth { get; set; }
        new int? Level { get; set; }
        new int? ContentTuningID { get; set; }
        new int? ScalingLevelMin { get; set; }
        new int? ScalingLevelMax { get; set; }
        new int? ScalingLevelDelta { get; set; }
        new int? FactionTemplate { get; set; }
        new uint? Flags { get; set; }
        new uint? Flags2 { get; set; }
        new uint? Flags3 { get; set; }
        new uint? RangedAttackRoundBaseTime { get; set; }
        new float? BoundingRadius { get; set; }
        new float? CombatReach { get; set; }
        new int? MountDisplayID { get; set; }
        new byte? StandState { get; set; }
        new byte? PetTalentPoints { get; set; }
        new byte? VisFlags { get; set; }
        new byte? AnimTier { get; set; }
        new int? CreatedBySpell { get; set; }
        new int? EmoteState { get; set; }
        new byte? SheatheState { get; set; }
        new byte? PvpFlags { get; set; }
        new byte? PetFlags { get; set; }
        new byte? ShapeshiftForm { get; set; }
        new float? HoverHeight { get; set; }
        new int? InteractSpellID { get; set; }
        new uint? StateSpellVisualID { get; set; }
        new uint? StateAnimID { get; set; }
        new uint? StateAnimKitID { get; set; }
        new uint? StateWorldEffectsQuestObjectiveID { get; set; }
        new uint?[] StateWorldEffectIDs { get; set; }
        new WowGuid Charm { get; set; }
        new WowGuid Summon { get; set; }
        new WowGuid Critter { get; set; }
        new WowGuid CharmedBy { get; set; }
        new WowGuid DemonCreator { get; set; }
        new WowGuid LookAtControllerTarget { get; set; }
        new WowGuid Target { get; set; }
        new byte? Race { get; set; }
        new byte? DisplayPower { get; set; }
        new int? EffectiveLevel { get; set; }
        new uint? AuraState { get; set; }
        new float? DisplayScale { get; set; }
        new int? CreatureFamily { get; set; }
        new int? CreatureType { get; set; }
        new int? NativeDisplayID { get; set; }
        new float? NativeXDisplayScale { get; set; }
        new int? BaseMana { get; set; }
        new int? BaseHealth { get; set; }
    }

    public static partial class Extensions
    {
        public static void UpdateData(this IMutableUnitData data, IUnitData update)
        {
            data.DisplayID = update.DisplayID ?? data.DisplayID;
            data.Flags = update.Flags ?? data.Flags;
            data.Flags2 = update.Flags2 ?? data.Flags2;
            data.Flags3 = update.Flags3 ?? data.Flags3;
            data.RangedAttackRoundBaseTime = update.RangedAttackRoundBaseTime ?? data.RangedAttackRoundBaseTime;
            data.BoundingRadius = update.BoundingRadius ?? data.BoundingRadius;
            data.CombatReach = update.CombatReach ?? data.CombatReach;
            data.MountDisplayID = update.MountDisplayID ?? data.MountDisplayID;
            data.StandState = update.StandState ?? data.StandState;
            data.PetTalentPoints = update.PetTalentPoints ?? data.PetTalentPoints;
            data.VisFlags = update.VisFlags ?? data.VisFlags;
            data.AnimTier = update.AnimTier ?? data.AnimTier;
            data.CreatedBySpell = update.CreatedBySpell ?? data.CreatedBySpell;
            data.EmoteState = update.EmoteState ?? data.EmoteState;
            data.SheatheState = update.SheatheState ?? data.SheatheState;
            data.PvpFlags = update.PvpFlags ?? data.PvpFlags;
            data.PetFlags = update.PetFlags ?? data.PetFlags;
            data.ShapeshiftForm = update.ShapeshiftForm ?? data.ShapeshiftForm;
            data.HoverHeight = update.HoverHeight ?? data.HoverHeight;
            data.InteractSpellID = update.InteractSpellID ?? data.InteractSpellID;
            data.ScalingLevelMin = update.ScalingLevelMin ?? data.ScalingLevelMin;
            data.ScalingLevelMax = update.ScalingLevelMax ?? data.ScalingLevelMax;
            data.StateSpellVisualID = update.StateSpellVisualID ?? data.StateSpellVisualID;
            data.StateAnimID = update.StateAnimID ?? data.StateAnimID;
            data.StateAnimKitID = update.StateAnimKitID ?? data.StateAnimKitID;
            data.StateWorldEffectsQuestObjectiveID = update.StateWorldEffectsQuestObjectiveID ?? data.StateWorldEffectsQuestObjectiveID;
            data.StateWorldEffectIDs = update.StateWorldEffectIDs ?? data.StateWorldEffectIDs;
            data.Charm = update.Charm ?? data.Charm;
            data.Summon = update.Summon ?? data.Summon;
            data.Critter = update.Critter ?? data.Critter;
            data.CharmedBy = update.CharmedBy ?? data.CharmedBy;
            data.DemonCreator = update.DemonCreator ?? data.DemonCreator;
            data.LookAtControllerTarget = update.LookAtControllerTarget ?? data.LookAtControllerTarget;
            data.Target = update.Target ?? data.Target;
            data.Race = update.Race ?? data.Race;
            data.DisplayPower = update.DisplayPower ?? data.DisplayPower;
            data.EffectiveLevel = update.EffectiveLevel ?? data.EffectiveLevel;
            data.AuraState = update.AuraState ?? data.AuraState;
            data.DisplayScale = update.DisplayScale ?? data.DisplayScale;
            data.CreatureFamily = update.CreatureFamily ?? data.CreatureFamily;
            data.CreatureType = update.CreatureType ?? data.CreatureType;
            data.NativeDisplayID = update.NativeDisplayID ?? data.NativeDisplayID;
            data.NativeXDisplayScale = update.NativeXDisplayScale ?? data.NativeXDisplayScale;
            data.BaseMana = update.BaseMana ?? data.BaseMana;
            data.BaseHealth = update.BaseHealth ?? data.BaseHealth;
            data.SummonedBy = update.SummonedBy ?? data.SummonedBy;
            data.CreatedBy = update.CreatedBy ?? data.CreatedBy;
            data.ClassId = update.ClassId ?? data.ClassId;
            data.Sex = update.Sex ?? data.Sex;
            data.Health = update.Health ?? data.Health;
            data.MaxHealth = update.MaxHealth ?? data.MaxHealth;
            data.Level = update.Level ?? data.Level;
            data.ContentTuningID = update.ContentTuningID ?? data.ContentTuningID;
            data.ScalingLevelDelta = update.ScalingLevelDelta ?? data.ScalingLevelDelta;
            data.FactionTemplate = update.FactionTemplate ?? data.FactionTemplate;

            for (int i = 0; i < Math.Min(data.NpcFlags.Length, update.NpcFlags.Length); i++)
                data.NpcFlags[i] = update.NpcFlags[i] ?? data.NpcFlags[i];
            for (int i = 0; i < Math.Min(data.Power.Length, update.Power.Length); i++)
                data.Power[i] = update.Power[i] ?? data.Power[i];
            for (int i = 0; i < Math.Min(data.MaxPower.Length, update.MaxPower.Length); i++)
                data.MaxPower[i] = update.MaxPower[i] ?? data.MaxPower[i];
            for (int i = 0; i < Math.Min(data.AttackRoundBaseTime.Length, update.AttackRoundBaseTime.Length); i++)
                data.AttackRoundBaseTime[i] = update.AttackRoundBaseTime[i] ?? data.AttackRoundBaseTime[i];
            for (int i = 0; i < Math.Min(data.Resistances.Length, update.Resistances.Length); i++)
                data.Resistances[i] = update.Resistances[i] ?? data.Resistances[i];
            for (int i = 0; i < Math.Min(data.VirtualItems.Length, update.VirtualItems.Length); i++)
            {
                if (update.VirtualItems[i] != null)
                {
                    if (data.VirtualItems[i] == null)
                        data.VirtualItems[i] = update.VirtualItems[i];
                    else if (data.VirtualItems[i] is IMutableVisibleItem mut)
                        mut.UpdateData(update.VirtualItems[i]);
                }
            }
        }
    }
}
