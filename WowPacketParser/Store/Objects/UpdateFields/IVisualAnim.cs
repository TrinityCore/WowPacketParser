
namespace WowPacketParser.Store.Objects.UpdateFields
{
    public interface IVisualAnim
    {
        uint? AnimationDataID { get; }
        uint? AnimKitID { get;}
        bool? IsDecay { get; }
    }
}
