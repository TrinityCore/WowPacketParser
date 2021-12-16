#nullable enable

using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects.UpdateFields
{
    // when adding new properties, remember to include them in 
    // IMutableGameObjectData and in Extensions.UpdateData
    public interface IGameObjectData
    {
        WowGuid? CreatedBy { get; }
        uint? Flags { get; }
        Quaternion? ParentRotation { get; }
        int? FactionTemplate { get; }
        sbyte? State { get; }
        sbyte? TypeID { get; }
        byte? PercentHealth { get; }
        int? DisplayID { get; }
        uint? ArtKit { get; }
        int? Level { get; }
    }
    
    public interface IMutableGameObjectData : IGameObjectData
    {
        WowGuid? CreatedBy { get; set; }
        uint? Flags { get; set; }
        Quaternion? ParentRotation { get; set; }
        int? FactionTemplate { get; set; }
        sbyte? State { get; set; }
        sbyte? TypeID { get; set; }
        byte? PercentHealth { get; set; }
        int? DisplayID { get; set; }
        uint? ArtKit { get; set; }
        int? Level { get; set; }
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
        }
    }
}
