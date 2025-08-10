using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects.UpdateFields
{
    public interface IAreaTriggerSplineCalculator
    {
        DynamicUpdateField<Vector3> Points { get; }
    }
}
