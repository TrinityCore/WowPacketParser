using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects.UpdateFields
{
    public interface IAreaTriggerBoundedPlane
    {
        Vector2 Extents { get; }
        Vector2 ExtentsTarget { get; }
    }
}
