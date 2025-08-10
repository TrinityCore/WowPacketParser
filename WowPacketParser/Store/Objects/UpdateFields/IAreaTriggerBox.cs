using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects.UpdateFields
{
    public interface IAreaTriggerBox
    {
        Vector3 Extents { get; }
        Vector3 ExtentsTarget { get; }
    }
}
