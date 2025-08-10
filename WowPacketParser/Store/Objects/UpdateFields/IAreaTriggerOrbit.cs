namespace WowPacketParser.Store.Objects.UpdateFields
{
    public interface IAreaTriggerOrbit
    {
        float? Radius { get; }
        float? InitialAngle { get; }
        float? BlendFromRadius { get; }
        int? ExtraTimeForBlending { get; }
        bool? CounterClockwise { get; }
    }
}
