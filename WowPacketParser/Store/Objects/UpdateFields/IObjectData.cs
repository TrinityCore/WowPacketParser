
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
        new int? EntryID { get; set; }
        new uint? DynamicFlags { get; set; }
        new float? Scale { get; set; }
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
