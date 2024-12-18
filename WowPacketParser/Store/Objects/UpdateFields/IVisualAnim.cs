
namespace WowPacketParser.Store.Objects.UpdateFields
{
    public interface IVisualAnim
    {
        uint? AnimationDataID { get; set; }
        uint? AnimKitID { get; set; }
        bool? IsDecay { get; set; }
    }
}
