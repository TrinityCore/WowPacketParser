#nullable enable

namespace WowPacketParser.Store.Objects.UpdateFields
{
    public interface IObjectData
    {
        int? EntryID { get; }
        uint? DynamicFlags { get; }
        float? Scale { get; }
    }
    
    public interface IMutableObjectData : IObjectData
    {
        int? EntryID { get; set; }
        uint? DynamicFlags { get; set; }
        float? Scale { get; set; }
    }

    public static partial class Extensions
    {
        public static void UpdateData(this IMutableObjectData data, IObjectData update)
        {
            data.EntryID = update.EntryID ?? data.EntryID;
            data.DynamicFlags = update.DynamicFlags ?? data.DynamicFlags;
            data.Scale = update.Scale ?? data.Scale;
        }
    }
}
