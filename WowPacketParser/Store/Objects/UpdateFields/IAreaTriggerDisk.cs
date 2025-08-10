namespace WowPacketParser.Store.Objects.UpdateFields
{
    public interface IAreaTriggerDisk
    {
        float? InnerRadius { get; }
        float? InnerRadiusTarget { get; }
        float? OuterRadius { get; }
        float? OuterRadiusTarget { get; }
        float? Height { get; }
        float? HeightTarget { get; }
        float? LocationZOffset { get; }
        float? LocationZOffsetTarget { get; }
    }
}
