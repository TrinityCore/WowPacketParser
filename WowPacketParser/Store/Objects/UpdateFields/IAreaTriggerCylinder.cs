namespace WowPacketParser.Store.Objects.UpdateFields
{
    public interface IAreaTriggerCylinder
    {
        float? Radius { get; }
        float? RadiusTarget { get; }
        float? Height { get; }
        float? HeightTarget { get; }
        float? LocationZOffset { get; }
        float? LocationZOffsetTarget { get; }
    }
}
