
using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects.UpdateFields
{
    // when adding new properties, remember to include them in
    // IMutableGameObjectData and in Extensions.UpdateData
    public interface IGameObjectData
    {
        WowGuid CreatedBy { get; }
        uint? Flags { get; }
        Quaternion? ParentRotation { get; }
        int? FactionTemplate { get; }
        sbyte? State { get; }
        sbyte? TypeID { get; }
        byte? PercentHealth { get; }
        int? DisplayID { get; }
        uint? ArtKit { get; }
        int? Level { get; }
        uint? SpellVisualID { get; }
        uint? StateSpellVisualID { get; }
        uint? SpawnTrackingStateAnimID { get; }
        uint? SpawnTrackingStateAnimKitID { get; }
        uint? StateWorldEffectsQuestObjectiveID { get; }
        uint?[] StateWorldEffectIDs { get; }
    }

    public interface IMutableGameObjectData : IGameObjectData
    {
        new WowGuid CreatedBy { get; set; }
        new uint? Flags { get; set; }
        new Quaternion? ParentRotation { get; set; }
        new int? FactionTemplate { get; set; }
        new sbyte? State { get; set; }
        new sbyte? TypeID { get; set; }
        new byte? PercentHealth { get; set; }
        new int? DisplayID { get; set; }
        new uint? ArtKit { get; set; }
        new int? Level { get; set; }
        new uint? SpellVisualID { get; set; }
        new uint? StateSpellVisualID { get; set; }
        new uint? SpawnTrackingStateAnimID { get; set; }
        new uint? SpawnTrackingStateAnimKitID { get; set; }
        new uint? StateWorldEffectsQuestObjectiveID { get; set; }
        new uint?[] StateWorldEffectIDs { get; set; }
    }

    public static partial class Extensions
    {
        public static void UpdateData(this IMutableGameObjectData data, IGameObjectData update)
        {
            data.CreatedBy = update.CreatedBy ?? data.CreatedBy;
            data.Flags = update.Flags ?? data.Flags;
            data.ParentRotation = update.ParentRotation ?? data.ParentRotation;
            data.FactionTemplate = update.FactionTemplate ?? data.FactionTemplate;
            data.State = update.State ?? data.State;
            data.TypeID = update.TypeID ?? data.TypeID;
            data.PercentHealth = update.PercentHealth ?? data.PercentHealth;
            data.DisplayID = update.DisplayID ?? data.DisplayID;
            data.ArtKit = update.ArtKit ?? data.ArtKit;
            data.Level = update.Level ?? data.Level;
            data.SpellVisualID = update.SpellVisualID ?? data.SpellVisualID;
            data.StateSpellVisualID = update.StateSpellVisualID ?? data.StateSpellVisualID;
            data.SpawnTrackingStateAnimID = update.SpawnTrackingStateAnimID ?? data.SpawnTrackingStateAnimID;
            data.SpawnTrackingStateAnimKitID = update.SpawnTrackingStateAnimKitID ?? data.SpawnTrackingStateAnimKitID;
            data.StateWorldEffectsQuestObjectiveID = update.StateWorldEffectsQuestObjectiveID ?? data.StateWorldEffectsQuestObjectiveID;
            data.StateWorldEffectIDs = update.StateWorldEffectIDs ?? data.StateWorldEffectIDs;
        }
    }
}
