using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects.UpdateFields
{
    public interface IUnitData
    {
        int DisplayID { get; }
        uint[] NpcFlags { get; }
        WowGuid SummonedBy { get; }
        WowGuid CreatedBy { get; }
        byte ClassId { get; }
        byte Sex { get; }
        int Level { get; }
        int ScalingLevelMin { get; }
        int ScalingLevelMax { get; }
        int ScalingLevelDelta { get; }
        int FactionTemplate { get; }
        IVisibleItem[] VirtualItems { get; }
        uint Flags { get; }
        uint Flags2 { get; }
        uint Flags3 { get; }
        uint[] AttackRoundBaseTime { get; }
        uint RangedAttackRoundBaseTime { get; }
        float BoundingRadius { get; }
        float CombatReach { get; }
        int MountDisplayID { get; }
        byte StandState { get; }
        byte PetTalentPoints { get; }
        byte VisFlags { get; }
        byte AnimTier { get; }
        int CreatedBySpell { get; }
        byte SheatheState { get; }
        byte PvpFlags { get; }
        byte PetFlags { get; }
        byte ShapeshiftForm { get; }
        float HoverHeight { get; }
        int InteractSpellID { get; }
    }
}
